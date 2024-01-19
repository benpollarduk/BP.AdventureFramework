using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Examples.Assets.Regions.Zelda.Items;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Regions.Zelda.Rooms
{
    internal class Stream : RoomTemplate
    {
        #region Constants

        private const string Name = "Stream";
        private const string Description = "";

        #endregion

        #region Overrides of RoomTemplate

        /// <summary>
        /// Instantiate a new instance of the room.
        /// </summary>
        /// <returns>The room.</returns>
        public override Room Instantiate()
        {
            var room = new Room(Name, Description, new Exit(Direction.South));

            room.Description = new ConditionalDescription("A small stream flows east to west in front of you. The water is clear, and looks good enough to drink. On the bank is a small bush. To the south is the Kokiri forest", "A small stream flows east to west infront of you. The water is clear, and looks good enough to drink. On the bank is a stump where the bush was. To the south is the Kokiri forest.", () => room.ContainsItem(Bush.Name));

            var bush = new Bush().Instantiate();
            var rupee = new Rupee().Instantiate();

            bush.Interaction = (item, _) =>
            {
                if (Sword.Name.EqualsExaminable(item))
                {
                    bush.Morph(new Stump().Instantiate());
                    rupee.IsPlayerVisible = true;
                    return new InteractionResult(InteractionEffect.ItemMorphed, item, "You slash wildly at the bush and reduce it to a stump. This exposes a red rupee, that must have been what was glinting from within the bush...");
                }

                return new InteractionResult(InteractionEffect.NoEffect, item);
            };

            room.AddItem(bush);
            room.AddItem(rupee);

            return room;
        }

        #endregion
    }
}
