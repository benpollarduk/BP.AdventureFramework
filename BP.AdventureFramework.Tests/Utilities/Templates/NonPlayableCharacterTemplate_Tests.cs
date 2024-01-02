using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Utilities.Templates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BP.AdventureFramework.Tests.Utilities
{
    [TestClass]
    public class NonPlayableCharacterTemplate_Tests
    {
        private class ExampleNonPlayableCharacter : NonPlayableCharacterTemplate<ExampleNonPlayableCharacter>
        {
            protected override NonPlayableCharacter OnCreate()
            {
                return new NonPlayableCharacter("", "");
            }
        }

        [TestMethod]
        public void GivenExample_WhenCreate_ThenNoExceptionThrown()
        {
            var pass = true;

            try
            {
                ExampleNonPlayableCharacter.Create();
            }
            catch (Exception)
            {
                pass = false;
            }

            Assert.IsTrue(pass);
        }
    }
}
