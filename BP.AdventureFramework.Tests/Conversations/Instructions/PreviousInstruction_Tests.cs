using System.Linq;
using BP.AdventureFramework.Conversations;
using BP.AdventureFramework.Conversations.Instructions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Conversations.Instructions
{
    [TestClass]
    public class PreviousInstruction_Tests
    {
        [TestMethod]
        public void GiveCurrentlyOn0_WhenNext_ThenReturn0()
        {
            var paragraphs = new[]
            {
                new Paragraph("Test"),
                new Paragraph("Test2")
            };
            var instruction = new PreviousInstruction();

            var result = instruction.GetIndexOfNext(paragraphs[0], paragraphs);

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GiveCurrentlyOn1_WhenNext_ThenReturn0()
        {
            var paragraphs = new[]
            {
                new Paragraph("Test"),
                new Paragraph("Test2"),
                new Paragraph("Test3")
            };
            var instruction = new PreviousInstruction();

            var result = instruction.GetIndexOfNext(paragraphs[1], paragraphs);

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GiveCurrentlyOn2_WhenNext_ThenReturn1()
        {
            var paragraphs = new[]
            {
                new Paragraph("Test"),
                new Paragraph("Test2"),
                new Paragraph("Test3")
            };
            var instruction = new PreviousInstruction();

            var result = instruction.GetIndexOfNext(paragraphs[2], paragraphs);

            Assert.AreEqual(1, result);
        }
    }
}
