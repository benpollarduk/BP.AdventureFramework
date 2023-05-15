using System;
using System.Linq;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Rendering.FrameBuilders.Appenders;
using BP.AdventureFramework.Rendering.FrameBuilders.Appenders.Legacy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Rendering.FrameBuilders.Appenders.Legacy
{
    [TestClass]
    public class LegacyRegionMapFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenWidthOf80HeightOf50_WhenBuild_ThenFrameWithWidthOf80AndHeightOf50Returned()
        {
            var builder = new LegacyRegionMapFrameBuilder(new LineStringBuilder(), new LegacyRegionMapBuilder());
            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(new Room(string.Empty, string.Empty), 0, 0, 0);

            var result = builder.Build(region,  80, 50);
            var lines = result.ToString().Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var lineCount = lines.Length;
            var lineLength = lines.Average(x => x.Length);

            Assert.AreEqual(80, lineLength);
            Assert.AreEqual(50, lineCount);
        }
    }
}
