using BP.AdventureFramework.Interaction;
using BP.AdventureFramework.Parsing.Commands.Frame;
using BP.AdventureFramework.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Commands.Frame
{
    [TestClass]
    public class KeyOff_Tests
    {
        [TestMethod]
        public void GivenNullMapDrawer_WhenInvoke_ThenNoReaction()
        {
            var command = new KeyOff(null);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.NoReaction, result.Result);
        }

        [TestMethod]
        public void GivenValidMapDrawer_WhenInvoke_ThenReacted()
        {
            var command = new KeyOff(new MapDrawer());

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.Reacted, result.Result);
        }
    }
}
