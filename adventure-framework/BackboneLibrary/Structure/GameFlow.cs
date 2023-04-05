using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventureFramework.Locations;
using AdventureFramework.Rendering;
using AdventureFramework.Interaction;
using AdventureFramework.IO;
using AdventureFramework.Rendering.Frames;
using System.ComponentModel;
using AdventureFramework.Sound.Players;
using System.Diagnostics;
using System.IO;
using System.Drawing;

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
        private const String defaultFileName = "Save";

        #endregion

        #region Properties

        /// <summary>
        /// Get the Game
        /// </summary>
        public Game Game
        {
            get { return this.game; }
            protected set 
            {
                // if an old game
                if (this.game != null)
                {
                    // handle frame updating
                    this.game.CurrentFrameUpdated -= new FrameEventHandler(game_CurrentFrameUpdated);

                    // handle game ended
                    this.game.Ended -= new GameEndedEventHandler(game_Ended);

                    // handle completed
                    this.game.Completed -= new GameEndedEventHandler(game_Completed);
                }

                // set game
                this.game = value;

                // if a new value
                if (value != null)
                {
                    // handle frame updating
                    this.game.CurrentFrameUpdated += new FrameEventHandler(game_CurrentFrameUpdated);

                    // handle game ended
                    this.game.Ended += new GameEndedEventHandler(game_Ended);

                    // handle completed
                    this.game.Completed += new GameEndedEventHandler(game_Completed);
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
            get { return this.creator; }
            set { this.creator = value; }
        }

        /// <summary>
        /// Get or set the game creator 
        /// </summary>
        private GameCreator creator;

        /// <summary>
        /// Get or set the error prefix
        /// </summary>
        public String ErrorPrefix
        {
            get { return this.errorPrefix; }
            set { this.errorPrefix = value; }
        }

        /// <summary>
        /// Get or set the error prefix
        /// </summary>
        private String errorPrefix = "OOPS";

        /// <summary>
        /// Get or set the drawer for drawing all frames
        /// </summary>
        public FrameDrawer FrameDrawer
        {
            get { return this.frameDrawer; }
            set { this.frameDrawer = value; }
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
            get { return this.mapDrawer; }
            set { this.mapDrawer = value; }
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
        public Boolean IsProcessingAsyncFileIOOperation
        {
            get { return this.isProcessingAsyncFileIOOperation; }
            protected set { this.isProcessingAsyncFileIOOperation = value; }
        }

        /// <summary>
        /// Get or set if this is processing an asyncronous file IO operation
        /// </summary>
        private Boolean isProcessingAsyncFileIOOperation = false;

        /// <summary>
        /// Get if this should encrypt all files
        /// </summary>
        public Boolean EncryptFiles
        {
            get { return this.encryptFiles; }
            protected set { this.encryptFiles = value; }
        }

        /// <summary>
        /// Get if this should encrypt all files
        /// </summary>
        private Boolean encryptFiles = true;

        /// <summary>
        /// Get or set the output stream
        /// </summary>
        public TextWriter Output
        {
            get { return this.output; }
            set { this.output = value; }
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
            get { return this.input; }
            set { this.input = value; }
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
            get { return this.error; }
            set { this.error = value; }
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
            get { return this.displaySize; }
            set { this.displaySize = value; }
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
            get { return this.waitForKeyPressCallback; }
            set { this.waitForKeyPressCallback = value; }
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
            this.Creator = gameCreator;
        }

        /// <summary>
        /// Initializes a new instance of the GameFlow class
        /// </summary>
        /// <param name="helper">A game helper to create the GameFlow.Creator property from</param>
        public GameFlow(GameCreationHelper helper)
        {
            // set creator
            this.Creator = helper.Creator;
        }

        /// <summary>
        /// Begin the game
        /// </summary>
        public virtual void Begin()
        {
            // reset ID seed so that ID's align between games
            ExaminableObject.ResetIDSeed();

            // create new game
            this.Game = this.Creator.Invoke();

            // begin the game
            this.EnterGameLoop();
        }

        /// <summary>
        /// Display the load file screen
        /// </summary>
        public virtual void DisplayLoadScreen()
        {
            // if directory does not exist
            if (!Directory.Exists(GameSave.DefaultDirectory))
            {
                // create
                Directory.CreateDirectory(GameSave.DefaultDirectory);
            }

            // show save 
            this.Game.Refresh(new LoadFileFrame(GameSave.DefaultDirectory, this.EncryptFiles ? "bin" : "xml"));
        }

        /// <summary>
        /// Display the save file screen
        /// </summary>
        public virtual void DisplaySaveScreen()
        {
            // if directory does not exist
            if (!Directory.Exists(GameSave.DefaultDirectory))
            {
                // create
                Directory.CreateDirectory(GameSave.DefaultDirectory);
            }

            // show save 
            this.Game.Refresh(new SaveFileFrame(GameSave.DefaultDirectory, this.EncryptFiles ? "bin" : "xml"));
        }

        /// <summary>
        /// Try to handle the input at a flow level, i.e higher operations on a Game such as saving, loading and creating new games
        /// </summary>
        /// <param name="input">The input to handle</param>
        /// <returns>The decision based on the input</returns>
        protected virtual Decision TryHandleInputAtFlowLevel(String input)
        {
            // if a command
            if (this.Game.Parser.IsEGameCommand(input))
            {
                // hold command
                EGameCommand command;

                // parse command
                this.Game.Parser.TryParseToEGameCommand(input, out command);

                // handle command
                switch (command)
                {
                    case (EGameCommand.About):
                        {
                            // if some description for game
                            if (!String.IsNullOrEmpty(this.Game.Description))
                            {
                                // show about
                                this.Game.Refresh(this.Game.Description + "\n\nAdventureFramework Copyright © Ben Pollard 2011-2013");
                            }
                            else
                            {
                                // show about
                                this.Game.Refresh("AdventureFramework Copyright © Ben Pollard 2011-2013");
                            }

                            // reacted
                            return new Decision(EReactionToInput.SelfContainedReaction);
                        }
                    case (EGameCommand.Exit):
                        {
                            // exit
                            this.Game.End();

                            // reacted
                            return new Decision(EReactionToInput.SelfContainedReaction, "Exiting...");
                        }
                    case (EGameCommand.Help):
                        {
                            // show
                            this.Game.Refresh(this.Game.HelpFrame);

                            // reacted
                            return new Decision(EReactionToInput.SelfContainedReaction, String.Empty);
                        }
                    case (EGameCommand.Load):
                        {
                            // display the load screen
                            this.DisplayLoadScreen();

                            // reacted
                            return new Decision(EReactionToInput.SelfContainedReaction, "Showing load...");
                        }
                    case (EGameCommand.Map):
                        {
                            // show
                            this.Game.Refresh(new RegionMapFrame(this.Game.Overworld.CurrentRegion, this.MapDrawer));

                            // reacted
                            return new Decision(EReactionToInput.SelfContainedReaction, String.Empty);
                        }
                    case (EGameCommand.New):
                        {
                            // return to title
                            this.Game.Refresh(this.Game.TitleFrame);

                            // reacted
                            return new Decision(EReactionToInput.SelfContainedReaction, "New game");
                        }
                    case (EGameCommand.Save):
                        {
                            // display save screen
                            this.DisplaySaveScreen();

                            // reacted
                            return new Decision(EReactionToInput.SelfContainedReaction, "Showing save...");
                        }
                    case (EGameCommand.SoundOff):
                        {
                            // sounds off
                            SoundPlayer.UseSounds = false;

                            // reacted
                            return new Decision(EReactionToInput.CouldReact, "Sounds are off");
                        }
                    case (EGameCommand.SoundOn):
                        {
                            // sounds on
                            SoundPlayer.UseSounds = true;

                            // reacted
                            return new Decision(EReactionToInput.CouldReact, "Sounds are on");
                        }
                    default: { throw new NotImplementedException(); }
                }
            }
            else if (this.game.Parser.IsEFrameDrawerOption(input))
            {
                // hold command
                EFrameDrawerOption command;

                // parse command
                this.game.Parser.TryParseToEFrameDrawerOption(input, out command);

                // handle command
                switch (command)
                {
                    case (EFrameDrawerOption.CommandsOff):
                        {
                            // turn commands off
                            this.FrameDrawer.DisplayCommands = false;

                            // reacted
                            return new Decision(EReactionToInput.CouldReact, "Commands have been turned off");
                        }
                    case (EFrameDrawerOption.CommandsOn):
                        {
                            // turn commands on
                            this.FrameDrawer.DisplayCommands = true;

                            // reacted
                            return new Decision(EReactionToInput.CouldReact, "Commands have been turned on");
                        }
                    case (EFrameDrawerOption.Invert):
                        {
                            // if subscribers
                            if (this.DisplayInverted != null)
                            {
                                // dispatch changed
                                this.DisplayInverted(this, new EventArgs());

                                // reacted
                                return new Decision(EReactionToInput.CouldReact, "Colours have been inverted");
                            }
                            else
                            {
                                // not reacted
                                return new Decision(EReactionToInput.CouldntReact, "Colours have been not been inverted as no handling has been specified");
                            }
                        }
                    case (EFrameDrawerOption.KeyOff):
                        {
                            // turn key off
                            this.MapDrawer.Key = EKeyType.None;

                            // reacted
                            return new Decision(EReactionToInput.CouldReact, "Key has been turned off");
                        }
                    case (EFrameDrawerOption.KeyOn):
                        {
                            // turn key off
                            this.MapDrawer.Key = EKeyType.Dynamic;

                            // reacted
                            return new Decision(EReactionToInput.CouldReact, "Key has been turned on");
                        }
                    default: { throw new NotImplementedException(); }
                }
            }
            else
            {
                // reacted
                return new Decision(EReactionToInput.CouldntReact);
            }
        }

        /// <summary>
        /// Enter the game loop
        /// </summary>
        protected virtual void EnterGameLoop()
        {
            try
            {
                // hold input
                String input = String.Empty;

                // hold any message
                String message = String.Empty;

                // do title
                this.Game.Refresh(this.Game.TitleFrame);

                // hold is new is already loaded
                Boolean newHasBeenLoaded = true;

                // remove handle for displaying custom frames
                FrameDrawer.DisplayedSpecialFrame += new FrameEventHandler(FrameDrawer_DisplayedSpecialFrame);

                // handle loads
                GameSave.GameLoaded += new GameIOEventHandler(GameSave_GameLoaded);

                // hold reaction to input
                Decision reaction = null;

                // hold if input handling should be displayed
                Boolean displayReactionToInput = true;

                do
                {
                    // reset message
                    message = String.Empty;

                    // reset reaction
                    reaction = null;

                    // reset if handling
                    displayReactionToInput = true;

                    // if processing background operation
                    while (this.IsProcessingAsyncFileIOOperation)
                    {
                        // wait for the IO operation to finish
                    }

                    // if don't accept input
                    if (!this.Game.CurrentFrame.AcceptsInput)
                    {
                        // hold scene
                        Frame frame = this.Game.CurrentFrame;

                        // wait for enter, or frame change
                        while ((!this.WaitForKeyPressCallback(Convert.ToChar(13))) &&
                               (this.Game.CurrentFrame == frame))
                        {
                            // redraw
                            this.DrawFrame(this.Game.CurrentFrame);
                        }
                    }
                    else
                    {
                        // get input
                        input = this.Input.ReadLine();
                    }

                    // if a title screne
                    if (this.Game.CurrentFrame is TitleFrame)
                    {
                        // if not new loaded already
                        if (!newHasBeenLoaded)
                        {
                            // reset ID seed so that ID's align
                            ExaminableObject.ResetIDSeed();

                            // reset game
                            this.Game = this.Creator.Invoke();
                        }

                        // enter game
                        this.Game.EnterGame(this.DisplaySize.Width, this.DisplaySize.Height, this.MapDrawer);

                        // skip input handling
                        displayReactionToInput = false;
                    }
                    else if (this.Game.CurrentFrame is EndFrame)
                    {
                        // goto title screen
                        this.Game.Refresh(this.Game.TitleFrame);

                        // skip input handling
                        displayReactionToInput = false;
                    }
                    else if (this.Game.CurrentFrame is SaveFileFrame)
                    {
                        // if some input
                        if (!String.IsNullOrEmpty(input))
                        {
                            // hold file name
                            String filename;

                            // hold file index
                            Int32 fileIndex;

                            // if parses
                            if (Int32.TryParse(input, out fileIndex))
                            {
                                // hold reaction
                                reaction = ((FileIOFrame)this.Game.CurrentFrame).TryDetermineValidFileName(fileIndex, out filename);
                            }
                            else
                            {
                                // if can get a file name
                                reaction = ((FileIOFrame)this.Game.CurrentFrame).TryDetermineValidFileName(input, out filename);
                            }

                            // handle reaction
                            switch (reaction.Result)
                            {
                                case (EReactionToInput.CouldntReact):
                                    {
                                        // set decision
                                        reaction = new Decision(EReactionToInput.CouldntReact, "Save failed: " + reaction.Reason);

                                        break;
                                    }
                                case (EReactionToInput.CouldReact):
                                    {
                                        // save
                                        this.BeginAsyncSave(filename, this.EncryptFiles);

                                        // set decision
                                        reaction = new Decision(EReactionToInput.SelfContainedReaction, "Saving...");

                                        break;
                                    }
                                case (EReactionToInput.SelfContainedReaction):
                                    {
                                        // don't handle
                                        displayReactionToInput = false;

                                        break;
                                    }
                                default: { throw new NotImplementedException(); }
                            }
                        }
                        else
                        {
                            // refresh
                            this.Game.Refresh();

                            // skip input handling
                            displayReactionToInput = false;
                        }
                    }
                    else if (this.Game.CurrentFrame is LoadFileFrame)
                    {
                        // if accepts input and some input
                        if ((this.Game.CurrentFrame.AcceptsInput) &&
                            (!String.IsNullOrEmpty(input)))
                        {
                            // hold file name
                            String filename;

                            // hold file index
                            Int32 fileIndex;

                            // if parses
                            if (Int32.TryParse(input, out fileIndex))
                            {
                                // if can get a file name
                                reaction = ((FileIOFrame)this.Game.CurrentFrame).TryDetermineValidFileName(fileIndex, out filename);
                            }
                            else
                            {
                                // if can get a file name
                                reaction = ((FileIOFrame)this.Game.CurrentFrame).TryDetermineValidFileName(input, out filename);
                            }

                            // handle reaction
                            switch (reaction.Result)
                            {
                                case (EReactionToInput.CouldntReact):
                                    {
                                        // set decision
                                        reaction = new Decision(EReactionToInput.CouldntReact, "Load failed: " + reaction.Reason);

                                        break;
                                    }
                                case (EReactionToInput.CouldReact):
                                    {
                                        // clear previous delegates
                                        this.sourceLoadedDelegation.Clear();

                                        // save interaction
                                        this.Game.RegisterTransferableChildren(ref this.sourceLoadedDelegation);

                                        // save
                                        this.BeginAsyncLoad(filename, this.EncryptFiles);

                                        // set decision
                                        reaction = new Decision(EReactionToInput.SelfContainedReaction, "Loading...");

                                        break;
                                    }
                                case (EReactionToInput.SelfContainedReaction):
                                    {
                                        // don't handle
                                        displayReactionToInput = false;

                                        break;
                                    }
                                default: { throw new NotImplementedException(); }
                            }
                        }
                        else
                        {
                            // refresh
                            this.Game.Refresh();

                            // skip input handling
                            displayReactionToInput = false;
                        }
                    }
                    else if (!this.Game.CurrentFrame.AcceptsInput)
                    {
                        // refresh
                        this.Game.Refresh();

                        // skip input handling
                        displayReactionToInput = false;
                    }
                    else
                    {
                        // if game was unmodified before
                        if (newHasBeenLoaded)
                        {
                            // new now modified
                            newHasBeenLoaded = false;
                        }

                        // hold reaction
                        reaction = this.TryHandleInputAtFlowLevel(input);

                        // try and handle result of command as a flow command
                        switch (reaction.Result)
                        {
                            case (EReactionToInput.CouldReact):
                            case (EReactionToInput.SelfContainedReaction):
                                {
                                    // all done

                                    break;
                                }
                            default:
                                {
                                    // get the reaction of the game
                                    reaction = this.Game.ReactToInput(input);

                                    break;
                                }
                        }
                    }

                    // if displaying reaction to input
                    if (displayReactionToInput)
                    {
                        // select reaction
                        switch (reaction.Result)
                        {
                            case (EReactionToInput.CouldntReact):
                                {
                                    // display
                                    message = this.ErrorPrefix + ": " + reaction.Reason;

                                    // draw scene latest scene
                                    this.UpdateScreenWithCurrentFrame(message);

                                    break;
                                }
                            case (EReactionToInput.CouldReact):
                                {
                                    // display message
                                    message = reaction.Reason;

                                    // draw scene latest scene
                                    this.UpdateScreenWithCurrentFrame(message);

                                    break;
                                }
                            case (EReactionToInput.SelfContainedReaction):
                                {
                                    // do nothing

                                    break;
                                }
                            default: { throw new NotImplementedException(); }
                        }
                    }
                }
                while (!this.Game.HasEnded);
            }
            finally
            {
                // remove handle for displaying custom frames
                FrameDrawer.DisplayedSpecialFrame -= new FrameEventHandler(FrameDrawer_DisplayedSpecialFrame);

                // remove handle for loads
                GameSave.GameLoaded -= new GameIOEventHandler(GameSave_GameLoaded);
            }
        }

        /// <summary>
        /// Update the screen with the current Frame, provided by the GameFlow.Game property
        /// </summary>
        /// <param name="message">An additional message to display to the user</param>
        protected virtual void UpdateScreenWithCurrentFrame(String message)
        {
            // get the scene
            SceneFrame scene = this.Game.GetScene(this.MapDrawer, this.DisplaySize.Width, this.DisplaySize.Height, message);

            // draw frame
            this.DrawFrame(scene);
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
                if (this.StartingFrameDraw != null)
                {
                    // dispatch updated
                    this.StartingFrameDraw(this, new FrameEventArgs(frame));
                }

                // if was a last frame
                if (this.lastFrame != null)
                {
                    // remove handle for invalidation
                    this.lastFrame.Invalidated -= new FrameEventHandler(frame_Invalidated);
                }

                // if frame changed
                if ((this.lastFrame != frame) &&
                    (this.lastFrame != null))
                {
                    // dispose
                    this.lastFrame.Dispose();

                    // reset
                    this.lastFrame = null;
                }

                // set last frame
                this.lastFrame = frame;

                // handle invalidation
                this.lastFrame.Invalidated += new FrameEventHandler(frame_Invalidated);

                // update output display
                this.Output.WriteLine(frame.BuildFrame(this.DisplaySize.Width, this.DisplaySize.Height, this.FrameDrawer));

                // if subscribers
                if (this.FinishingFrameDraw != null)
                {
                    // dispatch updated
                    this.FinishingFrameDraw(this, new FrameEventArgs(frame));
                }
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
            this.DrawFrame(frame);
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
                case (EExitMode.ExitApplication):
                    {
                        // exit with OK code
                        Environment.Exit(0);

                        break;
                    }
                case (EExitMode.ReturnToTitleScreen):
                    {
                        // return to title
                        this.Game.Refresh(this.Game.TitleFrame);

                        break;
                    }
                default: { throw new NotImplementedException(); }
            }
        }

        /// <summary>
        /// Handle the GameFlow.Game.Completed event
        /// </summary>
        protected virtual void OnGameCompleted()
        {
            // draw completion screen
            this.DrawFrame(game.CompletionFrame);
        }

        /// <summary>
        /// Handle disposal of this GameFlow
        /// </summary>
        protected virtual void OnDisposed()
        {
            // if input not null
            if (this.Input != null)
            {
                // dispose
                this.Input.Dispose();

                // release
                this.Input = null;
            }

            // if output not null
            if (this.Output != null)
            {
                // dispose
                this.Output.Dispose();

                // release
                this.Output = null;
            }

            // if error not null
            if (this.Error != null)
            {
                // dispose
                this.Error.Dispose();

                // release
                this.Error = null;
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
            if ((this.ioWorker != null) &&
                (this.ioWorker.IsBusy))
            {
                // throw exception
                //throw new Exception("Can't perform an IO operation while one is already is progress");
            }
            else
            {
                // mark that is processing
                this.IsProcessingAsyncFileIOOperation = true;

                // create worker
                this.ioWorker = new BackgroundWorker();

                // handle work
                this.ioWorker.DoWork += new DoWorkEventHandler((object sender, DoWorkEventArgs e) =>
                {
                    try
                    {
                        // hold message
                        String message;

                        // handle operation
                        operation.Invoke(out message);

                        // show scene update
                        this.Game.Refresh(message);

                        // mark that is not processing
                        this.IsProcessingAsyncFileIOOperation = false;
                    }
                    catch (Exception ex)
                    {
                        // show scene update
                        this.Game.Refresh(ex.Message);

                        // mark that is not processing
                        this.IsProcessingAsyncFileIOOperation = false;
                    }
                    finally
                    {
                        // if marked as processing
                        if (this.IsProcessingAsyncFileIOOperation)
                        {
                            // mark that is not processing
                            this.IsProcessingAsyncFileIOOperation = false;
                        }
                    }
                });

                // work -^
                this.ioWorker.RunWorkerAsync();

                // now handle loading operation 
                this.Game.Refresh(frameToDisplayWhileInOperation);
            }
        }

        /// <summary>
        /// Start an async IO operations
        /// </summary>
        /// <param name="operation">The operation to carry out</param>
        /// <param name="fileName">The full file name of the file to perform all IO operation on</param>
        /// <param name="frameToDisplayWhileInOperation">The frame to display while in operation</param>
        protected void StartAsyncIOOperation(IOSprecifiedFileOperationCallback operation, String fileName, Frame frameToDisplayWhileInOperation)
        {
            // if worker is already working
            if ((this.ioWorker != null) &&
                (this.ioWorker.IsBusy))
            {
                // throw exception
                //throw new Exception("Can't perform an IO operation while one is already is progress");
            }
            else
            {
                // mark that is processing
                this.IsProcessingAsyncFileIOOperation = true;

                // create worker
                this.ioWorker = new BackgroundWorker();

                // handle work
                this.ioWorker.DoWork += new DoWorkEventHandler((object sender, DoWorkEventArgs e) =>
                {
                    try
                    {
                        // hold message
                        String message;

                        // handle operation
                        operation.Invoke(out message, fileName);

                        // show scene update
                        this.Game.Refresh(message);

                        // mark that is not processing
                        this.IsProcessingAsyncFileIOOperation = false;
                    }
                    catch (Exception ex)
                    {
                        // show scene update
                        this.Game.Refresh(ex.Message);

                        // mark that is not processing
                        this.IsProcessingAsyncFileIOOperation = false;
                    }
                    finally
                    {
                        // if marked as processing
                        if (this.IsProcessingAsyncFileIOOperation)
                        {
                            // mark that is not processing
                            this.IsProcessingAsyncFileIOOperation = false;
                        }
                    }
                });

                // work -^
                this.ioWorker.RunWorkerAsync();

                // now handle loading operation 
                this.Game.Refresh(frameToDisplayWhileInOperation);
            }
        }

        #region Saving

        /// <summary>
        /// Save the game as xml
        /// </summary>
        /// <returns>If the save was sucsessful</returns>
        public virtual Boolean SaveGameAsXML()
        {
            // hold message
            String message;

            // save
            return this.SaveGameAsXML(out message);
        }

        /// <summary>
        /// Save the game as xml
        /// </summary>
        /// <param name="message">Any message regarding the save</param>
        /// <returns>If the save was sucsessful</returns>
        public virtual Boolean SaveGameAsXML(out String message)
        {
            // save
            return GameSave.SaveGameAsXML(AppDomain.CurrentDomain.BaseDirectory + GameFlow.defaultFileName + ".xml", this.Game, out message, false);
        }

        /// <summary>
        /// Save the game as xml
        /// </summary>
        /// <param name="message">Any message regarding the save</param>
        /// <param name="fileName">The full file name to save the file as</param>
        /// <returns>If the save was sucsessful</returns>
        public virtual Boolean SaveGameAsXML(out String message, String fileName)
        {
            // save
            return GameSave.SaveGameAsXML(fileName, this.Game, out message, false);
        }

        /// <summary>
        /// Save the game as encrypted xml
        /// </summary>
        /// <returns>If the save was sucsessful</returns>
        public virtual Boolean SaveGameAsEncyptedXML()
        {
            // hold message
            String message;

            // save
            return this.SaveGameAsEncyptedXML(out message);
        }

        /// <summary>
        /// Save the game as encrypted xml
        /// </summary>
        /// <param name="message">Any message regarding the save</param>
        /// <returns>If the save was sucsessful</returns>
        public virtual Boolean SaveGameAsEncyptedXML(out String message)
        {
            // save
            return GameSave.SaveGameAsXML(AppDomain.CurrentDomain.BaseDirectory + GameFlow.defaultFileName + ".bin", this.Game, out message, true);
        }

        /// <summary>
        /// Save the game as encrypted xml
        /// </summary>
        /// <param name="message">Any message regarding the save</param>
        /// <param name="fileName">The full file name to save the file as</param>
        /// <returns>If the save was sucsessful</returns>
        public virtual Boolean SaveGameAsEncyptedXML(out String message, String fileName)
        {
            // save
            return GameSave.SaveGameAsXML(fileName, this.Game, out message, true);
        }

        /// <summary>
        /// Begin an encrypted asyncronous save operation
        /// </summary>
        /// <param name="fileName">The file name to save the file as</param>
        /// <param name="isEncrypted">Specify if the file is encrypted</param>
        public virtual void BeginAsyncSave(String fileName, Boolean isEncrypted)
        {
            // if using encryption
            if (isEncrypted)
            {
                // do operation
                this.StartAsyncIOOperation(new IOSprecifiedFileOperationCallback(this.SaveGameAsEncyptedXML), fileName, new AsyncIOOperationFrame(EIOOperation.Save));
            }
            else
            {
                // do operation
                this.StartAsyncIOOperation(new IOSprecifiedFileOperationCallback(this.SaveGameAsXML), fileName, new AsyncIOOperationFrame(EIOOperation.Save));
            }
        }
        
        #endregion

        #region Loading

        /// <summary>
        /// Load a game from an xml file
        /// </summary>
        /// <returns>If the load was sucsessful</returns>
        public virtual Boolean LoadGameAsXML()
        {
            // hold message
            String message;

            // load
            return this.LoadGameAsXML(out message);
        }

        /// <summary>
        /// Load a game from an xml file
        /// </summary>
        /// <param name="message">Any message regarding the load</param>
        /// <returns>If the load was sucsessful</returns>
        public virtual Boolean LoadGameAsXML(out String message)
        {
            // load
            return GameSave.LoadGameAsXML(AppDomain.CurrentDomain.BaseDirectory + GameFlow.defaultFileName + ".xml", this.Game, out message, false);
        }

        /// <summary>
        /// Load a game from an xml file
        /// </summary>
        /// <param name="message">Any message regarding the load</param>
        /// <param name="fileName">The full file name of the file to load</param>
        /// <returns>If the load was sucsessful</returns>
        public virtual Boolean LoadGameAsXML(out String message, String fileName)
        {
            // load
            return GameSave.LoadGameAsXML(fileName, this.Game, out message, false);
        }

        /// <summary>
        /// Load a game from an encrypted xml file
        /// </summary>
        /// <returns>If the load was sucsessful</returns>
        public virtual Boolean LoadGameAsEncryptedXML()
        {
            // hold message
            String message;

            // load
            return this.LoadGameAsEncryptedXML(out message);
        }

        /// <summary>
        /// Load a game from an encrypted xml file
        /// </summary>
        /// <param name="message">Any message regarding the load</param>
        /// <returns>If the load was sucsessful</returns>
        public virtual Boolean LoadGameAsEncryptedXML(out String message)
        {
            // load
            return GameSave.LoadGameAsXML(AppDomain.CurrentDomain.BaseDirectory + GameFlow.defaultFileName + ".bin", this.Game, out message, true);
        }

        /// <summary>
        /// Load a game from an encrypted xml file
        /// </summary>
        /// <param name="message">Any message regarding the load</param>
        /// <param name="fileName">The full file name of the file to load</param>
        /// <returns>If the load was sucsessful</returns>
        public virtual Boolean LoadGameAsEncryptedXML(out String message, String fileName)
        {
            // load
            return GameSave.LoadGameAsXML(fileName, this.Game, out message, true);
        }

        /// <summary>
        /// Begin an encrypted asyncronous load operation
        /// </summary>
        /// <param name="fileName">The full file name of the file to load</param>
        /// <param name="isEncrypted">Specify if the file is encrypted</param>
        public virtual void BeginAsyncLoad(String fileName, Boolean isEncrypted)
        {
            // if encrypted
            if (isEncrypted)
            {
                // do operation
                this.StartAsyncIOOperation(new IOSprecifiedFileOperationCallback(this.LoadGameAsEncryptedXML), fileName, new AsyncIOOperationFrame(EIOOperation.Load));
            }
            else
            {
                // do operation
                this.StartAsyncIOOperation(new IOSprecifiedFileOperationCallback(this.LoadGameAsXML), fileName, new AsyncIOOperationFrame(EIOOperation.Load));
            }
        }

        #endregion

        #endregion

        #region EventHandlers

        private void game_CurrentFrameUpdated(object sender, FrameEventArgs e)
        {
            this.OnFrameUpdated(e.Frame);
        }

        private void game_Ended(object sender, GameEndedEventArgs e)
        {
            this.OnGameEnded(e.ExitMode);
        }

        void game_Completed(object sender, GameEndedEventArgs e)
        {
            this.OnGameCompleted();
        }

        void FrameDrawer_DisplayedSpecialFrame(object sender, FrameEventArgs e)
        {
            // refresh
            this.Game.Refresh(e.Frame);
        }

        void frame_Invalidated(object sender, FrameEventArgs e)
        {
            // redraw
            this.DrawFrame(e.Frame);
        }

        void GameSave_GameLoaded(object sender, GameIOEventArgs e)
        {
            // this is the 'clever' bit where all delegation is passed between games, allowing for complex saves

            // get the list for the new game
            List<ITransferableDelegation> targetList = new List<ITransferableDelegation>();

            // now get targets
            e.Game.RegisterTransferableChildren(ref targetList);

            // itterate all source children
            foreach (ITransferableDelegation source in this.sourceLoadedDelegation)
            {
                // get transferal ID
                String sourceID = source.GenerateTransferalID();

                // hold matches of ID
                IEnumerable<ITransferableDelegation> matches = targetList.Where<ITransferableDelegation>((ITransferableDelegation targetObj) => targetObj.GenerateTransferalID() == sourceID);

                // itterate all matches
                foreach (ITransferableDelegation match in matches)
                {
                    // do the transfer!
                    match.TransferFrom(source);
                }
            }
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Dispose this GameFlow
        /// </summary>
        public void Dispose()
        {
            this.OnDisposed();

            // if a worker
            if (this.ioWorker != null)
            {
                // dispose
                this.ioWorker.Dispose();
            }
        }

        #endregion
    }

    /// <summary>
    /// Callback that invokes a callback for waiting for a key press
    /// </summary>
    /// <param name="key">The ASCII code of the key to wait for</param>
    /// <returns>If the key pressed returned the same ASCII character as the key property</returns>
    public delegate Boolean WaitForKeyPressCallback(Char key);
}
