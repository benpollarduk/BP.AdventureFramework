﻿using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Examples.Assets.Regions.Flat.Items;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Regions.Flat.Rooms
{
    internal class SpareBedroom : RoomTemplate
    {
        #region Constants

        private const string Name = "Spare Bedroom";

        #endregion

        #region Overrides of RoomTemplate

        /// <summary>
        /// Instantiate a new instance of the room.
        /// </summary>
        /// <returns>The room.</returns>
        public override Room Instantiate()
        {
            var room = new Room(Name, string.Empty, new Exit(Direction.North));

            room.AddItem(new GameCube().Instantiate());
            room.AddItem(new Guitar().Instantiate());

            room.Description = new ConditionalDescription("You are in a very tidy room. The eastern wall is painted in a dark red colour. Against the south wall is a line of guitar amplifiers, all turned on. A very tidy blue guitar rests against the amps just begging to be played. There is a Gamecube against the northern wall. A doorway to the north leads back to the Western Hallway.",
                "You are in a very tidy room. The eastern wall is painted in a dark red colour. Against the south wall is a line of guitar amplifiers, all turned on. There is a Gamecube against the northern wall. A doorway to the north leads back to the Western Hallway.",
                () => room.ContainsItem(Guitar.Name));

            return room;
        }

        #endregion
    }
}
