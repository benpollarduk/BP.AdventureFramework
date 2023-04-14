using BP.AdventureFramework.Commands.Game;
using BP.AdventureFramework.GameAssets;
using BP.AdventureFramework.GameAssets.Characters;
using BP.AdventureFramework.GameAssets.Interaction;
using BP.AdventureFramework.GameAssets.Locations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Commands.Game
{
    [TestClass]
    public class Drop_Tests
    {
        [TestMethod]
        public void GivenNoCharacter_WhenInvoke_ThenNone()
        {
            var room = new Room(Identifier.Empty, Description.Empty);
            var command = new Drop(null, null, room);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.None, result.Result);
        }

        [TestMethod]
        public void GivenNoItem_WhenInvoke_ThenNone()
        {
            var room = new Room(Identifier.Empty, Description.Empty);
            var character = new PlayableCharacter(Identifier.Empty, Description.Empty);
            var command = new Drop(character, null, room);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.None, result.Result);
        }

        [TestMethod]
        public void GivenPlayerDoesNotHaveItem_WhenInvoke_ThenNone()
        {
            var room = new Room(Identifier.Empty, Description.Empty);
            var character = new PlayableCharacter(Identifier.Empty, Description.Empty);
            var item = new Item(new Identifier("A"), Description.Empty, true);
            var command = new Drop(character, item, room);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.None, result.Result);
        }

        [TestMethod]
        public void GivenItemIsDroppable_WhenInvoke_ThenReacted()
        {
            var room = new Room(Identifier.Empty, Description.Empty);
            var character = new PlayableCharacter(Identifier.Empty, Description.Empty);
            var item = new Item(new Identifier("A"), Description.Empty, true);
            character.AquireItem(item);
            var command = new Drop(character, item, room);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.Reacted, result.Result);
        }
    }
}
