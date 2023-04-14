using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Logic;
using BP.AdventureFramework.Interpretation;
using BP.AdventureFramework.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Parsing
{
    [TestClass]
    public class GlobalCommandInterpreter_Tests
    {
        [TestInitialize]
        public void Setup()
        {
            overworld = new Overworld(Identifier.Empty, Description.Empty);
            var region = new Region(Identifier.Empty, Description.Empty);
            region.AddRoom(new Room(Identifier.Empty, Description.Empty, new Assets.Locations.Exit(CardinalDirection.North)), 0, 0);
            region.AddRoom(new Room(Identifier.Empty, Description.Empty, new Assets.Locations.Exit(CardinalDirection.South)), 0, 1);
            overworld.Regions.Add(region);
        }

        private Overworld overworld;

        [TestMethod]
        public void GivenEmptyString_WhenInterpret_ThenReturnFalse()
        {
            var interpreter = new GlobalCommandInterpreter(new MapDrawer());
            var game = new Game("", "", new PlayableCharacter(Identifier.Empty, Description.Empty), overworld);

            var result = interpreter.Interpret(string.Empty, game);

            Assert.IsFalse(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenNew_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new GlobalCommandInterpreter(new MapDrawer());
            var game = new Game("", "", new PlayableCharacter(Identifier.Empty, Description.Empty), overworld);

            var result = interpreter.Interpret(GlobalCommandInterpreter.New, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenAbout_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new GlobalCommandInterpreter(new MapDrawer());
            var game = new Game("", "", new PlayableCharacter(Identifier.Empty, Description.Empty), overworld);

            var result = interpreter.Interpret(GlobalCommandInterpreter.About, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenExit_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new GlobalCommandInterpreter(new MapDrawer());
            var game = new Game("", "", new PlayableCharacter(Identifier.Empty, Description.Empty), overworld);

            var result = interpreter.Interpret(GlobalCommandInterpreter.Exit, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenHelp_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new GlobalCommandInterpreter(new MapDrawer());
            var game = new Game("", "", new PlayableCharacter(Identifier.Empty, Description.Empty), overworld);

            var result = interpreter.Interpret(GlobalCommandInterpreter.Help, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenMap_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new GlobalCommandInterpreter(new MapDrawer());
            var game = new Game("", "", new PlayableCharacter(Identifier.Empty, Description.Empty), overworld);

            var result = interpreter.Interpret(GlobalCommandInterpreter.Map, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }
    }
}
