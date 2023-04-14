using BP.AdventureFramework.Interaction;
using BP.AdventureFramework.Parsing.Commands.Game;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Commands.Game
{
    [TestClass]
    public class Unactionable_Tests
    {
        [TestMethod]
        public void GivenDefault_WhenInvoke_ThenNone()
        {
            var command = new Unactionable();

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.None, result.Result);
        }
    }
}
