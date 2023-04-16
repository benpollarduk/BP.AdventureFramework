using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Commands.Game;
using BP.AdventureFramework.Interpretation;
using BP.AdventureFramework.Rendering;
using BP.AdventureFramework.Rendering.Frames;

namespace BP.AdventureFramework.Logic
{
    /// <summary>
    /// Represents a class for managing a Game.
    /// </summary>
    public sealed class GameManager : IDisposable
    {
        #region Fields

        private Game game;
        private Frame lastFrame;

        #endregion

        #region Properties

        /// <summary>
        /// Get the Game.
        /// </summary>
        public Game Game
        {
            get { return game; }
            private set
            {
                if (game != null)
                {
                    game.CurrentFrameUpdated -= Game_CurrentFrameUpdated;
                    game.Ended -= Game_Ended;
                    game.Completed -= Game_Completed;
                }

                game = value;

                if (value != null)
                {
                    game.CurrentFrameUpdated += Game_CurrentFrameUpdated;
                    game.Ended += Game_Ended;
                    game.Completed += Game_Completed;
                }
            }
        }

        /// <summary>
        /// Get or set the game creator.
        /// </summary>
        public GameCreationCallback Creator { get; set; }

        /// <summary>
        /// Get or set the error prefix.
        /// </summary>
        public string ErrorPrefix { get; set; } = "OOPS";

        /// <summary>
        /// Get the drawer for drawing all frames.
        /// </summary>
        internal FrameDrawer FrameDrawer { get; } = new FrameDrawer();

        /// <summary>
        /// Get the drawer used for constructing room maps.
        /// </summary>
        internal MapDrawer MapDrawer { get; } = new MapDrawer();

        /// <summary>
        /// Get the interpreter.
        /// </summary>
        internal IInterpreter Interpreter { get; }

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
        /// Get or set the callback to invoke when waiting for key presses.
        /// </summary>
        internal WaitForKeyPressCallback WaitForKeyPressCallback { get; set; }

        /// <summary>
        /// Occurs when the frame draw begins.
        /// </summary>
        internal event EventHandler<Frame> StartingFrameDraw;

        /// <summary>
        /// Occurs when the frame draw exits.
        /// </summary>
        internal event EventHandler<Frame> FinishingFrameDraw;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the GameManager class.
        /// </summary>
        /// <param name="creator">A game creator.</param>
        public GameManager(GameCreationCallback creator)
        {
            Creator = creator;
            Interpreter = new InputInterpreter(new FrameCommandInterpreter(MapDrawer), new GlobalCommandInterpreter(MapDrawer), new GameCommandInterpreter());
        }

        /// <summary>
        /// Initializes a new instance of the GameManager class.
        /// </summary>
        /// <param name="creator">A game creator.</param>
        /// <param name="interpreter">The interpreter.</param>
        public GameManager(GameCreationCallback creator, IInterpreter interpreter)
        {
            Creator = creator;
            Interpreter = interpreter;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Begin the game.
        /// </summary>
        public void Begin()
        {
            Game = Creator.Invoke();
            EnterGameLoop();
        }

        /// <summary>
        /// Enter the game loop.
        /// </summary>
        private void EnterGameLoop()
        {
            var input = string.Empty;
            var newHasBeenLoaded = true;

            Game.Refresh(Game.TitleFrame);

            do
            {
                var displayReactionToInput = true;
                var reaction = new Reaction(ReactionResult.None, "Error.");

                if (!Game.CurrentFrame.AcceptsInput)
                {
                    var frame = Game.CurrentFrame;

                    while (!WaitForKeyPressCallback(Convert.ToChar(13)) && Game.CurrentFrame == frame)
                        DrawFrame(Game.CurrentFrame);
                }
                else
                {
                    input = Input.ReadLine();
                }

                if (Game.CurrentFrame is TitleFrame)
                {
                    if (!newHasBeenLoaded)
                    {
                        Game = Creator.Invoke();
                    }

                    Game.EnterGame(DisplaySize.Width, DisplaySize.Height, MapDrawer);
                    displayReactionToInput = false;
                }
                else if (Game.CurrentFrame is EndFrame)
                {
                    Game.Refresh(Game.TitleFrame);
                    displayReactionToInput = false;
                }
                else if (!Game.CurrentFrame.AcceptsInput)
                {
                    Game.Refresh();
                    displayReactionToInput = false;
                }
                else
                {
                    if (newHasBeenLoaded)
                        newHasBeenLoaded = false;

                    var interpretation = Interpreter?.Interpret(input, Game) ?? new InterpretationResult(false, new Unactionable("No interpreter."));

                    if (interpretation.WasInterpretedSuccessfully)
                        reaction = Game.RunCommand(interpretation.Command);
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
            while (!Game.HasEnded);
        }

        /// <summary>
        /// Update the screen with the current Frame, provided by the GameManager.Game property.
        /// </summary>
        /// <param name="message">An additional message to display to the user.</param>
        private void UpdateScreenWithCurrentFrame(string message)
        {
            var scene = Game.GetScene(MapDrawer, DisplaySize.Width, DisplaySize.Height, message);
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

                FinishingFrameDraw?.Invoke(this, frame);
            }
            catch (Exception e)
            {
                Debug.WriteLine("An exception was caught drawing the frame: {0}", e.Message);
            }
        }

        #endregion

        #region EventHandlers

        private void Game_CurrentFrameUpdated(object sender, Frame e)
        {
            DrawFrame(e);
        }

        private void Game_Ended(object sender, ExitMode e)
        {
            switch (e)
            {
                case ExitMode.ExitApplication:

                    Environment.Exit(0);
                    break;

                case ExitMode.ReturnToTitleScreen:

                    Game.Refresh(Game.TitleFrame);
                    break;

                default:
                    throw new NotImplementedException();
            }
        }

        private void Game_Completed(object sender, ExitMode e)
        {
            DrawFrame(game.CompletionFrame);
        }

        private void Frame_Invalidated(object sender, Frame e)
        {
            DrawFrame(e);
        }

        #endregion

        #region IDisposable

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