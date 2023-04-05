using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using AdventureFramework.Rendering;
using AdventureFramework.Rendering.Frames;
using AdventureFramework.Sound.Players;
using BP.AdventureFramework.Interaction;

namespace AdventureFramework.Structure
{
    /// <summary>
    /// Represents a class for controling the flow of a Game
    /// </summary>
    public class GameFlow : IDisposable
    {
        #region StaticProperties

        /// <summary>
        /// Get or set the default file name
        /// </summary>
        private const string defaultFileName = "Save";

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Dispose this GameFlow
        /// </summary>
        public void Dispose()
        {
            OnDisposed();

            // if a worker
            if (ioWorker != null)
                // dispose
                ioWorker.Dispose();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Get the Game
        /// </summary>
        public Game Game
        {
            get { return game; }
            protected set
            {
                // if an old game
                if (game != null)
                {
                    // handle frame updating
                    game.CurrentFrameUpdated -= game_CurrentFrameUpdated;

                    // handle game ended
                    game.Ended -= game_Ended;

                    // handle completed
                    game.Completed -= game_Completed;
                }

                // set game
                game = value;

                // if a new value
                if (value != null)
                {
                    // handle frame updating
                    game.CurrentFrameUpdated += game_CurrentFrameUpdated;

                    // handle game ended
                    game.Ended += game_Ended;

                    // handle completed
                    game.Completed += game_Completed;
                }
            }
        }

        /// <summary>
        /// Get or set the game
        /// </summary>
        private Game game;

        /// <summary>
        /// Get or set the game creator
        /// </summary>
        public GameCreator Creator
        {
            get { return creator; }
            set { creator = value; }
        }

        /// <summary>
        /// Get or set the game creator
        /// </summary>
        private GameCreator creator;

        /// <summary>
        /// Get or set the error prefix
        /// </summary>
        public string ErrorPrefix
        {
            get { return errorPrefix; }
            set { errorPrefix = value; }
        }

        /// <summary>
        /// Get or set the error prefix
        /// </summary>
        private string errorPrefix = "OOPS";

        /// <summary>
        /// Get or set the drawer for drawing all frames
        /// </summary>
        public FrameDrawer FrameDrawer
        {
            get { return frameDrawer; }
            set { frameDrawer = value; }
        }

        /// <summary>
        /// Get or set the drawer for drawing all frames
        /// </summary>
        private FrameDrawer frameDrawer = new FrameDrawer();

        /// <summary>
        /// Get or set the drawer used for constructing room maps
        /// </summary>
        public MapDrawer MapDrawer
        {
            get { return mapDrawer; }
            set { mapDrawer = value; }
        }

        /// <summary>
        /// Get or set the drawer used for constructing room maps
        /// </summary>
        private MapDrawer mapDrawer = new MapDrawer();

        /// <summary>
        /// Get or set the worker used for all IO operations
        /// </summary>
        private BackgroundWorker ioWorker;

        /// <summary>
        /// Get if this is processing an asyncronous file IO operation
        /// </summary>
        public bool IsProcessingAsyncFileIOOperation
        {
            get { return isProcessingAsyncFileIOOperation; }
            protected set { isProcessingAsyncFileIOOperation = value; }
        }

        /// <summary>
        /// Get or set if this is processing an asyncronous file IO operation
        /// </summary>
        private bool isProcessingAsyncFileIOOperation;

        /// <summary>
        /// Get if this should encrypt all files
        /// </summary>
        public bool EncryptFiles
        {
            get { return encryptFiles; }
            protected set { encryptFiles = value; }
        }

        /// <summary>
        /// Get if this should encrypt all files
        /// </summary>
        private bool encryptFiles = true;

        /// <summary>
        /// Get or set the output stream
        /// </summary>
        public TextWriter Output
        {
            get { return output; }
            set { output = value; }
        }

        /// <summary>
        /// Get or set the output stream
        /// </summary>
        private TextWriter output;

        /// <summary>
        /// Get or set input stream
        /// </summary>
        public TextReader Input
        {
            get { return input; }
            set { input = value; }
        }

        /// <summary>
        /// Get or set input stream
        /// </summary>
        private TextReader input;

        /// <summary>
        /// Get or set the error stream
        /// </summary>
        public TextWriter Error
        {
            get { return error; }
            set { error = value; }
        }

        /// <summary>
        /// Get or set the error stream
        /// </summary>
        private TextWriter error;

        /// <summary>
        /// Get or set the standard size of the display area
        /// </summary>
        public Size DisplaySize
        {
            get { return displaySize; }
            set { displaySize = value; }
        }

        /// <summary>
        /// Get or set the standard size of the display area
        /// </summary>
        private Size displaySize = new Size(0, 0);

        /// <summary>
        /// Get or set the callback to invoke when waiting for key presses
        /// </summary>
        public WaitForKeyPressCallback WaitForKeyPressCallback
        {
            get { return waitForKeyPressCallback; }
            set { waitForKeyPressCallback = value; }
        }

        /// <summary>
        /// Get or set the callback to invoke when waiting for key presses
        /// </summary>
        private WaitForKeyPressCallback waitForKeyPressCallback;

        /// <summary>
        /// Get or set the last frame
        /// </summary>
        private Frame lastFrame;

        /// <summary>
        /// Get or set the source delegation from the last loaded game
        /// </summary>
        private List<ITransferableDelegation> sourceLoadedDelegation = new List<ITransferableDelegation>();

        /// <summary>
        /// Occurs when the frame draw begins
        /// </summary>
        public event FrameEventHandler StartingFrameDraw;

        /// <summary>
        /// Occurs when the frame draw exits
        /// </summary>
        public event FrameEventHandler FinishingFrameDraw;

        /// <summary>
        /// Occurs when the display is inverted
        /// </summary>
        public event EventHandler DisplayInverted;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the GameFlow class
        /// </summary>
        public GameFlow()
        {
        }

        /// <summary>
        /// Initializes a new instance of the GameFlow class
        /// </summary>
        /// <param name="gameCreator">A game creator to create the GameFlow.Game property from</param>
        public GameFlow(GameCreator gameCreator)
        {
            // set creator
            Creator = gameCreator;
        }

        /// <summary>
        /// Initializes a new instance of the GameFlow class
        /// </summary>
        /// <param name="helper">A game helper to create the GameFlow.Creator property from</param>
        public GameFlow(GameCreationHelper helper)
        {
            // set creator
            Creator = helper.Creator;
        }

        /// <summary>
        /// Begin the game
        /// </summary>
        public virtual void Begin()
        {
            // reset ID seed so that ID's align between games
            ExaminableObject.ResetIDSeed();

            // create new game
            Game = Creator.Invoke();

            // begin the game
            EnterGameLoop();
        }

        /// <summary>
        /// Display the load file screen
        /// </summary>
        public virtual void DisplayLoadScreen()
        {
            // if directory does not exist
            if (!Directory.Exists(GameSave.DefaultDirectory))
                // create
                Directory.CreateDirectory(GameSave.DefaultDirectory);

            // show save 
            Game.Refresh(new LoadFileFrame(GameSave.DefaultDirectory, EncryptFiles ? "bin" : "xml"));
        }

        /// <summary>
        /// Display the save file screen
        /// </summary>
        public virtual void DisplaySaveScreen()
        {
            // if directory does not exist
            if (!Directory.Exists(GameSave.DefaultDirectory))
                // create
                Directory.CreateDirectory(GameSave.DefaultDirectory);

            // show save 
            Game.Refresh(new SaveFileFrame(GameSave.DefaultDirectory, EncryptFiles ? "bin" : "xml"));
        }

        /// <summary>
        /// Try to handle the input at a flow level, i.e higher operations on a Game such as saving, loading and creating new games
        /// </summary>
        /// <param name="input">The input to handle</param>
        /// <returns>The decision based on the input</returns>
        protected virtual Decision TryHandleInputAtFlowLevel(string input)
        {
            // if a command
            if (Game.Parser.IsEGameCommand(input))
            {
                // hold command
                EGameCommand command;

                // parse command
                Game.Parser.TryParseToEGameCommand(input, out command);

                // handle command
                switch (command)
                {
                    case EGameCommand.About:
                        {
                            // if some description for game
                            if (!string.IsNullOrEmpty(Game.Description))
                                // show about
                                Game.Refresh(Game.Description + "\n\nAdventureFramework Copyright © Ben Pollard 2011-2013");
                            else
                                // show about
                                Game.Refresh("AdventureFramework Copyright © Ben Pollard 2011-2013");

                            // reacted
                            return new Decision(EReactionToInput.SelfContainedReaction);
                        }
                    case EGameCommand.Exit:
                        {
                            // exit
                            Game.End();

                            // reacted
                            return new Decision(EReactionToInput.SelfContainedReaction, "Exiting...");
                        }
                    case EGameCommand.Help:
                        {
                            // show
                            Game.Refresh(Game.HelpFrame);

                            // reacted
                            return new Decision(EReactionToInput.SelfContainedReaction, string.Empty);
                        }
                    case EGameCommand.Load:
                        {
                            // display the load screen
                            DisplayLoadScreen();

                            // reacted
                            return new Decision(EReactionToInput.SelfContainedReaction, "Showing load...");
                        }
                    case EGameCommand.Map:
                        {
                            // show
                            Game.Refresh(new RegionMapFrame(Game.Overworld.CurrentRegion, MapDrawer));

                            // reacted
                            return new Decision(EReactionToInput.SelfContainedReaction, string.Empty);
                        }
                    case EGameCommand.New:
                        {
                            // return to title
                            Game.Refresh(Game.TitleFrame);

                            // reacted
                            return new Decision(EReactionToInput.SelfContainedReaction, "New game");
                        }
                    case EGameCommand.Save:
                        {
                            // display save screen
                            DisplaySaveScreen();

                            // reacted
                            return new Decision(EReactionToInput.SelfContainedReaction, "Showing save...");
                        }
                    case EGameCommand.SoundOff:
                        {
                            // sounds off
                            SoundPlayer.UseSounds = false;

                            // reacted
                            return new Decision(EReactionToInput.CouldReact, "Sounds are off");
                        }
                    case EGameCommand.SoundOn:
                        {
                            // sounds on
                            SoundPlayer.UseSounds = true;

                            // reacted
                            return new Decision(EReactionToInput.CouldReact, "Sounds are on");
                        }
                    default:
                        {
                            throw new NotImplementedException();
                        }
                }
            }

            if (game.Parser.IsEFrameDrawerOption(input))
            {
                // hold command
                EFrameDrawerOption command;

                // parse command
                game.Parser.TryParseToEFrameDrawerOption(input, out command);

                // handle command
                switch (command)
                {
                    case EFrameDrawerOption.CommandsOff:
                        {
                            // turn commands off
                            FrameDrawer.DisplayCommands = false;

                            // reacted
                            return new Decision(EReactionToInput.CouldReact, "Commands have been turned off");
                        }
                    case EFrameDrawerOption.CommandsOn:
                        {
                            // turn commands on
                            FrameDrawer.DisplayCommands = true;

                            // reacted
                            return new Decision(EReactionToInput.CouldReact, "Commands have been turned on");
                        }
                    case EFrameDrawerOption.Invert:
                        {
                            // if subscribers
                            if (DisplayInverted != null)
                            {
                                // dispatch changed
                                DisplayInverted(this, new EventArgs());

                                // reacted
                                return new Decision(EReactionToInput.CouldReact, "Colours have been inverted");
                            }

                            // not reacted
                            return new Decision(EReactionToInput.CouldntReact, "Colours have been not been inverted as no handling has been specified");
                        }
                    case EFrameDrawerOption.KeyOff:
                        {
                            // turn key off
                            MapDrawer.Key = EKeyType.None;

                            // reacted
                            return new Decision(EReactionToInput.CouldReact, "Key has been turned off");
                        }
                    case EFrameDrawerOption.KeyOn:
                        {
                            // turn key off
                            MapDrawer.Key = EKeyType.Dynamic;

                            // reacted
                            return new Decision(EReactionToInput.CouldReact, "Key has been turned on");
                        }
                    default:
                        {
                            throw new NotImplementedException();
                        }
                }
            }

            // reacted
            return new Decision(EReactionToInput.CouldntReact);
        }

        /// <summary>
        /// Enter the game loop
        /// </summary>
        protected virtual void EnterGameLoop()
        {
            try
            {
                // hold input
                var input = string.Empty;

                // hold any message
                var message = string.Empty;

                // do title
                Game.Refresh(Game.TitleFrame);

                // hold is new is already loaded
                var newHasBeenLoaded = true;

                // remove handle for displaying custom frames
                FrameDrawer.DisplayedSpecialFrame += FrameDrawer_DisplayedSpecialFrame;

                // handle loads
                GameSave.GameLoaded += GameSave_GameLoaded;

                // hold reaction to input
                Decision reaction = null;

                // hold if input handling should be displayed
                var displayReactionToInput = true;

                do
                {
                    // reset message
                    message = string.Empty;

                    // reset reaction
                    reaction = null;

                    // reset if handling
                    displayReactionToInput = true;

                    // if processing background operation
                    while (IsProcessingAsyncFileIOOperation)
                    {
                        // wait for the IO operation to finish
                    }

                    // if don't accept input
                    if (!Game.CurrentFrame.AcceptsInput)
                    {
                        // hold scene
                        var frame = Game.CurrentFrame;

                        // wait for enter, or frame change
                        while (!WaitForKeyPressCallback(Convert.ToChar(13)) &&
                               Game.CurrentFrame == frame)
                            // redraw
                            DrawFrame(Game.CurrentFrame);
                    }
                    else
                    {
                        // get input
                        input = Input.ReadLine();
                    }

                    // if a title screne
                    if (Game.CurrentFrame is TitleFrame)
                    {
                        // if not new loaded already
                        if (!newHasBeenLoaded)
                        {
                            // reset ID seed so that ID's align
                            ExaminableObject.ResetIDSeed();

                            // reset game
                            Game = Creator.Invoke();
                        }

                        // enter game
                        Game.EnterGame(DisplaySize.Width, DisplaySize.Height, MapDrawer);

                        // skip input handling
                        displayReactionToInput = false;
                    }
                    else if (Game.CurrentFrame is EndFrame)
                    {
                        // goto title screen
                        Game.Refresh(Game.TitleFrame);

                        // skip input handling
                        displayReactionToInput = false;
                    }
                    else if (Game.CurrentFrame is SaveFileFrame)
                    {
                        // if some input
                        if (!string.IsNullOrEmpty(input))
                        {
                            // hold file name
                            string filename;

                            // hold file index
                            int fileIndex;

                            // if parses
                            if (int.TryParse(input, out fileIndex))
                                // hold reaction
                                reaction = ((FileIOFrame)Game.CurrentFrame).TryDetermineValidFileName(fileIndex, out filename);
                            else
                                // if can get a file name
                                reaction = ((FileIOFrame)Game.CurrentFrame).TryDetermineValidFileName(input, out filename);

                            // handle reaction
                            switch (reaction.Result)
                            {
                                case EReactionToInput.CouldntReact:
                                    {
                                        // set decision
                                        reaction = new Decision(EReactionToInput.CouldntReact, "Save failed: " + reaction.Reason);

                                        break;
                                    }
                                case EReactionToInput.CouldReact:
                                    {
                                        // save
                                        BeginAsyncSave(filename, EncryptFiles);

                                        // set decision
                                        reaction = new Decision(EReactionToInput.SelfContainedReaction, "Saving...");

                                        break;
                                    }
                                case EReactionToInput.SelfContainedReaction:
                                    {
                                        // don't handle
                                        displayReactionToInput = false;

                                        break;
                                    }
                                default:
                                    {
                                        throw new NotImplementedException();
                                    }
                            }
                        }
                        else
                        {
                            // refresh
                            Game.Refresh();

                            // skip input handling
                            displayReactionToInput = false;
                        }
                    }
                    else if (Game.CurrentFrame is LoadFileFrame)
                    {
                        // if accepts input and some input
                        if (Game.CurrentFrame.AcceptsInput &&
                            !string.IsNullOrEmpty(input))
                        {
                            // hold file name
                            string filename;

                            // hold file index
                            int fileIndex;

                            // if parses
                            if (int.TryParse(input, out fileIndex))
                                // if can get a file name
                                reaction = ((FileIOFrame)Game.CurrentFrame).TryDetermineValidFileName(fileIndex, out filename);
                            else
                                // if can get a file name
                                reaction = ((FileIOFrame)Game.CurrentFrame).TryDetermineValidFileName(input, out filename);

                            // handle reaction
                            switch (reaction.Result)
                            {
                                case EReactionToInput.CouldntReact:
                                    {
                                        // set decision
                                        reaction = new Decision(EReactionToInput.CouldntReact, "Load failed: " + reaction.Reason);

                                        break;
                                    }
                                case EReactionToInput.CouldReact:
                                    {
                                        // clear previous delegates
                                        sourceLoadedDelegation.Clear();

                                        // save interaction
                                        Game.RegisterTransferableChildren(ref sourceLoadedDelegation);

                                        // save
                                        BeginAsyncLoad(filename, EncryptFiles);

                                        // set decision
                                        reaction = new Decision(EReactionToInput.SelfContainedReaction, "Loading...");

                                        break;
                                    }
                                case EReactionToInput.SelfContainedReaction:
                                    {
                                        // don't handle
                                        displayReactionToInput = false;

                                        break;
                                    }
                                default:
                                    {
                                        throw new NotImplementedException();
                                    }
                            }
                        }
                        else
                        {
                            // refresh
                            Game.Refresh();

                            // skip input handling
                            displayReactionToInput = false;
                        }
                    }
                    else if (!Game.CurrentFrame.AcceptsInput)
                    {
                        // refresh
                        Game.Refresh();

                        // skip input handling
                        displayReactionToInput = false;
                    }
                    else
                    {
                        // if game was unmodified before
                        if (newHasBeenLoaded)
                            // new now modified
                            newHasBeenLoaded = false;

                        // hold reaction
                        reaction = TryHandleInputAtFlowLevel(input);

                        // try and handle result of command as a flow command
                        switch (reaction.Result)
                        {
                            case EReactionToInput.CouldReact:
                            case EReactionToInput.SelfContainedReaction:
                                {
                                    // all done

                                    break;
                                }
                            default:
                                {
                                    // get the reaction of the game
                                    reaction = Game.ReactToInput(input);

                                    break;
                                }
                        }
                    }

                    // if displaying reaction to input
                    if (displayReactionToInput)
                        // select reaction
                        switch (reaction.Result)
                        {
                            case EReactionToInput.CouldntReact:
                                {
                                    // display
                                    message = ErrorPrefix + ": " + reaction.Reason;

                                    // draw scene latest scene
                                    UpdateScreenWithCurrentFrame(message);

                                    break;
                                }
                            case EReactionToInput.CouldReact:
                                {
                                    // display message
                                    message = reaction.Reason;

                                    // draw scene latest scene
                                    UpdateScreenWithCurrentFrame(message);

                                    break;
                                }
                            case EReactionToInput.SelfContainedReaction:
                                {
                                    // do nothing

                                    break;
                                }
                            default:
                                {
                                    throw new NotImplementedException();
                                }
                        }
                } while (!Game.HasEnded);
            }
            finally
            {
                // remove handle for displaying custom frames
                FrameDrawer.DisplayedSpecialFrame -= FrameDrawer_DisplayedSpecialFrame;

                // remove handle for loads
                GameSave.GameLoaded -= GameSave_GameLoaded;
            }
        }

        /// <summary>
        /// Update the screen with the current Frame, provided by the GameFlow.Game property
        /// </summary>
        /// <param name="message">An additional message to display to the user</param>
        protected virtual void UpdateScreenWithCurrentFrame(string message)
        {
            // get the scene
            var scene = Game.GetScene(MapDrawer, DisplaySize.Width, DisplaySize.Height, message);

            // draw frame
            DrawFrame(scene);
        }

        /// <summary>
        /// Draw a Frame onto the ouput stream
        /// </summary>
        /// <param name="frame">The frame to draw</param>
        protected virtual void DrawFrame(Frame frame)
        {
            try
            {
                // if subscribers
                if (StartingFrameDraw != null)
                    // dispatch updated
                    StartingFrameDraw(this, new FrameEventArgs(frame));

                // if was a last frame
                if (lastFrame != null)
                    // remove handle for invalidation
                    lastFrame.Invalidated -= frame_Invalidated;

                // if frame changed
                if (lastFrame != frame &&
                    lastFrame != null)
                {
                    // dispose
                    lastFrame.Dispose();

                    // reset
                    lastFrame = null;
                }

                // set last frame
                lastFrame = frame;

                // handle invalidation
                lastFrame.Invalidated += frame_Invalidated;

                // update output display
                Output.WriteLine(frame.BuildFrame(DisplaySize.Width, DisplaySize.Height, FrameDrawer));

                // if subscribers
                if (FinishingFrameDraw != null)
                    // dispatch updated
                    FinishingFrameDraw(this, new FrameEventArgs(frame));
            }
            catch (Exception e)
            {
                // display in debug
                Debug.WriteLine("An exception was caught drawing the frame: {0}", e.Message);
            }
        }

        /// <summary>
        /// Handle GameFlow.Frame property updating
        /// </summary>
        /// <param name="frame">The new frame</param>
        protected virtual void OnFrameUpdated(Frame frame)
        {
            // draw
            DrawFrame(frame);
        }

        /// <summary>
        /// Handle the GameFlow.Game.Ended event
        /// </summary>
        /// <param name="exitMode">The exit mode from the game</param>
        protected virtual void OnGameEnded(EExitMode exitMode)
        {
            // select exit mode
            switch (exitMode)
            {
                case EExitMode.ExitApplication:
                    {
                        // exit with OK code
                        Environment.Exit(0);

                        break;
                    }
                case EExitMode.ReturnToTitleScreen:
                    {
                        // return to title
                        Game.Refresh(Game.TitleFrame);

                        break;
                    }
                default:
                    {
                        throw new NotImplementedException();
                    }
            }
        }

        /// <summary>
        /// Handle the GameFlow.Game.Completed event
        /// </summary>
        protected virtual void OnGameCompleted()
        {
            // draw completion screen
            DrawFrame(game.CompletionFrame);
        }

        /// <summary>
        /// Handle disposal of this GameFlow
        /// </summary>
        protected virtual void OnDisposed()
        {
            // if input not null
            if (Input != null)
            {
                // dispose
                Input.Dispose();

                // release
                Input = null;
            }

            // if output not null
            if (Output != null)
            {
                // dispose
                Output.Dispose();

                // release
                Output = null;
            }

            // if error not null
            if (Error != null)
            {
                // dispose
                Error.Dispose();

                // release
                Error = null;
            }
        }

        /// <summary>
        /// Start an async IO operations
        /// </summary>
        /// <param name="operation">The operation to carry out</param>
        /// <param name="frameToDisplayWhileInOperation">The frame to display while in operation</param>
        protected void StartAsyncIOOperation(IOOperationCallback operation, Frame frameToDisplayWhileInOperation)
        {
            // if worker is already working
            if (ioWorker != null &&
                ioWorker.IsBusy)
            {
                // throw exception
                //throw new Exception("Can't perform an IO operation while one is already is progress");
            }
            else
            {
                // mark that is processing
                IsProcessingAsyncFileIOOperation = true;

                // create worker
                ioWorker = new BackgroundWorker();

                // handle work
                ioWorker.DoWork += (sender, e) =>
                {
                    try
                    {
                        // hold message
                        string message;

                        // handle operation
                        operation.Invoke(out message);

                        // show scene update
                        Game.Refresh(message);

                        // mark that is not processing
                        IsProcessingAsyncFileIOOperation = false;
                    }
                    catch (Exception ex)
                    {
                        // show scene update
                        Game.Refresh(ex.Message);

                        // mark that is not processing
                        IsProcessingAsyncFileIOOperation = false;
                    }
                    finally
                    {
                        // if marked as processing
                        if (IsProcessingAsyncFileIOOperation)
                            // mark that is not processing
                            IsProcessingAsyncFileIOOperation = false;
                    }
                };

                // work -^
                ioWorker.RunWorkerAsync();

                // now handle loading operation 
                Game.Refresh(frameToDisplayWhileInOperation);
            }
        }

        /// <summary>
        /// Start an async IO operations
        /// </summary>
        /// <param name="operation">The operation to carry out</param>
        /// <param name="fileName">The full file name of the file to perform all IO operation on</param>
        /// <param name="frameToDisplayWhileInOperation">The frame to display while in operation</param>
        protected void StartAsyncIOOperation(IOSprecifiedFileOperationCallback operation, string fileName, Frame frameToDisplayWhileInOperation)
        {
            // if worker is already working
            if (ioWorker != null &&
                ioWorker.IsBusy)
            {
                // throw exception
                //throw new Exception("Can't perform an IO operation while one is already is progress");
            }
            else
            {
                // mark that is processing
                IsProcessingAsyncFileIOOperation = true;

                // create worker
                ioWorker = new BackgroundWorker();

                // handle work
                ioWorker.DoWork += (sender, e) =>
                {
                    try
                    {
                        // hold message
                        string message;

                        // handle operation
                        operation.Invoke(out message, fileName);

                        // show scene update
                        Game.Refresh(message);

                        // mark that is not processing
                        IsProcessingAsyncFileIOOperation = false;
                    }
                    catch (Exception ex)
                    {
                        // show scene update
                        Game.Refresh(ex.Message);

                        // mark that is not processing
                        IsProcessingAsyncFileIOOperation = false;
                    }
                    finally
                    {
                        // if marked as processing
                        if (IsProcessingAsyncFileIOOperation)
                            // mark that is not processing
                            IsProcessingAsyncFileIOOperation = false;
                    }
                };

                // work -^
                ioWorker.RunWorkerAsync();

                // now handle loading operation 
                Game.Refresh(frameToDisplayWhileInOperation);
            }
        }

        #region Saving

        /// <summary>
        /// Save the game as xml
        /// </summary>
        /// <returns>If the save was sucsessful</returns>
        public virtual bool SaveGameAsXML()
        {
            // hold message
            string message;

            // save
            return SaveGameAsXML(out message);
        }

        /// <summary>
        /// Save the game as xml
        /// </summary>
        /// <param name="message">Any message regarding the save</param>
        /// <returns>If the save was sucsessful</returns>
        public virtual bool SaveGameAsXML(out string message)
        {
            // save
            return GameSave.SaveGameAsXML(AppDomain.CurrentDomain.BaseDirectory + defaultFileName + ".xml", Game, out message, false);
        }

        /// <summary>
        /// Save the game as xml
        /// </summary>
        /// <param name="message">Any message regarding the save</param>
        /// <param name="fileName">The full file name to save the file as</param>
        /// <returns>If the save was sucsessful</returns>
        public virtual bool SaveGameAsXML(out string message, string fileName)
        {
            // save
            return GameSave.SaveGameAsXML(fileName, Game, out message, false);
        }

        /// <summary>
        /// Save the game as encrypted xml
        /// </summary>
        /// <returns>If the save was sucsessful</returns>
        public virtual bool SaveGameAsEncyptedXML()
        {
            // hold message
            string message;

            // save
            return SaveGameAsEncyptedXML(out message);
        }

        /// <summary>
        /// Save the game as encrypted xml
        /// </summary>
        /// <param name="message">Any message regarding the save</param>
        /// <returns>If the save was sucsessful</returns>
        public virtual bool SaveGameAsEncyptedXML(out string message)
        {
            // save
            return GameSave.SaveGameAsXML(AppDomain.CurrentDomain.BaseDirectory + defaultFileName + ".bin", Game, out message, true);
        }

        /// <summary>
        /// Save the game as encrypted xml
        /// </summary>
        /// <param name="message">Any message regarding the save</param>
        /// <param name="fileName">The full file name to save the file as</param>
        /// <returns>If the save was sucsessful</returns>
        public virtual bool SaveGameAsEncyptedXML(out string message, string fileName)
        {
            // save
            return GameSave.SaveGameAsXML(fileName, Game, out message, true);
        }

        /// <summary>
        /// Begin an encrypted asyncronous save operation
        /// </summary>
        /// <param name="fileName">The file name to save the file as</param>
        /// <param name="isEncrypted">Specify if the file is encrypted</param>
        public virtual void BeginAsyncSave(string fileName, bool isEncrypted)
        {
            // if using encryption
            if (isEncrypted)
                // do operation
                StartAsyncIOOperation(SaveGameAsEncyptedXML, fileName, new AsyncIOOperationFrame(EIOOperation.Save));
            else
                // do operation
                StartAsyncIOOperation(SaveGameAsXML, fileName, new AsyncIOOperationFrame(EIOOperation.Save));
        }

        #endregion

        #region Loading

        /// <summary>
        /// Load a game from an xml file
        /// </summary>
        /// <returns>If the load was sucsessful</returns>
        public virtual bool LoadGameAsXML()
        {
            // hold message
            string message;

            // load
            return LoadGameAsXML(out message);
        }

        /// <summary>
        /// Load a game from an xml file
        /// </summary>
        /// <param name="message">Any message regarding the load</param>
        /// <returns>If the load was sucsessful</returns>
        public virtual bool LoadGameAsXML(out string message)
        {
            // load
            return GameSave.LoadGameAsXML(AppDomain.CurrentDomain.BaseDirectory + defaultFileName + ".xml", Game, out message, false);
        }

        /// <summary>
        /// Load a game from an xml file
        /// </summary>
        /// <param name="message">Any message regarding the load</param>
        /// <param name="fileName">The full file name of the file to load</param>
        /// <returns>If the load was sucsessful</returns>
        public virtual bool LoadGameAsXML(out string message, string fileName)
        {
            // load
            return GameSave.LoadGameAsXML(fileName, Game, out message, false);
        }

        /// <summary>
        /// Load a game from an encrypted xml file
        /// </summary>
        /// <returns>If the load was sucsessful</returns>
        public virtual bool LoadGameAsEncryptedXML()
        {
            // hold message
            string message;

            // load
            return LoadGameAsEncryptedXML(out message);
        }

        /// <summary>
        /// Load a game from an encrypted xml file
        /// </summary>
        /// <param name="message">Any message regarding the load</param>
        /// <returns>If the load was sucsessful</returns>
        public virtual bool LoadGameAsEncryptedXML(out string message)
        {
            // load
            return GameSave.LoadGameAsXML(AppDomain.CurrentDomain.BaseDirectory + defaultFileName + ".bin", Game, out message, true);
        }

        /// <summary>
        /// Load a game from an encrypted xml file
        /// </summary>
        /// <param name="message">Any message regarding the load</param>
        /// <param name="fileName">The full file name of the file to load</param>
        /// <returns>If the load was sucsessful</returns>
        public virtual bool LoadGameAsEncryptedXML(out string message, string fileName)
        {
            // load
            return GameSave.LoadGameAsXML(fileName, Game, out message, true);
        }

        /// <summary>
        /// Begin an encrypted asyncronous load operation
        /// </summary>
        /// <param name="fileName">The full file name of the file to load</param>
        /// <param name="isEncrypted">Specify if the file is encrypted</param>
        public virtual void BeginAsyncLoad(string fileName, bool isEncrypted)
        {
            // if encrypted
            if (isEncrypted)
                // do operation
                StartAsyncIOOperation(LoadGameAsEncryptedXML, fileName, new AsyncIOOperationFrame(EIOOperation.Load));
            else
                // do operation
                StartAsyncIOOperation(LoadGameAsXML, fileName, new AsyncIOOperationFrame(EIOOperation.Load));
        }

        #endregion

        #endregion

        #region EventHandlers

        private void game_CurrentFrameUpdated(object sender, FrameEventArgs e)
        {
            OnFrameUpdated(e.Frame);
        }

        private void game_Ended(object sender, GameEndedEventArgs e)
        {
            OnGameEnded(e.ExitMode);
        }

        private void game_Completed(object sender, GameEndedEventArgs e)
        {
            OnGameCompleted();
        }

        private void FrameDrawer_DisplayedSpecialFrame(object sender, FrameEventArgs e)
        {
            // refresh
            Game.Refresh(e.Frame);
        }

        private void frame_Invalidated(object sender, FrameEventArgs e)
        {
            // redraw
            DrawFrame(e.Frame);
        }

        private void GameSave_GameLoaded(object sender, GameIOEventArgs e)
        {
            // this is the 'clever' bit where all delegation is passed between games, allowing for complex saves

            // get the list for the new game
            var targetList = new List<ITransferableDelegation>();

            // now get targets
            e.Game.RegisterTransferableChildren(ref targetList);

            // itterate all source children
            foreach (var source in sourceLoadedDelegation)
            {
                // get transferal ID
                var sourceID = source.GenerateTransferalID();

                // hold matches of ID
                var matches = targetList.Where(targetObj => targetObj.GenerateTransferalID() == sourceID);

                // itterate all matches
                foreach (var match in matches)
                    // do the transfer!
                    match.TransferFrom(source);
            }
        }

        #endregion
    }

    /// <summary>
    /// Callback that invokes a callback for waiting for a key press
    /// </summary>
    /// <param name="key">The ASCII code of the key to wait for</param>
    /// <returns>If the key pressed returned the same ASCII character as the key property</returns>
    public delegate bool WaitForKeyPressCallback(char key);
}