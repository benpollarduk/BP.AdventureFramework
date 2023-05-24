using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Locations;

namespace BP.AdventureFramework.Examples.Assets
{
    /// <summary>
    /// Provides a template for a room.
    /// </summary>
    public interface IRoomTemplate
    {
        /// <summary>
        /// Get the identifier for the room.
        /// </summary>
        Identifier Identifier { get; }
        /// <summary>
        /// Get the identifiers for the room contents.
        /// </summary>
        Identifier[] ContentIdentifiers { get; }
        /// <summary>
        /// Convert this template to a room.
        /// </summary>
        /// <param name="pC">The playable character.</param>
        /// <returns>The converted room.</returns>
        Room ToRoom(PlayableCharacter pC);
    }
}
