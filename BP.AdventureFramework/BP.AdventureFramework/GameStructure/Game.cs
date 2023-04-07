using System;
using System.Collections.Generic;
using System.Linq;
using BP.AdventureFramework.Characters;
using BP.AdventureFramework.Interaction;
using BP.AdventureFramework.Locations;
using BP.AdventureFramework.Parsing;
using BP.AdventureFramework.Rendering;
using BP.AdventureFramework.Rendering.Frames;

namespace BP.AdventureFramework.GameStructure
{
    /// <summary>
    /// Represents the structure of the game
    /// </summary>
    public class Game : ITransferableDelegation
    {
        #region Fields

        private PlayableCharacter player;

        #endregion

        #region Properties

        /// <summary>
        /// Get or set the player.
        /// </summary>
        public PlayableCharacter Player
        {
            get { return player; }
            set
            {
                if (player != null)
                    player.Died -= player_Died;

                player = value;

                if (player != null)
                    player.Died += player_Died;
            }
        }

        /// <summary>
        /// Get or set the Overworld.
        /// </summary>
        public Overworld Overworld { get; set; }

        /// <summary>
        /// Get or set the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Get or set the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Get if this game has ended.
        /// </summary>
        public bool HasEnded { get; protected set; }

        /// <summary>
        /// Get or set this Games frame to display for the title screen.
        /// </summary>
        public TitleFrame TitleFrame { get; set; }

        /// <summary>
        /// Get or set this Games frame to display upon completion.
        /// </summary>
        public Frame CompletionFrame { get; set; }

        /// <summary>
        /// Get or set this Games help screen.
        /// </summary>
        public HelpFrame HelpFrame { get; set; }

        /// <summary>
        /// Get the current Frame.
        /// </summary>
        public Frame CurrentFrame { get; protected set; }

        /// <summary>
        /// Occurs when the CurrentFrame is updated.
        /// </summary>
        public event EventHandler<Frame> CurrentFrameUpdated;

        /// <summary>
        /// Occurs when the Game has ended.
        /// </summary>
        public event EventHandler<ExitMode> Ended;

        /// <summary>
        /// Occurs when the Game has been completed.
        /// </summary>
        public event EventHandler<ExitMode> Completed;

        /// <summary>
        /// Get or set the last used width.
        /// </summary>
        protected int lastUsedWidth;

        /// <summary>
        /// Get or set the last used height.
        /// </summary>
        protected int lastUsedHeight;

        /// <summary>
        /// Get or set the last used map drawer.
        /// </summary>
        protected MapDrawer lastUsedMapDrawer;

        /// <summary>
        /// Get or set the completion condition.
        /// </summary>
        public CompletionCheck CompletionCondition { get; set; }

        /// <summary>
        /// Get or set the parser for this Game.
        /// </summary>
        public TextParser Parser { get; set; } = new TextParser();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Game class.
        /// </summary>
        /// <param name="name">The name of this Game.</param>
        /// <param name="description">A description of this Game.</param>
        /// <param name="player">The Player to use for this Game.</param>
        /// <param name="overworld">A Overworld to use for this Game.</param>
        public Game(string name, string description, PlayableCharacter player, Overworld overworld)
        {
            Name = name;
            Description = description;
            Player = player;
            Overworld = overworld;
        }

        #endregion

        #region Methods

        /// <summary>
        /// End the Game.
        /// </summary>
        public void End()
        {
            OnGameEnded(ExitMode.ExitApplication);
        }

        /// <summary>
        /// Handle CurrentFrame updating.
        /// </summary>
        /// <param name="frame">The updated frame.</param>
        protected virtual void OnCurrentFrameUpdated(Frame frame)
        {
            CurrentFrame = frame;
            CurrentFrameUpdated?.Invoke(this, frame);
        }

        /// <summary>
        /// Handle game ended.
        /// </summary>
        /// <param name="mode">The exit mode.</param>
        protected virtual void OnGameEnded(ExitMode mode)
        {
            HasEnded = true;
            Ended?.Invoke(this, mode);
        }

        /// <summary>
        /// React to input.
        /// </summary>
        /// <param name="input">The input to react to.</param>
        /// <returns>A result detailing the reaction.</returns>
        public virtual Decision ReactToInput(string input)
        {
            var reacted = Parser.ReactToInput(input, this, out var message);

            if (!CompletionCondition(this))
                return new Decision(reacted, message);

            Completed?.Invoke(this, ExitMode.ReturnToTitleScreen);

            CurrentFrame = CompletionFrame;

            return new Decision(ReactionToInput.SelfContainedReaction, "You have finished the game");
        }

        /// <summary>
        /// Enter the game.
        /// </summary>
        /// <param name="width">The width of the game.</param>
        /// <param name="height">The height of the game.</param>
        /// <param name="drawer">A drawer to use for constructing the map.</param>
        public void EnterGame(int width, int height, MapDrawer drawer)
        {
            lastUsedWidth = width;
            lastUsedHeight = height;
            lastUsedMapDrawer = drawer;
            OnCurrentFrameUpdated(GetScene());
        }

        /// <summary>
        /// Get a scene based on the current game.
        /// </summary>
        /// <returns>A constructed frame of the scene.</returns>
        public SceneFrame GetScene()
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
        public SceneFrame GetScene(MapDrawer drawer, int width, int height)
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
        public virtual SceneFrame GetScene(MapDrawer drawer, int width, int height, string messageToUser)
        {
            lastUsedWidth = width;
            lastUsedHeight = height;
            lastUsedMapDrawer = drawer;
            return new SceneFrame(Overworld.CurrentRegion.CurrentRoom, Player, messageToUser, drawer);
        }

        /// <summary>
        /// Handle player deaths.
        /// </summary>
        /// <param name="titleMessage">A title message to display.</param>
        /// <param name="reason">A reason for the death.</param>
        protected virtual void OnHandlePlayerDied(string titleMessage, string reason)
        {
            OnCurrentFrameUpdated(new EndFrame(titleMessage, reason));
        }

        /// <summary>
        /// Get all objects in this Game (that are within the current scope) that implement IImplementOwnActions.
        /// </summary>
        /// <returns>All IImplementOwnActions objects.</returns>
        public virtual IImplementOwnActions[] GetAllObjectsWithAdditionalCommands()
        {
            var commandables = new List<IImplementOwnActions>();
            commandables.AddRange(Overworld.CurrentRegion.CurrentRoom.GetAllObjectsWithAdditionalCommands());
            commandables.AddRange(Player.GetAllObjectsWithAdditionalCommands());
            return commandables.ToArray<IImplementOwnActions>();
        }

        /// <summary>
        /// Get if a string is a valid ActionableCommand.
        /// </summary>
        /// <param name="command">The command to search for.</param>
        /// <returns>True if the command was found, else false.</returns>
        public bool IsValidActionableCommand(string command)
        {
            return FindActionableCommand(command) != null;
        }

        /// <summary>
        /// Find a ActionableCommand in this Games IImplementOwnActions objects
        /// </summary>
        /// <param name="command">The command to search for</param>
        /// <returns>The first ActionableCommand whose Command property matches the command parameter</returns>
        public virtual ActionableCommand FindActionableCommand(string command)
        {
            var commandables = GetAllObjectsWithAdditionalCommands();

            foreach (var commandable in commandables)
            {
                var customCommand = commandable.FindCommand(command);

                if (customCommand != null)
                    return customCommand;
            }

            return null;
        }

        /// <summary>
        /// Find a IImplementOwnActions in this objects IImplementOwnActions objects.
        /// </summary>
        /// <param name="command">The command to search for.</param>
        /// <returns>The first IImplementOwnActions object whose Command property matches the command parameter.</returns>
        public virtual IImplementOwnActions FindIImplementOwnActionsObject(string command)
        {
            var commandables = GetAllObjectsWithAdditionalCommands();

            foreach (var commandable in commandables)
            {
                commandable.FindCommand(command);

                if (command != null)
                    return commandable;
            }

            return null;
        }

        /// <summary>
        /// Find an interaction target within the current scope for this Game.
        /// </summary>
        /// <param name="name">The targets name.</param>
        /// <returns>The first IInteractWithItem object which has a name that matches the name parameter.</returns>
        public virtual IInteractWithItem FindInteractionTarget(string name)
        {
            name = name.ToUpper();

            if (Player.Name.ToUpper() == name)
                return Player;

            if (Player.Items.Any(i => string.Equals(i.Name.ToUpper(), name, StringComparison.InvariantCultureIgnoreCase)))
            {
                Player.FindItem(name, out var i);
                return i;
            }

            if (string.Equals(Overworld.CurrentRegion.CurrentRoom.Name.ToUpper(), name, StringComparison.InvariantCultureIgnoreCase))
                return Overworld.CurrentRegion.CurrentRoom;

            if (!Overworld.CurrentRegion.CurrentRoom.ContainsInteractionTarget(name))
                return null;

            Overworld.CurrentRegion.CurrentRoom.FindInteractionTarget(name, out var target);
            return target;
        }

        /// <summary>
        /// Refresh the current frame.
        /// </summary>
        public virtual void Refresh()
        {
            Refresh(string.Empty);
        }

        /// <summary>
        /// Refresh the current frame.
        /// </summary>
        /// <param name="message">Any message to display.</param>
        public virtual void Refresh(string message)
        {
            OnCurrentFrameUpdated(new SceneFrame(Overworld.CurrentRegion.CurrentRoom, Player, message));
        }

        /// <summary>
        /// Refresh the current frame.
        /// </summary>
        /// <param name="frame">A frame to display.</param>
        public virtual void Refresh(Frame frame)
        {
            OnCurrentFrameUpdated(frame);
        }

        #endregion

        #region EventHandlers

        private void player_Died(object sender, string e)
        {
            OnHandlePlayerDied("YOU ARE DEAD!!!", e);
        }

        #endregion

        #region ITransferableDelegation Members

        /// <summary>
        /// Generate a transferable ID for this Game.
        /// </summary>
        /// <returns>The ID of this object as a string.</returns>
        public virtual string GenerateTransferalID()
        {
            return Name;
        }

        /// <summary>
        /// Transfer delegation to this Game from a source ITransferableDelegation object. This should only concern top level properties and fields.
        /// </summary>
        /// <param name="source">The source ITransferableDelegation object to transfer from.</param>
        public void TransferFrom(ITransferableDelegation source)
        {
            var g = source as Game;
            CompletionCondition = g.CompletionCondition;
        }

        /// <summary>
        /// Register all child properties of this Game that are ITransferableDelegation.
        /// </summary>
        /// <param name="children">A list containing all the ITransferableDelegation properties of this Game.</param>
        public void RegisterTransferableChildren(ref List<ITransferableDelegation> children)
        {
            children.Add(Player);
            Player.RegisterTransferableChildren(ref children);
            children.Add(Overworld);
            Overworld.RegisterTransferableChildren(ref children);
        }

        #endregion
    }
}