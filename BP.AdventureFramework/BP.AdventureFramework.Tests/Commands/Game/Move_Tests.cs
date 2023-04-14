using BP.AdventureFramework.Commands.Game;
using BP.AdventureFramework.GameAssets;
using BP.AdventureFramework.GameAssets.Interaction;
using BP.AdventureFramework.GameAssets.Locations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Commands.Game
{
    [TestClass]
    public class Move_Tests
    {
        [TestMethod]
        public void GivenCantMove_WhenInvoke_ThenNone()
        {
            var region = new Region(Identifier.Empty, Description.Empty);
            region.AddRoom(new Room(Identifier.Empty, Description.Empty, new Exit(CardinalDirection.North)), 0, 0);
            region.AddRoom(new Room(Identifier.Empty, Description.Empty, new Exit(CardinalDirection.South)), 0, 1);
            var command = new Move(region, CardinalDirection.East);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.None, result.Result);
        }

        [TestMethod]
        public void GivenCanMove_WhenInvoke_ThenReacted()
        {
            var region = new Region(Identifier.Empty, Description.Empty);
            region.AddRoom(new Room(Identifier.Empty, Description.Empty, new Exit(CardinalDirection.North)), 0, 0);
            region.AddRoom(new Room(Identifier.Empty, Description.Empty, new Exit(CardinalDirection.South)), 0, 1);
            var command = new Move(region, CardinalDirection.North);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.Reacted, result.Result);
        }
    }
}
