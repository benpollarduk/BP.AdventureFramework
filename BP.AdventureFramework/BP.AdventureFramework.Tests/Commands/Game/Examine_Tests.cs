﻿using BP.AdventureFramework.Interaction;
using BP.AdventureFramework.Locations;
using BP.AdventureFramework.Parsing.Commands.Game;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Commands.Game
{
    [TestClass]
    public class Examine_Tests
    {
        [TestMethod]
        public void GivenNothingToExamine_WhenInvoke_ThenNoReaction()
        {
            var command = new Examine(null);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.NoReaction, result.Result);
        }

        [TestMethod]
        public void GivenSomethingToExamine_WhenInvoke_ThenReacted()
        {
            var region = new Region(new Identifier(""), new Description(""));
            var command = new Examine(region);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.Reacted, result.Result);
        }
    }
}