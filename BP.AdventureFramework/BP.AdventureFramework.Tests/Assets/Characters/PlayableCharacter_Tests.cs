using BP.AdventureFramework.Assets;
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

        [TestMethod]
        public void GivenDoesntHaveItem_WhenHasItem_ThenFalse()
        {
            var pc = new PlayableCharacter(string.Empty, string.Empty);
            var item = new Item(string.Empty, string.Empty);

            var result = pc.HasItem(item);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenHasItem_WhenHasItem_ThenFalse()
        {
            var pc = new PlayableCharacter(string.Empty, string.Empty);
            var item = new Item(string.Empty, string.Empty);
            pc.AquireItem(item);

            var result = pc.HasItem(item);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenAnItem_WhenDequireItem_ThenHasItemIsFalse()
        {
            var pc = new PlayableCharacter(string.Empty, string.Empty);
            var item = new Item(string.Empty, string.Empty);
            pc.AquireItem(item);
            pc.DequireItem(item);
            
            var result = pc.HasItem(item);

            Assert.IsFalse(result);
        }
    }
}
