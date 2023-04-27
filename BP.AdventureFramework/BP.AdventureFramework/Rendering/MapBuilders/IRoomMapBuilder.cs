using BP.AdventureFramework.Assets.Locations;

namespace BP.AdventureFramework.Rendering.MapBuilders
{
    /// <summary>
    /// Represents any object that can build room maps.
    /// </summary>
    public interface IRoomMapBuilder
    {
        /// <summary>
        /// Build a map for a room.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="key">The key type.</param>
        /// <param name="availableColumns">The available horizontal space, in columns, to build the map within.</param>
        /// <returns>A string representing a map for the room.</returns>
        string BuildRoomMap(Room room, KeyType key, int availableColumns);
    }
}
