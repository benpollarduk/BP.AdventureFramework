﻿using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Rendering.FrameBuilders;
using BP.AdventureFramework.Rendering.FrameBuilders.Color;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Rendering.FrameBuilders.Color
{
    [TestClass]
    public class ColorRegionMapBuilder_Tests
    {
        [TestMethod]
        public void GivenDefaultValues_WhenBuildRegionMap_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var builder = new ColorRegionMapBuilder();
                var region = new Region(string.Empty, string.Empty);
                region.AddRoom(new Room(string.Empty, string.Empty), 0, 0, 0);
                region.AddRoom(new Room(string.Empty, string.Empty), 0, 1, 0);
                var stringBuilder = new GridStringBuilder();
                stringBuilder.Resize(new Size(80, 50));

                builder.BuildRegionMap(stringBuilder, region, 0, 0, 80, 50);
            });
        }
    }
}
