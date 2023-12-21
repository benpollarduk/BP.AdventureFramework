using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Extensions
{
    [TestClass]
    public class ArrayExtensions_Tests
    {
        [TestMethod]
        public void GivenOneElement_WhenRemoveThatElement_ThenNotElements()
        {
            var value = new[] { new Item(string.Empty, string.Empty) };

            var result = value.Remove(value[0]);

            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void GivenOneElement_WhenRemoveADifferentElement_Then1Element()
        {
            var value = new[] { new Item(string.Empty, string.Empty) };

            var result = value.Remove(new Item(string.Empty, string.Empty));

            Assert.AreEqual(1, result.Length);
        }

        [TestMethod]
        public void GivenEmptyArray_WhenRemove_ThenNotNull()
        {
            var value = new Item[0];

            var result = value.Remove(new Item(string.Empty, string.Empty));

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GivenEmptyArray_WhenAdd_ThenOneElement()
        {
            var value = new Item[0];

            var result = value.Remove(new Item(string.Empty, string.Empty));

            Assert.AreEqual(0, result.Length);
        }
    }
}
