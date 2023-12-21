using BP.AdventureFramework.Assets.Locations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Assets.Locations
{
    [TestClass]
    public class Overworld_Tests
    {
        [TestMethod]
        public void GivenNoRegions_WhenFindName_ThenFalse()
        {
            var overworld = new Overworld(string.Empty, string.Empty);

            var result = overworld.FindRegion("abc", out _);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenRegionNotPresent_WhenFindName_ThenFalse()
        {
            var overworld = new Overworld(string.Empty, string.Empty);
            overworld.AddRegion(new Region(string.Empty, string.Empty));

            var result = overworld.FindRegion("abc", out _);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenRegionPresent_WhenFindName_ThenTrue()
        {
            var overworld = new Overworld(string.Empty, string.Empty);
            overworld.AddRegion(new Region("abc", string.Empty));

            var result = overworld.FindRegion("abc", out _);

            Assert.IsTrue(result);
        }
    }
}
