using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Extensions
{
    [TestClass]
    public class CardinalDirectionExtensions_Tests
    {
        [TestMethod]
        public void GivenNorth_WhenInverse_ThenSouth()
        {
            var result = CardinalDirection.North.Inverse();
            
            Assert.AreEqual(CardinalDirection.South, result);
        }

        [TestMethod]
        public void GivenEast_WhenInverse_ThenWest()
        {
            var result = CardinalDirection.East.Inverse();

            Assert.AreEqual(CardinalDirection.West, result);
        }

        [TestMethod]
        public void GivenSouth_WhenInverse_ThenNorth()
        {
            var result = CardinalDirection.South.Inverse();

            Assert.AreEqual(CardinalDirection.North, result);
        }

        [TestMethod]
        public void GivenWest_WhenInverse_ThenEast()
        {
            var result = CardinalDirection.West.Inverse();

            Assert.AreEqual(CardinalDirection.East, result);
        }
    }
}
