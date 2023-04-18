using BP.AdventureFramework.Assets.Characters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Assets.Characters
{
    [TestClass]
    public class PlayableCharacter_Tests
    {
        [TestMethod]
        public void GivenNewCharacter_WhenGetIsAlive_ThenReturnTrue()
        {
            var pc = new PlayableCharacter(string.Empty, string.Empty);

            Assert.IsTrue(pc.IsAlive);
        }

        [TestMethod]
        public void GivenNewCharacter_WhenKill_ThenIsAliveIsFalse()
        {
            var pc = new PlayableCharacter(string.Empty, string.Empty);

            pc.Kill(string.Empty);

            Assert.IsFalse(pc.IsAlive);
        }
    }
}
