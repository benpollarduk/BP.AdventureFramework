using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Utils.Generation.Simple;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Utils.Generation.Simple
{
    [TestClass]
    public class DescriptionGenerator_Tests
    {
        [TestMethod]
        public void GivenRustySword_WhenGenerate_ThenARustySword()
        {
            var generator = new DescriptionGenerator();

            var result = generator.Generate(new Identifier("Rusty Sword"));

            Assert.AreEqual("A rusty sword.", result.GetDescription());
        }

        [TestMethod]
        public void GivenOrangeSun_WhenGenerate_ThenAnOrangeSun()
        {
            var generator = new DescriptionGenerator();

            var result = generator.Generate(new Identifier("Orange Sun"));

            Assert.AreEqual("An orange sun.", result.GetDescription());
        }
    }
}
