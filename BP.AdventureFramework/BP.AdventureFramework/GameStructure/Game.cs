using System;
using System.Linq;
using BP.AdventureFramework.Characters;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Interaction;
using BP.AdventureFramework.Locations;
using BP.AdventureFramework.Parsing.Commands;
using BP.AdventureFramework.Rendering;
using BP.AdventureFramework.Rendering.Frames;

namespace BP.AdventureFramework.GameStructure
{
    /// <summary>
    /// Represents the structure of the game
    /// </summary>
    public class Game
    {
        #region Fields

        private PlayableCharacter player;
        private int lastUsedWidth;
        private int lastUsedHeight;

        /// <summary>
        /// Get or set the last used map drawer.
        /// </summary>
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
        /// Get or set the completion condition.
        /// </summary>
        public CompletionCheck CompletionCondition { get; set; }

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
        protected void OnCurrentFrameUpdated(Frame frame)
        {
            CurrentFrame = frame;
            CurrentFrameUpdated?.Invoke(this, frame);
        }

        /// <summary>
        /// Handle game ended.
        /// </summary>
        /// <param name="mode">The exit mode.</param>
        protected void OnGameEnded(ExitMode mode)
        {
            HasEnded = true;
            Ended?.Invoke(this, mode);
        }

        /// <summary>
        /// Handle a reaction.
        /// </summary>
        /// <param name="command">The command to react to.</param>
        /// <returns>The reaction to the command.</returns>
        public Reaction RunCommand(ICommand command)
        {
            var reaction = command.Invoke();

            if (CompletionCondition(this))
            {
                Completed?.Invoke(this, ExitMode.ReturnToTitleScreen);
                CurrentFrame = CompletionFrame;
            }

            return reaction;
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
        public SceneFrame GetScene(MapDrawer drawer, int width, int height, string messageToUser)
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
        protected void OnHandlePlayerDied(string titleMessage, string reason)
        {
            OnCurrentFrameUpdated(new EndFrame(titleMessage, reason));
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
        public void Refresh()
        {
            Refresh(string.Empty);
        }

        /// <summary>
        /// Refresh the current frame.
        /// </summary>
        /// <param name="message">Any message to display.</param>
        public void Refresh(string message)
        {
            OnCurrentFrameUpdated(new SceneFrame(Overworld.CurrentRegion.CurrentRoom, Player, message));
        }

        /// <summary>
        /// Refresh the current frame.
        /// </summary>
        /// <param name="frame">A frame to display.</param>
        public void Refresh(Frame frame)
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
    }
}