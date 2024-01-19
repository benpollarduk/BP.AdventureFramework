using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Commands.Conversation;
using BP.AdventureFramework.Conversations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Commands.Conversation
{
    [TestClass]
    public class Respond_Tests
    {
        [TestMethod]
        public void GivenNullGame_WhenInvoke_ThenError()
        {
            var command = new Respond(null);

            var result = command.Invoke(null);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenNullResponse_WhenInvoke_ThenError()
        {
            var game = AdventureFramework.Logic.Game.Create(string.Empty, string.Empty, string.Empty, null, null, null, null).Invoke();
            var command = new Respond(null);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenNoConverser_WhenInvoke_ThenError()
        {
            var game = AdventureFramework.Logic.Game.Create(string.Empty, string.Empty, string.Empty, null, null, null, null).Invoke();
            var response = new Response("");
            var command = new Respond(response);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenValidGame_WhenInvoke_ThenInternal()
        {
            var game = AdventureFramework.Logic.Game.Create(string.Empty, string.Empty, string.Empty, null, null, null, null).Invoke();
            var response = new Response("");
            var paragraph = new Paragraph(string.Empty) { Responses = new[] { response } };
            var conversation = new AdventureFramework.Conversations.Conversation(paragraph);
            var converser = new NonPlayableCharacter(string.Empty, string.Empty) { Conversation = conversation };
            game.StartConversation(converser);
            var command = new Respond(response);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Internal, result.Result);
        }
    }
}
