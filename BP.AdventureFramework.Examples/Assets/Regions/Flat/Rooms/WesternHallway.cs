﻿using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Utilities;

namespace BP.AdventureFramework.Examples.Assets.Regions.Flat.Rooms
{
    internal class WesternHallway : IAssetTemplate<Room>
    {
        #region Constants

        private const string Name = "Western Hallway";
        private const string Description = "This hallway is a continuation of the Eastern Hallway, to the north is the Bathroom, to the west is the Kitchen, to the South is a neat looking Spare Room. The hallway continues to the East.";

        #endregion

        #region Implementation of IAssetTemplate<Room>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The asset.</returns>
        public Room Instantiate()
        {
            return new Room(Name, Description, new Exit(Direction.North), new Exit(Direction.South), new Exit(Direction.East), new Exit(Direction.West));
        }

        #endregion
    }
}
