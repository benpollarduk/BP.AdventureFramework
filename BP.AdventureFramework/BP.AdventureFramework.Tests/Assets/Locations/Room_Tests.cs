using BP.AdventureFramework.Assets.Locations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Assets.Locations
{
    [TestClass]
    public class Room_Tests
    {
        [TestMethod]
        public void GivenNotBeenVisited_WhenGetHasBeenVisited_ThenFalse()
        {
            var room = new Room(string.Empty, string.Empty);
            
            Assert.IsFalse(room.HasBeenVisited);
        }

        [TestMethod]
        public void GivenVisited_WhenGetHasBeenVisited_ThenTrue()
        {
            var room = new Room(string.Empty, string.Empty);

            room.MovedInto(null);

            Assert.IsTrue(room.HasBeenVisited);
        }
    }
}
