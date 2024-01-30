using BP.AdventureFramework.Assets.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Assets.Attributes
{
    [TestClass]
    public class AttributeManager_Tests
    {
        [TestMethod]
        public void GivenNoAttributes_WhenGetAttrubutes_ThenReturnEmptyArray()
        {
            var manager = new AttributeManager();

            var result = manager.GetAttributes();

            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void GivenOneAttribute_WhenGetAttrubutes_ThenReturnArrayWithOneElement()
        {
            var manager = new AttributeManager();
            manager.Add("test", 0);

            var result = manager.GetAttributes();

            Assert.AreEqual(1, result.Length);
        }

        [TestMethod]
        public void GivenNoAttributes_WhenAdd_ThenOneAttribute()
        {
            var manager = new AttributeManager();
            manager.Add("test", 0);

            var result = manager.GetAttributes();

            Assert.AreEqual(1, result.Length);
        }

        [TestMethod]
        public void GivenOneAttributes_WhenAddDuplicateAttribute_ThenOneAttribute()
        {
            var manager = new AttributeManager();
            manager.Add("test", 0);
            manager.Add("test", 0);

            var result = manager.GetAttributes();

            Assert.AreEqual(1, result.Length);
        }

        [TestMethod]
        public void GivenOneAttributes_WhenAddNonDuplicateAttribute_ThenTwoAttribute()
        {
            var manager = new AttributeManager();
            manager.Add("test", 0);
            manager.Add("test1", 0);

            var result = manager.GetAttributes();

            Assert.AreEqual(2, result.Length);
        }

        [TestMethod]
        public void GivenOneAttributes_WhenAddNonDuplicateAttribute_ThenAttributeValueAdded()
        {
            var manager = new AttributeManager();
            manager.Add("test", 1);
            manager.Add(new Attribute("test", "", 100d, 2), 2);

            var result = manager.GetValue("test");

            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void GivenNoAttributes_WhenRemove_ThenNoAttributes()
        {
            var manager = new AttributeManager();
            manager.Remove("test");

            var result = manager.GetAttributes();

            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void GivenTwoAttributes_WhenRemove_ThenOneAttribute()
        {
            var manager = new AttributeManager();
            manager.Add("test", 0);
            manager.Add("test1", 0);
            manager.Remove(new Attribute("test", "", 0, 100));

            var result = manager.GetAttributes();

            Assert.AreEqual(1, result.Length);
        }

        [TestMethod]
        public void GivenTwoAttributes_WhenRemoveAll_ThenNoAttributes()
        {
            var manager = new AttributeManager();
            manager.Add("test", 0);
            manager.Add("test1", 0);
            manager.RemoveAll();

            var result = manager.GetAttributes();

            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void GivenNoAttributes_WhenSubtract_ThenNoAttributes()
        {
            var manager = new AttributeManager();
            manager.Subtract("test", 0);

            var result = manager.GetAttributes();

            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void GivenOneAttributes_WhenSubtractDuplicateAttribute_ThenOneAttribute()
        {
            var manager = new AttributeManager();
            manager.Add("test", 0);
            manager.Subtract("test", 0);

            var result = manager.GetAttributes();

            Assert.AreEqual(1, result.Length);
        }

        [TestMethod]
        public void GivenOneAttributes_WhenSubtractAttribute_ThenAttributeValueSubtracted()
        {
            var manager = new AttributeManager();
            manager.Add("test", 100);
            manager.Subtract(new Attribute("test", "", 100d, 2), 50);

            var result = manager.GetValue("test");

            Assert.AreEqual(50, result);
        }
    }
}
