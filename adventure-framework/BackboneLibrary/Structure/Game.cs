using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventureFramework.Locations;
using AdventureFramework.Rendering;
using System.Xml.Serialization;
using AdventureFramework.Interaction;
using AdventureFramework.IO;
using System.Xml;
using System.ComponentModel;
using System.Threading;
using AdventureFramework.Rendering.Frames;

namespace AdventureFramework.Structure
{
    /// <summary>
    /// Represents the structure of the game
    /// </summary>
    public class Game : XMLSerializableObject, ITransferableDelegation
    {
        #region Properties

        /// <summary>
        /// Get or set the player
        /// </summary>
        public PlayableCharacter Player
        {
            get { return this.player; }
            set 
            {
                // if a player
                if (this.player != null)
                {
                    // handle deaths
                    this.player.Died -= new ReasonEventHandler(player_Died);
                }

                // set new value    
                this.player = value;

                // if a player
                if (this.player != null)
                {
                    // handle deaths
                    this.player.Died += new ReasonEventHandler(player_Died);
                }
            }
        }

        /// <summary>
        /// Get or set the player
        /// </summary>
        private PlayableCharacter player;

        /// <summary>
        /// Get or set the Overworld
        /// </summary>
        public Overworld Overworld
        {
            get { return this.overworld; }
            set { this.overworld = value; }
        }

        /// <summary>
        /// Get or set the Overworld
        /// </summary>
        private Overworld overworld;

        /// <summary>
        /// Get or set the name
        /// </summary>
        public String Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        /// <summary>
        /// Get or set the name
        /// </summary>
        private String name;

        /// <summary>
        /// Get or set the description
        /// </summary>
        public String Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        /// <summary>
        /// Get or set the description
        /// </summary>
        private String description;

        /// <summary>
        /// Get if this game has ended
        /// </summary>
        public Boolean HasEnded
        {
            get { return this.hasEnded; }
            protected set { this.hasEnded = value; }
        }

        /// <summary>
        /// Get or set if this game has ended
        /// </summary>
        private Boolean hasEnded = false;

        /// <summary>
        /// Get or set this Games frame to display for the title screen
        /// </summary>
        public TitleFrame TitleFrame
        {
            get { return this.titleFrame; }
            set { this.titleFrame = value; }
        }

        /// <summary>
        /// Get or set this Games frame to display for the title screen
        /// </summary>
        private TitleFrame titleFrame;

        /// <summary>
        /// Get or set this Games frame to display upon completion
        /// </summary>
        public Frame CompletionFrame
        {
            get { return this.completionFrame; }
            set { this.completionFrame = value; }
        }

        /// <summary>
        /// Get or set this Games frame to display upon completion
        /// </summary>
        private Frame completionFrame;

        /// <summary>
        /// Get or set this Games help screen
        /// </summary>
        public HelpFrame HelpFrame
        {
            get { return this.helpFrame; }
            set { this.helpFrame = value; }
        }

        /// <summary>
        /// Get or set this Games help screen
        /// </summary>
        private HelpFrame helpFrame;

        /// <summary>
        /// Get the current Frame
        /// </summary>
        public Frame CurrentFrame
        {
            get { return this.currentFrame; }
            protected set { this.currentFrame = value; }
        }

        /// <summary>
        /// Get or set the current Frame
        /// </summary>
        private Frame currentFrame;

        /// <summary>
        /// Occurs when the CurrentFrame is updated
        /// </summary>
        public event FrameEventHandler CurrentFrameUpdated;

        /// <summary>
        /// Occurs when the Game has ended
        /// </summary>
        public event GameEndedEventHandler Ended;

        /// <summary>
        /// Occurs when the Game has been completed
        /// </summary>
        public event GameEndedEventHandler Completed;

        /// <summary>
        /// Get or set the last used width
        /// </summary>
        protected Int32 lastUsedWidth;

        /// <summary>
        /// Get or set the last used height
        /// </summary>
        protected Int32 lastUsedHeight;

        /// <summary>
        /// Get or set the last used map drawer
        /// </summary>
        protected MapDrawer lastUsedMapDrawer;

        /// <summary>
        /// Get or set the completion condition
        /// </summary>
        public CompletionCheck CompletionCondition
        {
            get { return this.completionCondition; }
            set { this.completionCondition = value; }
        }

        /// <summary>
        /// Get or set the completion condition
        /// </summary>
        private CompletionCheck completionCondition;

        /// <summary>
        /// Get or set the parser for this Game
        /// </summary>
        public TextParser Parser
        {
            get { return this.parser; }
            set { this.parser = value; }
        }

        /// <summary>
        /// Get or set the parser for this Game
        /// </summary>
        private TextParser parser = new TextParser();

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the Game class
        /// </summary>
        protected Game()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Game class
        /// </summary>
        /// <param name="name">The name of this Game</param>
        /// <param name="description">A description of this Game</param>
        /// <param name="player">The Player to use for this Game</param>
        /// <param name="overworld">A Overworld to use for this Game</param>
        public Game(String name, String description, PlayableCharacter player, Overworld overworld)
        {
            // set name
            this.Name = name;

            // set description
            this.Description = description;

            // set player
            this.Player = player;
            
            // set overworld
            this.Overworld = overworld;
        }

        /// <summary>
        /// End the Game
        /// </summary>
        public void End()
        { 
            // handle
            this.OnGameEnded(EExitMode.ExitApplication);
        }

        /// <summary>
        /// Handle CurrentFrame updating
        /// </summary>
        /// <param name="frame">The updated frame</param>
        protected virtual void OnCurrentFrameUpdated(Frame frame)
        {
            // set the current frame
            this.CurrentFrame = frame;

            // check event has subscribers
            if (this.CurrentFrameUpdated != null)
            {
                // dispatch event
                this.CurrentFrameUpdated(this, new FrameEventArgs(frame));
            }
        }

        /// <summary>
        /// Handle game ended
        /// </summary>
        /// <param name="mode">The exit mode</param>
        protected virtual void OnGameEnded(EExitMode mode)
        {
            // ended
            this.HasEnded = true;

            // check for subscribers
            if (this.Ended != null)
            {
                // dispatch event
                this.Ended(this, new GameEndedEventArgs(mode));
            }
        }

        /// <summary>
        /// React to input
        /// </summary>
        /// <param name="input">The input to react to</param>
        /// <returns>A result detailing the reaction</returns>
        public virtual Decision ReactToInput(String input)
        {
            // hold message
            String message;

            // react
            EReactionToInput reacted = this.Parser.ReactToInput(input, this, out message);

            // check for completion
            if (this.CompletionCondition(this))
            {
                // check for completion subscribers
                if (this.Completed != null)
                {
                    // dispatch event
                    this.Completed(this, new GameEndedEventArgs(EExitMode.ReturnToTitleScreen));
                }

                // set frame to completion frame
                this.CurrentFrame = this.CompletionFrame;

                // return completion
                return new Decision(EReactionToInput.SelfContainedReaction, "You have finished the game");
            }
            else
            {            
                // return result
                return new Decision(reacted, message);
            }
        }

        /// <summary>
        /// Enter the game
        /// </summary>
        /// <param name="width">The width of the game</param>
        /// <param name="height">The height of the game</param>
        /// <param name="drawer">A drawer to use for constructing the map</param>
        public void EnterGame(Int32 width, Int32 height, MapDrawer drawer)
        {
            // set width
            this.lastUsedWidth = width;

            // set height
            this.lastUsedHeight = height;

            // set drawer
            this.lastUsedMapDrawer = drawer;

            // update
            this.OnCurrentFrameUpdated(this.GetScene());
        }

        /// <summary>
        /// Get a scene based on the current game
        /// </summary>
        /// <returns>A constructed frame of the scene</returns>
        public SceneFrame GetScene()
        {
            // get scene
            return this.GetScene(this.lastUsedMapDrawer, this.lastUsedWidth, this.lastUsedHeight);
        }

        /// <summary>
        /// Get a scene based on the current game
        /// </summary>
        /// <param name="drawer">A drawer to use for constructing the map</param>
        /// <returns>A constructed frame of the scene</returns>
        public SceneFrame GetScene(MapDrawer drawer)
        {
            // get scene
            return this.GetScene(drawer, this.lastUsedWidth, this.lastUsedHeight);
        }

        /// <summary>
        /// Get a scene based on the current game
        /// </summary>
        /// <param name="drawer">A drawer to use for constructing the map</param>
        /// <param name="width">The width of the scene</param>
        /// <param name="height">The height of the scene</param>
        /// <returns>A constructed frame of the scene</returns>
        public SceneFrame GetScene(MapDrawer drawer, Int32 width, Int32 height)
        {
            // get scene
            return this.GetScene(drawer, width, height, String.Empty);
        }

        /// <summary>
        /// Get a scene based on the current game
        /// </summary>
        /// <param name="drawer">A drawer to use for constructing the map</param>
        /// <param name="width">The width of the scene</param>
        /// <param name="height">The height of the scene</param>
        /// <param name="messageToUser">A message to the user</param>
        /// <returns>A constructed frame of the scene</returns>
        public virtual SceneFrame GetScene(MapDrawer drawer, Int32 width, Int32 height, String messageToUser)
        {
            // set width
            this.lastUsedWidth = width;

            // set height
            this.lastUsedHeight = height;

            // set map drawer
            this.lastUsedMapDrawer = drawer;

            // construct scene
            return new SceneFrame(this.Overworld.CurrentRegion.CurrentRoom, this.Player, messageToUser, drawer);
        }

        /// <summary>
        /// Handle player deaths
        /// </summary>
        /// <param name="titleMessage">A title message to display</param>
        /// <param name="reason">A reason for the death</param>
        protected virtual void OnHandlePlayerDied(String titleMessage, String reason)
        {
            // update scene
            this.OnCurrentFrameUpdated(new EndFrame(titleMessage, reason));
        }

        /// <summary>
        /// Get all objects in this Game (that are within the current scope) that implement IImplementOwnActions
        /// </summary>
        /// <returns>All IImplementOwnActions objects</returns>
        public virtual IImplementOwnActions[] GetAllObjectsWithAdditionalCommands()
        {
            // maybe a custom comandable object
            List<IImplementOwnActions> commandables = new List<IImplementOwnActions>();

            // add room
            commandables.AddRange(this.Overworld.CurrentRegion.CurrentRoom.GetAllObjectsWithAdditionalCommands());

            // add player
            commandables.AddRange(this.Player.GetAllObjectsWithAdditionalCommands());

            // return commandables
            return commandables.ToArray<IImplementOwnActions>();
        }

        /// <summary>
        /// Get if a string is a valid ActionableCommand
        /// </summary>
        /// <param name="command">The command to search for</param>
        /// <returns>True if the command was fouund, else false</returns>
        public Boolean IsValidActionableCommand(String command)
        {
            // get if valid
            return this.FindActionableCommand(command) != null;
        }

        /// <summary>
        /// Find a ActionableCommand in this Games IImplementOwnActions objects
        /// </summary>
        /// <param name="command">The command to search for</param>
        /// <returns>The first ActionableCommand whose Command property matches the command parameter</returns>
        public virtual ActionableCommand FindActionableCommand(String command)
        {
            // get all commandable objects
            IImplementOwnActions[] commandables = this.GetAllObjectsWithAdditionalCommands();

            // hold command
            ActionableCommand customCommand;

            // get all custom commands
            foreach (IImplementOwnActions commandable in commandables)
            {
                // try and find command
                customCommand = commandable.FindCommand(command);

                // if found
                if (customCommand != null)
                {
                    // return
                    return customCommand;
                }
            }

            // retunr null as not found
            return null;
        }

        /// <summary>
        /// Find a IImplementOwnActions in this objects IImplementOwnActions objects
        /// </summary>
        /// <param name="command">The command to search for</param>
        /// <returns>The first IImplementOwnActions object whose Command property matches the command parameter</returns>
        public virtual IImplementOwnActions FindIImplementOwnActionsObject(String command)
        {
            // get all commandable objects
            IImplementOwnActions[] commandables = this.GetAllObjectsWithAdditionalCommands();

            // hold command
            ActionableCommand customCommand;

            // get all custom commands
            foreach (IImplementOwnActions commandable in commandables)
            {
                // try and find command
                customCommand = commandable.FindCommand(command);

                // if found
                if (command != null)
                {
                    // return
                    return commandable;
                }
            }

            // return null as not found
            return null;
        }

        /// <summary>
        /// Find an interaction target within the current scope fo this Game
        /// </summary>
        /// <param name="name">The targets name</param>
        /// <returns>The first IInteractWithItem object which has a name that matches the name parameter</returns>
        public virtual IInteractWithItem FindInteractionTarget(String name)
        {
            // hold name in upper case
            name = name.ToUpper();

            // if player
            if (this.Player.Name.ToUpper() == name)
            {
                // player
                return this.Player;
            }
            else if (this.Player.Items.Where<Item>((Item i) => i.Name.ToUpper() == name).Count<Item>() > 0)
            {
                // a player item
                
                // hold item
                Item i;

                // find item
                this.Player.FindItem(name, out i);

                // return item
                return i;
            }
            else if (this.Overworld.CurrentRegion.CurrentRoom.Name.ToUpper() == name)
            {
                // room
                return this.Overworld.CurrentRegion.CurrentRoom;
            }
            else if (this.Overworld.CurrentRegion.CurrentRoom.ContainsInteractionTarget(name))
            {
                // hold target
                IInteractWithItem target;

                // find
                this.Overworld.CurrentRegion.CurrentRoom.FindInteractionTarget(name, out target);

                // return
                return target;
            }
            else
            {
                // not found
                return null;
            }
        }

        /// <summary>
        /// Refresh the current frame
        /// </summary>
        public virtual void Refresh()
        {
            // refresh
            this.Refresh(String.Empty);
        }

        /// <summary>
        /// Refresh the current frame
        /// </summary>
        /// <param name="message">Any message to display</param>
        public virtual void Refresh(String message)
        {
            // show scene update
            this.OnCurrentFrameUpdated(new SceneFrame(this.Overworld.CurrentRegion.CurrentRoom, this.Player, message));
        }

        /// <summary>
        /// Refresh the current frame
        /// </summary>
        /// <param name="frame">A frame to display</param>
        public virtual void Refresh(Frame frame)
        {
            // show scene update
            this.OnCurrentFrameUpdated(frame);
        }

        /// <summary>
        /// Handle generation of a transferable ID for this Game
        /// </summary>
        /// <returns>The ID of this object as a string</returns>
        protected virtual String OnGenerateTransferalID()
        {
            return this.Name;
        }

        /// <summary>
        /// Handle transferal of delegation to this Game from a source ITransferableDelegation object. This sould only concern top level properties and fields
        /// </summary>
        /// <param name="source">The source ITransferableDelegation object to transfer from</param>
        protected virtual void OnTransferFrom(ITransferableDelegation source)
        {
            // get game
            Game g = source as Game;

            // set condition
            this.CompletionCondition = g.CompletionCondition;
        }

        /// <summary>
        /// Handle registration of all child properties of this Game that are ITransferableDelegation
        /// </summary>
        /// <param name="children">A list containing all the ITransferableDelegation properties of this Game</param>
        protected virtual void OnRegisterTransferableChildren(ref List<ITransferableDelegation> children)
        {
            // add player
            children.Add(this.Player);

            // register children
            this.Player.RegisterTransferableChildren(ref children);

            // add overworld
            children.Add(this.Overworld);

            // register children
            this.Overworld.RegisterTransferableChildren(ref children);
        }

        #region XmlSerialization

        /// <summary>
        /// Handle writing of Xml for this Game
        /// </summary>
        /// <param name="writer">The XmlWriter to write Xml with</param>
        protected override void OnWriteXml(System.Xml.XmlWriter writer)
        {
            // write name
            writer.WriteAttributeString("Name", this.Name);

            // write description
            writer.WriteAttributeString("Description", this.Description);

            // write player
            this.Player.WriteXml(writer);
            
            // write overworld
            this.Overworld.WriteXml(writer);
        }

        /// <summary>
        /// Handle reading of Xml for this Game
        /// </summary>
        /// <param name="node">The node to read Xml from</param>
        protected override void OnReadXmlNode(System.Xml.XmlNode node)
        {
            // read name
            this.Name = XMLSerializableObject.GetAttribute(node, "Name").Value;

            // read description
            this.Description = XMLSerializableObject.GetAttribute(node, "Description").Value;

            // if no player
            if (this.Player == null)
            {
                // create new player
                this.Player = new PlayableCharacter(String.Empty, String.Empty);
            }
            
            // load player from xml node
            this.Player.ReadXmlNode(XMLSerializableObject.GetNode(node, typeof(PlayableCharacter).Name));

            // if no overworld
            if (this.Overworld == null)
            {
                // create empty overworld
                this.Overworld = new Overworld(String.Empty, String.Empty);
            }

            // load overworld from xml node
            this.Overworld.ReadXmlNode(XMLSerializableObject.GetNode(node, typeof(Overworld).Name));
        }

        #endregion

        #endregion

        #region StaticMethods

        /// <summary>
        /// Find the root node in an XML document for a Game
        /// </summary>
        /// <param name="doc">The XmlDocument to search for the root node in</param>
        /// <returns>The root node</returns>
        public static XmlNode FindRootNode(XmlDocument doc)
        {
            // hold node
            XmlNode node = null;

            // itterate all child nodes
            foreach (XmlNode nodeElement in doc.ChildNodes)
            {
                // dig
                node = Game.digForRootNode(nodeElement);

                // if node found
                if (node != null)
                {
                    // break itteration
                    break;
                }
            }

            // return
            return node;
        }

        /// <summary>
        /// Recursively dig for a root node
        /// </summary>
        /// <param name="node">The parent node</param>
        /// <returns>The root node, if it is found</returns>
        private static XmlNode digForRootNode(XmlNode node)
        {
            // if a root
            if (Game.isRootNode(node))
            {
                // return
                return node;
            }
            else
            {
                // itterate all nodes
                foreach (XmlNode nodeElement in node.ChildNodes)
                {
                    // if a root node
                    if (Game.isRootNode(node))
                    {
                        // return node
                        return nodeElement;
                    }
                    else
                    {
                        // dig further
                        XmlNode foundNode = Game.digForRootNode(nodeElement);

                        // if found
                        if (foundNode != null)
                        {
                            // return
                            return foundNode;
                        }
                    }
                }

                // return null
                return null;
            }
        }

        /// <summary>
        /// Get if a XmlNode is the root node for a Game object
        /// </summary>
        /// <param name="node">The node to check</param>
        /// <returns>If the node was the root</returns>
        private static Boolean isRootNode(XmlNode node)
        {
            // if a node
            if (node != null)
            {
                // check if it is a game
                return node.Name == "Game";
            }
            else
            {
                // can't be true if no node
                return false;
            }
        }

        #endregion

        #region EventHandlers

        void player_Died(object sender, ReasonEventArgs e)
        {
            // handle
            this.OnHandlePlayerDied("YOU ARE DEAD!!!", e.Reason);
        }

        #endregion

        #region ITransferableDelegation Members

        /// <summary>
        /// Generate a transferable ID for this Game
        /// </summary>
        /// <returns>The ID of this object as a string</returns>
        public string GenerateTransferalID()
        {
            return this.OnGenerateTransferalID();
        }

        /// <summary>
        /// Transfer delegation to this Game from a source ITransferableDelegation object. This sould only concern top level properties and fields
        /// </summary>
        /// <param name="source">The source ITransferableDelegation object to transfer from</param>
        public void TransferFrom(ITransferableDelegation source)
        {
            this.OnTransferFrom(source);
        }

        /// <summary>
        /// Register all child properties of this Game that are ITransferableDelegation
        /// </summary>
        /// <param name="children">A list containing all the ITransferableDelegation properties of this Game</param>
        public void RegisterTransferableChildren(ref List<ITransferableDelegation> children)
        {
            this.OnRegisterTransferableChildren(ref children);
        }

        #endregion
    }

    /// <summary>
    /// Represents the callback used for creating new Game's
    /// </summary>
    /// <returns>A game created by the callback</returns>
    public delegate Game GameCreator();

    /// <summary>
    /// Represents the callback for an IO operation
    /// </summary>
    /// <param name="message">Any message retunred by the operation</param>
    /// <returns>The result of the IO operation</returns>
    public delegate Boolean IOOperationCallback(out String message);

    /// <summary>
    /// Represents the callback for an IO operation
    /// </summary>
    /// <param name="message">Any message retunred by the operation</param>
    /// <param name="fileName">The full file name of the file to interact with</param>
    /// <returns>The result of the IO operation</returns>
    public delegate Boolean IOSprecifiedFileOperationCallback(out String message, String fileName);

    /// <summary>
    /// Represents the callback used for completion checks
    /// </summary>
    /// <param name="game">The Game to check for completion</param>
    /// <returns>Returns if the condition if fulfilled</returns>
    public delegate Boolean CompletionCheck(Game game);

    /// <summary>
    /// Enumeration of game commands
    /// </summary>
    public enum EGameCommand
    {
        /// <summary>
        /// New game
        /// </summary>
        New = 0,
        /// <summary>
        /// Save the game
        /// </summary>
        Save,
        /// <summary>
        /// Load a game
        /// </summary>
        Load,
        /// <summary>
        /// Exit the game
        /// </summary>
        Exit,
        /// <summary>
        /// View help
        /// </summary>
        Help,
        /// <summary>
        /// View the region map
        /// </summary>
        Map,
        /// <summary>
        /// View information about the game
        /// </summary>
        About,
        /// <summary>
        /// Turn sounds on
        /// </summary>
        SoundOn,
        /// <summary>
        /// Turn sounds off
        /// </summary>
        SoundOff
    }

    /// <summary>
    /// Enumeration of IO operations
    /// </summary>
    public enum EIOOperation
    {
        /// <summary>
        /// Saving
        /// </summary>
        Save = 0,
        /// <summary>
        /// Loaidng
        /// </summary>
        Load
    }
}
