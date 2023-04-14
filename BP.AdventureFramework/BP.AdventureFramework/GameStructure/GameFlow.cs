using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using BP.AdventureFramework.Interaction;
using BP.AdventureFramework.Parsing.Interpretation;
using BP.AdventureFramework.Rendering;
using BP.AdventureFramework.Rendering.Frames;

namespace BP.AdventureFramework.GameStructure
{
    /// <summary>
    /// Represents a class for controlling the flow of a Game.
    /// </summary>
    public class GameFlow : IDisposable
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
            protected set
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
        public GameCreator Creator { get; set; }

        /// <summary>
        /// Get or set the error prefix.
        /// </summary>
        public string ErrorPrefix { get; set; } = "OOPS";

        /// <summary>
        /// Get the drawer for drawing all frames.
        /// </summary>
        public FrameDrawer FrameDrawer { get; } = new FrameDrawer();

        /// <summary>
        /// Get the drawer used for constructing room maps.
        /// </summary>
        public MapDrawer MapDrawer { get; } = new MapDrawer();

        /// <summary>
        /// Get the input interpreter.
        /// </summary>
        public InputInterpreter InputInterpreter { get; }

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
        public WaitForKeyPressCallback WaitForKeyPressCallback { get; set; }

        /// <summary>
        /// Occurs when the frame draw begins.
        /// </summary>
        public event EventHandler<Frame> StartingFrameDraw;

        /// <summary>
        /// Occurs when the frame draw exits.
        /// </summary>
        public event EventHandler<Frame> FinishingFrameDraw;

        /// <summary>
        /// Occurs when the display is inverted.
        /// </summary>
        public event EventHandler DisplayInverted;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the GameFlow class.
        /// </summary>
        /// <param name="helper">A game helper to create the GameFlow.Creator property from.</param>
        public GameFlow(GameCreationHelper helper)
        {
            Creator = helper.Creator;
            InputInterpreter = new InputInterpreter(FrameDrawer, MapDrawer);
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
        protected void EnterGameLoop()
        {
            try
            {
                var input = string.Empty;
                var newHasBeenLoaded = true;

                Game.Refresh(Game.TitleFrame);

                FrameDrawer.DisplayedSpecialFrame += FrameDrawer_DisplayedSpecialFrame;

                do
                {
                    string message;
                    var displayReactionToInput = true;
                    var reaction = new Reaction(ReactionResult.NoReaction, "Error.");

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

                        var interpretation = InputInterpreter.Interpret(input, Game);

                        if (interpretation.WasInterpretedSuccessfully)
                            reaction = Game.RunCommand(interpretation.Command);
                    }

                    if (!displayReactionToInput) 
                        continue;
                    
                    switch (reaction.Result)
                    {
                        case ReactionResult.NoReaction:

                            message = ErrorPrefix + ": " + reaction.Description;
                            UpdateScreenWithCurrentFrame(message);
                            break;

                        case ReactionResult.Reacted:
                            
                            UpdateScreenWithCurrentFrame(reaction.Description);
                            break;

                        default:
                            throw new NotImplementedException();
                    }
                } while (!Game.HasEnded);
            }
            finally
            {
                FrameDrawer.DisplayedSpecialFrame -= FrameDrawer_DisplayedSpecialFrame;
            }
        }

        /// <summary>
        /// Update the screen with the current Frame, provided by the GameFlow.Game property.
        /// </summary>
        /// <param name="message">An additional message to display to the user.</param>
        protected void UpdateScreenWithCurrentFrame(string message)
        {
            var scene = Game.GetScene(MapDrawer, DisplaySize.Width, DisplaySize.Height, message);
            DrawFrame(scene);
        }

        /// <summary>
        /// Draw a Frame onto the output stream.
        /// </summary>
        /// <param name="frame">The frame to draw.</param>
        protected void DrawFrame(Frame frame)
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

        /// <summary>
        /// Handle GameFlow.Frame property updating.
        /// </summary>
        /// <param name="frame">The new frame.</param>
        protected void OnFrameUpdated(Frame frame)
        {
            DrawFrame(frame);
        }

        /// <summary>
        /// Handle the GameFlow.Game.Ended event.
        /// </summary>
        /// <param name="exitMode">The exit mode from the game.</param>
        protected void OnGameEnded(ExitMode exitMode)
        {
            switch (exitMode)
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

        /// <summary>
        /// Handle the GameFlow.Game.Completed event.
        /// </summary>
        protected void OnGameCompleted()
        {
            DrawFrame(game.CompletionFrame);
        }

        /// <summary>
        /// Handle disposal of this GameFlow.
        /// </summary>
        protected void OnDisposed()
        {
            if (Input != null)
            {
                Input.Dispose();
                Input = null;
            }

            if (Output != null)
            {
                Output.Dispose();
                Output = null;
            }

            if (Error != null)
            {
                Error.Dispose();
                Error = null;
            }
        }

        #endregion

        #region EventHandlers

        private void Game_CurrentFrameUpdated(object sender, Frame e)
        {
            OnFrameUpdated(e);
        }

        private void Game_Ended(object sender, ExitMode e)
        {
            OnGameEnded(e);
        }

        private void Game_Completed(object sender, ExitMode e)
        {
            OnGameCompleted();
        }

        private void FrameDrawer_DisplayedSpecialFrame(object sender, Frame e)
        {
            Game.Refresh(e);
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