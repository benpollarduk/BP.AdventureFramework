using System;
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
    }
}
