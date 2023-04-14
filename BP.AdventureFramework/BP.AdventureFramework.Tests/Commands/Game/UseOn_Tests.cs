using BP.AdventureFramework.Commands.Game;
using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Assets.Locations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Commands.Game
{
    [TestClass]
    public class UseOn_Tests
    {
        [TestMethod]
        public void GivenNoItem_WhenInvoke_ThenNone()
        {
            var command = new UseOn(null, null, null, null);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.None, result.Result);
        }

        [TestMethod]
        public void GivenNoTarget_WhenInvoke_ThenNone()
        {
            var item = new Item(Identifier.Empty, Description.Empty, true);
            var command = new UseOn(item, null, null, null);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.None, result.Result);
        }

        [TestMethod]
        public void GivenNoPlayer_WhenInvoke_ThenNone()
        {
            var item = new Item(Identifier.Empty, Description.Empty, true);
            var command = new UseOn(item, null, null, null);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.None, result.Result);
        }

        [TestMethod]
        public void GivenTargetIsNpc_WhenInvoke_ThenReacted()
        {
            var item = new Item(Identifier.Empty, Description.Empty, true);
            var npc = new NonPlayableCharacter(Identifier.Empty, Description.Empty);
            var pc = new PlayableCharacter(Identifier.Empty, Description.Empty);
            var command = new UseOn(item, npc, pc, null);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.Reacted, result.Result);
        }

        [TestMethod]
        public void GivenTargetIsPlayer_WhenInvoke_ThenReacted()
        {
            var item = new Item(Identifier.Empty, Description.Empty, true);
            var pc = new PlayableCharacter(Identifier.Empty, Description.Empty);
            var command = new UseOn(item, pc, pc, null);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.Reacted, result.Result);
        }

        [TestMethod]
        public void GivenTargetIsRoom_WhenInvoke_ThenReacted()
        {
            var item = new Item(Identifier.Empty, Description.Empty, true);
            var pc = new PlayableCharacter(Identifier.Empty, Description.Empty);
            var room = new Room(Identifier.Empty, Description.Empty);
            var command = new UseOn(item, room, pc, room);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.Reacted, result.Result);
        }
    }
}
