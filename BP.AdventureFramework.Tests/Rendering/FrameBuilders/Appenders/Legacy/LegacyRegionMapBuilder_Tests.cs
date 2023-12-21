using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Rendering.FrameBuilders.Appenders;
using BP.AdventureFramework.Rendering.FrameBuilders.Appenders.Legacy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Rendering.FrameBuilders.Appenders.Legacy
{
    [TestClass]
    public class LegacyRegionMapBuilder_Tests
    {
        [TestMethod]
        public void GivenDefaultValues_WhenBuildRegionMap_ThenNotEmptyOrNull()
        {
            var builder = new LegacyRegionMapBuilder();
            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(new Room(string.Empty, string.Empty), 0, 0, 0);
            region.AddRoom(new Room(string.Empty, string.Empty), 0, 1, 0);

            var result = builder.BuildRegionMap(new LineStringBuilder(), region, 50, 50);

            Assert.IsFalse(string.IsNullOrEmpty(result));
        }
    }
}
