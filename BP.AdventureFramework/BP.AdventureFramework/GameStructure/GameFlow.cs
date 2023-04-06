using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using BP.AdventureFramework.Interaction;
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
        /// Get or set the drawer for drawing all frames.
        /// </summary>
        public FrameDrawer FrameDrawer { get; set; } = new FrameDrawer();

        /// <summary>
        /// Get or set the drawer used for constructing room maps.
        /// </summary>
        public MapDrawer MapDrawer { get; set; } = new MapDrawer();

        /// <summary>
        /// Get if this should encrypt all files.
        /// </summary>
        public bool EncryptFiles { get; protected set; } = true;

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
        public event FrameEventHandler StartingFrameDraw;

        /// <summary>
        /// Occurs when the frame draw exits.
        /// </summary>
        public event FrameEventHandler FinishingFrameDraw;

        /// <summary>
        /// Occurs when the display is inverted.
        /// </summary>
        public event EventHandler DisplayInverted;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the GameFlow class.
        /// </summary>
        /// <param name="gameCreator">A game creator to create the GameFlow.Game property from.</param>
        public GameFlow(GameCreator gameCreator)
        {
            Creator = gameCreator;
        }

        /// <summary>
        /// Initializes a new instance of the GameFlow class.
        /// </summary>
        /// <param name="helper">A game helper to create the GameFlow.Creator property from.</param>
        public GameFlow(GameCreationHelper helper)
        {
            Creator = helper.Creator;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Begin the game.
        /// </summary>
        public virtual void Begin()
        {
            ExaminableObject.ResetIDSeed();
            Game = Creator.Invoke();
            EnterGameLoop();
        }

        /// <summary>
        /// Try to handle the input at a global level, i.e higher operations on a Game such creating new games.
        /// </summary>
        /// <param name="input">The input to handle.</param>
        /// <returns>The decision based on the input.</returns>
        protected virtual Decision TryHandleInputAtGlobalLevel(string input)
        {
            if (Game.Parser.TryParseToGameCommand(input, out var gameCommand))
            {
                switch (gameCommand)
                {
                    case GameCommand.About:
                        
                        if (!string.IsNullOrEmpty(Game.Description))
                            Game.Refresh(Game.Description + "\n\nAdventureFramework by Ben Pollard 2011-2023");
                        else
                            Game.Refresh("AdventureFramework by Ben Pollard 2011-2023");

                        return new Decision(ReactionToInput.SelfContainedReaction);
                    
                    case GameCommand.Exit:

                        Game.End();
                        return new Decision(ReactionToInput.SelfContainedReaction, "Exiting...");
                    
                    case GameCommand.Help:

                        Game.Refresh(Game.HelpFrame);
                        return new Decision(ReactionToInput.SelfContainedReaction, string.Empty);

                    case GameCommand.Map:

                        Game.Refresh(new RegionMapFrame(Game.Overworld.CurrentRegion, MapDrawer));
                        return new Decision(ReactionToInput.SelfContainedReaction, string.Empty);
                    
                    case GameCommand.New:
                        
                        Game.Refresh(Game.TitleFrame);
                        return new Decision(ReactionToInput.SelfContainedReaction, "New game");
                    
                    default:
                        throw new NotImplementedException();
                }
            }

            if (!game.Parser.IsFrameDrawingOption(input)) 
                return new Decision(ReactionToInput.CouldntReact);

            game.Parser.TryParseToFrameDrawingOption(input, out var drawCommand);

            switch (drawCommand)
            {
                case FrameDrawingOption.CommandsOff:

                    FrameDrawer.DisplayCommands = false;
                    return new Decision(ReactionToInput.CouldReact, "Commands have been turned off");
                
                case FrameDrawingOption.CommandsOn:
                    
                    FrameDrawer.DisplayCommands = true;
                    return new Decision(ReactionToInput.CouldReact, "Commands have been turned on");
                
                case FrameDrawingOption.Invert:

                    if (DisplayInverted == null) 
                        return new Decision(ReactionToInput.CouldntReact, "Colours have been not been inverted as no handling has been specified");
                    
                    DisplayInverted(this, new EventArgs());
                    return new Decision(ReactionToInput.CouldReact, "Colours have been inverted");

                case FrameDrawingOption.KeyOff:
                    
                    MapDrawer.Key = KeyType.None;
                    return new Decision(ReactionToInput.CouldReact, "Key has been turned off");
                
                case FrameDrawingOption.KeyOn:
                    
                    MapDrawer.Key = KeyType.Dynamic;
                    return new Decision(ReactionToInput.CouldReact, "Key has been turned on");
                
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Enter the game loop.
        /// </summary>
        protected virtual void EnterGameLoop()
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
                    Decision reaction = null;

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
                            ExaminableObject.ResetIDSeed();
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

                        reaction = TryHandleInputAtGlobalLevel(input);

                        switch (reaction.Result)
                        {
                            case ReactionToInput.CouldReact:
                            case ReactionToInput.SelfContainedReaction:
                                break;
                            default:
                                reaction = Game.ReactToInput(input);
                                break;
                        }
                    }

                    if (!displayReactionToInput) 
                        continue;
                    
                    switch (reaction.Result)
                    {
                        case ReactionToInput.CouldntReact:

                            message = ErrorPrefix + ": " + reaction.Reason;
                            UpdateScreenWithCurrentFrame(message);
                            break;

                        case ReactionToInput.CouldReact:
                            
                            message = reaction.Reason;
                            UpdateScreenWithCurrentFrame(message);
                            break;

                        case ReactionToInput.SelfContainedReaction:
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
        protected virtual void UpdateScreenWithCurrentFrame(string message)
        {
            var scene = Game.GetScene(MapDrawer, DisplaySize.Width, DisplaySize.Height, message);
            DrawFrame(scene);
        }

        /// <summary>
        /// Draw a Frame onto the output stream.
        /// </summary>
        /// <param name="frame">The frame to draw.</param>
        protected virtual void DrawFrame(Frame frame)
        {
            try
            {
                StartingFrameDraw?.Invoke(this, new FrameEventArgs(frame));

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

                FinishingFrameDraw?.Invoke(this, new FrameEventArgs(frame));
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
        protected virtual void OnFrameUpdated(Frame frame)
        {
            DrawFrame(frame);
        }

        /// <summary>
        /// Handle the GameFlow.Game.Ended event.
        /// </summary>
        /// <param name="exitMode">The exit mode from the game.</param>
        protected virtual void OnGameEnded(ExitMode exitMode)
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
        protected virtual void OnGameCompleted()
        {
            DrawFrame(game.CompletionFrame);
        }

        /// <summary>
        /// Handle disposal of this GameFlow.
        /// </summary>
        protected virtual void OnDisposed()
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

        private void Game_CurrentFrameUpdated(object sender, FrameEventArgs e)
        {
            OnFrameUpdated(e.Frame);
        }

        private void Game_Ended(object sender, GameEndedEventArgs e)
        {
            OnGameEnded(e.ExitMode);
        }

        private void Game_Completed(object sender, GameEndedEventArgs e)
        {
            OnGameCompleted();
        }

        private void FrameDrawer_DisplayedSpecialFrame(object sender, FrameEventArgs e)
        {
            Game.Refresh(e.Frame);
        }

        private void Frame_Invalidated(object sender, FrameEventArgs e)
        {
            DrawFrame(e.Frame);
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