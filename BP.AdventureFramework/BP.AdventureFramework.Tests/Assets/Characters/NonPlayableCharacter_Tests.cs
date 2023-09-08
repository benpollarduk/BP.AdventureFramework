using BP.AdventureFramework.Assets;
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

        [TestMethod]
        public void GivenDoesntHaveItem_WhenHasItem_ThenFalse()
        {
            var npc = new NonPlayableCharacter(string.Empty, string.Empty);
            var item = new Item(string.Empty, string.Empty);

            var result = npc.HasItem(item);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenHasItem_WhenHasItem_ThenFalse()
        {
            var npc = new NonPlayableCharacter(string.Empty, string.Empty);
            var item = new Item(string.Empty, string.Empty);
            npc.AcquireItem(item);

            var result = npc.HasItem(item);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenAnItem_WhenDequireItem_ThenHasItemIsFalse()
        {
            var npc = new NonPlayableCharacter(string.Empty, string.Empty);
            var item = new Item(string.Empty, string.Empty);
            npc.AcquireItem(item);
            npc.DequireItem(item);

            var result = npc.HasItem(item);

            Assert.IsFalse(result);
        }
    }
}
