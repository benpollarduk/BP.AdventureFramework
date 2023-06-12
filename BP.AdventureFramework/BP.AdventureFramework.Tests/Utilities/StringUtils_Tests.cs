using BP.AdventureFramework.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Utilities
{
    [TestClass]
    public class StringUtilities_Tests
    {
        [TestMethod]
        public void GivenOneTwoThree_WhenExtractNextWordFromString_ThenExtractOne()
        {
            var test = "One two three";

            var result = StringUtilities.ExtractNextWordFromString(ref test);

            Assert.AreEqual("One", result);
        }

        [TestMethod]
        public void GivenOneTwoThreeAndExtractTwice_WhenExtractNextWordFromString_ThenExtractTwo()
        {
            var test = "One two three";

            StringUtilities.ExtractNextWordFromString(ref test);
            var result = StringUtilities.ExtractNextWordFromString(ref test);

            Assert.AreEqual("two", result);
        }

        [TestMethod]
        public void GivenOneTwoThreeAndExtractThreeTimes_WhenExtractNextWordFromString_ThenExtractThree()
        {
            var test = "One two three";

            StringUtilities.ExtractNextWordFromString(ref test);
            StringUtilities.ExtractNextWordFromString(ref test);
            var result = StringUtilities.ExtractNextWordFromString(ref test);

            Assert.AreEqual("three", result);
        }

        [TestMethod]
        public void GivenADogSatAndWidth10_WhenCutLineFromParagraph_ThenADogSat()
        {
            var paragraph = "A Dog Sat";

            var result = StringUtilities.CutLineFromParagraph(ref paragraph, 10);

            Assert.AreEqual("A Dog Sat", result);
        }

        [TestMethod]
        public void GivenADogSatAndWidth10_WhenCutLineFromParagraph_ThenParagraphEmpty()
        {
            var paragraph = "A Dog Sat";

            StringUtilities.CutLineFromParagraph(ref paragraph, 10);

            Assert.AreEqual(string.Empty, paragraph);
        }

        [TestMethod]
        public void GivenADogSatOnTheMatAndWidth10_WhenCutLineFromParagraph_ThenADogSat()
        {
            var paragraph = "A Dog Sat On The Mat";

            var result = StringUtilities.CutLineFromParagraph(ref paragraph, 10);

            Assert.AreEqual("A Dog Sat", result);
        }

        [TestMethod]
        public void GivenADogSatAndWidth10_WhenCutLineFromParagraph_ThenParagraphOnTheMat()
        {
            var paragraph = "A Dog Sat On The Mat";

            StringUtilities.CutLineFromParagraph(ref paragraph, 10);

            Assert.AreEqual(" On The Mat", paragraph);
        }

        [TestMethod]
        public void GivenABC_WhenPreenInput_ThenABC()
        {
            var paragraph = "ABC";

            var result = StringUtilities.PreenInput(paragraph);

            Assert.AreEqual("ABC", result);
        }

        [TestMethod]
        public void GivenABCLF_WhenPreenInput_ThenABCLF()
        {
            var paragraph = $"ABC{StringUtilities.LF}";

            var result = StringUtilities.PreenInput(paragraph);

            Assert.AreEqual($"ABC{StringUtilities.LF}", result);
        }

        [TestMethod]
        public void GivenABCCR_WhenPreenInput_ThenABCLF()
        {
            var paragraph = $"ABC{StringUtilities.CR}";

            var result = StringUtilities.PreenInput(paragraph);

            Assert.AreEqual($"ABC{StringUtilities.LF}", result);
        }

        [TestMethod]
        public void GivenABCCRLF_WhenPreenInput_ThenABCLF()
        {
            var paragraph = $"ABC{StringUtilities.CR}{StringUtilities.LF}";

            var result = StringUtilities.PreenInput(paragraph);

            Assert.AreEqual($"ABC{StringUtilities.LF}", result);
        }

        [TestMethod]
        public void GivenABCLFCR_WhenPreenInput_ThenABCLF()
        {
            var paragraph = $"ABC{StringUtilities.LF}{StringUtilities.CR}";

            var result = StringUtilities.PreenInput(paragraph);

            Assert.AreEqual($"ABC{StringUtilities.LF}", result);
        }
    }
}
