using System;
using BP.AdventureFramework.Assets.Locations;

namespace BP.AdventureFramework.Extensions
{
    /// <summary>
    /// Provides extension versions for Directions.
    /// </summary>
    public static class DirectionExtensions
    {
        /// <summary>
        /// Get an inverse direction.
        /// </summary>
        /// <param name="value">The direction.</param>
        /// <returns>The inverse direction.</returns>
        public static Direction Inverse(this Direction value)
        {
            switch (value)
            {
                case Direction.North:
                    return Direction.South;
                case Direction.East:
                    return Direction.West;
                case Direction.South:
                    return Direction.North;
                case Direction.West:
                    return Direction.East;
                case Direction.Up:
                    return Direction.Down;
                case Direction.Down:
                    return Direction.Up;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }
    }
}
