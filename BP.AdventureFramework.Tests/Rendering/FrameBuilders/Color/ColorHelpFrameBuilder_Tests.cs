using System;
using BP.AdventureFramework.Interpretation;
using BP.AdventureFramework.Rendering.FrameBuilders;
using BP.AdventureFramework.Rendering.FrameBuilders.Color;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Rendering.FrameBuilders.Color
{
    [TestClass]
    public class ColorHelpFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenDefaults_WhenBuild_ThenNoException()
        {
            var gridStringBuilder = new GridStringBuilder();
            var builder = new ColorHelpFrameBuilder(gridStringBuilder);

            builder.Build(string.Empty, string.Empty, Array.Empty<CommandHelp>(), 80, 50);

            Assert.IsTrue(true);
        }
    }
}
