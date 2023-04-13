﻿using BP.AdventureFramework.Interaction;
using BP.AdventureFramework.Locations;
using BP.AdventureFramework.Parsing.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Commands
{
    [TestClass]
    public class Move_Tests
    {
        [TestMethod]
        public void GivenInvoke_WhenCantMove_ThenNoReaction()
        {
            var region = new Region(new Identifier(""), new Description(""));
            region.AddRoom(new Room(new Identifier(""), new Description(""), new Exit(CardinalDirection.North)), 0, 0);
            region.AddRoom(new Room(new Identifier(""), new Description(""), new Exit(CardinalDirection.South)), 0, 1);
            var command = new Move(region, CardinalDirection.East);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.NoReaction, result.Result);
        }

        [TestMethod]
        public void GivenInvoke_WhenCanMove_ThenReacted()
        {
            var region = new Region(new Identifier(""), new Description(""));
            region.AddRoom(new Room(new Identifier(""), new Description(""), new Exit(CardinalDirection.North)), 0, 0);
            region.AddRoom(new Room(new Identifier(""), new Description(""), new Exit(CardinalDirection.South)), 0, 1);
            var command = new Move(region, CardinalDirection.North);

            var result = command.Invoke();

            Assert.AreEqual(ReactionResult.Reacted, result.Result);
        }
    }
}