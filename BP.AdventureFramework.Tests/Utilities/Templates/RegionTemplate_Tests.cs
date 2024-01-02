using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Utilities.Templates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BP.AdventureFramework.Tests.Utilities
{
    [TestClass]
    public class RegionTemplate_Tests
    {
        private class ExampleRegion : RegionTemplate<ExampleRegion>
        {
            protected override Region OnCreate()
            {
                return new Region("", "");
            }
        }

        [TestMethod]
        public void GivenExample_WhenCreate_ThenNoExceptionThrown()
        {
            var pass = true;

            try
            {
                ExampleRegion.Create();
            }
            catch (Exception)
            {
                pass = false;
            }

            Assert.IsTrue(pass);
        }
    }
}
