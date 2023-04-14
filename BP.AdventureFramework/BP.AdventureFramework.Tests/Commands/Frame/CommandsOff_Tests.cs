﻿using BP.AdventureFramework.Interaction;
using BP.AdventureFramework.Parsing.Commands.Frame;
using BP.AdventureFramework.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Commands.Frame
{
    [TestClass]
    public class CommandsOff_Tests
    {
        [TestMethod]
        public void GivenNullFrameDrawer_WhenInvoke_ThenNoReaction()
        {
            var command = new CommandsOff(null);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.NoReaction, result.Result);
        }

        [TestMethod]
        public void GivenValidFrameDrawer_WhenInvoke_ThenReacted()
        {
            var command = new CommandsOff(new FrameDrawer());

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.Reacted, result.Result);
        }
    }
}
