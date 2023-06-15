using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Utilities;

namespace BP.AdventureFramework.Examples.Assets.Regions.Zelda.Rooms
{
    internal class Stream : RoomTemplate<Stream>
    {
        #region Constants

        private const string Name = "Stream";
        private const string Description = "";
        private const string Bush = "Bush";
        private const string Stump = "Stump";
        internal const string Rupee = "Rupee";

        #endregion

        #region Overrides of RoomTemplate<EngineRoom>

        /// <summary>
        /// Create a new instance of the room.
        /// </summary>
        /// <param name="pC">The playable character.</param>
        /// <returns>The room.</returns>
        protected override Room OnCreate(PlayableCharacter pC)
        {
            var room = new Room(Name, Description, new Exit(Direction.South));

            room.Description = new ConditionalDescription("A small stream flows east to west in front of you. The water is clear, and looks good enough to drink. On the bank is a small bush. To the south is the Kokiri forest", "A small stream flows east to west infront of you. The water is clear, and looks good enough to drink. On the bank is a stump where the bush was. To the south is the Kokiri forest.", () => room.ContainsItem(Bush));

            var bush = new Item(Bush, "The bush is small, but very dense. Something is gleaming inside, but you cant reach it because the bush is so thick.");
            var rupee = new Item(Rupee, "A red rupee! Wow this thing is worth 10 normal rupees.", true) { IsPlayerVisible = false };

            bush.Interaction = (item, target) =>
            {
                if (LinksHouse.Sword.EqualsExaminable(item))
                {
                    bush.Morph(new Item(Stump, "A small, hacked up stump from where the bush once was, until you decimated it."));
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
