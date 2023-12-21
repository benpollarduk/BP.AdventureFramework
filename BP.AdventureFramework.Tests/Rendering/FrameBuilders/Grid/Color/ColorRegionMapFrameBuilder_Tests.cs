using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Rendering.FrameBuilders.Grid;
using BP.AdventureFramework.Rendering.FrameBuilders.Grid.Color;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Rendering.FrameBuilders.Grid.Color
{
    [TestClass]
    public class ColorRegionMapFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenWidthOf80HeightOf50_WhenBuild_ThenNotNull()
        {
            var stringBuilder = new GridStringBuilder();
            stringBuilder.Resize(new Size(80, 50));
            var builder = new ColorRegionMapFrameBuilder(stringBuilder, new ColorRegionMapBuilder());
            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(new Room(string.Empty, string.Empty), 0, 0, 0);

            var result = builder.Build(region, 80, 50);

            Assert.IsNotNull(result);
        }
    }
}
