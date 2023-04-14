using BP.AdventureFramework.Commands.Global;
using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Assets.Locations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Exit = BP.AdventureFramework.Assets.Locations.Exit;

namespace BP.AdventureFramework.Tests.Commands.Global
{
    [TestClass]
    public class About_Tests
    {
        [TestMethod]
        public void GivenNullGame_WhenInvoke_ThenNone()
        {
            var command = new About(null);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.None, result.Result);
        }

        [TestMethod]
        public void GivenValidGame_WhenInvoke_ThenSelfContained()
        {
            var overworld = new Overworld(Identifier.Empty, Description.Empty);
            var region = new Region(Identifier.Empty, Description.Empty);
            region.AddRoom(new Room(Identifier.Empty, Description.Empty, new Exit(CardinalDirection.North)), 0, 0);
            region.AddRoom(new Room(Identifier.Empty, Description.Empty, new Exit(CardinalDirection.South)), 0, 1);
            overworld.Regions.Add(region);
            var command = new About(new Logic.Game(string.Empty, string.Empty, new PlayableCharacter(Identifier.Empty, Description.Empty), overworld));

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.SelfContained, result.Result);
        }
    }
}
