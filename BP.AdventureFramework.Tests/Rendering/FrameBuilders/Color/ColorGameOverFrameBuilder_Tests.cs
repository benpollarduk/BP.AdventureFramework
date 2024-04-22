using BP.AdventureFramework.Rendering.FrameBuilders;
using BP.AdventureFramework.Rendering.FrameBuilders.Color;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Rendering.FrameBuilders.Color
{
    [TestClass]
    public class ColorGameOverFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenDefaults_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var gridStringBuilder = new GridStringBuilder();
                var builder = new ColorGameOverFrameBuilder(gridStringBuilder);

                builder.Build(string.Empty, string.Empty, 80, 50);
            });
        }
    }
}
