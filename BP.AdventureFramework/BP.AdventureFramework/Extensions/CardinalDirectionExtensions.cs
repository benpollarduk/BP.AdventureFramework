using System;
using BP.AdventureFramework.Assets.Locations;

namespace BP.AdventureFramework.Extensions
{
    /// <summary>
    /// Provides extension versions for CardinalDirections.
    /// </summary>
    public static class CardinalDirectionExtensions
    {
        /// <summary>
        /// Get an inverse direction.
        /// </summary>
        /// <param name="value">The direction.</param>
        /// <returns>The inverse direction.</returns>
        public static CardinalDirection Inverse(this CardinalDirection value)
        {
            switch (value)
            {
                case CardinalDirection.North:
                    return CardinalDirection.South;
                case CardinalDirection.East:
                    return CardinalDirection.West;
                case CardinalDirection.South:
                    return CardinalDirection.North;
                case CardinalDirection.West:
                    return CardinalDirection.East;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }
    }
}
