﻿using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Extensions
{
    [TestClass]
    public class StringExtensions_Tests
    {
        [TestMethod]
        public void GivenEmptyString_WhenIsVowel_ThenFalse()
        {
            var result = "".IsVowel();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Given123_WhenIsVowel_ThenFalse()
        {
            var result = "123".IsVowel();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenAAA_WhenIsVowel_ThenFalse()
        {
            var result = "AAA".IsVowel();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenZ_WhenIsVowel_ThenFalse()
        {
            var result = "Z".IsVowel();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenA_WhenIsVowel_ThenTrue()
        {
            var result = "A".IsVowel();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenE_WhenIsVowel_ThenTrue()
        {
            var result = "E".IsVowel();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenI_WhenIsVowel_ThenTrue()
        {
            var result = "I".IsVowel();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenO_WhenIsVowel_ThenTrue()
        {
            var result = "O".IsVowel();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenU_WhenIsVowel_ThenTrue()
        {
            var result = "U".IsVowel();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenEmptyString_WhenEnsureFinishedSentence_ThenEmptyString()
        {
            var result = "".EnsureFinishedSentence();

            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void GivenA_WhenEnsureFinishedSentence_ThenAStop()
        {
            var result = "A".EnsureFinishedSentence();

            Assert.AreEqual("A.", result);
        }

        [TestMethod]
        public void GivenAStop_WhenEnsureFinishedSentence_ThenAStop()
        {
            var result = "A.".EnsureFinishedSentence();

            Assert.AreEqual("A.", result);
        }

        [TestMethod]
        public void GivenAQuestion_WhenEnsureFinishedSentence_ThenAQuestion()
        {
            var result = "A?".EnsureFinishedSentence();

            Assert.AreEqual("A?", result);
        }

        [TestMethod]
        public void GivenAExclamation_WhenEnsureFinishedSentence_ThenAExclamation()
        {
            var result = "A!".EnsureFinishedSentence();

            Assert.AreEqual("A!", result);
        }

        [TestMethod]
        public void GivenAComma_WhenEnsureFinishedSentence_ThenAStop()
        {
            var result = "A,".EnsureFinishedSentence();

            Assert.AreEqual("A.", result);
        }

        [TestMethod]
        public void GivenEmptyString_WhenIsPlural_ThenFalse()
        {
            var result = "".IsPlural();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenA_WhenIsPlural_ThenFalse()
        {
            var result = "A".IsPlural();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenFoxes_WhenIsPlural_ThenTrue()
        {
            var result = "Foxes".IsPlural();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenEmptyString_WhenGetObjectifier_ThenEmptyString()
        {
            var result = "".GetObjectifier();

            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void GivenTub_WhenGetObjectifier_ThenA()
        {
            var result = "Tub".GetObjectifier();

            Assert.AreEqual("a", result);
        }

        [TestMethod]
        public void GivenElephant_WhenGetObjectifier_ThenAn()
        {
            var result = "Elephant".GetObjectifier();

            Assert.AreEqual("an", result);
        }

        [TestMethod]
        public void GivenRedOlives_WhenGetObjectifier_ThenSome()
        {
            var result = "Red Olives".GetObjectifier();

            Assert.AreEqual("some", result);
        }

        [TestMethod]
        public void GivenStringIsEqualToIdentifier_WhenEqualsIdentifier_ThenTrue()
        {
            var result = "A".EqualsIdentifier("A".ToIdentifier());

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenStringIsNotEqualToIdentifier_WhenEqualsIdentifier_ThenFalse()
        {
            var result = "A".EqualsIdentifier("B".ToIdentifier());

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenStringIsEqualToExaminable_WhenEqualsExaminable_ThenTrue()
        {
            var result = "A".EqualsExaminable(new Item("A".ToIdentifier(), Description.Empty));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenStringIsNotEqualToDescription_WhenEqualsDescription_ThenFalse()
        {
            var result = "A".EqualsExaminable(new Item("B".ToIdentifier(), Description.Empty));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenAString_WhenToIdentifier_ThenNotNull()
        {
            var result = "A".ToIdentifier();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GivenAString_WhenToDescription_ThenNotNull()
        {
            var result = "A".ToDescription();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GivenEmpty_WhenToSentenceCase_ThenEmpty()
        {
            var result = string.Empty.ToSentenceCase();

            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void GivenLowerCaseA_WhenToSentenceCase_ThenA()
        {
            var result = "a".ToSentenceCase();

            Assert.AreEqual("A", result);
        }

        [TestMethod]
        public void GivenAn_WhenToSentenceCase_ThenAn()
        {
            var result = "An".ToSentenceCase();

            Assert.AreEqual("An", result);
        }

        [TestMethod]
        public void GivenAN_WhenToSentenceCase_ThenAN()
        {
            var result = "AN".ToSentenceCase();

            Assert.AreEqual("AN", result);
        }

        [TestMethod]
        public void GivenNoNewline_WhenLineCount_Then1()
        {
            var result = "AN".LineCount();

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void GivenOneNewline_WhenLineCount_Then2()
        {
            var result = $"AN{StringUtilities.Newline}AN".LineCount();

            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void Given2Newline_WhenLineCount_Then3()
        {
            var result = $"AN{StringUtilities.Newline}AN{StringUtilities.Newline}AN".LineCount();

            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void GivenEmptyString_WhenToSpeech_ThenReturn2SpeechMarks()
        {
            var value = string.Empty;

            var result = value.ToSpeech();

            Assert.AreEqual("\"\"", result);
        }

        [TestMethod]
        public void GivenABC_WhenToSpeech_ThenReturnSpeechMarkABCSpeechMark()
        {
            var value = "ABC";

            var result = value.ToSpeech();

            Assert.AreEqual("\"ABC\"", result);
        }

        [TestMethod]

        public void GivenSentenceEndingInFullStop_WhenRemoveSentenceEnd_ThenReturnStringMinusFullStop()
        {
            var value = "ABC.";

            var result = value.RemoveSentenceEnd();

            Assert.AreEqual("ABC", result);
        }

        [TestMethod]
        public void GivenSentenceEndingInExclamationMark_WhenRemoveSentenceEnd_ThenReturnStringMinusExclamationMark()
        {
            var value = "ABC!";

            var result = value.RemoveSentenceEnd();

            Assert.AreEqual("ABC", result);
        }

        [TestMethod]
        public void GivenSentenceEndingInQuestionMark_WhenRemoveSentenceEnd_ThenReturnStringMinusQuestionMark()
        {
            var value = "ABC?";

            var result = value.RemoveSentenceEnd();

            Assert.AreEqual("ABC", result);
        }

        [TestMethod]
        public void GivenABC_WhenRemoveSentenceEnd_ThenABC()
        {
            var value = "ABC";

            var result = value.RemoveSentenceEnd();

            Assert.AreEqual("ABC", result);
        }

        [TestMethod]
        public void GivenABCSeekingb_WhenCaseInsensitiveContains_ThenTrue()
        {
            var value = "ABC";

            var result = value.CaseInsensitiveContains("B");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenAbCSeekingD_WhenCaseInsensitiveContains_ThenFalse()
        {
            var value = "ABC";

            var result = value.CaseInsensitiveContains("D");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenCompareABCToABC_WhenInsensitiveEquals_ThenReturnTrue()
        {
            var result = "ABC".InsensitiveEquals("ABC");
            
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenCompareABCToabc_WhenInsensitiveEquals_ThenReturnTrue()
        {
            var result = "ABC".InsensitiveEquals("abc");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenValueIsEmptyString_WhenInsensitiveEquals_ThenReturnOther()
        {
            const string value = "";
            const string other = "abc";
            var result = value.AddSentence(other);

            Assert.AreEqual(other, result);
        }

        [TestMethod]
        public void GivenOtherIsEmptyString_WhenInsensitiveEquals_ThenReturnValue()
        {
            const string value = "abc";
            const string other = "";
            var result = value.AddSentence(other);

            Assert.AreEqual(value, result);
        }

        [TestMethod]
        public void GivenAbcAnd123_WhenInsensitiveEquals_ThenReturnAbcSpace123()
        {
            const string value = "abc";
            const string other = "123";
            var result = value.AddSentence(other);

            Assert.AreEqual($"{value} {other}", result);
        }

        [TestMethod]
        public void GivenEmptyString_WhenInsensitiveEquals_ThenReturnEmptyString()
        {
            var value = string.Empty;
            var result = value.StartWithLower();

            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void GivenA_WhenInsensitiveEquals_ThenReturna()
        {
            var value = "A";
            var result = value.StartWithLower();

            Assert.AreEqual("a", result);
        }

        [TestMethod]
        public void Givena_WhenInsensitiveEquals_ThenReturna()
        {
            var value = "a";
            var result = value.StartWithLower();

            Assert.AreEqual("a", result);
        }

        [TestMethod]
        public void GivenAA_WhenInsensitiveEquals_ThenReturnaA()
        {
            var value = "AA";
            var result = value.StartWithLower();

            Assert.AreEqual("aA", result);
        }
    }
}
