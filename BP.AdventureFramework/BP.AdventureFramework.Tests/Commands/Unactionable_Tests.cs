using BP.AdventureFramework.Interaction;
using BP.AdventureFramework.Parsing.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Commands
{
    [TestClass]
    public class Unactionable_Tests
    {
        [TestMethod]
        public void GivenInvoke_WhenDefault_ThenNoReaction()
        {
            var command = new Unactionable();

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.NoReaction, result.Result);
        }
    }
}
