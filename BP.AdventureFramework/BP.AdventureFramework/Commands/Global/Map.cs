using BP.AdventureFramework.GameAssets.Interaction;
using BP.AdventureFramework.Parsing.Commands;
using BP.AdventureFramework.Rendering;
using BP.AdventureFramework.Rendering.Frames;

namespace BP.AdventureFramework.Commands.Global
{
    /// <summary>
    /// Represents the Map command.
    /// </summary>
    public class Map : ICommand
    {
        #region Properties

        /// <summary>
        /// Get the game.
        /// </summary>
        public GameStructure.Game Game { get; }

        /// <summary>
        /// Get the map drawer.
        /// </summary>
        public MapDrawer MapDrawer { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Map class.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="mapDrawer">The map drawer.</param>
        public Map(GameStructure.Game game, MapDrawer mapDrawer)
        {
            Game = game;
            MapDrawer = mapDrawer;
        }

        #endregion

        #region Implementation of ICommand

        /// <summary>
        /// Invoke the command.
        /// </summary>
        /// <returns>The reaction.</returns>
        public Reaction Invoke()
        {
            if (Game == null)
                return new Reaction(ReactionResult.None, "No game specified.");

            if (MapDrawer == null)
                return new Reaction(ReactionResult.None, "No map drawer specified.");

            Game.Refresh(new RegionMapFrame(Game.Overworld.CurrentRegion, MapDrawer));
            return new Reaction(ReactionResult.SelfContained, string.Empty);
        }

        #endregion
    }
}