using BP.AdventureFramework.Interaction;
using BP.AdventureFramework.Locations;
using BP.AdventureFramework.Parsing.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Commands
{
    [TestClass]
    public class Examine_Tests
    {
        [TestMethod]
        public void GivenInvoke_WhenNothingToExamine_ThenNoReaction()
        {
            var command = new Examine(null);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.NoReaction, result.Result);
        }

        [TestMethod]
        public void GivenInvoke_WhenSomethingToExamine_ThenReacted()
        {
            var region = new Region(new Identifier(""), new Description(""));
            var command = new Examine(region);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.Reacted, result.Result);
        }
    }
}
