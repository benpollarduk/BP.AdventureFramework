using BP.AdventureFramework.Characters;
using BP.AdventureFramework.Interaction;
using BP.AdventureFramework.Locations;
using BP.AdventureFramework.Parsing.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Commands
{
    [TestClass]
    public class Take_Tests
    {
        [TestMethod]
        public void GivenInvoke_WhenNoCharacter_ThenNoReaction()
        {
            var room = new Room(new Identifier(""), new Description(""));
            var command = new Take(null, null, room);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.NoReaction, result.Result);
        }

        [TestMethod]
        public void GivenInvoke_WhenNoItem_ThenNoReaction()
        {
            var room = new Room(new Identifier(""), new Description(""));
            var character = new PlayableCharacter(new Identifier(""), new Description(""));
            var command = new Take(character, null, room);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.NoReaction, result.Result);
        }

        [TestMethod]
        public void GivenInvoke_WhenRoomDoesNotContainThatItem_ThenNoReaction()
        {
            var room = new Room(new Identifier(""), new Description(""));
            var character = new PlayableCharacter(new Identifier(""), new Description(""));
            var item = new Item(new Identifier("A"), new Description(""), true);
            var command = new Take(character, item, room);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.NoReaction, result.Result);
        }


        [TestMethod]
        public void GivenInvoke_WhenItemIsNotTakeable_ThenNoReaction()
        {
            var room = new Room(new Identifier(""), new Description(""));
            var character = new PlayableCharacter(new Identifier(""), new Description(""));
            var item = new Item(new Identifier("A"), new Description(""), false);
            room.AddItem(item);
            var command = new Take(character, item, room);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.NoReaction, result.Result);
        }

        [TestMethod]
        public void GivenInvoke_WhenItemIsDroppable_ThenReacted()
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
