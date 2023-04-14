using BP.AdventureFramework.Commands.Frame;
using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Commands.Frame
{
    [TestClass]
    public class KeyOff_Tests
    {
        [TestMethod]
        public void GivenNullMapDrawer_WhenInvoke_ThenNone()
        {
            var command = new KeyOff(null);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.None, result.Result);
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
