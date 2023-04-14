using BP.AdventureFramework.Interaction;
using BP.AdventureFramework.Parsing.Commands.Global;
using BP.AdventureFramework.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Commands.Global
{
    [TestClass]
    public class Map_Tests
    {
        [TestMethod]
        public void GivenNullGame_WhenInvoke_ThenNoReaction()
        {
            var command = new Map(null, null);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.NoReaction, result.Result);
        }

        [TestMethod]
        public void GivenNullMapDrawer_WhenInvoke_ThenNoReaction()
        {
            var command = new Map(new GameStructure.Game(string.Empty, string.Empty, null, null), null);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.NoReaction, result.Result);
        }

        [TestMethod]
        public void GivenValidGame_WhenInvoke_ThenReacted()
        {
            var command = new Map(new GameStructure.Game(string.Empty, string.Empty, null, null), new MapDrawer());

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.Reacted, result.Result);
        }
    }
}
