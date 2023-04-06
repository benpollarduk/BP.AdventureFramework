using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using BP.AdventureFramework.Characters;
using BP.AdventureFramework.Interaction;
using BP.AdventureFramework.Parsing;
using static System.String;

namespace BP.AdventureFramework.Locations
{
    /// <summary>
    /// Represents a room
    /// </summary>
    public class Room : GameLocation, IInteractWithItem, IImplementOwnActions
    {
        #region Fields

        #endregion

        #region Properties

        /// <summary>
        /// Get the exits.
        /// </summary>
        public List<Exit> Exits { get; protected set; } = new List<Exit>();

        /// <summary>
        /// Get all unlocked exits.
        /// </summary>
        public Exit[] UnlockedExits => Exits.Where(x => !x.IsLocked).ToArray();

        /// <summary>
        /// Get the characters of this Room
        /// </summary>
        public List<NonPlayableCharacter> Characters { get; protected set; } = new List<NonPlayableCharacter>();

        /// <summary>
        /// Get the items in this Room.
        /// </summary>
        public List<Item> Items { get; protected set; } = new List<Item>();

        /// <summary>
        /// Get or set the interaction.
        /// </summary>
        public InteractionCallback Interaction { get; set; } = (i, target) => new InteractionResult(InteractionEffect.NoEffect, i);

        /// <summary>
        /// Get an exit.
        /// </summary>
        /// <param name="direction">The direction of an exit.</param>
        /// <returns>The exit.</returns>
        public Exit this[CardinalDirection direction] => Exits.FirstOrDefault(e => e.Direction == direction);

        /// <summary>
        /// Get which direction this Room was entered from.
        /// </summary>
        public CardinalDirection? EnteredFrom { get; protected set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Room class.
        /// </summary>
        /// <param name="name">This rooms name.</param>
        /// <param name="description">This rooms description.</param>
        public Room(string name, Description description)
        {
            Name = name;
            Description = description;
        }

        /// <summary>
        /// Initializes a new instance of the Room class.
        /// </summary>
        /// <param name="name">This rooms name.</param>
        /// <param name="description">This rooms description.</param>
        /// <param name="exits">The exits from this room.</param>
        public Room(string name, Description description, params Exit[] exits) : this(name, description)
        {
            Exits.AddRange(exits);
        }

        /// <summary>
        /// Initializes a new instance of the Room class.
        /// </summary>
        /// <param name="name">This rooms name.</param>
        /// <param name="description">This rooms description.</param>
        /// <param name="exits">The exits from this room.</param>
        /// <param name="items">The items in this room.</param>
        public Room(string name, Description description, Exit[] exits, params Item[] items) : this(name, description, exits)
        {
            Items.AddRange(items);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Add a character to this room.
        /// </summary>
        /// <param name="character">The character to add.</param>
        public void AddCharacter(NonPlayableCharacter character)
        {
           Characters.Add(character);
        }

        /// <summary>
        /// Add an item to this room.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public void AddItem(Item item)
        {
            Items.Add(item);
        }

        /// <summary>
        /// Add an exit to this room.
        /// </summary>
        /// <param name="exit">The exit to add.</param>
        public void AddExit(Exit exit)
        {
            Exits.Add(exit);
        }

        /// <summary>
        /// Remove an item from the room.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <returns>The item removed from this room.</returns>
        public Item RemoveItemFromRoom(Item item)
        {
            Items.Remove(item);
            return item;
        }

        /// <summary>
        /// Remove an item from the room.
        /// </summary>
        /// <param name="itemName">The name of the item to remove.</param>
        /// <returns>If the item was removed correctly.</returns>
        public Decision RemoveItemFromRoom(string itemName)
        {
            return RemoveItemFromRoom(itemName, out _);
        }

        /// <summary>
        /// Remove an item from the room.
        /// </summary>
        /// <param name="itemName">The name of the item to remove.</param>
        /// <param name="removedItem">The item removed from this room.</param>
        /// <returns>If the item was removed correctly.</returns>
        public Decision RemoveItemFromRoom(string itemName, out Item removedItem)
        {
            var matchingItems = Items.Where(x => String.Equals(x.Name, itemName, StringComparison.CurrentCultureIgnoreCase)).ToArray();

            if (matchingItems.Length <= 0)
            {
                removedItem = null;
                return new Decision(ReactionToInput.CouldntReact, "That is not an item");
            }

            if (!matchingItems[0].IsPlayerVisible)
            {
                removedItem = null;
                return new Decision(ReactionToInput.CouldntReact, "That is not an item");
            }

            if (!matchingItems[0].IsTakeable)
            {
                removedItem = null;
                return new Decision(ReactionToInput.CouldntReact, matchingItems[0].Name + " is not takeable");
            }

            removedItem = matchingItems[0];
            Items.Remove(removedItem);
            return new Decision(ReactionToInput.CouldReact, "Took " + removedItem.Name);
        }

        /// <summary>
        /// Remove a character from the room.
        /// </summary>
        /// <param name="character">The character to remove.</param>
        /// <returns>The character removed from this room.</returns>
        public Character RemoveCharacterFromRoom(NonPlayableCharacter character)
        {
            Characters.Remove(character);
            return character;
        }

        /// <summary>
        /// Remove a character from the room.
        /// </summary>
        /// <param name="characterName">The name of the character to remove.</param>
        /// <returns>If the character was removed correctly.</returns>
        public Decision RemoveCharacterFromRoom(string characterName)
        {
            return RemoveCharacterFromRoom(characterName, out _);
        }

        /// <summary>
        /// Remove a character from the room.
        /// </summary>
        /// <param name="characterName">The name of the character to remove.</param>
        /// <param name="removedCharacter">The character removed from this room.</param>
        /// <returns>If the character was removed correctly.</returns>
        public Decision RemoveCharacterFromRoom(string characterName, out NonPlayableCharacter removedCharacter)
        {
            var matchingCharacters = Characters.Where(x => String.Equals(x.Name, characterName, StringComparison.CurrentCultureIgnoreCase)).ToArray();

            if (matchingCharacters.Length > 0)
            {
                removedCharacter = matchingCharacters[0];
                Characters.Remove(removedCharacter);
                return new Decision(ReactionToInput.CouldReact, "Removed " + removedCharacter.Name);
            }

            removedCharacter = null;
            return new Decision(ReactionToInput.CouldntReact, "That is not an character");
        }

        /// <summary>
        /// Remove an interaction target from the room.
        /// </summary>
        /// <param name="target">The target to remove.</param>
        /// <returns>The target removed from this room.</returns>
        public virtual IInteractWithItem RemoveInteractionTargetFromRoom(IInteractWithItem target)
        {
            if (Items.Contains(target))
            {
                Items.Remove(target as Item);
                return target;
            }

            if (!Characters.Contains(target))
                return null;

            Characters.Remove(target as NonPlayableCharacter);
            return target;
        }

        /// <summary>
        /// Test if a move is possible.
        /// </summary>
        /// <param name="direction">The direction to test.</param>
        /// <returns>If a move in the specified direction is possible.</returns>
        public virtual bool CanMove(CardinalDirection direction)
        {
            return UnlockedExits.Any(x => x.Direction == direction);
        }

        /// <summary>
        /// Interact with a specified item.
        /// </summary>
        /// <param name="item">The item to interact with.</param>
        /// <returns>The result of the interaction.</returns>
        protected virtual InteractionResult InteractWithItem(Item item)
        {
            return Interaction.Invoke(item, this);
        }

        /// <summary>
        /// Handle examination this Room.
        /// </summary>
        /// <returns>The result of this examination.</returns>
        public override ExaminationResult Examime()
        {
            if (!Items.Where(i => i.IsPlayerVisible).ToArray().Any())
                return new ExaminationResult("There is nothing to examine");

            if (Items.Where(i => i.IsPlayerVisible).ToArray().Length == 1)
            {
                var singularItem = Items.Where(i => i.IsPlayerVisible).ToArray()[0];
                return new ExaminationResult($"There {(TextParser.IsPlural(singularItem.Name) ? "are" : "is")} {TextParser.GetObjectifier(singularItem.Name)} {singularItem.Name}");
            }

            var sentance = GetItemsAsString();
            var somethingLeftToCheck = true;
            var index = 0;
            string currentItemName;

            while (somethingLeftToCheck)
            {
                index = sentance.IndexOf(",", index, StringComparison.Ordinal);

                if (index == sentance.LastIndexOf(", ", StringComparison.Ordinal))
                {
                    sentance = sentance.Remove(index, 2);
                    currentItemName = sentance.Substring(index).Trim(Convert.ToChar(" "));
                    sentance = sentance.Insert(index, $" and {TextParser.GetObjectifier(currentItemName)} ");
                    somethingLeftToCheck = false;
                }
                else
                {
                    sentance = sentance.Remove(index, 2);
                    currentItemName = sentance.Substring(index, sentance.IndexOf(", ", index, StringComparison.Ordinal) - index).Trim(Convert.ToChar(" "));
                    sentance = sentance.Insert(index, $", {TextParser.GetObjectifier(currentItemName)} ");
                    index += 1;
                }
            }

            currentItemName = sentance.Substring(0, sentance.Contains(", ") ? sentance.IndexOf(", ", StringComparison.Ordinal) : sentance.IndexOf(" and ", StringComparison.Ordinal));
            return new ExaminationResult($"There {(TextParser.IsPlural(currentItemName) ? "are" : "is")} {TextParser.GetObjectifier(currentItemName)} {sentance}");
        }

        /// <summary>
        /// Get all Items as a string.
        /// </summary>
        /// <returns>A string representing all items as a string.</returns>
        private string GetItemsAsString()
        {
            if (!Items.Any()) 
                return Empty;

            var itemsInRoom = Empty;
            var itemNames = new List<string>();

            foreach (var i in Items)
            {
                if (i.IsPlayerVisible)
                    itemNames.Add(i.Name);
            }

            itemNames.Sort();

            foreach (var name in itemNames)
                itemsInRoom += name + ", ";

            itemsInRoom = itemsInRoom.Remove(itemsInRoom.Length - 2);
            return itemsInRoom;
        }

        /// <summary>
        /// Get everything that can be examined within this room.
        /// </summary>
        /// <returns>An array of everything that can be examined in this room.</returns>
        public virtual IExaminable[] GetExaminableObjects()
        {
            var examinable = new List<IExaminable>();
            examinable.AddRange(Characters.Where<Character>(x => x.IsPlayerVisible).ToArray());
            examinable.AddRange(Items.Where(x => x.IsPlayerVisible).ToArray());
            return examinable.ToArray<IExaminable>();
        }

        /// <summary>
        /// Get if this room has a visible locked exit in a specified direction. This will not include exits whose ExaminableObject.IsPlayerVisible property is set to false.
        /// </summary>
        /// <param name="direction">The direction to check.</param>
        /// <returns>If there is a locked exit in the specified direction.</returns>
        public bool HasLockedExitInDirection(CardinalDirection direction)
        {
            return HasLockedExitInDirection(direction, false);
        }

        /// <summary>
        /// Get if this room has a visible locked exit in a specified direction.
        /// </summary>
        /// <param name="direction">The direction to check.</param>
        /// <param name="includeInvisibleExits">Specify if invisible exits should be included.</param>
        /// <returns>If there is a locked exit in the specified direction.</returns>
        public bool HasLockedExitInDirection(CardinalDirection direction, bool includeInvisibleExits)
        {
            return Exits.Any(x => x.Direction == direction && x.IsLocked && (includeInvisibleExits || x.IsPlayerVisible));
        }

        /// <summary>
        /// Get if this room has a visible unlocked exit in a specified direction. This will not include exits whose ExaminableObject.IsPlayerVisible property is set to false.
        /// </summary>
        /// <param name="direction">The direction to check.</param>
        /// <returns>If there is a unlocked exit in the specified direction.</returns>
        public bool HasUnlockedExitInDirection(CardinalDirection direction)
        {
            return HasUnlockedExitInDirection(direction, false);
        }

        /// <summary>
        /// Get if this room has a visible unlocked exit in a specified direction.
        /// </summary>
        /// <param name="direction">The direction to check.</param>
        /// <param name="includeInvisibleExits">Specify if invisible exits should be included.</param>
        /// <returns>If there is a unlocked exit in the specified direction.</returns>
        public bool HasUnlockedExitInDirection(CardinalDirection direction, bool includeInvisibleExits)
        {
            return Exits.Any(x => x.Direction == direction && !x.IsLocked && (includeInvisibleExits || x.IsPlayerVisible));
        }

        /// <summary>
        /// Get if this Room contains an exit.
        /// </summary>
        /// <param name="exit">The exit to check for.</param>
        /// <param name="includeInvisibleExits">Specify if invisible exits should be included.</param>
        /// <returns>True if the exit exists, else false.</returns>
        public bool ContainsExit(Exit exit, bool includeInvisibleExits)
        {
            return Exits.Contains(exit) && (includeInvisibleExits || exit.IsPlayerVisible);
        }

        /// <summary>
        /// Get if this Room contains an exit. This will not include exits whose ExaminableObject.IsPlayerVisible property is set to false.
        /// </summary>
        /// <param name="direction">The direction of the exit to check for.</param>
        /// <returns>True if the exit exists, else false.</returns>
        public bool ContainsExit(CardinalDirection direction)
        {
            return ContainsExit(direction, false);
        }

        /// <summary>
        /// Get if this Room contains an exit.
        /// </summary>
        /// <param name="direction">The direction of the exit to check for.</param>
        /// <param name="includeInvisibleExits">Specify if invisible exits should be included.</param>
        /// <returns>True if the exit exists, else false.</returns>
        public bool ContainsExit(CardinalDirection direction, bool includeInvisibleExits)
        {
            return Exits.Any(exit => exit.Direction == direction && (includeInvisibleExits || exit.IsPlayerVisible));
        }

        /// <summary>
        /// Find an exit. This will not include exits whose ExaminableObject.IsPlayerVisible property is set to false.
        /// </summary>
        /// <param name="direction">The exits direction.</param>
        /// <param name="exit">The exit.</param>
        /// <returns>True if the exit was found.</returns>
        public bool FindExit(CardinalDirection direction, out Exit exit)
        {
            return FindExit(direction, out exit, false);
        }

        /// <summary>
        /// Find an exit.
        /// </summary>
        /// <param name="direction">The exits direction.</param>
        /// <param name="exit">The exit.</param>
        /// <param name="includeInvisibleExits">Specify if invisible exists should be included.</param>
        /// <returns>True if the exit was found.</returns>
        public bool FindExit(CardinalDirection direction, out Exit exit, bool includeInvisibleExits)
        {
            var exits = Exits.Where(x => x.Direction == direction && (includeInvisibleExits || x.IsPlayerVisible)).ToArray();

            if (exits.Length > 0)
            {
                exit = exits[0];
                return true;
            }

            exit = null;
            return false;
        }

        /// <summary>
        /// Get if this Room contains an item. This will not include items whose ExaminableObject.IsPlayerVisible property is set to false.
        /// </summary>
        /// <param name="item">The item to check for.</param>
        /// <returns>True if the item is in this room, else false.</returns>
        public bool ContainsItem(Item item)
        {
            return Items.Contains(item);
        }

        /// <summary>
        /// Get if this Room contains an item. This will not include items whose ExaminableObject.IsPlayerVisible property is set to false.
        /// </summary>
        /// <param name="itemName">The item name to check for. This is case insensitive.</param>
        /// <returns>True if the item is in this room, else false.</returns>
        public bool ContainsItem(string itemName)
        {
            return ContainsItem(itemName, false);
        }

        /// <summary>
        /// Get if this Room contains an item.
        /// </summary>
        /// <param name="itemName">The item name to check for. This is case insensitive.</param>
        /// <param name="includeInvisibleItems">Specify if invisible items should be included.</param>
        /// <returns>True if the item is in this room, else false.</returns>
        public bool ContainsItem(string itemName, bool includeInvisibleItems)
        {
            return Items.Any(item => item.Name.ToUpper() == itemName.ToUpper() && (includeInvisibleItems || item.IsPlayerVisible));
        }

        /// <summary>
        /// Get if this Room contains an interaction target.
        /// </summary>
        /// <param name="targetName">The name of the target to check for. This is case insensitive.</param>
        /// <returns>True if the target is in this room, else false.</returns>
        public virtual bool ContainsInteractionTarget(string targetName)
        {
            return Items.Any(i => String.Equals(i.Name, targetName, StringComparison.CurrentCultureIgnoreCase)) || Characters.Any(nPC => String.Equals(nPC.Name, targetName, StringComparison.CurrentCultureIgnoreCase));
        }

        /// <summary>
        /// Find an item. This will not include items whose ExaminableObject.IsPlayerVisible property is set to false
        /// </summary>
        /// <param name="itemName">The items name. This is case insensitive</param>
        /// <param name="item">The item</param>
        /// <returns>True if the item was found</returns>
        public bool FindItem(string itemName, out Item item)
        {
            return FindItem(itemName, out item, false);
        }

        /// <summary>
        /// Find an item.
        /// </summary>
        /// <param name="itemName">The items name. This is case insensitive.</param>
        /// <param name="item">The item.</param>
        /// <param name="includeInvisibleItems">Specify is invisible items should be included.</param>
        /// <returns>True if the item was found.</returns>
        public bool FindItem(string itemName, out Item item, bool includeInvisibleItems)
        {
            var items = Items.Where(x => String.Equals(x.Name, itemName, StringComparison.CurrentCultureIgnoreCase) && (includeInvisibleItems || x.IsPlayerVisible)).ToArray();

            if (items.Length > 0)
            {
                item = items[0];
                return true;
            }

            item = null;
            return false;
        }

        /// <summary>
        /// Find an item.
        /// </summary>
        /// <param name="itemID">The items ID.</param>
        /// <param name="item">The item.</param>
        /// <param name="includeInvisibleItems">Specify if invisible items should be included.</param>
        /// <returns>True if the item was found.</returns>
        internal bool FindItemByID(string itemID, out Item item, bool includeInvisibleItems)
        {
            var items = Items.Where(x => x.ID == itemID && (includeInvisibleItems || x.IsPlayerVisible)).ToArray();

            if (items.Length > 0)
            {
                item = items[0];
                return true;
            }

            item = null;
            return false;
        }

        /// <summary>
        /// Find an interaction target.
        /// </summary>
        /// <param name="targetName">The targets name. This is case insensitive.</param>
        /// <param name="target">The target.</param>
        /// <returns>True if the target was found.</returns>
        public virtual bool FindInteractionTarget(string targetName, out IInteractWithItem target)
        {
            var items = Items.Where(x => String.Equals(x.Name, targetName, StringComparison.CurrentCultureIgnoreCase)).ToArray();
            var nPCS = Characters.Where(n => String.Equals(n.Name, targetName, StringComparison.CurrentCultureIgnoreCase)).ToArray();
            var interactions = new List<IInteractWithItem>(items);
            interactions.AddRange(nPCS);

            if (interactions.Count > 0)
            {
                target = interactions[0];
                return true;
            }

            target = null;
            return false;
        }

        /// <summary>
        /// Get if this Room contains a character. This will not include characters whose ExaminableObject.IsPlayerVisible property is set to false.
        /// </summary>
        /// <param name="character">The item name to check for. This is case insensitive.</param>
        /// <returns>True if the item is in this room, else false.</returns>
        public bool ContainsCharacter(string character)
        {
            return ContainsCharacter(character, false);
        }

        /// <summary>
        /// Get if this Room contains a character.
        /// </summary>
        /// <param name="characterName">The character name to check for. This is case insensitive.</param>
        /// <param name="includeInvisibleCharacters">Specify if invisible characters should be included.</param>
        /// <returns>True if the item is in this room, else false.</returns>
        public bool ContainsCharacter(string characterName, bool includeInvisibleCharacters)
        {
            return Characters.Any(character => String.Equals(character.Name, characterName, StringComparison.CurrentCultureIgnoreCase) && (includeInvisibleCharacters || character.IsPlayerVisible));
        }

        /// <summary>
        /// Find a character. This will not include characters whose ExaminableObject.IsPlayerVisible property is set to false.
        /// </summary>
        /// <param name="character">The character name. This is case insensitive.</param>
        /// <param name="characterName">The character.</param>
        /// <returns>True if the character was found.</returns>
        public bool FindCharacter(string characterName, out NonPlayableCharacter character)
        {
            return FindCharacter(characterName, out character, false);
        }

        /// <summary>
        /// Find a character.
        /// </summary>
        /// <param name="characterName">The character name. This is case insensitive.</param>
        /// <param name="character">The character.</param>
        /// <param name="includeInvisibleCharacters">Specify if invisible characters should be included.</param>
        /// <returns>True if the character was found.</returns>
        public bool FindCharacter(string characterName, out NonPlayableCharacter character, bool includeInvisibleCharacters)
        {
            var characters = Characters.Where(x => String.Equals(x.Name, characterName, StringComparison.CurrentCultureIgnoreCase) && (includeInvisibleCharacters || x.IsPlayerVisible)).ToArray();

            if (characters.Length > 0)
            {
                character = characters[0];
                return true;
            }

            character = null;
            return false;
        }

        /// <summary>
        /// Specify a conditional description of this room.
        /// </summary>
        /// <param name="description">The description of this room.</param>
        public void SpecifyConditionalDescription(ConditionalDescription description)
        {
            Description = description;
        }

        /// <summary>
        /// Get all IImplementOwnActions objects within this Room.
        /// </summary>
        /// <returns>An array of all IImplementOwnActions objects within this Room.</returns>
        public virtual IImplementOwnActions[] GetAllObjectsWithAdditionalCommands()
        {
            var customCommands = new List<IImplementOwnActions>();
            customCommands.AddRange(Items.Where(i => i.IsPlayerVisible).ToArray());
            customCommands.AddRange(Characters.Where<Character>(c => c.IsPlayerVisible).ToArray());
            customCommands.Add(this);
            return customCommands.ToArray<IImplementOwnActions>();
        }

        /// <summary>
        /// Handle movement into this Room.
        /// </summary>
        /// <param name="fromDirection">The direction movement into this Room is from. Use null if there should be no direction.</param>
        public override void MovedInto(CardinalDirection? fromDirection)
        {
            EnteredFrom = fromDirection;
            base.MovedInto(fromDirection);
        }

        /// <summary>
        /// Handle transferal of delegation to this Room from a source ITransferableDelegation object. This should only concern top level properties and fields.
        /// </summary>
        /// <param name="source">The source ITransferableDelegation object to transfer from.</param>
        public override void TransferFrom(ITransferableDelegation source)
        {
            Interaction = ((Room)source).Interaction;
        }

        /// <summary>
        /// Handle registration of all child properties of this Room that are ITransferableDelegation.
        /// </summary>
        /// <param name="children">A list containing all the ITransferableDelegation properties of this Room.</param>
        public override void RegisterTransferableChildren(ref List<ITransferableDelegation> children)
        {
            foreach (var i in Items)
            {
                children.Add(i);
                i.RegisterTransferableChildren(ref children);
            }

            foreach (var c in Characters)
            {
                children.Add(c);
                c.RegisterTransferableChildren(ref children);
            }

            foreach (var aC in AdditionalCommands)
            {
                children.Add(aC);
                aC.RegisterTransferableChildren(ref children);
            }

            base.RegisterTransferableChildren(ref children);
        }

        #endregion

        #region IImplementOwnActions Members

        /// <summary>
        /// Get or set the ActionableCommands this object can interact with.
        /// </summary>
        public List<ActionableCommand> AdditionalCommands { get; set; } = new List<ActionableCommand>();

        /// <summary>
        /// React to an ActionableCommand.
        /// </summary>
        /// <param name="command">The command to react to.</param>
        /// <returns>The result of the interaction.</returns>
        public virtual InteractionResult ReactToAction(ActionableCommand command)
        {
            if (AdditionalCommands.Contains(command))
                return command.Action.Invoke();

            throw new ArgumentException($"Command {command.Command} was not found on object {Name}");
        }

        /// <summary>
        /// Find a command by it's name.
        /// </summary>
        /// <param name="command">The name of the command to find.</param>
        /// <returns>The ActionableCommand (if it is found).</returns>
        public ActionableCommand FindCommand(string command)
        {
            return AdditionalCommands.FirstOrDefault(c => string.Equals(c.Command, command, StringComparison.CurrentCultureIgnoreCase));
        }

        #endregion

        #region IInteractWithInGameItem Members

        /// <summary>
        /// Interact with an item.
        /// </summary>
        /// <param name="item">The item to interact with.</param>
        /// <returns>The result of the interaction.</returns>
        public InteractionResult Interact(Item item)
        {
            return InteractWithItem(item);
        }

        #endregion

    }
}