using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Locations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Assets.Locations
{
    [TestClass]
    public class Region_Tests
    {
        [TestMethod]
        public void Given0Rooms_WhenGetCurrentRoom_ThenNull()
        {
            var region = new Region(string.Empty, string.Empty);
          
            Assert.IsNull(region.CurrentRoom);
        }

        [TestMethod]
        public void Given1Room_WhenGetCurrentRoom_ThenNotNull()
        {
            var room1 = new Room(Identifier.Empty, Description.Empty);
            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room1, 0, 0, 0);

            Assert.IsNotNull(region.CurrentRoom);
        }

        [TestMethod]
        public void Given3Rooms_WhenGetCurrentRoom_ThenFirstRoom()
        {
            var room1 = new Room(Identifier.Empty, Description.Empty);
            var room2 = new Room(Identifier.Empty, Description.Empty);
            var room3 = new Room(Identifier.Empty, Description.Empty);

            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room1, 0, 0, 0);
            region.AddRoom(room2, 0, 1, 0);
            region.AddRoom(room3, 1, 0, 0);

            Assert.AreEqual(room1, region.CurrentRoom);
        }

        [TestMethod]
        public void GivenX1Y1_WhenSetStartRoom_ThenLastRoom()
        {
            var room1 = new Room(Identifier.Empty, Description.Empty);
            var room2 = new Room(Identifier.Empty, Description.Empty);
            var room3 = new Room(Identifier.Empty, Description.Empty);

            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room1, 0, 0, 0);
            region.AddRoom(room2, 0, 1, 0);
            region.AddRoom(room3, 1, 1, 0);

            region.SetStartRoom(1, 1, 0);

            Assert.AreEqual(room3, region.CurrentRoom);
        }

        [TestMethod]
        public void Given4Rooms_WhenGetRooms_Then4()
        {
            var room1 = new Room(Identifier.Empty, Description.Empty);
            var room2 = new Room(Identifier.Empty, Description.Empty);
            var room3 = new Room(Identifier.Empty, Description.Empty);
            var room4 = new Room(Identifier.Empty, Description.Empty);
            
            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room1, 0, 0, 0);
            region.AddRoom(room2, 0, 1, 0);
            region.AddRoom(room3, 1, 0, 0);
            region.AddRoom(room4, 1, 1, 0);
            
            Assert.AreEqual(4, region.Rooms);
        }

        [TestMethod]
        public void Given1Room_WhenAdding1Room_Then2Rooms()
        {
            var room1 = new Room(Identifier.Empty, Description.Empty);
            var room2 = new Room(Identifier.Empty, Description.Empty);

            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room1, 0, 0, 0);
            region.AddRoom(room2, 0, 1, 0);

            Assert.AreEqual(2, region.Rooms);
        }

        [TestMethod]
        public void GivenLastRoom_WhenSetStartRoom_ThenCurrentRoomIsLastRoom()
        {
            var room1 = new Room(Identifier.Empty, Description.Empty);
            var room2 = new Room(Identifier.Empty, Description.Empty);

            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room1, 0, 0, 0);
            region.AddRoom(room2, 0, 1, 0);

            region.SetStartRoom(room2);

            Assert.AreEqual(room2, region.CurrentRoom);
        }

        [TestMethod]
        public void GivenCanMove_WhenMove_ThenTrue()
        {
            var room1 = new Room(Identifier.Empty, Description.Empty, new Exit(Direction.East));
            var room2 = new Room(Identifier.Empty, Description.Empty, new Exit(Direction.West));

            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room1, 0, 0, 0);
            region.AddRoom(room2, 1, 0, 0);

            var result = region.Move(Direction.East);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenCantMove_WhenMove_ThenFalse()
        {
            var room1 = new Room(Identifier.Empty, Description.Empty, new Exit(Direction.East));
            var room2 = new Room(Identifier.Empty, Description.Empty, new Exit(Direction.West));

            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room1, 0, 0, 0);
            region.AddRoom(room2, 1, 0, 0);

            var result = region.Move(Direction.West);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenCanMove_WhenMove_ThenCurrentRoomIsNewRoom()
        {
            var room1 = new Room(Identifier.Empty, Description.Empty, new Exit(Direction.East));
            var room2 = new Room(Identifier.Empty, Description.Empty, new Exit(Direction.West));

            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room1, 0, 0, 0);
            region.AddRoom(room2, 1, 0, 0);

            region.Move(Direction.East);

            Assert.AreEqual(room2, region.CurrentRoom);
        }

        [TestMethod]
        public void GivenCantMove_WhenMove_ThenCurrentRoomIsNotChanged()
        {
            var room1 = new Room(Identifier.Empty, Description.Empty, new Exit(Direction.East));
            var room2 = new Room(Identifier.Empty, Description.Empty, new Exit(Direction.West));

            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room1, 0, 0, 0);
            region.AddRoom(room2, 1, 0, 0);

            region.Move(Direction.West);
            
            Assert.AreEqual(room1, region.CurrentRoom);
        }

        [TestMethod]
        public void GivenNoAdjoiningRoom_WhenGetAdjoiningRoom_ThenNull()
        {
            var room1 = new Room(Identifier.Empty, Description.Empty, new Exit(Direction.East));
            var room2 = new Room(Identifier.Empty, Description.Empty, new Exit(Direction.West));

            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room1, 0, 0, 0);
            region.AddRoom(room2, 1, 0, 0);

            var result = region.GetAdjoiningRoom(Direction.West, region.CurrentRoom);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GivenAdjoiningRoom_WhenGetAdjoiningRoom_ThenNotNull()
        {
            var room1 = new Room(Identifier.Empty, Description.Empty, new Exit(Direction.East));
            var room2 = new Room(Identifier.Empty, Description.Empty, new Exit(Direction.West));

            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room1, 0, 0, 0);
            region.AddRoom(room2, 1, 0, 0);

            var result = region.GetAdjoiningRoom(Direction.East, region.CurrentRoom);
            
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Given2LockedDoors_WhenUnlockDoorPair_ThenTrue()
        {
            var room1 = new Room(Identifier.Empty, Description.Empty, new Exit(Direction.East, true));
            var room2 = new Room(Identifier.Empty, Description.Empty, new Exit(Direction.West, true));

            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room1, 0, 0, 0);
            region.AddRoom(room2, 1, 0, 0);

            var result = region.UnlockDoorPair(Direction.East);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Given2LockedDoors_WhenUnlockDoorPair_ThenBothDoorsUnlocked()
        {
            var room1 = new Room(Identifier.Empty, Description.Empty, new Exit(Direction.East, true));
            var room2 = new Room(Identifier.Empty, Description.Empty, new Exit(Direction.West, true));

            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room1, 0, 0, 0);
            region.AddRoom(room2, 1, 0, 0);

            room1.FindExit(Direction.East, true, out var room1Exit);
            room2.FindExit(Direction.West, true, out var room2Exit);

            region.UnlockDoorPair(Direction.East);

            Assert.IsFalse(room1Exit.IsLocked);
            Assert.IsFalse(room2Exit.IsLocked);
        }

        [TestMethod]
        public void GivenNorth_WhenNextPosition_ThenXTheSameYIncrements()
        {
            Region.NextPosition(0, 0, 0, Direction.North, out var x, out var y, out _);

            Assert.AreEqual(0, x);
            Assert.AreEqual(1, y);
        }

        [TestMethod]
        public void GivenSouth_WhenNextPosition_ThenXTheSameYDecrements()
        {
            Region.NextPosition(0, 0, 0, Direction.South, out var x, out var y, out _);

            Assert.AreEqual(0, x);
            Assert.AreEqual(-1, y);
        }

        [TestMethod]
        public void GivenWest_WhenNextPosition_ThenXDecrementsYTheSame()
        {
            Region.NextPosition(0, 0, 0, Direction.West, out var x, out var y, out _);

            Assert.AreEqual(-1, x);
            Assert.AreEqual(0, y);
        }

        [TestMethod]
        public void GivenEast_WhenNextPosition_ThenXIncrementsYTheSame()
        {
            Region.NextPosition(0, 0, 0, Direction.East, out var x, out var y, out _);

            Assert.AreEqual(1, x);
            Assert.AreEqual(0, y);
        }

        [TestMethod]
        public void GivenRoomNotInRegion_WhenNextPosition_ThenNull()
        {
            var region = new Region(string.Empty, string.Empty);
            var room = new Room(Identifier.Empty, Description.Empty);

            var result = region.GetPositionOfRoom(room);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GivenRoomaAtX0Y0Z0_WhenNextPosition_ThenNotNull()
        {
            var region = new Region(string.Empty, string.Empty);
            var room = new Room(Identifier.Empty, Description.Empty);
            region.AddRoom(room, 0, 0, 0);

            var result = region.GetPositionOfRoom(room);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GivenRoomaAtX0Y0Z0_WhenNextPosition_ThenResultRoomQualsInputRoom()
        {
            var region = new Region(string.Empty, string.Empty);
            var room = new Room(Identifier.Empty, Description.Empty);
            region.AddRoom(room, 0, 0, 0);

            var result = region.GetPositionOfRoom(room);

            Assert.AreEqual(room, result.Room);
        }

        [TestMethod]
        public void GivenRoomaAtX0Y0Z0_WhenNextPosition_ThenX0Y0Z0()
        {
            var region = new Region(string.Empty, string.Empty);
            var room = new Room(Identifier.Empty, Description.Empty);
            region.AddRoom(room, 0, 0, 0);

            var result = region.GetPositionOfRoom(room);

            Assert.AreEqual(0, result.X);
            Assert.AreEqual(0, result.Y);
            Assert.AreEqual(0, result.Z);
        }
    }
}
