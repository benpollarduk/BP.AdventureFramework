using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Examples.Assets.Regions.Flat.Items;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Regions.Flat.Rooms
{
    internal class Kitchen : RoomTemplate<Kitchen>
    {
        #region Constants

        private const string Name = "Kitchen";
        private const string Description = "The kitchen is a small area with work tops along the northern and eastern walls. There is a kettle on the work top, it has steam rising out of it's spout. There is a also window along the northern wall. Underneath the window is a hamster cage. To the south is the living room, the Western Hallway is to the east.";

        #endregion

        #region Overrides of RoomTemplate<Kitchen>

        /// <summary>
        /// Create a new instance of the room.
        /// </summary>
        /// <returns>The room.</returns>
        protected override Room OnCreate()
        {
            var room = new Room(Name, Description, new Exit(Direction.South), new Exit(Direction.East));

            room.AddItem(HamsterCage.Create());
            room.AddItem(Kettle.Create());

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
