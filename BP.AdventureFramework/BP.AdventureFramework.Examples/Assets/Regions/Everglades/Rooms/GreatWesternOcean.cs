using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Utilities;

namespace BP.AdventureFramework.Examples.Assets.Regions.Everglades.Rooms
{
    internal class GreatWesternOcean : RoomTemplate<GreatWesternOcean>
    {
        #region Constants

        private const string Name = "Great Western Ocean";
        private const string Description = "The Great Western Ocean stretches to the horizon. The shore runs to the north and south. You can hear the lobstosities clicking hungrily. To the east is a small clearing.";
        internal const string ConchShell = "Conch Shell";

        #endregion

        #region StaticMethods

        private static Item CreateConchShell()
        {
            var conchShell = new Item(ConchShell, "A pretty conch shell, it is about the size of a coconut", true)
            {
                Interaction = (item, target) =>
                {
                    switch (item.Identifier.IdentifiableName)
                    {
                        case Hub.Knife:
                            return new InteractionResult(InteractionEffect.FatalEffect, item, "You slash at the conch shell and it shatters into tiny pieces. Without the conch shell you are well and truly fucked");
                        default:
                            return new InteractionResult(InteractionEffect.NoEffect, item);
                    }
                }
            };

            return conchShell;
        }

        #endregion

        #region Overrides of RoomTemplate<EngineRoom>

        /// <summary>
        /// Create a new instance of the room.
        /// </summary>
        /// <param name="pC">The playable character.</param>
        /// <returns>The room.</returns>
        protected override Room OnCreate(PlayableCharacter pC)
        {
            var room = new Room(Name, Description, new Exit(Direction.East));
            room.AddItem(CreateConchShell());
            return room;
        }

        #endregion
    }
}
