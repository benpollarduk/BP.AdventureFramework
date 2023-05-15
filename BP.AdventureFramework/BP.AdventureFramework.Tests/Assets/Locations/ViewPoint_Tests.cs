using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Assets.Locations
{
    [TestClass]
    public class ViewPoint_Tests
    {
        [TestMethod]
        public void GivenARegionWithACurrentRoomWith4SurroundingRooms_WhenCreate_ThenViewPointHas4Rooms()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty)
            {
                [1, 0, 0] = new Room(string.Empty, string.Empty, new Exit(Direction.North)),
                [0, 1, 0] = new Room(string.Empty, string.Empty, new Exit(Direction.East)),
                [1, 1, 0] = new Room(string.Empty, string.Empty, new Exit(Direction.North), new Exit(Direction.East), new Exit(Direction.South), new Exit(Direction.West)),
                [2, 1, 0] = new Room(string.Empty, string.Empty, new Exit(Direction.West)),
                [1, 2, 0] = new Room(string.Empty, string.Empty, new Exit(Direction.South))
            };
            var region = regionMaker.Make(1, 1, 0);

            var result = ViewPoint.Create(region);

            Assert.IsNotNull(result[Direction.North]);
            Assert.IsNotNull(result[Direction.East]);
            Assert.IsNotNull(result[Direction.South]);
            Assert.IsNotNull(result[Direction.West]);
        }

        [TestMethod]
        public void GivenARegionWithACurrentRoomWith1SurroundingRoom_WhenCreate_ThenViewPointHas1Room()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty)
            {
                [1, 0, 0] = new Room(string.Empty, string.Empty, new Exit(Direction.North)),
                [0, 1, 0] = new Room(string.Empty, string.Empty, new Exit(Direction.East)),
                [1, 1, 0] = new Room(string.Empty, string.Empty, new Exit(Direction.North), new Exit(Direction.East), new Exit(Direction.South), new Exit(Direction.West)),
                [2, 1, 0] = new Room(string.Empty, string.Empty, new Exit(Direction.West)),
                [1, 2, 0] = new Room(string.Empty, string.Empty, new Exit(Direction.South))
            };
            var region = regionMaker.Make(1, 0, 0);

            var result = ViewPoint.Create(region);

            Assert.IsNotNull(result[Direction.North]);
            Assert.IsNull(result[Direction.East]);
            Assert.IsNull(result[Direction.South]);
            Assert.IsNull(result[Direction.West]);
        }

        [TestMethod]
        public void GivenAView_WhenGettingAny_ThenTrue()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty)
            {
                [1, 0, 0] = new Room(string.Empty, string.Empty, new Exit(Direction.North)),
                [0, 1, 0] = new Room(string.Empty, string.Empty, new Exit(Direction.East)),
                [1, 1, 0] = new Room(string.Empty, string.Empty, new Exit(Direction.North), new Exit(Direction.East), new Exit(Direction.South), new Exit(Direction.West)),
                [2, 1, 0] = new Room(string.Empty, string.Empty, new Exit(Direction.West)),
                [1, 2, 0] = new Room(string.Empty, string.Empty, new Exit(Direction.South))
            };
            var region = regionMaker.Make(1, 0, 0);

            var result = ViewPoint.Create(region).Any;

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenNoView_WhenGettingAny_ThenFalse()
        {
            var result = ViewPoint.NoView.Any;

            Assert.IsFalse(result);
        }
    }
}
