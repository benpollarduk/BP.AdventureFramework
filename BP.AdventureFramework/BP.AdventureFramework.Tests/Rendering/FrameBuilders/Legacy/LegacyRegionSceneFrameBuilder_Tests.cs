using System;
using System.Linq;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Rendering;
using BP.AdventureFramework.Rendering.FrameBuilders.Legacy;
using BP.AdventureFramework.Rendering.LayoutBuilders;
using BP.AdventureFramework.Rendering.MapBuilders.Legacy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Rendering.FrameBuilders.Legacy
{
    [TestClass]
    public class LegacyRegionSceneFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenWidthOf80HeightOf50_WhenBuild_ThenFrameWithWidthOf80AndHeightOf50Returned()
        {
            var builder = new LegacySceneFrameBuilder(new StringLayoutBuilder(), new LegacyRoomMapBuilder(new StringLayoutBuilder()));

            var result = builder.Build(new Room(string.Empty, string.Empty), new PlayableCharacter(string.Empty, string.Empty), string.Empty, false, KeyType.None, 80, 50);
            var lines = result.ToString().Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var lineCount = lines.Length;
            var lineLength = lines.Average(x => x.Length);

            Assert.AreEqual(80, lineLength);
            Assert.AreEqual(50, lineCount);
        }
    }
}
