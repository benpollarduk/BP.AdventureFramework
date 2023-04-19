using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Commands.Game;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Commands.Game
{
    [TestClass]
    public class Take_Tests
    {
        [TestMethod]
        public void GivenNoCharacter_WhenInvoke_ThenNone()
        {
            var room = new Room(Identifier.Empty, Description.Empty);
            var command = new Take(null, null, room);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.None, result.Result);
        }

        [TestMethod]
        public void GivenNoItem_WhenInvoke_ThenNone()
        {
            var room = new Room(Identifier.Empty, Description.Empty);
            var character = new PlayableCharacter(Identifier.Empty, Description.Empty);
            var command = new Take(character, null, room);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.None, result.Result);
        }

        [TestMethod]
        public void GivenRoomDoesNotContainThatItem_WhenInvoke_ThenNone()
        {
            var room = new Room(Identifier.Empty, Description.Empty);
            var character = new PlayableCharacter(Identifier.Empty, Description.Empty);
            var item = new Item(new Identifier("A"), Description.Empty, true);
            var command = new Take(character, item, room);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.None, result.Result);
        }


        [TestMethod]
        public void GivenItemIsNotTakeable_WhenInvoke_ThenNone()
        {
            var room = new Room(Identifier.Empty, Description.Empty);
            var character = new PlayableCharacter(Identifier.Empty, Description.Empty);
            var item = new Item(new Identifier("A"), Description.Empty);
            room.AddItem(item);
            var command = new Take(character, item, room);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.None, result.Result);
        }

        [TestMethod]
        public void GivenItemIsDroppable_WhenInvoke_ThenReacted()
        {
            var room = new Room(Identifier.Empty, Description.Empty);
            var character = new PlayableCharacter(Identifier.Empty, Description.Empty);
            var item = new Item(new Identifier("A"), Description.Empty, true);
            room.AddItem(item);
            var command = new Take(character, item, room);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.Reacted, result.Result);
        }
    }
}
