using BP.AdventureFramework.Characters;
using BP.AdventureFramework.Interaction;
using BP.AdventureFramework.Parsing.Commands.Game;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Commands.Game
{
    [TestClass]
    public class Talk_Tests
    {
        [TestMethod]
        public void GivenNoTarget_WhenInvoke_ThenNoReaction()
        {
            var command = new Talk(null);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.NoReaction, result.Result);
        }

        [TestMethod]
        public void GivenTargetIsDead_WhenInvoke_ThenNoReaction()
        {
            var npc = new NonPlayableCharacter(new Identifier(""), new Description(""), null, false, null);
            var command = new Talk(npc);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.NoReaction, result.Result);
        }

        [TestMethod]
        public void GivenTarget_WhenInvoke_ThenReacted()
        {
            var npc = new NonPlayableCharacter(new Identifier(""), new Description(""));
            var command = new Talk(npc);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.Reacted, result.Result);
        }
    }
}
