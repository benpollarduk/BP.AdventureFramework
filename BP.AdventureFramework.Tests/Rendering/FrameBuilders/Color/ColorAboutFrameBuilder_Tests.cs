using BP.AdventureFramework.Logic;
using BP.AdventureFramework.Rendering.FrameBuilders;
using BP.AdventureFramework.Rendering.FrameBuilders.Color;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Rendering.FrameBuilders.Color
{
    [TestClass]
    public class ColorAboutFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenDefaults_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var gridStringBuilder = new GridStringBuilder();
                var builder = new ColorAboutFrameBuilder(gridStringBuilder);

                builder.Build(string.Empty, Game.Create(string.Empty, string.Empty, string.Empty, null, () => null, _ => EndCheckResult.NotEnded, _ => EndCheckResult.NotEnded).Invoke(), 80, 50);
            });
        }
    }
}
