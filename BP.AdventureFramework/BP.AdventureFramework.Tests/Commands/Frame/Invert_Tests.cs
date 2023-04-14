using BP.AdventureFramework.Interaction;
using BP.AdventureFramework.Parsing.Commands.Frame;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Commands.Frame
{
    [TestClass]
    public class Invert_Tests
    {
        [TestMethod]
        public void GivenDefault_WhenInvoke_ThenReacted()
        {
            var command = new Invert();

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.Reacted, result.Result);
        }
    }
}
