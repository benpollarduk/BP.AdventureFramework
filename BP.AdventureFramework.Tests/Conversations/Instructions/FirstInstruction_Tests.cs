using System.Linq;
using BP.AdventureFramework.Conversations;
using BP.AdventureFramework.Conversations.Instructions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Conversations.Instructions
{
    [TestClass]
    public class FirstInstruction_Tests
    {
        [TestMethod]
        public void Give2Paragraphs_WhenNext_ThenReturn0()
        {
            var paragraphs = new[]
            {
                new Paragraph("Test"),
                new Paragraph("Test2")
            };
            var instruction = new FirstInstruction();

            var result = instruction.GetIndexOfNext(paragraphs.First(), paragraphs);

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Give3Paragraphs_WhenNext_ThenReturn0()
        {
            var paragraphs = new[]
            {
                new Paragraph("Test"),
                new Paragraph("Test2"),
                new Paragraph("Test3")
            };
            var instruction = new FirstInstruction();

            var result = instruction.GetIndexOfNext(paragraphs.First(), paragraphs);

            Assert.AreEqual(0, result);
        }
    }
}
