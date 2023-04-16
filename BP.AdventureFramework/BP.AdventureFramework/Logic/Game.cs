using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Commands;
using BP.AdventureFramework.Commands.Game;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Interpretation;
using BP.AdventureFramework.Rendering;
using BP.AdventureFramework.Rendering.Frames;

namespace BP.AdventureFramework.Logic
{
    /// <summary>
    /// Represents the structure of the game
    /// </summary>
    public class Game : IDisposable
    {
        #region Fields

        private PlayableCharacter player;
        private int lastUsedWidth;
        private int lastUsedHeight;
        private Frame lastFrame;
        private MapDrawer lastUsedMapDrawer;

        #endregion

        #region Properties

        /// <summary>
        /// Get the player.
        /// </summary>
        public PlayableCharacter Player
        {
            get { return player; }
            private set
            {
                if (player != null)
                    player.Died -= player_Died;

                player = value;

                if (player != null)
                    player.Died += player_Died;
            }
        }

        /// <summary>
        /// Get the Overworld.
        /// </summary>
        public Overworld Overworld { get; }

        /// <summary>
        /// Get the name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Get the description.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Get or set the error prefix.
        /// </summary>
        public string ErrorPrefix { get; set; }

        /// <summary>
        /// Get if this is executing.
        /// </summary>
        public bool IsExecuting { get; private set; }

        /// <summary>
        /// Get the drawer for drawing all frames.
        /// </summary>
        protected FrameDrawer FrameDrawer { get; private set; }

        /// <summary>
        /// Get the drawer used for constructing room maps.
        /// </summary>
        protected MapDrawer MapDrawer { get; private set; }

        /// <summary>
        /// Get the interpreter.
        /// </summary>
        protected IInterpreter Interpreter { get; private set; }

        /// <summary>
        /// Get or set the output stream.
        /// </summary>
        public TextWriter Output { get; set; }

        /// <summary>
        /// Get or set input stream.
        /// </summary>
        public TextReader Input { get; set; }

        /// <summary>
        /// Get or set the error stream.
        /// </summary>
        public TextWriter Error { get; set; }

        /// <summary>
        /// Get or set the standard size of the display area.
        /// </summary>
        public Size DisplaySize { get; set; } = new Size(0, 0);

        /// <summary>
        /// Get if this game has ended.
        /// </summary>
        public bool HasEnded { get; private set; }

        /// <summary>
        /// Get or set the exit mode for this game.
        /// </summary>
        public ExitMode ExitMode { get; set; } = ExitMode.ReturnToTitleScreen;

        /// <summary>
        /// Get this Games frame to display for the title screen.
        /// </summary>
        internal Frame TitleFrame { get; private set; }

        /// <summary>
        /// Get this Games frame to display upon completion.
        /// </summary>
        internal Frame CompletionFrame { get; private set; }

        /// <summary>
        /// Get this Games help screen.
        /// </summary>
        internal Frame HelpFrame { get; private set; }

        /// <summary>
        /// Get the current Frame.
        /// </summary>
        protected Frame CurrentFrame { get; private set; }

        /// <summary>
        /// Get or set the completion condition.
        /// </summary>
        public CompletionCheck CompletionCondition { get; set; }

        /// <summary>
        /// Get or set the callback to invoke when waiting for key presses.
        /// </summary>
        internal WaitForKeyPressCallback WaitForKeyPressCallback { get; set; }

        /// <summary>
        /// Occurs when the game begins drawing a frame.
        /// </summary>
        public event EventHandler<Frame> StartingFrameDraw;

        /// <summary>
        /// Occurs when the game finishes drawing a frame.
        /// </summary>
        public event EventHandler<Frame> FinishedFrameDraw;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Game class.
        /// </summary>
        /// <param name="name">The name of this Game.</param>
        /// <param name="description">A description of this Game.</param>
        /// <param name="player">The Player to use for this Game.</param>
        /// <param name="overworld">A Overworld to use for this Game.</param>
        private Game(string name, string description, PlayableCharacter player, Overworld overworld)
        {
            Name = name;
            Description = description;
            Player = player;
            Overworld = overworld;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Execute the game.
        /// </summary>
        public void Execute()
        {
            if (IsExecuting)
                return;

            IsExecuting = true;

            var input = string.Empty;

            Refresh(TitleFrame);

            do
            {
                var displayReactionToInput = true;
                var reaction = new Reaction(ReactionResult.None, "Error.");

                if (!CurrentFrame.AcceptsInput)
                {
                    var frame = CurrentFrame;

                    while (!WaitForKeyPressCallback(Convert.ToChar(13)) && CurrentFrame == frame)
                        DrawFrame(CurrentFrame);
                }
                else
                {
                    input = Input.ReadLine();
                }

                if (CurrentFrame is TitleFrame)
                {
                    EnterGame(DisplaySize.Width, DisplaySize.Height, MapDrawer);
                    displayReactionToInput = false;
                }
                else if (CurrentFrame is EndFrame)
                {
                    Refresh(TitleFrame);
                    displayReactionToInput = false;
                }
                else if (!CurrentFrame.AcceptsInput)
                {
                    Refresh();
                    displayReactionToInput = false;
                }
                else
                {
                    var interpretation = Interpreter?.Interpret(input, this) ?? new InterpretationResult(false, new Unactionable("No interpreter."));

                    if (interpretation.WasInterpretedSuccessfully)
                        reaction = RunCommand(interpretation.Command);
                }

                if (!displayReactionToInput)
                    continue;

                switch (reaction.Result)
                {
                    case ReactionResult.None:

                        var message = ErrorPrefix + ": " + reaction.Description;
                        UpdateScreenWithCurrentFrame(message);
                        break;

                    case ReactionResult.Reacted:

                        UpdateScreenWithCurrentFrame(reaction.Description);
                        break;

                    case ReactionResult.SelfContained:

                        break;

                    default:
                        throw new NotImplementedException();
                }
            }
            while (!HasEnded);

            IsExecuting = false;
        }

        /// <summary>
        /// Update the screen with the current Frame, provided by the GameManager.Game property.
        /// </summary>
        /// <param name="message">An additional message to display to the user.</param>
        private void UpdateScreenWithCurrentFrame(string message)
        {
            var scene = GetScene(MapDrawer, DisplaySize.Width, DisplaySize.Height, message);
            DrawFrame(scene);
        }

        /// <summary>
        /// Draw a Frame onto the output stream.
        /// </summary>
        /// <param name="frame">The frame to draw.</param>
        private void DrawFrame(Frame frame)
        {
            try
            {
                StartingFrameDraw?.Invoke(this, frame);

                if (lastFrame != null)
                    lastFrame.Invalidated -= Frame_Invalidated;

                if (lastFrame != frame && lastFrame != null)
                {
                    lastFrame.Dispose();
                    lastFrame = null;
                }

                lastFrame = frame;
                lastFrame.Invalidated += Frame_Invalidated;

                Output.WriteLine(frame.BuildFrame(DisplaySize.Width, DisplaySize.Height, FrameDrawer));

                FinishedFrameDraw?.Invoke(this, frame);
            }
            catch (Exception e)
            {
                Debug.WriteLine("An exception was caught drawing the frame: {0}", e.Message);
            }
        }

        /// <summary>
        /// End the Game.
        /// </summary>
        internal void End()
        {
            HasEnded = true;

            switch (ExitMode)
            {
                case ExitMode.ExitApplication:

                    Environment.Exit(0);
                    break;

                case ExitMode.ReturnToTitleScreen:

                    Refresh(TitleFrame);
                    break;

                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Handle a reaction.
        /// </summary>
        /// <param name="command">The command to react to.</param>
        /// <returns>The reaction to the command.</returns>
        private Reaction RunCommand(ICommand command)
        {
            var reaction = command.Invoke();

            if (CompletionCondition(this))
                Refresh(CompletionFrame);

            return reaction;
        }

        /// <summary>
        /// Enter the game.
        /// </summary>
        /// <param name="width">The width of the game.</param>
        /// <param name="height">The height of the game.</param>
        /// <param name="drawer">A drawer to use for constructing the map.</param>
        private void EnterGame(int width, int height, MapDrawer drawer)
        {
            lastUsedWidth = width;
            lastUsedHeight = height;
            lastUsedMapDrawer = drawer;
            Refresh(GetScene());
        }

        /// <summary>
        /// Get a scene based on the current game.
        /// </summary>
        /// <returns>A constructed frame of the scene.</returns>
        private SceneFrame GetScene()
        {
            return GetScene(lastUsedMapDrawer, lastUsedWidth, lastUsedHeight);
        }

        /// <summary>
        /// Get a scene based on the current game.
        /// </summary>
        /// <param name="drawer">A drawer to use for constructing the map.</param>
        /// <param name="width">The width of the scene.</param>
        /// <param name="height">The height of the scene.</param>
        /// <returns>A constructed frame of the scene.</returns>
        private SceneFrame GetScene(MapDrawer drawer, int width, int height)
        {
            return GetScene(drawer, width, height, string.Empty);
        }

        /// <summary>
        /// Get a scene based on the current game.
        /// </summary>
        /// <param name="drawer">A drawer to use for constructing the map.</param>
        /// <param name="width">The width of the scene.</param>
        /// <param name="height">The height of the scene.</param>
        /// <param name="messageToUser">A message to the user.</param>
        /// <returns>A constructed frame of the scene.</returns>
        private SceneFrame GetScene(MapDrawer drawer, int width, int height, string messageToUser)
        {
            lastUsedWidth = width;
            lastUsedHeight = height;
            lastUsedMapDrawer = drawer;
            return new SceneFrame(Overworld.CurrentRegion.CurrentRoom, Player, messageToUser, drawer);
        }

        /// <summary>
        /// Find an interaction target within the current scope for this Game.
        /// </summary>
        /// <param name="name">The targets name.</param>
        /// <returns>The first IInteractWithItem object which has a name that matches the name parameter.</returns>
        public IInteractWithItem FindInteractionTarget(string name)
        {
            if (name.EqualsExaminable(player))
                return Player;

            if (Player.Items.Any(name.EqualsExaminable))
            {
                Player.FindItem(name, out var i);
                return i;
            }

            if (name.EqualsExaminable(Overworld.CurrentRegion.CurrentRoom))
                return Overworld.CurrentRegion.CurrentRoom;

            if (!Overworld.CurrentRegion.CurrentRoom.ContainsInteractionTarget(name))
                return null;

            Overworld.CurrentRegion.CurrentRoom.FindInteractionTarget(name, out var target);
            return target;
        }

        /// <summary>
        /// Refresh the current frame.
        /// </summary>
        internal void Refresh()
        {
            Refresh(string.Empty);
        }

        /// <summary>
        /// Refresh the current frame.
        /// </summary>
        /// <param name="message">Any message to display.</param>
        internal void Refresh(string message)
        {
            Refresh(new SceneFrame(Overworld.CurrentRegion.CurrentRoom, Player, message));
        }

        /// <summary>
        /// Refresh the display.
        /// </summary>
        /// <param name="frame">The frame to display.</param>
        internal void Refresh(Frame frame)
        {
            CurrentFrame = frame;
            DrawFrame(frame);
        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Create a new callback for generating instances of a game.
        /// </summary>
        /// <param name="name">The name of the game.</param>
        /// <param name="description">A description of the game.</param>
        /// <param name="overworldGenerator">A function to generate the Overworld with.</param>
        /// <param name="playerGenerator">The function to generate the Player with.</param>
        /// <param name="completionCondition">The callback used to check game completion.</param>
        /// <returns>A new GameCreationHelper that will create a GameCreator with the parameters specified.</returns>
        public static GameCreationCallback Create(string name, string description, OverworldCreationCallback overworldGenerator, PlayerCreationCallback playerGenerator, CompletionCheck completionCondition)
        {
            var mapDrawer = new MapDrawer();

            return Create(
                name,
                description,
                overworldGenerator,
                playerGenerator,
                completionCondition,
                new FrameDrawer(),
                mapDrawer,
                new TitleFrame(name, description),
                new TitleFrame("Finished", $"Congratulations, you finished {name}!"),
                new HelpFrame(),
                new InputInterpreter(new FrameCommandInterpreter(mapDrawer), new GlobalCommandInterpreter(mapDrawer), new GameCommandInterpreter()),
                ExitMode.ReturnToTitleScreen,
                "Oops");
        }

        /// <summary>
        ///  Create a new callback for generating instances of a game.
        /// </summary>
        /// <param name="name">The name of the game.</param>
        /// <param name="description">A description of the game.</param>
        /// <param name="overworldGenerator">A function to generate the Overworld with.</param>
        /// <param name="playerGenerator">The function to generate the Player with.</param>
        /// <param name="completionCondition">The callback used to check game completion.</param>
        /// <param name="frameDrawer">The drawer to use for rendering frames.</param>
        /// <param name="mapDrawer">The drawer to use for rendering maps.</param>
        /// <param name="titleFrame">The title frame.</param>
        /// <param name="completionFrame">The completion frame.</param>
        /// <param name="helpFrame">The help frame.</param>
        /// <param name="interpreter">The interpreter.</param>
        /// <param name="exitMode">The exit mode.</param>
        /// <param name="errorPrefix">A prefix to use when displaying errors.</param>
        /// <returns>A new GameCreationHelper that will create a GameCreator with the parameters specified.</returns>
        public static GameCreationCallback Create(string name, string description, OverworldCreationCallback overworldGenerator, PlayerCreationCallback playerGenerator, CompletionCheck completionCondition, FrameDrawer frameDrawer, MapDrawer mapDrawer, Frame titleFrame, Frame completionFrame, Frame helpFrame, IInterpreter interpreter, ExitMode exitMode, string errorPrefix)
        {
            return () =>
            {
                var pC = playerGenerator.Invoke();
                var game = new Game(name, description, pC, overworldGenerator.Invoke(pC))
                {
                    TitleFrame = titleFrame,
                    CompletionFrame = completionFrame,
                    HelpFrame = helpFrame,
                    FrameDrawer = frameDrawer,
                    MapDrawer = mapDrawer,
                    CompletionCondition = completionCondition,
                    Interpreter = interpreter,
                    ExitMode = exitMode,
                    ErrorPrefix = errorPrefix
                };

                return game;
            };
        }

        #endregion

        #region EventHandlers

        private void player_Died(object sender, string e)
        {
            Refresh(new EndFrame("YOU ARE DEAD!!!", e));
        }

        private void Frame_Invalidated(object sender, Frame e)
        {
            Refresh(e);
        }

        #endregion

        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            lastFrame?.Dispose();
            Output?.Dispose();
            Input?.Dispose();
            Error?.Dispose();
        }

        #endregion
    }
}