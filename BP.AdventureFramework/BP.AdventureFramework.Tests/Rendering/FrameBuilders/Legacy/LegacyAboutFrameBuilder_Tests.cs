using System;
using System.Linq;
using BP.AdventureFramework.Logic;
using BP.AdventureFramework.Rendering.FrameBuilders.Legacy;
using BP.AdventureFramework.Rendering.LayoutBuilders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Rendering.FrameBuilders.Legacy
{
    [TestClass]
    public class LegacyAboutFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenWidthOf80HeightOf50_WhenBuild_ThenFrameWithWidthOf80AndHeightOf50Returned()
        {
            var builder = new LegacyAboutFrameBuilder(new StringLayoutBuilder());

            var game = Game.Create(string.Empty, string.Empty, x => null, () => null, p => new CompletionCheckResult(false, string.Empty, string.Empty)).Invoke();
            var result = builder.Build(string.Empty, game,  80, 50);
            var lines = result.ToString().Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var lineCount = lines.Length;
            var lineLength = lines.Average(x => x.Length);

            Assert.AreEqual(80, lineLength);
            Assert.AreEqual(50, lineCount);
        }
    }
}
