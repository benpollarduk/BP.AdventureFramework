using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Utilities
{
    [TestClass]
    public class OverworldMaker_Tests
    {
        [TestMethod]
        public void Given1RegionMakerWith2Rooms_WhenMake_ThenNotNull()
        {
            var room1 = new Room(Identifier.Empty, Description.Empty);
            var room2 = new Room(Identifier.Empty, Description.Empty);
            var regionMaker = new RegionMaker(string.Empty, string.Empty)
            {
                [0, 0, 0] = room1,
                [1, 0, 0] = room2
            };
            var maker = new OverworldMaker(string.Empty, string.Empty, regionMaker);

            var result = maker.Make();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Given1RegionMakerWith2Rooms_WhenMake_ThenRegionsCountEquals1()
        {
            var room1 = new Room(Identifier.Empty, Description.Empty);
            var room2 = new Room(Identifier.Empty, Description.Empty);
            var regionMaker = new RegionMaker(string.Empty, string.Empty)
            {
                [0, 0, 0] = room1,
                [1, 0, 0] = room2
            };
            var maker = new OverworldMaker(string.Empty, string.Empty, regionMaker);

            var result = maker.Make();

            Assert.AreEqual(1, result.Regions.Length);
        }
    }
}
