using BP.AdventureFramework.Conversations;
using BP.AdventureFramework.Conversations.Instructions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Conversations.Instructions
{
    [TestClass]
    public class Repeat_Tests
    {
        [TestMethod]
        public void GivenFirst_WhenNext_ThenReturn0()
        {
            var paragraphs = new[]
            {
                new Paragraph("Test"),
                new Paragraph("Test2")
            };
            var instruction = new Repeat();

            var result = instruction.GetIndexOfNext(paragraphs[0], paragraphs);

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GivenSecond_WhenNext_ThenReturn1()
        {
            var paragraphs = new[]
            {
                new Paragraph("Test"),
                new Paragraph("Test2")
            };
            var instruction = new Repeat();

            var result = instruction.GetIndexOfNext(paragraphs[1], paragraphs);

            Assert.AreEqual(1, result);
        }
    }
}
