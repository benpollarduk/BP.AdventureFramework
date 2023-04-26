using System;
using BP.AdventureFramework.Utils.Generation.Simple;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Utils.Generation.Simple
{
    [TestClass]
    public class ItemGenerator_Tests
    {
        [TestMethod]
        public void GivenAllDefaults_WhenGenerate_ThenNotNull()
        {
            var examinableGenerator = new ExaminableGenerator(new[] { "a" }, new[] { "b" }, new DescriptionGenerator(), true);
            var generator = new ItemGenerator(examinableGenerator, true);

            var result = generator.Generate(new Random(1234));

            Assert.IsNotNull(result);
        }
    }
}
