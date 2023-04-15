using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Rendering;

namespace BP.AdventureFramework.Commands.Frame
{
    /// <summary>
    /// Represents the KeyOn command.
    /// </summary>
    internal class KeyOn : ICommand
    {
        #region Properties

        /// <summary>
        /// Get the map drawer.
        /// </summary>
        public MapDrawer MapDrawer { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the KeyOn class.
        /// </summary>
        /// <param name="mapDrawer">The map drawer.</param>
        public KeyOn(MapDrawer mapDrawer)
        {
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
            if (MapDrawer == null)
                return new Reaction(ReactionResult.None, "No map drawer specified.");

            MapDrawer.Key = KeyType.Dynamic;
            return new Reaction(ReactionResult.Reacted, "Key has been turned on.");
        }

        #endregion
    }
}