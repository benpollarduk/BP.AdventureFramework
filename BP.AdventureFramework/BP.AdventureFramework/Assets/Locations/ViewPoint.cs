using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace BP.AdventureFramework.Assets.Locations
{
    /// <summary>
    /// Represents a view point from a room.
    /// </summary>
    public sealed class ViewPoint
    {
        #region StaticProperties

        /// <summary>
        /// Get a view point representing no view.
        /// </summary>
        public static ViewPoint NoView { get; } = new ViewPoint();

        #endregion

        #region Properties

        /// <summary>
        /// Get the room that lies in a specified direction.
        /// </summary>
        /// <param name="direction">The direction to check.</param>
        /// <returns>The room.</returns>
        public Room this[Direction direction] => SurroundingRooms.ContainsKey(direction) ? SurroundingRooms[direction] : null;

        /// <summary>
        /// Get if there is a view in any direction.
        /// </summary>
        public bool Any => SurroundingRooms.Any();

        /// <summary>
        /// Get if there is a view in any direction.
        /// </summary>
        public bool AnyVisited => SurroundingRooms.Any(x => x.Value.HasBeenVisited);

        /// <summary>
        /// Get if there is a view in any direction.
        /// </summary>
        public bool AnyNotVisited => SurroundingRooms.Any(x => !x.Value.HasBeenVisited);

        /// <summary>
        /// Get the surrounding rooms.
        /// </summary>
        private Dictionary<Direction, Room> SurroundingRooms { get; } = new Dictionary<Direction, Room>();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ViewPoint class.
        /// </summary>
        private ViewPoint()
        {

        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Create a new ViewPoint.
        /// </summary>
        /// <param name="region">The region to create the view point from.</param>
        /// <returns>The view point.</returns>
        public static ViewPoint Create(Region region)
        {
            var viewPoint = new ViewPoint();

            foreach (var direction in new[] { Direction.North, Direction.East, Direction.South, Direction.West, Direction.Up, Direction.Down })
            {
                if (region.CurrentRoom.FindExit(direction, false, out _))
                    viewPoint.SurroundingRooms.Add(direction, region.GetAdjoiningRoom(direction));
            }

            return viewPoint;
        }

        #endregion
    }
}
