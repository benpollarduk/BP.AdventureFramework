using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Interaction;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Assets
{
    [TestClass]
    public class Item_Tests
    {
        [TestMethod]
        public void Given2Items_WhenUse_ThenNoEffect()
        {
            var item = new Item(string.Empty, string.Empty);
            var item2 = new Item(string.Empty, string.Empty);

            var result = item.Use(item2);

            Assert.AreEqual(InteractionEffect.NoEffect, result.Effect);
        }
    }
}
