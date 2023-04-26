using System;
using BP.AdventureFramework.Utils.Generation.Simple;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Utils.Generation.Simple
{
    [TestClass]
    public class ExaminableGenerator_Tests
    {
        [TestMethod]
        public void GivenASingleElement_WhenGenerate_ThenReturnMatchingExaminable()
        {
            var generator = new ExaminableGenerator(new[] { "Dog" }, new[] { "Brown" }, new DescriptionGenerator(), false);

            var result = generator.Generate(new Random(1234));

            Assert.AreEqual("Brown Dog", result.Identifier.Name);
        }

        [TestMethod]
        public void GivenASingleElementAndNoReuse_WhenGenerate_ThenSecondElementIsNull()
        {
            var generator = new ExaminableGenerator(new[] { "Dog" }, new[] { "Brown" }, new DescriptionGenerator(), false);
            var random = new Random(1234);

            generator.Generate(random);
            var result = generator.Generate(random);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GivenASingleElementAndReuse_WhenGenerate_ThenSecondElementIsNotNull()
        {
            var generator = new ExaminableGenerator(new[] { "Brown" }, new[] { "Dog" }, new DescriptionGenerator(), true);
            var random = new Random(1234);

            generator.Generate(random);
            var result = generator.Generate(random);

            Assert.IsNotNull(result);
        }
    }
}
