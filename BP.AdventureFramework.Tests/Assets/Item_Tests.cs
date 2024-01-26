using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Interaction;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Assets
{
    [TestClass]
    public class Item_Tests
    {
        [TestMethod]
        public void Given2Items_WhenInteract_ThenNoEffect()
        {
            var item = new Item(string.Empty, string.Empty);
            var item2 = new Item(string.Empty, string.Empty);

            var result = item.Interact(item2);

            Assert.AreEqual(InteractionEffect.NoEffect, result.Effect);
        }

        [TestMethod]
        public void Given2Items_WhenMorph_ThenItemHasMorphedItemIdentifier()
        {
            var item = new Item("Test1", string.Empty);
            var item2 = new Item("Test2", string.Empty);

            item.Morph(item2);

            Assert.AreEqual(item.Identifier.IdentifiableName, item2.Identifier.IdentifiableName);
        }

        [TestMethod]
        public void Given2Items_WhenMorph_ThenItemHasMorphedItemDescription()
        {
            var item = new Item(string.Empty, "Description1");
            var item2 = new Item(string.Empty, "Description2");

            item.Morph(item2);

            Assert.AreEqual(item.Description.GetDescription(), item2.Description.GetDescription());
        }
    }
}
