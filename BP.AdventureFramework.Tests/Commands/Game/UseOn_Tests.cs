using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Commands.Game;
using BP.AdventureFramework.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Commands.Game
{
    [TestClass]
    public class UseOn_Tests
    {
        [TestMethod]
        public void GivenNoItem_WhenInvoke_ThenError()
        {
            var game = AdventureFramework.Logic.Game.Create(string.Empty, string.Empty, string.Empty, null, null, null, null).Invoke();
            var command = new UseOn(null, null);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenNoTarget_WhenInvoke_ThenError()
        {
            var game = AdventureFramework.Logic.Game.Create(string.Empty, string.Empty, string.Empty, null, null, null, null).Invoke();
            var item = new Item(Identifier.Empty, Description.Empty, true);
            var command = new UseOn(item, null);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenNoPlayer_WhenInvoke_ThenError()
        {
            var game = AdventureFramework.Logic.Game.Create(string.Empty, string.Empty, string.Empty, null, null, null, null).Invoke();
            var item = new Item(Identifier.Empty, Description.Empty, true);
            var command = new UseOn(item, null);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenTargetIsNpc_WhenInvoke_ThenOK()
        {
            var item = new Item(Identifier.Empty, Description.Empty, true);
            var npc = new NonPlayableCharacter(Identifier.Empty, Description.Empty);
            var player = new PlayableCharacter(Identifier.Empty, Description.Empty);
            var room = new Room(string.Empty, string.Empty);
            room.Characters.Add(npc);
            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room, 0, 0, 0);
            var overworld = new Overworld(string.Empty, string.Empty);
            overworld.AddRegion(region);
            var game = AdventureFramework.Logic.Game.Create(string.Empty, string.Empty, string.Empty, () => overworld, () => player, null, null).Invoke();
            var command = new UseOn(item, npc);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.OK, result.Result);
        }

        [TestMethod]
        public void GivenTargetIsPlayer_WhenInvoke_ThenOK()
        {
            var item = new Item(Identifier.Empty, Description.Empty, true);
            var pc = new PlayableCharacter(Identifier.Empty, Description.Empty);
            var room = new Room(string.Empty, string.Empty);
            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room, 0, 0, 0);
            var overworld = new Overworld(string.Empty, string.Empty);
            overworld.AddRegion(region);
            var game = AdventureFramework.Logic.Game.Create(string.Empty, string.Empty, string.Empty, () => overworld, () => pc, null, null).Invoke();
            var command = new UseOn(item, pc);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.OK, result.Result);
        }

        [TestMethod]
        public void GivenTargetIsRoom_WhenInvoke_ThenOK()
        {
            var item = new Item(Identifier.Empty, Description.Empty, true);
            var room = new Room(Identifier.Empty, Description.Empty);
            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room, 0, 0, 0);
            var overworld = new Overworld(string.Empty, string.Empty);
            overworld.AddRegion(region);
            var player = new PlayableCharacter(Identifier.Empty, Description.Empty);
            var game = AdventureFramework.Logic.Game.Create(string.Empty, string.Empty, string.Empty, () => overworld, () => player, null, null).Invoke();
            var command = new UseOn(item, room);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.OK, result.Result);
        }
    }
}
