using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Utilities.Templates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BP.AdventureFramework.Tests.Utilities
{
    [TestClass]
    public class RoomTemplate_Tests
    {
        private class ExampleRoom : RoomTemplate<ExampleRoom>
        {
            protected override Room OnCreate()
            {
                return new Room("", "");
            }
        }

        [TestMethod]
        public void GivenExample_WhenCreate_ThenNoExceptionThrown()
        {
            var pass = true;

            try
            {
                ExampleRoom.Create();
            }
            catch (Exception)
            {
                pass = false;
            }

            Assert.IsTrue(pass);
        }
    }
}
