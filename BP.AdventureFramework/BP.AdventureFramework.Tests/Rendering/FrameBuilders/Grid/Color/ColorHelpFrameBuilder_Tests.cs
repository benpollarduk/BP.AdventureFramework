using BP.AdventureFramework.Interpretation;
using BP.AdventureFramework.Rendering.FrameBuilders.Grid;
using BP.AdventureFramework.Rendering.FrameBuilders.Grid.Color;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Rendering.FrameBuilders.Grid.Color
{
    [TestClass]
    public class ColorHelpFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenDefaults_WhenBuild_ThenNoException()
        {
            var gridStringBuilder = new GridStringBuilder();
            var builder = new ColorHelpFrameBuilder(gridStringBuilder);

            builder.Build(string.Empty, string.Empty, new CommandHelp[0],  80, 50);

            Assert.IsTrue(true);
        }
    }
}
