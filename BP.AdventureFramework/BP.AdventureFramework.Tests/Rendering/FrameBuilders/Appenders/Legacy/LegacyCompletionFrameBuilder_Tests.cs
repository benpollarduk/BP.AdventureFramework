using System;
using System.Linq;
using BP.AdventureFramework.Rendering.FrameBuilders.Appenders;
using BP.AdventureFramework.Rendering.FrameBuilders.Appenders.Legacy;
using BP.AdventureFramework.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Rendering.FrameBuilders.Appenders.Legacy
{
    [TestClass]
    public class LegacyCompletionFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenWidthOf80HeightOf50_WhenBuild_ThenFrameWithWidthOf80AndHeightOf50Returned()
        {
            var builder = new LegacyCompletionFrameBuilder(new LineStringBuilder());

            var result = builder.Build(string.Empty, string.Empty, 80, 50);
            var lines = result.ToString().Split(new[] { StringUtilities.Newline }, StringSplitOptions.RemoveEmptyEntries);
            var lineCount = lines.Length;
            var lineLength = lines.Average(x => x.Length);

            Assert.AreEqual(80, lineLength);
            Assert.AreEqual(50, lineCount);
        }
    }
}
