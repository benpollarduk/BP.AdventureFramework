using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Rendering;
using BP.AdventureFramework.Rendering.FrameBuilders;
using BP.AdventureFramework.Rendering.FrameBuilders.Color;
using BP.AdventureFramework.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Rendering.FrameBuilders.Color
{
    [TestClass]
    public class ColorRoomMapBuilder_Tests
    {
        [TestMethod]
        public void GivenFullyFeaturedRoom_WhenBuildRoomMap_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
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
                var mapBuilder = new ColorRoomMapBuilder();
                var stringBuilder = new GridStringBuilder();
                stringBuilder.Resize(new Size(50, 50));

                mapBuilder.BuildRoomMap(stringBuilder, room, ViewPoint.Create(region), KeyType.Full, 0, 0, out _, out _);
            });
        }

        [TestMethod]
        public void GivenFullyFeaturedRoomAndDynamicKey_WhenBuildRoomMap_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
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
                var mapBuilder = new ColorRoomMapBuilder();
                var stringBuilder = new GridStringBuilder();
                stringBuilder.Resize(new Size(50, 50));

                mapBuilder.BuildRoomMap(stringBuilder, room, ViewPoint.Create(region), KeyType.Dynamic, 0, 0, out _, out _);
            });
        }

        [TestMethod]
        public void GivenNoFeaturedRoom_WhenBuildRoomMap_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var regionMaker = new RegionMaker(string.Empty, string.Empty)
                {
                    [0, 0, 0] = new Room(string.Empty, string.Empty)
                };

                var region = regionMaker.Make(0, 0, 0);
                var mapBuilder = new ColorRoomMapBuilder();
                var stringBuilder = new GridStringBuilder();
                stringBuilder.Resize(new Size(50, 50));

                mapBuilder.BuildRoomMap(stringBuilder, regionMaker[0, 0, 0], ViewPoint.Create(region), KeyType.Full, 0, 0, out _, out _);
            });
        }

        [TestMethod]
        public void GivenFullyFeaturedRoomWithLockedDoors_WhenBuildRoomMap_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var room = new Room(string.Empty, string.Empty, new Exit(Direction.Up, true), new Exit(Direction.Down, true), new Exit(Direction.North, true), new Exit(Direction.East, true), new Exit(Direction.South, true), new Exit(Direction.West, true));
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
                var mapBuilder = new ColorRoomMapBuilder();
                var stringBuilder = new GridStringBuilder();
                stringBuilder.Resize(new Size(50, 50));

                mapBuilder.BuildRoomMap(stringBuilder, room, ViewPoint.Create(region), KeyType.Full, 0, 0, out _, out _);
            });
        }
    }
}
