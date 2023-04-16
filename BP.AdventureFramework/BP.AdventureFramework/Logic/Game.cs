using System;
using System.Linq;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Commands;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Rendering;
using BP.AdventureFramework.Rendering.Frames;

namespace BP.AdventureFramework.Logic
{
    /// <summary>
    /// Represents the structure of the game
    /// </summary>
    public sealed class Game
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
        public bool HasEnded { get; private set; }

        /// <summary>
        /// Get or set this Games frame to display for the title screen.
        /// </summary>
        internal TitleFrame TitleFrame { get; set; }

        /// <summary>
        /// Get or set this Games frame to display upon completion.
        /// </summary>
        internal Frame CompletionFrame { get; set; }

        /// <summary>
        /// Get or set this Games help screen.
        /// </summary>
        internal Frame HelpFrame { get; set; }

        /// <summary>
        /// Get the current Frame.
        /// </summary>
        internal Frame CurrentFrame { get; private set; }

        /// <summary>
        /// Occurs when the CurrentFrame is updated.
        /// </summary>
        internal event EventHandler<Frame> CurrentFrameUpdated;

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
            HasEnded = true;
            Ended?.Invoke(this, ExitMode.ExitApplication);
        }

        /// <summary>
        /// Handle CurrentFrame updating.
        /// </summary>
        /// <param name="frame">The updated frame.</param>
        private void OnCurrentFrameUpdated(Frame frame)
        {
            CurrentFrame = frame;
            CurrentFrameUpdated?.Invoke(this, frame);
        }

        /// <summary>
        /// Handle a reaction.
        /// </summary>
        /// <param name="command">The command to react to.</param>
        /// <returns>The reaction to the command.</returns>
        internal Reaction RunCommand(ICommand command)
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
        internal void EnterGame(int width, int height, MapDrawer drawer)
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
        internal SceneFrame GetScene()
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
        internal SceneFrame GetScene(MapDrawer drawer, int width, int height)
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
        internal SceneFrame GetScene(MapDrawer drawer, int width, int height, string messageToUser)
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
        internal void Refresh(Frame frame)
        {
            OnCurrentFrameUpdated(frame);
        }

        #endregion

        #region EventHandlers

        private void player_Died(object sender, string e)
        {
            OnCurrentFrameUpdated(new EndFrame("YOU ARE DEAD!!!", e));
        }

        #endregion
    }
}