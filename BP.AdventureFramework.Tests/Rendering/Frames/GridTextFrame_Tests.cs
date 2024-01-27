using System;
using System.IO;
using System.Linq;
using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Rendering.FrameBuilders;
using BP.AdventureFramework.Rendering.FrameBuilders.Color;
using BP.AdventureFramework.Rendering.Frames;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Rendering.Frames
{
    [TestClass]
    public class GridTextFrame_Tests
    {
        [TestMethod]
        public void GivenUnmodifiedEnvironment_WhenIsColorSuppressed_ThenReturnFalse()
        {
            var result = GridTextFrame.IsColorSuppressed();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenNoColorEnvironmentVariableSetToEmptyString_WhenIsColorSuppressed_ThenReturnFalse()
        {
            Environment.SetEnvironmentVariable(GridTextFrame.NO_COLOR, "");

            var result = GridTextFrame.IsColorSuppressed();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenNoColorEnvironmentVariableSetTo0_WhenIsColorSuppressed_ThenReturnFalse()
        {
            Environment.SetEnvironmentVariable(GridTextFrame.NO_COLOR, "0");

            var result = GridTextFrame.IsColorSuppressed();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenNoColorEnvironmentVariableSetToFalse_WhenIsColorSuppressed_ThenReturnFalse()
        {
            Environment.SetEnvironmentVariable(GridTextFrame.NO_COLOR, "False");

            var result = GridTextFrame.IsColorSuppressed();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenNoColorEnvironmentVariableSetTo1_WhenIsColorSuppressed_ThenReturnTrue()
        {
            Environment.SetEnvironmentVariable(GridTextFrame.NO_COLOR, "1");

            var result = GridTextFrame.IsColorSuppressed();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenNoColorEnvironmentVariableSetToTrue_WhenIsColorSuppressed_ThenReturnTrue()
        {
            Environment.SetEnvironmentVariable(GridTextFrame.NO_COLOR, "True");

            var result = GridTextFrame.IsColorSuppressed();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Given10x10GridWithBorder_WhenRender_ThenStreamContainsData()
        {
            var gridStringBuilder = new GridStringBuilder();
            gridStringBuilder.Resize(new Size(10, 10));
            gridStringBuilder.DrawBoundary(AnsiColor.Black);
            var frame = new GridTextFrame(gridStringBuilder, 0, 0, AnsiColor.Black);
            byte[] data;

            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    frame.Render(writer);
                    writer.Flush();
                    data = stream.ToArray();
                }
            }

            Assert.IsTrue(data.Any(x => x != 0));
        }
    }
}
