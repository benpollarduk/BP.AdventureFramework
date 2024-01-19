using BP.AdventureFramework.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Logic
{
    [TestClass]
    public class EndCheckResult_Tests
    {
        [TestMethod]
        public void GivenNotEnded_ThenHasEndedFalseTitleEmptyDescriptionEmpty()
        {
            var result = EndCheckResult.NotEnded;

            Assert.IsFalse(result.HasEnded);
            Assert.AreEqual(string.Empty, result.Title);
            Assert.AreEqual(string.Empty, result.Description);
        }
    }
}
