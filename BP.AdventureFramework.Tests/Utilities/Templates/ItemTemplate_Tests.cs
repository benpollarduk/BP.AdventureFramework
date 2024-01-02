using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Utilities.Templates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BP.AdventureFramework.Tests.Utilities
{
    [TestClass]
    public class ItemTemplate_Tests
    {
        private class ExampleItem : ItemTemplate<ExampleItem>
        {
            protected override Item OnCreate()
            {
                return new Item("", "");
            }
        }

        [TestMethod]
        public void GivenExample_WhenCreate_ThenNoExceptionThrown()
        {
            var pass = true;

            try
            {
                ExampleItem.Create();
            }
            catch (Exception)
            {
                pass = false;
            }

            Assert.IsTrue(pass);
        }
    }
}
