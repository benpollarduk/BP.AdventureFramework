using BP.AdventureFramework.Interaction;
using BP.AdventureFramework.Rendering;

namespace BP.AdventureFramework.Parsing.Commands.Frame
{
    /// <summary>
    /// Represents the KeyOff command.
    /// </summary>
    public class KeyOff : ICommand
    {
        #region Properties

        /// <summary>
        /// Get the map drawer.
        /// </summary>
        public MapDrawer MapDrawer { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the KeyOff class.
        /// </summary>
        /// <param name="mapDrawer">The map drawer.</param>
        public KeyOff(MapDrawer mapDrawer)
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
                return new Reaction(ReactionResult.NoReaction, "No map drawer specified.");

            MapDrawer.Key = KeyType.None;
            return new Reaction(ReactionResult.Reacted, "Key has been turned off.");
        }

        #endregion
    }
}