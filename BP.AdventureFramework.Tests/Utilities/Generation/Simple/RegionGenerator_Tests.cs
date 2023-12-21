﻿using System;
using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Utilities.Generation;
using BP.AdventureFramework.Utilities.Generation.Simple;
using BP.AdventureFramework.Utilities.Generation.Themes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Utilities.Generation.Simple
{
    [TestClass]
    public class RegionGenerator_Tests
    {
        [TestMethod]
        public void GivenDefaults_WhenGenerateRegion_ThenNotNull()
        {
            var generator = new RegionGenerator();
            var theme = new Castle();

            var result = generator.GenerateRegion(Identifier.Empty,
                Description.Empty, 
                new Random(1234),
                new RoomGenerator(new ExaminableGenerator(theme.RoomNouns, theme.RoomAdjectives, new DescriptionGenerator(), true)),
                new ItemGenerator(new ExaminableGenerator(theme.TakeableItemNouns, theme.TakeableItemAdjectives, new DescriptionGenerator(), true), true),
                new ItemGenerator(new ExaminableGenerator(theme.NonTakeableItemNouns, theme.NonTakeableItemAdjectives, new DescriptionGenerator(), true), false),
                new GameGenerationOptions());

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GivenDefaults_WhenGenerateItems_ThenNotNull()
        {
            var theme = new Castle();
            var result = RegionGenerator.GenerateItems(new Random(1234),
                new ItemGenerator(new ExaminableGenerator(theme.TakeableItemNouns, theme.TakeableItemAdjectives, new DescriptionGenerator(), true), true),
                new GameGenerationOptions(),
                1);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Given1RoomAnd1Item_WhenPopulateRooms_ThenRoomHas1Item()
        {
            var random = new Random(1234);
            var room = new Room("", "");
            var item = new Item("", "");

            RegionGenerator.PopulateRooms(random, new []{ room }, new[] { item });
            var result = room.Items.Length;

            Assert.AreEqual(1, result);
        }
    }
}
