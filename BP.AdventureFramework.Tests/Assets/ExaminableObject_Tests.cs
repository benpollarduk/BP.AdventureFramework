using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Commands;
using BP.AdventureFramework.Interpretation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Assets
{
    [TestClass]
    public class ExaminableObject_Tests
    {
        [TestMethod]
        public void GivenExamine_WhenDefaultExamination_ThenExaminableDescriptionIsIncluded()
        {
            var i = new Item("Test", "Test Description.");

            var result = i.Examine();

            Assert.IsTrue(result.Description.Contains(i.Description.GetDescription()));
        }

        [TestMethod]
        public void GivenExamine_WhenDefaultExaminationWith1CustomCommand_ThenCustomCommandIsIncuded()
        {
            var i = new Item("Test", "Test Description.")
            {
                Commands =
                [
                    new CustomCommand(new CommandHelp("Test Command", "Test Command Descritpion."), true, (_, _) => new Reaction(ReactionResult.OK, ""))
                ]
            };

            var result = i.Examine();

            Assert.IsTrue(result.Description.Contains(i.Commands[0].Help.Command));
        }

        [TestMethod]
        public void GivenExamine_WhenDefaultExaminationWith2CustomCommands_ThenCustomCommandsAreBothIncuded()
        {
            var i = new Item("Test", "Test Description.")
            {
                Commands =
                [
                    new CustomCommand(new CommandHelp("A*", "Test Command Descritpion."), true, (_, _) => new Reaction(ReactionResult.OK, "")),
                    new CustomCommand(new CommandHelp("B*", "Test Command Descritpion."), true, (_, _) => new Reaction(ReactionResult.OK, ""))
                ]
            };

            var result = i.Examine();

            Assert.IsTrue(result.Description.Contains(i.Commands[0].Help.Command));
            Assert.IsTrue(result.Description.Contains(i.Commands[1].Help.Command));
        }

        [TestMethod]
        public void GivenExamine_WhenNoDescription_ThenResultIncludesIdentifierName()
        {
            var i = new Item("Test", string.Empty);

            var result = i.Examine();

            Assert.IsTrue(result.Description.Contains("Test"));
        }

        [TestMethod]
        public void GivenExamine_WhenNoDescriptionOrIdentifierName_ThenResultIncludesClassName()
        {
            var i = new Item(string.Empty, string.Empty);

            var result = i.Examine();

            Assert.IsTrue(result.Description.Contains("Item"));
        }

        [TestMethod]
        public void GivenExamine_WhenSomeAttributes_ThenResultIncludesAttributeName()
        {
            var i = new Item("Test", string.Empty);
            i.Attributes.Add("Attribute", 1);

            var result = i.Examine();

            Assert.IsTrue(result.Description.Contains("Attribute"));
        }
    }
}
