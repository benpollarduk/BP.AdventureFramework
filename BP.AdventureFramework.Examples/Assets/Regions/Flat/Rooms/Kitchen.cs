﻿using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Examples.Assets.Regions.Flat.Items;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Utilities;

namespace BP.AdventureFramework.Examples.Assets.Regions.Flat.Rooms
{
    internal class Kitchen : IAssetTemplate<Room>
    {
        #region Constants

        private const string Name = "Kitchen";
        private const string Description = "The kitchen is a small area with work tops along the northern and eastern walls. There is a kettle on the work top, it has steam rising out of it's spout. There is a also window along the northern wall. Underneath the window is a hamster cage. To the south is the living room, the Western Hallway is to the east.";

        #endregion

        #region Implementation of IAssetTemplate<Room>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The asset.</returns>
        public Room Instantiate()
        {
            var room = new Room(Name, Description, new Exit(Direction.South), new Exit(Direction.East));

            room.AddItem(new HamsterCage().Instantiate());
            room.AddItem(new Kettle().Instantiate());

            room.Interaction = (i, target) =>
            {
                var obj = target as Room;

                if (obj != null)
                {
                    if (Guitar.Name.EqualsIdentifier(i.Identifier))
                        return new InteractionResult(InteractionEffect.NoEffect, i, "Playing guitar in the kitchen is pretty stupid don't you think?");
                }

                return new InteractionResult(InteractionEffect.NoEffect, i);
            };

            return room;
        }

        #endregion
    }
}
