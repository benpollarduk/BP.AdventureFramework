using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Conversations;
using BP.AdventureFramework.Conversations.Instructions;
using BP.AdventureFramework.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Conversations
{
    [TestClass]
    public class Conversation_Tests
    {
        [TestMethod]
        public void GivenConverserWithAConversationWithOneParagraph_WhenConstructed_ThenCurrentParagraphIsFirstParagraph()
        {
            var game = AdventureFramework.Logic.Game.Create(string.Empty, string.Empty, string.Empty, null, null, null, null).Invoke();
            var npc = new NonPlayableCharacter(string.Empty, string.Empty) { Conversation = new Conversation(new Paragraph(string.Empty)) };
            game.StartConversation(npc);

            var result = npc.Conversation.CurrentParagraph;

            Assert.AreEqual(npc.Conversation.Paragraphs[0], result);
        }

        [TestMethod]
        public void GivenNoParagraphs_WhenNext_ThenNoException()
        {
            var conversation = new Conversation();

            conversation.Next(null);

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void GivenNoParagraphs_WhenNext_ThenCurrentParagraphNull()
        {
            var conversation = new Conversation();

            conversation.Next(null);

            Assert.IsNull(conversation.CurrentParagraph);
        }

        [TestMethod]
        public void GivenOneParagraph_WhenNext_ThenCurrentParagraphNotNull()
        {
            var conversation = new Conversation(new Paragraph(string.Empty));

            conversation.Next(null);

            Assert.IsNotNull(conversation.CurrentParagraph);
        }

        [TestMethod]
        public void GivenConverserWithAConversation_WhenNext_ThenResultIsInternal()
        {
            var game = AdventureFramework.Logic.Game.Create(string.Empty, string.Empty, string.Empty, null, null, null, null).Invoke();
            var npc = new NonPlayableCharacter(string.Empty, string.Empty) { Conversation = new Conversation(new Paragraph(string.Empty)) };
            game.StartConversation(npc);

            var result = npc.Conversation.Next(game);

            Assert.AreEqual(ReactionResult.Internal, result.Result);
        }

        [TestMethod]
        public void GivenConverserWithAConversationWithOneParagraph_WhenNext_ThenCurrentParagraphIsUnchanged()
        {
            var game = AdventureFramework.Logic.Game.Create(string.Empty, string.Empty, string.Empty, null, null, null, null).Invoke();
            var npc = new NonPlayableCharacter(string.Empty, string.Empty) { Conversation = new Conversation(new Paragraph(string.Empty)) };
            game.StartConversation(npc);

            var startParagraph = npc.Conversation.CurrentParagraph;
            npc.Conversation.Next(game);
            var result = npc.Conversation.CurrentParagraph;

            Assert.AreEqual(startParagraph, result);
        }

        [TestMethod]
        public void GivenConverserWithAConversationWithTwoParagraphs_WhenNext_ThenCurrentParagraphIsSecondParagraph()
        {
            var game = AdventureFramework.Logic.Game.Create(string.Empty, string.Empty, string.Empty, null, null, null, null).Invoke();
            var npc = new NonPlayableCharacter(string.Empty, string.Empty) { Conversation = new Conversation(new Paragraph(string.Empty), new Paragraph(string.Empty)) };
            game.StartConversation(npc);

            npc.Conversation.Next(game);
            var result = npc.Conversation.CurrentParagraph;

            Assert.AreEqual(npc.Conversation.Paragraphs[1], result);
        }

        [TestMethod]
        public void GivenNull_WhenRespond_ThenReactionIsError()
        {
            var game = AdventureFramework.Logic.Game.Create(string.Empty, string.Empty, string.Empty, null, null, null, null).Invoke();
            var npc = new NonPlayableCharacter(string.Empty, string.Empty) { Conversation = new Conversation(new Paragraph(string.Empty)) };
            game.StartConversation(npc);

            var result = npc.Conversation.Respond(null, game);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenCurrentParagraphWithNoResponses_WhenRespond_ThenReactionIsError()
        {
            var game = AdventureFramework.Logic.Game.Create(string.Empty, string.Empty, string.Empty, null, null, null, null).Invoke();
            var npc = new NonPlayableCharacter(string.Empty, string.Empty) { Conversation = new Conversation(new Paragraph(string.Empty)) };
            game.StartConversation(npc);

            var result = npc.Conversation.Respond(new Response(string.Empty), game);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenCurrentParagraphWithResponse_WhenRespond_ThenReactionIsInternal()
        {
            var game = AdventureFramework.Logic.Game.Create(string.Empty, string.Empty, string.Empty, null, null, null, null).Invoke();
            var response = new Response(string.Empty, new Repeat());
            var paragraph = new Paragraph(string.Empty, new Repeat()) {  Responses = [response] };
            var npc = new NonPlayableCharacter(string.Empty, string.Empty) { Conversation = new Conversation(paragraph) };
            game.StartConversation(npc);

            var result = npc.Conversation.Respond(response, game);



            const string currency = "$";

            var player = new PlayableCharacter("Player", string.Empty);
            player.Attributes.Add(currency, 10);

            var trader = new NonPlayableCharacter("Trader", string.Empty);
            var spade = new Item("Spade", string.Empty);
            trader.AcquireItem(spade);

            trader.Conversation = new Conversation(
                new Paragraph("What will you buy?")
                {
                    Responses =
                    [
                        new Response("Spade", new ByCallback(() =>
                            player.Attributes.GetValue(currency) >= 5
                                ? new ToName("BoughtSpade")
                                : new ToName("NotEnough"))),
                        new Response("Nothing", new Last())
                    ]
                },
                new Paragraph("Here it is.", _ =>
                {
                    player.Attributes.Subtract(currency, 5);
                    trader.Attributes.Add(currency, 5);
                    trader.Give(spade, player);
                }, new GoTo(0), "BoughtSpade"),
                new Paragraph("You don't have enough money.", "NotEnough"),
                new Paragraph("Fine.")
            );








            Assert.AreEqual(ReactionResult.Internal, result.Result);
        }
    }
}
