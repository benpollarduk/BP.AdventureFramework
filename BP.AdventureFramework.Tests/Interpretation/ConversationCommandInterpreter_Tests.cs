using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Commands;
using BP.AdventureFramework.Conversations;
using BP.AdventureFramework.Interpretation;
using BP.AdventureFramework.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Interpretation
{
    [TestClass]
    public class ConversationCommandInterpreter_Tests
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
        public void GivenNoActiveConverser_WhenGetContextualCommands_ThenReturnEmptyArray()
        {
            var interpreter = new ConversationCommandInterpreter();
            var game = Game.Create(string.Empty, string.Empty, string.Empty, x => overworld, () => new PlayableCharacter(Identifier.Empty, Description.Empty), null).Invoke();

            var result = interpreter.GetContextualCommandHelp(game);

            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void GivenActiveConverserNoResponses_WhenGetContextualCommands_ThenReturnArrayWith1Element()
        {
            var interpreter = new ConversationCommandInterpreter();
            var game = Game.Create(string.Empty, string.Empty, string.Empty, x => overworld, () => new PlayableCharacter(Identifier.Empty, Description.Empty), null).Invoke();
            var npc = new NonPlayableCharacter(string.Empty, string.Empty);
            npc.Conversation = new Conversation(new Paragraph("Test"));

            game.StartConversation(npc);

            var result = interpreter.GetContextualCommandHelp(game);

            Assert.AreEqual(1, result.Length);
        }

        [TestMethod]
        public void GivenActiveConverser1CustomCommand_WhenGetContextualCommands_ThenReturnArrayWith2Elements()
        {
            var interpreter = new ConversationCommandInterpreter();
            var game = Game.Create(string.Empty, string.Empty, string.Empty, x => overworld, () => new PlayableCharacter(Identifier.Empty, Description.Empty), null).Invoke();
            var npc = new NonPlayableCharacter(string.Empty, string.Empty);
            npc.Conversation = new Conversation(
                new Paragraph("Test")
                {
                    Responses = new[]
                    {
                        new Response("First")
                    }
                }
            );

            game.StartConversation(npc);

            var result = interpreter.GetContextualCommandHelp(game);

            Assert.AreEqual(2, result.Length);
        }

        [TestMethod]
        public void GivenNoActiveConverser_WhenInterpret_ThenWasInterpretedSuccessfullyIsFalse()
        {
            var interpreter = new ConversationCommandInterpreter();
            var game = Game.Create(string.Empty, string.Empty, string.Empty, x => overworld, () => new PlayableCharacter(Identifier.Empty, Description.Empty), null).Invoke();

            var result = interpreter.Interpret(string.Empty, game);

            Assert.IsFalse(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenNoActiveConverserAndEnd_WhenInterpret_ThenWasInterpretedSuccessfullyIsTrue()
        {
            var interpreter = new ConversationCommandInterpreter();
            var game = Game.Create(string.Empty, string.Empty, string.Empty, x => overworld, () => new PlayableCharacter(Identifier.Empty, Description.Empty), null).Invoke();
            var npc = new NonPlayableCharacter(string.Empty, string.Empty);
            npc.Conversation = new Conversation(new Paragraph("Test"));

            game.StartConversation(npc);

            var result = interpreter.Interpret("End", game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenNoActiveConverserAndEmpty_WhenInterpret_ThenWasInterpretedSuccessfullyIsTrue()
        {
            var interpreter = new ConversationCommandInterpreter();
            var game = Game.Create(string.Empty, string.Empty, string.Empty, x => overworld, () => new PlayableCharacter(Identifier.Empty, Description.Empty), null).Invoke();
            var npc = new NonPlayableCharacter(string.Empty, string.Empty);
            npc.Conversation = new Conversation(new Paragraph("Test"));

            game.StartConversation(npc);

            var result = interpreter.Interpret("", game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }
    }
}
