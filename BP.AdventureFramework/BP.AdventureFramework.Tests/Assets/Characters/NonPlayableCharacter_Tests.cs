using BP.AdventureFramework.Assets.Characters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Assets.Characters
{
    [TestClass]
    public class NonPlayableCharacter_Tests
    {
        [TestMethod]
        public void GivenNewCharacter_WhenGetIsAlive_ThenReturnTrue()
        {
            var npc = new NonPlayableCharacter(string.Empty, string.Empty);

            Assert.IsTrue(npc.IsAlive);
        }

        [TestMethod]
        public void GivenNewCharacter_WhenKill_ThenIsAliveIsFalse()
        {
            var npc = new NonPlayableCharacter(string.Empty, string.Empty);

            npc.Kill(string.Empty);

            Assert.IsFalse(npc.IsAlive);
        }
    }
}
