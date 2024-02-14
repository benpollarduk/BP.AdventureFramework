using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Commands;
using BP.AdventureFramework.Interpretation;
using BP.AdventureFramework.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Interpretation
{
    [TestClass]
    public class CustomCommandInterpreter_Tests
    {
        [TestInitialize]
        public void Setup()
        {
            overworld = new Overworld(Identifier.Empty, Description.Empty);
            var region = new Region(Identifier.Empty, Description.Empty);
            region.AddRoom(new Room(Identifier.Empty, Description.Empty, new Exit(Direction.North)), 0, 0, 0);
            region.AddRoom(new Room(Identifier.Empty, Description.Empty, new Exit(Direction.South)), 0, 1, 0);
            overworld.AddRegion(region);
        }

        private Overworld overworld;

        [TestMethod]
        public void GivenNoCustomCommands_WhenGetContextualCommands_ThenReturnEmptyArray()
        {
            var interpreter = new CustomCommandInterpreter();
            var game = Game.Create(string.Empty, string.Empty, string.Empty, () => overworld, () => new PlayableCharacter(Identifier.Empty, Description.Empty), _ => EndCheckResult.NotEnded, _ => EndCheckResult.NotEnded).Invoke();

            var result = interpreter.GetContextualCommandHelp(game);

            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void GivenNoCustomCommands_WhenGetContextualCommands_ThenReturn1CommandHelp()
        {
            var interpreter = new CustomCommandInterpreter();
            overworld.Commands = [new CustomCommand(new CommandHelp("Test", string.Empty), true, (_, _) => new Reaction(ReactionResult.Error, string.Empty))];
            var game = Game.Create(string.Empty, string.Empty, string.Empty, () => overworld, () => new PlayableCharacter(Identifier.Empty, Description.Empty), _ => EndCheckResult.NotEnded, _ => EndCheckResult.NotEnded).Invoke();

            var result = interpreter.GetContextualCommandHelp(game);

            Assert.AreEqual(1, result.Length);
        }

        [TestMethod]
        public void GivenValidCustomCommand_WhenInterpret_ThenCommandInvoked()
        {
            var interpreter = new CustomCommandInterpreter();
            overworld.Commands =
            [
                new CustomCommand(new CommandHelp("Test", string.Empty), true, (_, _) =>
            {
                Assert.IsTrue(true);
                return new Reaction(ReactionResult.Error, string.Empty);

            })
            ];
            var game = Game.Create(string.Empty, string.Empty, string.Empty, () => overworld, () => new PlayableCharacter(Identifier.Empty, Description.Empty), _ => EndCheckResult.NotEnded, _ => EndCheckResult.NotEnded).Invoke();

            interpreter.Interpret("Test", game);
        }
    }
}
