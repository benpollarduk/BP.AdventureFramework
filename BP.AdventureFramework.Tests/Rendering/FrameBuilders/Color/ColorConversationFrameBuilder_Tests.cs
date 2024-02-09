using System;
using System.Linq;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Conversations;
using BP.AdventureFramework.Interpretation;
using BP.AdventureFramework.Rendering.FrameBuilders;
using BP.AdventureFramework.Rendering.FrameBuilders.Color;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Rendering.FrameBuilders.Color
{
    [TestClass]
    public class ColorConversationFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenDefaults_WhenBuild_ThenNoException()
        {
            var gridStringBuilder = new GridStringBuilder();
            var builder = new ColorConversationFrameBuilder(gridStringBuilder);

            builder.Build("Test", null, null, 80, 50);

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void GivenDefaultsWithLog_WhenBuild_ThenFrameReturned()
        {
            var gridStringBuilder = new GridStringBuilder();
            var builder = new ColorConversationFrameBuilder(gridStringBuilder);
            var converser = new NonPlayableCharacter("Test", "Test")
            {
                Conversation = new Conversation(
                [
                    new Paragraph("Line 1"),
                    new Paragraph("Line 2")
                ])
            };

            converser.Conversation.Next(null);
            converser.Conversation.Respond(new Response("Test2"), null);

            var result = builder.Build("Test", converser, null, 80, 50);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GivenDefaultsWith3CustomCommands_WhenBuild_ThenFrameReturned()
        {
            var gridStringBuilder = new GridStringBuilder();
            var builder = new ColorConversationFrameBuilder(gridStringBuilder);
            var commands = new[]
            {
                new CommandHelp("Test", "Test"),
                new CommandHelp("Test", "Test"),
                new CommandHelp("Test", "Test")
            };

            var result = builder.Build("Test", null, commands, 80, 50);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GivenNull_WhenTruncateLog_ThenReturnEmptyArray()
        {
            var gridStringBuilder = new GridStringBuilder();
            var builder = new ColorConversationFrameBuilder(gridStringBuilder);

            var log = builder.TruncateLog(0, 50, 10, null);

            Assert.IsFalse(log.Any());
        }

        [TestMethod]
        public void GivenEmptyLog_WhenTruncateLog_ThenReturnEmptyArray()
        {
            var gridStringBuilder = new GridStringBuilder();
            var builder = new ColorConversationFrameBuilder(gridStringBuilder);

            var log = builder.TruncateLog(0, 50, 10, Array.Empty<LogItem>());

            Assert.IsFalse(log.Any());
        }

        [TestMethod]
        public void GivenLogWith1EntryAnd2Spaces_WhenTruncateLog_ThenReturnArrayWith1Item()
        {
            var gridStringBuilder = new GridStringBuilder();
            var builder = new ColorConversationFrameBuilder(gridStringBuilder);

            var log = builder.TruncateLog(0, 50, 2, [ new LogItem(Participant.Other, "") ]);

            Assert.AreEqual(1, log.Length);
        }

        [TestMethod]
        public void GivenLogWith2EntryAnd1Space_WhenTruncateLog_ThenReturnArrayWith1Item()
        {
            var gridStringBuilder = new GridStringBuilder();
            var builder = new ColorConversationFrameBuilder(gridStringBuilder);

            var log = builder.TruncateLog(0, 50, 1, [ new LogItem(Participant.Other, ""), new LogItem(Participant.Player, "")]);

            Assert.AreEqual(1, log.Length);
        }
    }
}
