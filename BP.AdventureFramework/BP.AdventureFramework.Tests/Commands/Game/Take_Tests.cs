using BP.AdventureFramework.Characters;
using BP.AdventureFramework.Interaction;
using BP.AdventureFramework.Locations;
using BP.AdventureFramework.Parsing.Commands.Game;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Commands.Game
{
    [TestClass]
    public class Take_Tests
    {
        [TestMethod]
        public void GivenNoCharacter_WhenInvoke_ThenNone()
        {
            var room = new Room(new Identifier(""), new Description(""));
            var command = new Take(null, null, room);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.None, result.Result);
        }

        [TestMethod]
        public void GivenNoItem_WhenInvoke_ThenNone()
        {
            var room = new Room(new Identifier(""), new Description(""));
            var character = new PlayableCharacter(new Identifier(""), new Description(""));
            var command = new Take(character, null, room);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.None, result.Result);
        }

        [TestMethod]
        public void GivenRoomDoesNotContainThatItem_WhenInvoke_ThenNone()
        {
            var room = new Room(new Identifier(""), new Description(""));
            var character = new PlayableCharacter(new Identifier(""), new Description(""));
            var item = new Item(new Identifier("A"), new Description(""), true);
            var command = new Take(character, item, room);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.None, result.Result);
        }


        [TestMethod]
        public void GivenItemIsNotTakeable_WhenInvoke_ThenNone()
        {
            var room = new Room(new Identifier(""), new Description(""));
            var character = new PlayableCharacter(new Identifier(""), new Description(""));
            var item = new Item(new Identifier("A"), new Description(""), false);
            room.AddItem(item);
            var command = new Take(character, item, room);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.None, result.Result);
        }

        [TestMethod]
        public void GivenItemIsDroppable_WhenInvoke_ThenReacted()
        {
            var room = new Room(new Identifier(""), new Description(""));
            var character = new PlayableCharacter(new Identifier(""), new Description(""));
            var item = new Item(new Identifier("A"), new Description(""), true);
            room.AddItem(item);
            var command = new Take(character, item, room);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.Reacted, result.Result);
        }
    }
}
