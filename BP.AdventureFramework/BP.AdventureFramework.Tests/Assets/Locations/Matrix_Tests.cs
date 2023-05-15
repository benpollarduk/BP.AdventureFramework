using BP.AdventureFramework.Assets.Locations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Assets.Locations
{
    [TestClass]
    public class Matrix_Tests
    {
        [TestMethod]
        public void GivenNoRooms_WhenGetWidth_Then0()
        {
            var matrix = new Matrix(new Room[0,0,0]);

            var result = matrix.Width;

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GivenNoRooms_WhenGetHeight_Then0()
        {
            var matrix = new Matrix(new Room[0, 0, 0]);

            var result = matrix.Height;

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GivenNoRooms_WhenGetDepth_Then0()
        {
            var matrix = new Matrix(new Room[0, 0, 0]);

            var result = matrix.Depth;

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Given1RoomWide_WhenGetWidth_Then1()
        {
            var rooms = new Room[1, 2, 3];
            rooms[0, 0, 0] = new Room(string.Empty, string.Empty);
            rooms[0, 1, 0] = new Room(string.Empty, string.Empty);
            rooms[0, 1, 1] = new Room(string.Empty, string.Empty);
            rooms[0, 1, 2] = new Room(string.Empty, string.Empty);
            var matrix = new Matrix(rooms);

            var result = matrix.Width;

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Given2RoomsHigh_WhenGetHeight_Then2()
        {
            var rooms = new Room[1, 2, 3];
            rooms[0, 0, 0] = new Room(string.Empty, string.Empty);
            rooms[0, 1, 0] = new Room(string.Empty, string.Empty);
            rooms[0, 1, 1] = new Room(string.Empty, string.Empty);
            rooms[0, 1, 2] = new Room(string.Empty, string.Empty);
            var matrix = new Matrix(rooms);

            var result = matrix.Height;

            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void Given3RoomsDeep_WhenGetDepth_Then3()
        {
            var rooms = new Room[1, 2, 3];
            rooms[0, 0, 0] = new Room(string.Empty, string.Empty);
            rooms[0, 1, 0] = new Room(string.Empty, string.Empty);
            rooms[0, 1, 1] = new Room(string.Empty, string.Empty);
            rooms[0, 1, 2] = new Room(string.Empty, string.Empty);
            var matrix = new Matrix(rooms);

            var result = matrix.Depth;

            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void Given4Rooms_WhenToRooms_Then4Rooms()
        {
            var rooms = new Room[1, 2, 3];
            rooms[0, 0, 0] = new Room(string.Empty, string.Empty);
            rooms[0, 1, 0] = new Room(string.Empty, string.Empty);
            rooms[0, 1, 1] = new Room(string.Empty, string.Empty);
            rooms[0, 1, 2] = new Room(string.Empty, string.Empty);
            var matrix = new Matrix(rooms);

            var result = matrix.ToRooms();

            Assert.AreEqual(4, result.Length);
        }
    }
}
