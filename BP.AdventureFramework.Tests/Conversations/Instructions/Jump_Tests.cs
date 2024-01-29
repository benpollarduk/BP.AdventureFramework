using BP.AdventureFramework.Conversations;
using BP.AdventureFramework.Conversations.Instructions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Conversations.Instructions
{
    [TestClass]
    public class Jump_Tests
    {
        [TestMethod]
        public void GivenParagraph0WithDelta0_WhenNext_ThenReturn0()
        {
            var paragraphs = new[]
            {
                new Paragraph("Test1"),
                new Paragraph("Test2")
            };
            var instruction = new Jump(0);

            var result = instruction.GetIndexOfNext(paragraphs[0], paragraphs);

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GivenParagraph1WithDelta0_WhenNext_ThenReturn1()
        {
            var paragraphs = new[]
            {
                new Paragraph("Test1"),
                new Paragraph("Test2")
            };
            var instruction = new Jump(0);

            var result = instruction.GetIndexOfNext(paragraphs[1], paragraphs);

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void GivenParagraph0WithDelta1_WhenNext_ThenReturn1()
        {
            var paragraphs = new[]
            {
                new Paragraph("Test1"),
                new Paragraph("Test2")
            };
            var instruction = new Jump(1);

            var result = instruction.GetIndexOfNext(paragraphs[0], paragraphs);

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void GivenParagraph1WithDeltaMinus1_WhenNext_ThenReturn0()
        {
            var paragraphs = new[]
            {
                new Paragraph("Test1"),
                new Paragraph("Test2")
            };
            var instruction = new Jump(-1);

            var result = instruction.GetIndexOfNext(paragraphs[1], paragraphs);

            Assert.AreEqual(0, result);
        }


        [TestMethod]
        public void GivenParagraph1WithDelta8_WhenNext_ThenReturn1()
        {
            var paragraphs = new[]
            {
                new Paragraph("Test1"),
                new Paragraph("Test2")
            };
            var instruction = new Jump(8);

            var result = instruction.GetIndexOfNext(paragraphs[0], paragraphs);

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void GivenParagraph1WithDeltaMinus8_WhenNext_ThenReturn0()
        {
            var paragraphs = new[]
            {
                new Paragraph("Test1"),
                new Paragraph("Test2"),
            };
            var instruction = new Jump(-8);

            var result = instruction.GetIndexOfNext(paragraphs[0], paragraphs);

            Assert.AreEqual(0, result);
        }
    }
}
