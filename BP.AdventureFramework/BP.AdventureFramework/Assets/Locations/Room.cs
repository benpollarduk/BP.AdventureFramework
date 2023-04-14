using System;
using System.Collections.Generic;
using System.Linq;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Parsing;
using static System.String;

namespace BP.AdventureFramework.Assets.Locations
{
    /// <summary>
    /// Represents a room
    /// </summary>
    public class Room : GameLocation, IInteractWithItem
    {
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
        /// <param name="identifier">This rooms identifier.</param>
        /// <param name="description">This rooms description.</param>
        public Room(Identifier identifier, Description description)
        {
            Identifier = identifier;
            Description = description;
        }

        /// <summary>
        /// Initializes a new instance of the Room class.
        /// </summary>
        /// <param name="identifier">This rooms identifier.</param>
        /// <param name="description">This rooms description.</param>
        /// <param name="exits">The exits from this room.</param>
        public Room(Identifier identifier, Description description, params Exit[] exits) : this(identifier, description)
        {
            Exits.AddRange(exits);
        }

        /// <summary>
        /// Initializes a new instance of the Room class.
        /// </summary>
        /// <param name="identifier">This rooms identifier.</param>
        /// <param name="description">This rooms description.</param>
        /// <param name="exits">The exits from this room.</param>
        /// <param name="items">The items in this room.</param>
        public Room(Identifier identifier, Description description, Exit[] exits, params Item[] items) : this(identifier, description, exits)
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
        /// Remove an item from the room.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <returns>The item removed from this room.</returns>
        public void RemoveItemFromRoom(Item item)
        {
            Items.Remove(item);
        }

        /// <summary>
        /// Remove a character from the room.
        /// </summary>
        /// <param name="characterName">The name of the character to remove.</param>
        public void RemoveCharacterFromRoom(string characterName)
        {
            var matchingCharacters = Characters.Where(characterName.EqualsExaminable).ToArray();

            if (matchingCharacters.Length <= 0)
                return;
            
            var removedCharacter = matchingCharacters[0];
            Characters.Remove(removedCharacter);
        }

        /// <summary>
        /// Remove an interaction target from the room.
        /// </summary>
        /// <param name="target">The target to remove.</param>
        /// <returns>The target removed from this room.</returns>
        public IInteractWithItem RemoveInteractionTargetFromRoom(IInteractWithItem target)
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
        public bool CanMove(CardinalDirection direction)
        {
            return UnlockedExits.Any(x => x.Direction == direction);
        }

        /// <summary>
        /// Interact with a specified item.
        /// </summary>
        /// <param name="item">The item to interact with.</param>
        /// <returns>The result of the interaction.</returns>
        protected InteractionResult InteractWithItem(Item item)
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
                return new ExaminationResult($"There {(singularItem.Identifier.Name.IsPlural() ? "are" : "is")} {singularItem.Identifier.Name.GetObjectifier()} {singularItem.Identifier}");
            }

            var sentence = GetItemsAsString();
            var somethingLeftToCheck = true;
            var index = 0;
            string currentItemName;

            while (somethingLeftToCheck)
            {
                index = sentence.IndexOf(",", index, StringComparison.Ordinal);

                if (index == sentence.LastIndexOf(", ", StringComparison.Ordinal))
                {
                    sentence = sentence.Remove(index, 2);
                    currentItemName = sentence.Substring(index).Trim(Convert.ToChar(" "));
                    sentence = sentence.Insert(index, $" and {currentItemName.GetObjectifier()} ");
                    somethingLeftToCheck = false;
                }
                else
                {
                    sentence = sentence.Remove(index, 2);
                    currentItemName = sentence.Substring(index, sentence.IndexOf(", ", index, StringComparison.Ordinal) - index).Trim(Convert.ToChar(" "));
                    sentence = sentence.Insert(index, $", {currentItemName.GetObjectifier()} ");
                    index += 1;
                }
            }

            currentItemName = sentence.Substring(0, sentence.Contains(", ") ? sentence.IndexOf(", ", StringComparison.Ordinal) : sentence.IndexOf(" and ", StringComparison.Ordinal));
            return new ExaminationResult($"There {(currentItemName.IsPlural() ? "are" : "is")} {currentItemName.GetObjectifier()} {sentence}");
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
                    itemNames.Add(i.Identifier.Name);
            }

            itemNames.Sort();

            foreach (var name in itemNames)
                itemsInRoom += name + ", ";

            itemsInRoom = itemsInRoom.Remove(itemsInRoom.Length - 2);
            return itemsInRoom;
        }

        /// <summary>
        /// Get if this room has a visible locked exit in a specified direction.
        /// </summary>
        /// <param name="direction">The direction to check.</param>
        /// <param name="includeInvisibleExits">Specify if invisible exits should be included.</param>
        /// <returns>If there is a locked exit in the specified direction.</returns>
        public bool HasLockedExitInDirection(CardinalDirection direction, bool includeInvisibleExits = false)
        {
            return Exits.Any(x => x.Direction == direction && x.IsLocked && (includeInvisibleExits || x.IsPlayerVisible));
        }

        /// <summary>
        /// Get if this room has a visible unlocked exit in a specified direction.
        /// </summary>
        /// <param name="direction">The direction to check.</param>
        /// <param name="includeInvisibleExits">Specify if invisible exits should be included.</param>
        /// <returns>If there is a unlocked exit in the specified direction.</returns>
        public bool HasUnlockedExitInDirection(CardinalDirection direction, bool includeInvisibleExits = false)
        {
            return Exits.Any(x => x.Direction == direction && !x.IsLocked && (includeInvisibleExits || x.IsPlayerVisible));
        }

        /// <summary>
        /// Get if this Room contains an exit.
        /// </summary>
        /// <param name="direction">The direction of the exit to check for.</param>
        /// <param name="includeInvisibleExits">Specify if invisible exits should be included.</param>
        /// <returns>True if the exit exists, else false.</returns>
        public bool ContainsExit(CardinalDirection direction, bool includeInvisibleExits = false)
        {
            return Exits.Any(exit => exit.Direction == direction && (includeInvisibleExits || exit.IsPlayerVisible));
        }

        /// <summary>
        /// Find an exit.
        /// </summary>
        /// <param name="direction">The exits direction.</param>
        /// <param name="includeInvisibleExits">Specify if invisible exists should be included.</param>
        /// <param name="exit">The exit.</param>
        /// <returns>True if the exit was found.</returns>
        public bool FindExit(CardinalDirection direction, bool includeInvisibleExits, out Exit exit)
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
        /// Get if this Room contains an item.
        /// </summary>
        /// <param name="itemName">The item name to check for. This is case insensitive.</param>
        /// <param name="includeInvisibleItems">Specify if invisible items should be included.</param>
        /// <returns>True if the item is in this room, else false.</returns>
        public bool ContainsItem(string itemName, bool includeInvisibleItems = false)
        {
            return Items.Any(item => itemName.EqualsExaminable(item) && (includeInvisibleItems || item.IsPlayerVisible));
        }

        /// <summary>
        /// Get if this Room contains an interaction target.
        /// </summary>
        /// <param name="targetName">The name of the target to check for. This is case insensitive.</param>
        /// <returns>True if the target is in this room, else false.</returns>
        public bool ContainsInteractionTarget(string targetName)
        {
            return Items.Any(i => targetName.EqualsExaminable(i) || Characters.Any(targetName.EqualsExaminable));
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
            var items = Items.Where(x => itemName.EqualsExaminable(x) && (includeInvisibleItems || x.IsPlayerVisible)).ToArray();

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
        public bool FindInteractionTarget(string targetName, out IInteractWithItem target)
        {
            var items = Items.Where(targetName.EqualsExaminable).ToArray();
            var nPCS = Characters.Where(targetName.EqualsExaminable).ToArray();
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
        /// Get if this Room contains a character.
        /// </summary>
        /// <param name="characterName">The character name to check for. This is case insensitive.</param>
        /// <param name="includeInvisibleCharacters">Specify if invisible characters should be included.</param>
        /// <returns>True if the item is in this room, else false.</returns>
        public bool ContainsCharacter(string characterName, bool includeInvisibleCharacters = false)
        {
            return Characters.Any(character => characterName.EqualsExaminable(character) && (includeInvisibleCharacters || character.IsPlayerVisible));
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
            var characters = Characters.Where(x => characterName.EqualsExaminable(x) && (includeInvisibleCharacters || x.IsPlayerVisible)).ToArray();

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
        /// Handle movement into this Room.
        /// </summary>
        /// <param name="fromDirection">The direction movement into this Room is from. Use null if there should be no direction.</param>
        public override void MovedInto(CardinalDirection? fromDirection)
        {
            EnteredFrom = fromDirection;
            base.MovedInto(fromDirection);
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