using BP.AdventureFramework.Characters;
using BP.AdventureFramework.Interaction;
using BP.AdventureFramework.Locations;
using BP.AdventureFramework.Parsing.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Commands
{
    [TestClass]
    public class Drop_Tests
    {
        [TestMethod]
        public void GivenInvoke_WhenNoCharacter_ThenNoReaction()
        {
            var room = new Room(new Identifier(""), new Description(""));
            var command = new Drop(null, null, room);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.NoReaction, result.Result);
        }

        [TestMethod]
        public void GivenInvoke_WhenNoItem_ThenNoReaction()
        {
            var room = new Room(new Identifier(""), new Description(""));
            var character = new PlayableCharacter(new Identifier(""), new Description(""));
            var command = new Drop(character, null, room);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.NoReaction, result.Result);
        }

        [TestMethod]
        public void GivenInvoke_WhenPlayerDoesNotHaveItem_ThenNoReaction()
        {
            var room = new Room(new Identifier(""), new Description(""));
            var character = new PlayableCharacter(new Identifier(""), new Description(""));
            var item = new Item(new Identifier("A"), new Description(""), true);
            var command = new Drop(character, item, room);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.NoReaction, result.Result);
        }

        [TestMethod]
        public void GivenInvoke_WhenItemIsDroppable_ThenReacted()
        {
            var room = new Room(new Identifier(""), new Description(""));
            var character = new PlayableCharacter(new Identifier(""), new Description(""));
            var item = new Item(new Identifier("A"), new Description(""), true);
            character.AquireItem(item);
            var command = new Drop(character, item, room);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.Reacted, result.Result);
        }
    }
}
