using System;
using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Interpretation;
using BP.AdventureFramework.Rendering;
using BP.AdventureFramework.Rendering.FrameBuilders;
using BP.AdventureFramework.Rendering.FrameBuilders.Color;
using BP.AdventureFramework.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Rendering.FrameBuilders.Color
{
    [TestClass]
    public class ConsolidatedColorSceneFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenFullyFeaturedScene_WhenBuild_ThenNoException()
        {
            var room = new Room(string.Empty, string.Empty, new Exit(Direction.Up), new Exit(Direction.Down), new Exit(Direction.North), new Exit(Direction.East), new Exit(Direction.South), new Exit(Direction.West));
            room.AddItem(new Item(string.Empty, string.Empty));

            var regionMaker = new RegionMaker(string.Empty, string.Empty)
            {
                [1, 2, 0] = new Room(string.Empty, string.Empty, new Exit(Direction.South)),
                [0, 1, 0] = new Room(string.Empty, string.Empty, new Exit(Direction.East)),
                [2, 1, 0] = new Room(string.Empty, string.Empty, new Exit(Direction.West)),
                [1, 0, 0] = new Room(string.Empty, string.Empty, new Exit(Direction.North)),
                [1, 1, 1] = new Room(string.Empty, string.Empty, new Exit(Direction.Down)),
                [1, 1, -1] = new Room(string.Empty, string.Empty, new Exit(Direction.Up)),
                [1, 1, 0] = room
            };

            var region = regionMaker.Make(1, 1, 0);
            var gridStringBuilder = new GridStringBuilder();
            var builder = new ConsolidatedColorSceneFrameBuilder(gridStringBuilder, new ColorRoomMapBuilder());

            builder.Build(room, ViewPoint.Create(region), new PlayableCharacter(string.Empty, string.Empty), "Test", Array.Empty<CommandHelp>(), KeyType.Full, 80, 50);

            Assert.IsTrue(true);
        }
    }
}
