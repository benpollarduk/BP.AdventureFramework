using BP.AdventureFramework.Interaction;
using BP.AdventureFramework.Parsing.Commands.Global;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Commands.Global
{
    [TestClass]
    public class Help_Tests
    {
        [TestMethod]
        public void GivenNullGame_WhenInvoke_ThenNone()
        {
            var command = new Help(null);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.None, result.Result);
        }

        [TestMethod]
        public void GivenValidGame_WhenInvoke_ThenSelfContained()
        {
            var command = new Help(new GameStructure.Game(string.Empty, string.Empty, null, null));

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.SelfContained, result.Result);
        }
    }
}
