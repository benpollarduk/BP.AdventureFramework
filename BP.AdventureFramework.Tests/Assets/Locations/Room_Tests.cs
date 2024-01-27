using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Locations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Assets.Locations
{
    [TestClass]
    public class Room_Tests
    {
        [TestMethod]
        public void GivenNotBeenVisited_WhenGetHasBeenVisited_ThenFalse()
        {
            var room = new Room(string.Empty, string.Empty);
            Assert.IsFalse(room.HasBeenVisited);
        }

        [TestMethod]
        public void GivenVisited_WhenGetHasBeenVisited_ThenTrue()
        {
            var room = new Room(string.Empty, string.Empty);
            room.MovedInto(null);

            Assert.IsTrue(room.HasBeenVisited);
        }

        [TestMethod]
        public void GivenVisitedFromNorth_WhenGetHasBeenVisited_ThenEnteredFromIsNorth()
        {
            var room = new Room(string.Empty, string.Empty);
            room.MovedInto(Direction.North);

            Assert.AreEqual(Direction.North, room.EnteredFrom);
        }

        [TestMethod]
        public void Given0Characters_WhenAddCharacter_Then1Character()
        {
            var room = new Room(string.Empty, string.Empty);
            room.AddCharacter(new NonPlayableCharacter(string.Empty, string.Empty));

            Assert.AreEqual(1, room.Characters.Length);
        }

        [TestMethod]
        public void Given1Character_WhenRemoveCharacter_Then0Characters()
        {
            var room = new Room(string.Empty, string.Empty);
            var npc = new NonPlayableCharacter(string.Empty, string.Empty);
            room.AddCharacter(npc);
            room.RemoveCharacter(npc);

            Assert.AreEqual(0, room.Characters.Length);
        }

        [TestMethod]
        public void Given1Character_WhenRemoveDifferentCharacter_Then1Character()
        {
            var room = new Room(string.Empty, string.Empty);
            room.AddCharacter(new NonPlayableCharacter("A", string.Empty));
            room.RemoveCharacter(new NonPlayableCharacter("B", string.Empty));

            Assert.AreEqual(1, room.Characters.Length);
        }

        [TestMethod]
        public void Given0Items_WhenAddItem_Then1Item()
        {
            var room = new Room(string.Empty, string.Empty);
            room.AddItem(new Item(string.Empty, string.Empty));

            Assert.AreEqual(1, room.Items.Length);
        }

        [TestMethod]
        public void Given1Item_WhenRemoveItem_Then0Items()
        {
            var room = new Room(string.Empty, string.Empty);
            var item = new Item(string.Empty, string.Empty);
            room.AddItem(item);
            room.RemoveItem(item);

            Assert.AreEqual(0, room.Items.Length);
        }

        [TestMethod]
        public void Given1Item_WhenRemoveDifferentItem_Then1Item()
        {
            var room = new Room(string.Empty, string.Empty);
            room.AddItem(new Item("A", string.Empty));
            room.RemoveItem(new Item("B", string.Empty));

            Assert.AreEqual(1, room.Items.Length);
        }

        [TestMethod]
        public void GivenDoesntHasItem_WhenContainsItem_ThenFalse()
        {
            var room = new Room(string.Empty, string.Empty);
            var item = new Item("A", string.Empty);

            var result = room.ContainsItem(item);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenHasItem_WhenContainsItem_ThenTrue()
        {
            var room = new Room(string.Empty, string.Empty);
            var item = new Item("A", string.Empty);
            room.AddItem(item);

            var result = room.ContainsItem(item);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenHasItem_WhenContainsItemByName_ThenTrue()
        {
            var room = new Room(string.Empty, string.Empty);
            var item = new Item("A", string.Empty);
            room.AddItem(item);

            var result = room.ContainsItem("A");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenDoesntHaveCharacter_WhenContainsCharacter_ThenFalse()
        {
            var room = new Room(string.Empty, string.Empty);
            var character = new NonPlayableCharacter("A", string.Empty);

            var result = room.ContainsCharacter(character);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenHasCharacter_WhenContainsCharacter_ThenTrue()
        {
            var room = new Room(string.Empty, string.Empty);
            var character = new NonPlayableCharacter("A", string.Empty);
            room.AddCharacter(character);

            var result = room.ContainsCharacter(character);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenHasCharacter_WhenContainsCharacterByName_ThenTrue()
        {
            var room = new Room(string.Empty, string.Empty);
            var character = new NonPlayableCharacter("A", string.Empty);
            room.AddCharacter(character);

            var result = room.ContainsCharacter("A");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenHasExit_WhenRemoveExit_ThenExitRemoved()
        {
            var room = new Room(string.Empty, string.Empty);
            var exit = new Exit(Direction.Down);
            room.AddExit(exit);

            room.RemoveExit(exit);
            var result = room.ContainsExit(exit.Direction);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenValidExit_WhenRemoveInteractionTarget_ThenExitRemoved()
        {
            var room = new Room(string.Empty, string.Empty);
            var exit = new Exit(Direction.Down);
            room.AddExit(exit);

            room.RemoveInteractionTarget(exit);
            var result = room.ContainsExit(exit.Direction);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenValidCharacter_WhenRemoveInteractionTarget_ThenCharacterRemoved()
        {
            var room = new Room(string.Empty, string.Empty);
            var character = new NonPlayableCharacter("", "");
            room.AddCharacter(character);

            room.RemoveInteractionTarget(character);
            var result = room.ContainsCharacter(character);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenValidItem_WhenRemoveInteractionTarget_ThenItemRemoved()
        {
            var room = new Room(string.Empty, string.Empty);
            var item = new Item("", "");
            room.AddItem(item);

            room.RemoveInteractionTarget(item);
            var result = room.ContainsItem(item);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenInvalid_WhenRemoveInteractionTarget_ThenReturnNull()
        {
            var room = new Room(string.Empty, string.Empty);
            var item = new Item("", "");

            var result = room.RemoveInteractionTarget(item);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GivenExitName_WhenFindInteractionTarget_ThenReturnTrue()
        {
            var room = new Room(string.Empty, string.Empty);
            var exit = new Exit(Direction.North, false, new Identifier("North"));
            room.AddExit(exit);

            var result = room.FindInteractionTarget(exit.Identifier.IdentifiableName, out _);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenItemName_WhenFindInteractionTarget_ThenReturnTrue()
        {
            var room = new Room(string.Empty, string.Empty);
            var item = new Item("Sword", string.Empty);
            room.AddItem(item);

            var result = room.FindInteractionTarget(item.Identifier.IdentifiableName, out _);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenCharacterName_WhenFindInteractionTarget_ThenReturnTrue()
        {
            var room = new Room(string.Empty, string.Empty);
            var npc = new NonPlayableCharacter("Ben", string.Empty);
            room.AddCharacter(npc);

            var result = room.FindInteractionTarget(npc.Identifier.IdentifiableName, out _);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenInvalid_WhenFindInteractionTarget_ThenReturnFalse()
        {
            var room = new Room(string.Empty, string.Empty);

            var result = room.FindInteractionTarget("1234", out _);

            Assert.IsFalse(result);
        }
    }
}
