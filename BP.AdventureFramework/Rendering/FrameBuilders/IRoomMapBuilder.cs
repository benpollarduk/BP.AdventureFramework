using BP.AdventureFramework.Assets.Locations;

namespace BP.AdventureFramework.Rendering.FrameBuilders
{
    /// <summary>
    /// Represents any object that can build room maps.
    /// </summary>
    public interface IRoomMapBuilder
    {
        /// <summary>
        /// Build a map for a room.
        /// </summary>
        /// <param name="gridStringBuilder">The string builder to use.</param>
        /// <param name="room">The room.</param>
        /// <param name="viewPoint">The viewpoint from the room.</param>
        /// <param name="key">The key type.</param>
        /// <param name="startX">The start position, x.</param>
        /// <param name="startY">The start position, x.</param>
        /// <param name="endX">The end position, x.</param>
        /// <param name="endY">The end position, x.</param>
        void BuildRoomMap(GridStringBuilder gridStringBuilder, Room room, ViewPoint viewPoint, KeyType key, int startX, int startY, out int endX, out int endY);
    }
}
