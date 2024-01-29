using BP.AdventureFramework.Conversations;
using BP.AdventureFramework.Conversations.Instructions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Conversations.Instructions
{
    [TestClass]
    public class ToName_Tests
    {
        [TestMethod]
        public void GivenIDOfFirst_WhenNext_ThenReturn0()
        {
            var paragraphs = new[]
            {
                new Paragraph("", "ID"),
                new Paragraph("", "ID2")
            };
            var instruction = new ToName("ID");

            var result = instruction.GetIndexOfNext(paragraphs[0], paragraphs);

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GivenIDOfSecond_WhenNext_ThenReturn1()
        {
            var paragraphs = new[]
            {
                new Paragraph("", "ID"),
                new Paragraph("", "ID2")
            };
            var instruction = new ToName("ID2");

            var result = instruction.GetIndexOfNext(paragraphs[0], paragraphs);

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void GivenUnknownID_WhenNext_ThenReturn0()
        {
            var paragraphs = new[]
            {
                new Paragraph("", "ID"),
                new Paragraph("", "ID2")
            };
            var instruction = new ToName("ID3");

            var result = instruction.GetIndexOfNext(paragraphs[0], paragraphs);

            Assert.AreEqual(0, result);
        }
    }
}
