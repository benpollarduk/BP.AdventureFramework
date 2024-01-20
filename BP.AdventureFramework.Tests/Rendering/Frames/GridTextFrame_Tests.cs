using System;
using BP.AdventureFramework.Rendering.Frames;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Rendering.Frames
{
    [TestClass]
    public class GridTextFrame_Tests
    {
        [TestMethod]
        public void GivenUnmodifiedEnvironment_WhenIsColorSupressed_ThenReturnFalse()
        {
            var result = GridTextFrame.IsColorSupressed();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenNoColorEnvironmentVariableSetToEmptyString_WhenIsColorSupressed_ThenReturnFalse()
        {
            Environment.SetEnvironmentVariable(GridTextFrame.NO_COLOR, "");

            var result = GridTextFrame.IsColorSupressed();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenNoColorEnvironmentVariableSetTo0_WhenIsColorSupressed_ThenReturnFalse()
        {
            Environment.SetEnvironmentVariable(GridTextFrame.NO_COLOR, "0");

            var result = GridTextFrame.IsColorSupressed();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenNoColorEnvironmentVariableSetToFalse_WhenIsColorSupressed_ThenReturnFalse()
        {
            Environment.SetEnvironmentVariable(GridTextFrame.NO_COLOR, "False");

            var result = GridTextFrame.IsColorSupressed();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenNoColorEnvironmentVariableSetTo1_WhenIsColorSupressed_ThenReturnTrue()
        {
            Environment.SetEnvironmentVariable(GridTextFrame.NO_COLOR, "1");

            var result = GridTextFrame.IsColorSupressed();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenNoColorEnvironmentVariableSetToTrue_WhenIsColorSupressed_ThenReturnTrue()
        {
            Environment.SetEnvironmentVariable(GridTextFrame.NO_COLOR, "True");

            var result = GridTextFrame.IsColorSupressed();

            Assert.IsTrue(result);
        }
    }
}
