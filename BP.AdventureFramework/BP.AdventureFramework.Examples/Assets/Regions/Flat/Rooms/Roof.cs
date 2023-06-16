using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Examples.Assets.Regions.Flat.Items;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Regions.Flat.Rooms
{
    internal class Roof : RoomTemplate<Roof>
    {
        #region Constants

        private const string Name = "Faustos Roof";

        #endregion

        #region Overrides of RoomTemplate<Roof>

        /// <summary>
        /// Create a new instance of the room.
        /// </summary>
        /// <returns>The room.</returns>
        protected override Room OnCreate()
        {
            var room = new Room(Name, string.Empty, new Exit(Direction.South));

            room.AddItem(Skylight.Create());
            room.AddItem(EmptyCoffeeMug.Create());

            room.Description = new ConditionalDescription("The roof is small and gravely, and it hurts your shoe-less feet to stand on it. There is a large skylight in the center of the roof, and a empty coffee mug sits to the side, indicating someone has been here recently. The window behind you south leads back into the bathroom.",
                "The roof is small and gravely, and it hurts your shoe-less feet to stand on it. There is a large skylight in the center of the roof. The window behind you south leads back into the bathroom.",
                () => room.ContainsItem(MugOfCoffee.Name));

            return room;
        }

        #endregion
    }
}
