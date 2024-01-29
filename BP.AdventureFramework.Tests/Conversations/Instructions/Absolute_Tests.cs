using BP.AdventureFramework.Conversations;
using BP.AdventureFramework.Conversations.Instructions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Conversations.Instructions
{
    [TestClass]
    public class Absolute_Tests
    {
        [TestMethod]
        public void GivenIndex0_WhenNext_ThenReturn0()
        {
            var paragraphs = new[]
            {
                new Paragraph("Test1"),
                new Paragraph("Test2")
            };
            var instruction = new Absolute(0);

            var result = instruction.GetIndexOfNext(paragraphs[0], paragraphs);

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GivenIndex1_WhenNext_ThenReturn1()
        {
            var paragraphs = new[]
            {
                new Paragraph("Test1"),
                new Paragraph("Test2")
            };
            var instruction = new Absolute(1);

            var result = instruction.GetIndexOfNext(paragraphs[0], paragraphs);

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void GivenIndexMinus1_WhenNext_ThenReturn0()
        {
            var paragraphs = new[]
            {
                new Paragraph("Test1"),
                new Paragraph("Test2")
            };
            var instruction = new Absolute(-1);

            var result = instruction.GetIndexOfNext(paragraphs[0], paragraphs);

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GivenIndex3_WhenNext_ThenReturn1()
        {
            var paragraphs = new[]
            {
                new Paragraph("Test1"),
                new Paragraph("Test2")
            };
            var instruction = new Absolute(3);

            var result = instruction.GetIndexOfNext(paragraphs[0], paragraphs);

            Assert.AreEqual(1, result);
        }
    }
}
