﻿using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Commands.Game;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Commands.Game
{
    [TestClass]
    public class TakeAll_Tests
    {
        [TestMethod]
        public void GivenNoItem_WhenInvoke_ThenError()
        {
            var room = new Room(Identifier.Empty, Description.Empty);
            var character = new PlayableCharacter(Identifier.Empty, Description.Empty);
            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room, 0, 0, 0);
            var overworld = new Overworld(string.Empty, string.Empty);
            overworld.AddRegion(region);
            var game = AdventureFramework.Logic.Game.Create(string.Empty, string.Empty, string.Empty, p => overworld, () => character, null).Invoke();
            var command = new TakeAll();

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenNoItemsAreTakeable_WhenInvoke_ThenError()
        {
            var room = new Room(Identifier.Empty, Description.Empty);
            var character = new PlayableCharacter(Identifier.Empty, Description.Empty);
            var item = new Item(new Identifier("A"), Description.Empty);
            room.AddItem(item);
            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room, 0, 0, 0);
            var overworld = new Overworld(string.Empty, string.Empty);
            overworld.AddRegion(region);
            var game = AdventureFramework.Logic.Game.Create(string.Empty, string.Empty, string.Empty, p => overworld, () => character, null).Invoke();
            var command = new TakeAll();

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenItemsAreTakeable_WhenInvoke_ThenOK()
        {
            var room = new Room(Identifier.Empty, Description.Empty);
            var character = new PlayableCharacter(Identifier.Empty, Description.Empty);
            var item = new Item(new Identifier("A"), Description.Empty, true);
            room.AddItem(item);
            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room, 0, 0, 0);
            var overworld = new Overworld(string.Empty, string.Empty);
            overworld.AddRegion(region);
            var game = AdventureFramework.Logic.Game.Create(string.Empty, string.Empty, string.Empty, p => overworld, () => character, null).Invoke();
            var command = new TakeAll();

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.OK, result.Result);
        }
    }
}
