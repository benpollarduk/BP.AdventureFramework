using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Commands.Frame;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Commands.Frame
{
    [TestClass]
    public class CommandsOff_Tests
    {
        [TestMethod]
        public void GivenNullGame_WhenInvoke_ThenNone()
        {
            var command = new CommandsOff(null);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.None, result.Result);
        }

        [TestMethod]
        public void GivenValidGame_WhenInvoke_ThenReacted()
        {
            var game = Logic.Game.Create(string.Empty, string.Empty, null, null, null).Invoke();
            var command = new CommandsOff(game);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.Reacted, result.Result);
        }
    }
}
