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
            var i = new Item("Test", "Test Description.");
            i.Commands =
            [
                new CustomCommand(new CommandHelp("Test Command", "Test Command Descritpion."), true, (_, _) =>
                {
                    return new Reaction(ReactionResult.OK, "");
                })
            ];

            var result = i.Examine();

            Assert.IsTrue(result.Description.Contains(i.Commands[0].Help.Command));
        }

        [TestMethod]
        public void GivenExamine_WhenDefaultExaminationWith2CustomCommands_ThenCustomCommandsAreBothIncuded()
        {
            var i = new Item("Test", "Test Description.");
            i.Commands =
            [
                new CustomCommand(new CommandHelp("A*", "Test Command Descritpion."), true, (_, _) =>
                {
                    return new Reaction(ReactionResult.OK, "");
                }),
                new CustomCommand(new CommandHelp("B*", "Test Command Descritpion."), true, (_, _) =>
                {
                    return new Reaction(ReactionResult.OK, "");
                })
            ];

            var result = i.Examine();

            Assert.IsTrue(result.Description.Contains(i.Commands[0].Help.Command));
            Assert.IsTrue(result.Description.Contains(i.Commands[1].Help.Command));
        }
    }
}
