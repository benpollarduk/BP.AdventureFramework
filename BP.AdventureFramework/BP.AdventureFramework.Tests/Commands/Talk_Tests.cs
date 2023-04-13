using BP.AdventureFramework.Characters;
using BP.AdventureFramework.Interaction;
using BP.AdventureFramework.Parsing.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Commands
{
    [TestClass]
    public class Talk_Tests
    {
        [TestMethod]
        public void GivenInvoke_WhenNoTarget_ThenNoReaction()
        {
            var command = new Talk(null);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.NoReaction, result.Result);
        }

        [TestMethod]
        public void GivenInvoke_WhenTargetIsDead_ThenNoReaction()
        {
            var npc = new NonPlayableCharacter(new Identifier(""), new Description(""), null, false, null);
            var command = new Talk(npc);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.NoReaction, result.Result);
        }

        [TestMethod]
        public void GivenInvoke_WhenTarget_ThenReacted()
        {
            var npc = new NonPlayableCharacter(new Identifier(""), new Description(""));
            var command = new Talk(npc);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.Reacted, result.Result);
        }
    }
}
