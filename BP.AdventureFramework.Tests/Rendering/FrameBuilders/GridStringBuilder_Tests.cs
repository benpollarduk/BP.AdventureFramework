using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Rendering.FrameBuilders;
using BP.AdventureFramework.Rendering.FrameBuilders.Color;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Rendering.FrameBuilders
{
    [TestClass]
    public class GridStringBuilder_Tests
    {
        [TestMethod]
        public void GivenBlank_WhenDrawHorizontalDivider_ThenDividerIsDrawn()
        {
            var builder = new GridStringBuilder();
            builder.Resize(new Size(10, 10));

            builder.DrawHorizontalDivider(0, AnsiColor.Black);
            var left = builder.GetCharacter(1, 0);
            var right = builder.GetCharacter(8, 0);

            Assert.AreEqual(builder.HorizontalDividerCharacter, left);
            Assert.AreEqual(builder.HorizontalDividerCharacter, right);
        }

        [TestMethod]
        public void GivenBlank_WhenDrawUnderline_ThenUnderlineIsDrawn()
        {
            var builder = new GridStringBuilder();
            builder.Resize(new Size(10, 10));

            builder.DrawUnderline(0, 0, 1, AnsiColor.Black);
            var result = builder.GetCharacter(0, 0);

            Assert.AreEqual(builder.HorizontalDividerCharacter, result);
        }

        [TestMethod]
        public void GivenBlank_WhenDrawBoundary_ThenBoundaryIsDrawn()
        {
            var builder = new GridStringBuilder();
            builder.Resize(new Size(10, 10));

            builder.DrawBoundary(AnsiColor.Black);
            var topLeft = builder.GetCharacter(0, 0);
            var topRight = builder.GetCharacter(0, 9);
            var bottomLeft = builder.GetCharacter(0, 9);
            var bottomRight = builder.GetCharacter(9, 9);

            Assert.AreEqual(builder.LeftBoundaryCharacter, topLeft);
            Assert.AreEqual(builder.LeftBoundaryCharacter, bottomLeft);
            Assert.AreEqual(builder.RightBoundaryCharacter, topRight);
            Assert.AreEqual(builder.RightBoundaryCharacter, bottomRight);
        }

        [TestMethod]
        public void GivenA_WhenDrawWrapped_ThenDrawnCharacterIsA()
        {
            var builder = new GridStringBuilder();
            builder.Resize(new Size(10, 10));

            builder.DrawWrapped("A", 0, 0, 10, AnsiColor.Black, out _, out _);
            var result = builder.GetCharacter(0, 0);

            Assert.AreEqual("A", result.ToString());
        }

        [TestMethod]
        public void GivenAA_WhenDrawWrapped_ThenOutXIs1()
        {
            var builder = new GridStringBuilder();
            builder.Resize(new Size(10, 10));

            builder.DrawWrapped("AA", 0, 0, 10, AnsiColor.Black, out var x, out _);

            Assert.AreEqual(1, x);
        }

        [TestMethod]
        public void GivenAA_WhenDrawCentralisedWrapped_ThenDrawnCharacterIsA()
        {
            var builder = new GridStringBuilder();
            builder.Resize(new Size(10, 10));

            builder.DrawCentralisedWrapped("AA", 0, 10, AnsiColor.Black, out _, out _);
            var result = builder.GetCharacter(5, 0);

            Assert.AreEqual("A", result.ToString());
        }

        [TestMethod]
        public void GivenA_WhenDrawCentralisedWrapped_ThenOutXIs5()
        {
            var builder = new GridStringBuilder();
            builder.Resize(new Size(10, 10));

            builder.DrawCentralisedWrapped("A", 0, 10, AnsiColor.Black, out var x, out _);

            Assert.AreEqual(5, x);
        }

        [TestMethod]
        public void GivenWidth10And10Characters_WhenGetNumberOfLines_Then1()
        {
            var builder = new GridStringBuilder();
            builder.Resize(new Size(10, 10));

            var result = builder.GetNumberOfLines("AND THE DO", 0, 0, 10);

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void GivenWidth10And11Characters_WhenGetNumberOfLines_Then2()
        {
            var builder = new GridStringBuilder();
            builder.Resize(new Size(10, 10));

            var result = builder.GetNumberOfLines("AND THE DOG", 0, 0, 10);

            Assert.AreEqual(2, result);
        }
    }
}
