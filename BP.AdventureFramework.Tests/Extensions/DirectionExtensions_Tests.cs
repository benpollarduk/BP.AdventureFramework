using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Extensions
{
    [TestClass]
    public class DirectionExtensions_Tests
    {
        [TestMethod]
        public void GivenNorth_WhenInverse_ThenSouth()
        {
            var result = Direction.North.Inverse();
            
            Assert.AreEqual(Direction.South, result);
        }

        [TestMethod]
        public void GivenEast_WhenInverse_ThenWest()
        {
            var result = Direction.East.Inverse();

            Assert.AreEqual(Direction.West, result);
        }

        [TestMethod]
        public void GivenSouth_WhenInverse_ThenNorth()
        {
            var result = Direction.South.Inverse();

            Assert.AreEqual(Direction.North, result);
        }

        [TestMethod]
        public void GivenWest_WhenInverse_ThenEast()
        {
            var result = Direction.West.Inverse();

            Assert.AreEqual(Direction.East, result);
        }

        [TestMethod]
        public void GivenDown_WhenInverse_ThenUp()
        {
            var result = Direction.Down.Inverse();

            Assert.AreEqual(Direction.Up, result);
        }

        [TestMethod]
        public void GivenUp_WhenInverse_ThenDown()
        {
            var result = Direction.Up.Inverse();

            Assert.AreEqual(Direction.Down, result);
        }
    }
}
