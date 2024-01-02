using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Utilities.Templates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BP.AdventureFramework.Tests.Utilities
{
    [TestClass]
    public class PlayableCharacterTemplate_Tests
    {
        private class ExamplePlayableCharacter : PlayableCharacterTemplate<ExamplePlayableCharacter>
        {
            protected override PlayableCharacter OnCreate()
            {
                return new PlayableCharacter("", "");
            }
        }

        [TestMethod]
        public void GivenExample_WhenCreate_ThenNoExceptionThrown()
        {
            var pass = true;

            try
            {
                ExamplePlayableCharacter.Create();
            }
            catch (Exception)
            {
                pass = false;
            }

            Assert.IsTrue(pass);
        }
    }
}
