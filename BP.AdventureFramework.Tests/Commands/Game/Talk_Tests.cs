using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Commands.Game;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Commands.Game
{
    [TestClass]
    public class Talk_Tests
    {
        [TestMethod]
        public void GivenNoTarget_WhenInvoke_ThenError()
        {
            var command = new Talk(null);

            var result = command.Invoke(null);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenTargetIsDead_WhenInvoke_ThenError()
        {
            var game = AdventureFramework.Logic.Game.Create(string.Empty, string.Empty, string.Empty, null, null, null).Invoke();
            var npc = new NonPlayableCharacter(Identifier.Empty, Description.Empty, null, false, null);
            var command = new Talk(npc);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenTarget_WhenInvoke_ThenInternal()
        {
            var game = AdventureFramework.Logic.Game.Create(string.Empty, string.Empty, string.Empty, null, null, null).Invoke();
            var npc = new NonPlayableCharacter(Identifier.Empty, Description.Empty);
            var command = new Talk(npc);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Internal, result.Result);
        }
    }
}
