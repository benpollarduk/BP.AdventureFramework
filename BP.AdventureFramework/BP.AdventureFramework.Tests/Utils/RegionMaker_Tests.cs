using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Utils
{
    [TestClass]
    public class RegionMaker_Tests
    {
        [TestMethod]
        public void GivenNullCollection_WhenConvertToRoomMatrix_ThenIsNull()
        {
            var matrix = RegionMaker.ConvertToRoomMatrix(new RoomPosition[0]);

            Assert.IsNull(matrix);
        }

        [TestMethod]
        public void Given1RoomCollection_WhenConvertToRoomMatrix_ThenNotNull()
        {
            var room = new RoomPosition(new Room(Identifier.Empty, Description.Empty), 0, 0);

            var matrix = RegionMaker.ConvertToRoomMatrix(new[] { room });

            Assert.IsNotNull(matrix);
        }

        [TestMethod]
        public void Given1RoomCollection_WhenConvertToRoomMatrix_Then1x1Matrix()
        {
            var room = new RoomPosition(new Room(Identifier.Empty, Description.Empty), 0, 0);

            var matrix = RegionMaker.ConvertToRoomMatrix(new[] { room });

            Assert.AreEqual(1, matrix.GetLength(0));
            Assert.AreEqual(1, matrix.GetLength(1));
        }

        [TestMethod]
        public void Given2RoomCollection_WhenConvertToRoomMatrix_Then1x2Matrix()
        {
            var room1 = new RoomPosition(new Room(Identifier.Empty, Description.Empty), 0, 0);
            var room2 = new RoomPosition(new Room(Identifier.Empty, Description.Empty), 0, 1);

            var matrix = RegionMaker.ConvertToRoomMatrix(new[] { room1, room2 });

            Assert.AreEqual(1, matrix.GetLength(0));
            Assert.AreEqual(2, matrix.GetLength(1));
        }

        [TestMethod]
        public void Given2RoomCollection_WhenConvertToRoomMatrix_Then2x1Matrix()
        {
            var room1 = new RoomPosition(new Room(Identifier.Empty, Description.Empty), 0, 0);
            var room2 = new RoomPosition(new Room(Identifier.Empty, Description.Empty), 1, 0);

            var matrix = RegionMaker.ConvertToRoomMatrix(new[] { room1, room2 });

            Assert.AreEqual(2, matrix.GetLength(0));
            Assert.AreEqual(1, matrix.GetLength(1));
        }

        [TestMethod]
        public void GivenNoExits_WhenLinkExits_ThenNoExits()
        {
            var room1 = new Room(Identifier.Empty, Description.Empty);
            var room2 = new Room(Identifier.Empty, Description.Empty);
            var regionMaker = new RegionMaker(string.Empty, string.Empty)
            {
                [0, 0] = room1, 
                [1, 0] = room2
            };
            var region = regionMaker.Make();

            RegionMaker.LinkExits(region);

            Assert.IsFalse(region.CurrentRoom.ContainsExit(true));
        }
    }
}
