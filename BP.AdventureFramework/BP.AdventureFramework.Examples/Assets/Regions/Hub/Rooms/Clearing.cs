using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Conversations;
using BP.AdventureFramework.Rendering.FrameBuilders;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Regions.Hub.Rooms
{
    internal class Clearing : RoomTemplate<Clearing>
    {
        #region Constants

        private const string Name = "Jungle Clearing";
        private const string Description = "You are in a small clearing in a jungle, tightly enclosed by undergrowth. You have no idea how you got here. The chirps and buzzes coming from insects in the undergrowth are intense. There are some stone pedestals in front of you. Each has a small globe on top of it.";

        #endregion

        #region Overrides of RoomTemplate<Clearing>

        /// <summary>
        /// Create a new instance of the room.
        /// </summary>
        /// <returns>The room.</returns>
        protected override Room OnCreate()
        {
            var room = new Room(Name, Description);

            var conversation = new Conversation(
                new Paragraph("Squarrrkkk!"),
                new Paragraph("Would you like to change modes?")
                {
                    Responses = new[]
                    {
                        new Response("Yes please, change to default."),
                        new Response("Yes please, change to simple.", 2),
                        new Response("Yes please, change to legacy.", 3),
                        new Response("No thanks, keep things as they are.", 4)
                    }
                },
                new Paragraph("Arrk! Color it is.", g => g.FrameBuilders = FrameBuilderCollections.Default, -1),
                new Paragraph("Eeek, simple be fine too!", g => g.FrameBuilders = FrameBuilderCollections.Simple, -2),
                new Paragraph("Squarrk! Legacy, looks old. Arrk!", g => g.FrameBuilders = FrameBuilderCollections.Legacy, -3),
                new Paragraph("Fine, suit yourself! Squarrk!", -4)
            );

            room.AddCharacter(new NonPlayableCharacter(new Identifier("Parrot"), new Description("A brightly colored parrot."))
            {
                Conversation = conversation
            });

            return room;
        }

        #endregion
    }
}
