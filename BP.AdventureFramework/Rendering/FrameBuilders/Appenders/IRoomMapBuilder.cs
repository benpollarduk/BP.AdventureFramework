using BP.AdventureFramework.Assets.Locations;

namespace BP.AdventureFramework.Rendering.FrameBuilders.Appenders
{
    /// <summary>
    /// Represents any object that can build room maps.
    /// </summary>
    public interface IRoomMapBuilder
    {
        /// <summary>
        /// Build a map for a room.
        /// </summary>
        /// <param name="lineStringBuilder">The line string builder to use.</param>
        /// <param name="room">The room.</param>
        /// <param name="viewPoint">The viewpoint from the room.</param>
        /// <param name="key">The key type.</param>
        /// <param name="availableColumns">The available horizontal space, in columns, to build the map within.</param>
        /// <returns>A string representing a map for the room.</returns>
        string BuildRoomMap(LineStringBuilder lineStringBuilder, Room room, ViewPoint viewPoint, KeyType key, int availableColumns);
    }
}
