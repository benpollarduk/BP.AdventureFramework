using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Commands.Game;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Commands.Game
{
    [TestClass]
    public class Unactionable_Tests
    {
        [TestMethod]
        public void GivenDefault_WhenInvoke_ThenError()
        {
            var command = new Unactionable();

            var result = command.Invoke(null);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }
    }
}
