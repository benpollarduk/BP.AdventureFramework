using BP.AdventureFramework.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Logic
{
    [TestClass]
    public class CompletionCheckResult_Tests
    {
        [TestMethod]
        public void GivenNotComplete_ThenIsCompletedFalseTitleEmptyDescriptionEmpty()
        {
            var result = CompletionCheckResult.NotComplete;

            Assert.IsFalse(result.IsCompleted);
            Assert.AreEqual(string.Empty, result.Title);
            Assert.AreEqual(string.Empty, result.Description);
        }
    }
}
