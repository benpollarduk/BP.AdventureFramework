using BP.AdventureFramework.Conversations;
using BP.AdventureFramework.Conversations.Instructions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Conversations.Instructions
{
    [TestClass]
    public class Next_Tests
    {
        [TestMethod]
        public void GivenCurrentlyOn0_WhenNext_ThenReturn1()
        {
            var paragraphs = new[]
            {
                new Paragraph("Test"),
                new Paragraph("Test2")
            };
            var instruction = new Next();

            var result = instruction.GetIndexOfNext(paragraphs[0], paragraphs);

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void GivenCurrentlyOn1_WhenNext_ThenReturn2()
        {
            var paragraphs = new[]
            {
                new Paragraph("Test"),
                new Paragraph("Test2"),
                new Paragraph("Test3")
            };
            var instruction = new Next();

            var result = instruction.GetIndexOfNext(paragraphs[1], paragraphs);

            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void GivenCurrentlyOn2AndOnly3_WhenNext_ThenReturn2()
        {
            var paragraphs = new[]
            {
                new Paragraph("Test"),
                new Paragraph("Test2"),
                new Paragraph("Test3")
            };
            var instruction = new Next();

            var result = instruction.GetIndexOfNext(paragraphs[2], paragraphs);

            Assert.AreEqual(2, result);
        }
    }
}
