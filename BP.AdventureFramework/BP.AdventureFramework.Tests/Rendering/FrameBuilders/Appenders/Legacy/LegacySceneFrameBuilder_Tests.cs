using System;
using System.Linq;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Rendering;
using BP.AdventureFramework.Rendering.FrameBuilders.Appenders;
using BP.AdventureFramework.Rendering.FrameBuilders.Appenders.Legacy;
using BP.AdventureFramework.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Rendering.FrameBuilders.Appenders.Legacy
{
    [TestClass]
    public class LegacySceneFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenWidthOf80HeightOf50_WhenBuild_ThenFrameWithWidthOf80AndHeightOf50Returned()
        {
            var builder = new LegacySceneFrameBuilder(new LineStringBuilder(), new LegacyRoomMapBuilder());

            var result = builder.Build(new Room(string.Empty, string.Empty), ViewPoint.NoView, new PlayableCharacter(string.Empty, string.Empty), string.Empty, null, KeyType.None, 80, 50);
            var lines = result.ToString().Split(new[] { StringUtilities.Newline }, StringSplitOptions.RemoveEmptyEntries);
            var lineCount = lines.Length;
            var lineLength = lines.Average(x => x.Length);

            Assert.AreEqual(80, lineLength);
            Assert.AreEqual(50, lineCount);
        }
    }
}
