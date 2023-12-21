using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Rendering.FrameBuilders;
using BP.AdventureFramework.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Rendering.FrameBuilders
{
    [TestClass]
    public class SceneHelper_Tests
    {
        [TestMethod]
        public void GivenRoomWithNoView_WhenCreateViewpointAsString_ThenEmptyString()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var room = new Room(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            var region = regionMaker.Make();
            var viewPoint = ViewPoint.Create(region);

            var result = SceneHelper.CreateViewpointAsString(room, viewPoint);

            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void GivenRoomWithAView_WhenCreateViewpointAsString_ThenNonEmptyString()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var room = new Room(string.Empty, string.Empty, new Exit(Direction.East));
            regionMaker[0, 0, 0] = room;
            regionMaker[1, 0, 0] = new Room("Test", "Test", new Exit(Direction.West));
            var region = regionMaker.Make();
            var viewPoint = ViewPoint.Create(region);

            var result = SceneHelper.CreateViewpointAsString(room, viewPoint);

            Assert.AreNotEqual(string.Empty, result);
        }

        [TestMethod]
        public void GivenRoomWithNoNPCs_WhenCreateNPCString_ThenEmptyString()
        {
            var room = new Room(string.Empty, string.Empty);

            var result = SceneHelper.CreateNPCString(room);

            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void GivenRoomWithASingle_WhenCreateNPCString_ThenNonEmptyString()
        {
            var room = new Room(string.Empty, string.Empty);
            room.AddCharacter(new NonPlayableCharacter("Test", "Test"));

            var result = SceneHelper.CreateNPCString(room);

            Assert.AreNotEqual(string.Empty, result);
        }
    }
}
