using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Interpretation;
using BP.AdventureFramework.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Interpretation
{
    [TestClass]
    public class FrameCommandInterpreter_Tests
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
        public void GivenEmptyString_WhenInterpret_ThenReturnFalse()
        {
            var interpreter = new FrameCommandInterpreter();
            var game = Game.Create("", "", "", x => overworld, () => new PlayableCharacter(Identifier.Empty, Description.Empty), null).Invoke();

            var result = interpreter.Interpret(string.Empty, game);

            Assert.IsFalse(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenKeyOff_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new FrameCommandInterpreter();
            var game = Game.Create("", "", "", x => overworld, () => new PlayableCharacter(Identifier.Empty, Description.Empty), null).Invoke();

            var result = interpreter.Interpret(FrameCommandInterpreter.KeyOff, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenKeyOn_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new FrameCommandInterpreter();
            var game = Game.Create("", "", "", x => overworld, () => new PlayableCharacter(Identifier.Empty, Description.Empty), null).Invoke();

            var result = interpreter.Interpret(FrameCommandInterpreter.KeyOn, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenCommandsOff_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new FrameCommandInterpreter();
            var game = Game.Create("", "", "", x => overworld, () => new PlayableCharacter(Identifier.Empty, Description.Empty), null).Invoke();

            var result = interpreter.Interpret(FrameCommandInterpreter.CommandsOff, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenCommandsOn_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new FrameCommandInterpreter();
            var game = Game.Create("", "", "", x => overworld, () => new PlayableCharacter(Identifier.Empty, Description.Empty), null).Invoke();

            var result = interpreter.Interpret(FrameCommandInterpreter.CommandsOn, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }
    }
}
