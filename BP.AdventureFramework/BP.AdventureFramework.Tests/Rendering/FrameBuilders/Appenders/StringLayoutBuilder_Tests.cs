using System;
using BP.AdventureFramework.Rendering.FrameBuilders.Appenders;
using BP.AdventureFramework.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Rendering.FrameBuilders.Appenders
{
    [TestClass]
    public class StringLayoutBuilder_Tests
    {
        [TestInitialize]
        public void Setup()
        {
            builder = new LineStringBuilder(Convert.ToChar("|"), Convert.ToChar("|"), Convert.ToChar("-")) { LineTerminator = string.Empty };
        }

        private LineStringBuilder builder;

        [TestMethod]
        public void GivenASimpleString_WhenBuildCentralised_ThenCentralisedStringIsReturned()
        {
            var result = builder.BuildCentralised("A", 5);

            Assert.AreEqual("| A |", result);
        }

        [TestMethod]
        public void GivenASimpleString_WhenBuildCentralised_ThenLengthIs5()
        {
            var result = builder.BuildCentralised("A", 5);

            Assert.AreEqual(5, result.Length);
        }

        [TestMethod]
        public void GivenAWidthOf5_WhenBuildHorizontalDivider_ThenLeftBoundary3DividerCharactersAndRightBoundaryReturned()
        {
            var result = builder.BuildHorizontalDivider(5);

            Assert.AreEqual("|---|", result);
        }

        [TestMethod]
        public void GivenAWidthOf5_WhenBuildHorizontalDivider_ThenLenthIs5()
        {
            var result = builder.BuildHorizontalDivider(5);

            Assert.AreEqual(5, result.Length);
        }

        [TestMethod]
        public void GivenAWidthOf5HeightOf1_WhenBuildPaddedArea_ThenLeftBoundary3SpacesAndRightBoundaryReturned()
        {
            var result = builder.BuildPaddedArea(5, 1);

            Assert.AreEqual("|   |", result);
        }

        [TestMethod]
        public void GivenAWidthOf5HeightOf1_WhenBuildPaddedArea_ThenLengthIs5()
        {
            var result = builder.BuildPaddedArea(5, 1);

            Assert.AreEqual(5, result.Length);
        }

        [TestMethod]
        public void GivenAWidthOf5_WhenBuildWhitespace_Then5Spaces()
        {
            var result = builder.BuildWhitespace(5);

            Assert.AreEqual("     ", result);
        }

        [TestMethod]
        public void GivenAWidthOf5_WhenBuildWhitespace_ThenLengthIs5()
        {
            var result = builder.BuildWhitespace(5);

            Assert.AreEqual(5, result.Length);
        }

        [TestMethod]
        public void GivenAWidthOf15ValueOfHelloAndNoCentralisation_WhenBuildWrappedPadded_ThenReturnCorrectlyFormattedString()
        {
            var result = builder.BuildWrappedPadded("Hello", 15, false);

            Assert.AreEqual("| Hello       |", result);
        }

        [TestMethod]
        public void GivenAWidthOf15ValueOfHelloAndNoCentralisation_WhenBuildWrappedPadded_ThenLengthIs15()
        {
            var result = builder.BuildWrappedPadded("Hello", 15, false);

            Assert.AreEqual(15, result.Length);
        }

        [TestMethod]
        public void GivenAWidthOf15ValueWithLengthOf15_WhenBuildWrappedPadded_ThenReturn2Lines()
        {
            builder.LineTerminator = StringUtilities.Newline;
            var result = builder.BuildWrappedPadded("12345 78910 111", 15, false).Split(new[] { StringUtilities.Newline }, StringSplitOptions.RemoveEmptyEntries);

            Assert.AreEqual(2, result.Length);
        }

        [TestMethod]
        public void GivenAWidthOf10_WhenBuildUnderline_ThenAnUnderlineWith10Dash()
        {
            var result = builder.BuildUnderline(0, 10, 20);

            Assert.IsTrue(result.Contains("----------"));
        }
    }
}
