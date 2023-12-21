using BP.AdventureFramework.Logic;
using BP.AdventureFramework.Rendering.FrameBuilders.Grid;
using BP.AdventureFramework.Rendering.FrameBuilders.Grid.Color;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Rendering.FrameBuilders.Grid.Color
{
    [TestClass]
    public class ColorAboutFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenDefaults_WhenBuild_ThenNoException()
        {
            var gridStringBuilder = new GridStringBuilder();
            var builder = new ColorAboutFrameBuilder(gridStringBuilder);

            builder.Build(string.Empty, Game.Create(string.Empty, string.Empty, string.Empty, x => null, () => null, g => null).Invoke(), 80, 50);

            Assert.IsTrue(true);
        }
    }
}
