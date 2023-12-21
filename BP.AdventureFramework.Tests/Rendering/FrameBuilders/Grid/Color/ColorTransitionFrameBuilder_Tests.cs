using BP.AdventureFramework.Rendering.FrameBuilders.Grid;
using BP.AdventureFramework.Rendering.FrameBuilders.Grid.Color;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Rendering.FrameBuilders.Grid.Color
{
    [TestClass]
    public class ColorTransitionFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenDefaults_WhenBuild_ThenNoException()
        {
            var gridStringBuilder = new GridStringBuilder();
            var builder = new ColorTransitionFrameBuilder(gridStringBuilder);

            builder.Build(string.Empty, string.Empty, 80, 50);

            Assert.IsTrue(true);
        }
    }
}
