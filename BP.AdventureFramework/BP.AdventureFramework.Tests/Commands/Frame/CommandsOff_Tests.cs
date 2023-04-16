using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Commands.Frame;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Commands.Frame
{
    [TestClass]
    public class CommandsOff_Tests
    {
        [TestMethod]
        public void GivenDefault_WhenInvoke_ThenReacted()
        {
            var command = new CommandsOff();

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.Reacted, result.Result);
        }
    }
}
