using System;
using System.Linq;
using BP.AdventureFramework.Rendering.FrameBuilders.Legacy;
using BP.AdventureFramework.Rendering.LayoutBuilders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Rendering.FrameBuilders.Legacy
{
    [TestClass]
    public class LegacyEndFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenWidthOf80HeightOf50_WhenBuild_ThenFrameWithWidthOf80AndHeightOf50Returned()
        {
            var builder = new LegacyEndFrameBuilder(new StringLayoutBuilder());

            var result = builder.Build(string.Empty, string.Empty, 80, 50);
            var lines = result.ToString().Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var lineCount = lines.Length;
            var lineLength = lines.Average(x => x.Length);

            Assert.AreEqual(80, lineLength);
            Assert.AreEqual(50, lineCount);
        }
    }
}
