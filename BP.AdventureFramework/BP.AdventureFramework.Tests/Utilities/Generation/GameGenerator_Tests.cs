using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Utilities.Generation;
using BP.AdventureFramework.Utilities.Generation.Themes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Utilities.Generation
{
    [TestClass]
    public class GameGenerator_Tests
    {
        [TestMethod]
        public void GivenDefaultOptions_WhenGenerate_ThenNotNull()
        {
            var options = new GameGenerationOptions();
            var maker = new GameGenerator(Identifier.Empty, Description.Empty); 

            var result = maker.Generate(1234, options, new All());

            Assert.IsNotNull(result);
        }
    }
}
