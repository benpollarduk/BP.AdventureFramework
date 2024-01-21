using System;
using System.Collections.Generic;
using System.Linq;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Utilities;

namespace BP.AdventureFramework.Assets.Locations
{
    /// <summary>
    /// Represents a room
    /// </summary>
    public sealed class Room : ExaminableObject, IInteractWithItem
    {
        #region Properties

        /// <summary>
        /// Get if this location has been visited.
        /// </summary>
        public bool HasBeenVisited { get; private set; }

        /// <summary>
        /// Get the exits.
        /// </summary>
        public Exit[] Exits { get; private set; }

        /// <summary>
        /// Get all unlocked exits.
        /// </summary>
        public Exit[] UnlockedExits => Exits.Where(x => !x.IsLocked).ToArray();

        /// <summary>
        /// Get the characters in this Room.
        /// </summary>
        public NonPlayableCharacter[] Characters { get; private set; } = Array.Empty<NonPlayableCharacter>();

        /// <summary>
        /// Get the items in this Room.
        /// </summary>
        public Item[] Items { get; private set; }

        /// <summary>
        /// Get or set the interaction.
        /// </summary>
        public InteractionCallback Interaction { get; set; } = (i, target) => new InteractionResult(InteractionEffect.NoEffect, i);

        /// <summary>
        /// Get an exit.
        /// </summary>
        /// <param name="direction">The direction of an exit.</param>
        /// <returns>The exit.</returns>
        public Exit this[Direction direction] => Exits.FirstOrDefault(e => e.Direction == direction);

        /// <summary>
        /// Get which direction this Room was entered from.
        /// </summary>
        public Direction? EnteredFrom { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Room class.
        /// </summary>
        /// <param name="identifier">This rooms identifier.</param>
        /// <param name="description">This rooms description.</param>
        /// <param name="exits">The exits from this room.</param>
        public Room(string identifier, string description, params Exit[] exits) : this(new Identifier(identifier), new Description(description), exits, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Room class.
        /// </summary>
        /// <param name="identifier">This rooms identifier.</param>
        /// <param name="description">This rooms description.</param>
        /// <param name="exits">The exits from this room.</param>
        /// <param name="items">The items in this room.</param>
        public Room(string identifier, string description, Exit[] exits = null, params Item[] items) : this(new Identifier(identifier), new Description(description), exits, items)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Room class.
        /// </summary>
        /// <param name="identifier">This rooms identifier.</param>
        /// <param name="description">This rooms description.</param>
        /// <param name="exits">The exits from this room.</param>
        public Room(Identifier identifier, Description description, params Exit[] exits): this(identifier, description, exits, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Room class.
        /// </summary>
        /// <param name="identifier">This rooms identifier.</param>
        /// <param name="description">This rooms description.</param>
        /// <param name="exits">The exits from this room.</param>
        /// <param name="items">The items in this room.</param>
        public Room(Identifier identifier, Description description, Exit[] exits = null, params Item[] items)
        {
            Identifier = identifier;
            Description = description;
            Exits = exits ?? Array.Empty<Exit>();
            Items = items ?? Array.Empty<Item>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Add a character to this room.
        /// </summary>
        /// <param name="character">The character to add.</param>
        public void AddCharacter(NonPlayableCharacter character)
        {
            Characters = Characters.Add(character);
        }

        /// <summary>
        /// Add an exit to this room.
        /// </summary>
        /// <param name="exit">The exit to add.</param>
        public void AddExit(Exit exit)
        {
            Exits = Exits.Add(exit);
        }

        /// <summary>
        /// Add an item to this room.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public void AddItem(Item item)
        {
            Items = Items.Add(item);
        }

        /// <summary>
        /// Remove an item from the room.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <returns>The item removed from this room.</returns>
        public void RemoveItem(Item item)
        {
            Items = Items.Remove(item);
        }

        /// <summary>
        /// Remove a character from the room.
        /// </summary>
        /// <param name="character">The character to remove.</param>
        public void RemoveCharacter(NonPlayableCharacter character)
        {
            Characters = Characters.Remove(character);
        }

        /// <summary>
        /// Remove an exit from the room.
        /// </summary>
        /// <param name="exit">The exit to remove.</param>
        public void RemoveExit(Exit exit)
        {
            Exits = Exits.Remove(exit);
        }

        /// <summary>
        /// Remove an interaction target from the room.
        /// </summary>
        /// <param name="target">The target to remove.</param>
        /// <returns>The target removed from this room.</returns>
        public IInteractWithItem RemoveInteractionTarget(IInteractWithItem target)
        {
            if (Items.Contains(target))
            {
                RemoveItem(target as Item);
                return target;
            }

            if (!Characters.Contains(target))
                return null;

            RemoveCharacter(target as NonPlayableCharacter);
            return target;
        }

        /// <summary>
        /// Test if a move is possible.
        /// </summary>
        /// <param name="direction">The direction to test.</param>
        /// <returns>If a move in the specified direction is possible.</returns>
        public bool CanMove(Direction direction)
        {
            return UnlockedExits.Any(x => x.Direction == direction);
        }

        /// <summary>
        /// Interact with a specified item.
        /// </summary>
        /// <param name="item">The item to interact with.</param>
        /// <returns>The result of the interaction.</returns>
        private InteractionResult InteractWithItem(Item item)
        {
            return Interaction.Invoke(item, this);
        }

        /// <summary>
        /// Handle examination this Room.
        /// </summary>
        /// <returns>The result of this examination.</returns>
        public override ExaminationResult Examine()
        {
            if (!Items.Where(i => i.IsPlayerVisible).ToArray().Any())
                return new ExaminationResult("There is nothing to examine.");

            if (Items.Where(i => i.IsPlayerVisible).ToArray().Length == 1)
            {
                var singularItem = Items.Where(i => i.IsPlayerVisible).ToArray()[0];
                return new ExaminationResult($"There {(singularItem.Identifier.Name.IsPlural() ? "are" : "is")} {singularItem.Identifier.Name.GetObjectifier()} {singularItem.Identifier}");
            }

            var items = Items?.Cast<IExaminable>().ToArray();
            var sentence = StringUtilities.ConstructExaminablesAsSentence(items);
            var firstItemName = sentence.Substring(0, sentence.Contains(", ") ? sentence.IndexOf(", ", StringComparison.Ordinal) : sentence.IndexOf(" and ", StringComparison.Ordinal));
            return new ExaminationResult($"There {(firstItemName.IsPlural() ? "are" : "is")} {sentence.StartWithLower()}");
        }

        /// <summary>
        /// Get if this room has a visible locked exit in a specified direction.
        /// </summary>
        /// <param name="direction">The direction to check.</param>
        /// <param name="includeInvisibleExits">Specify if invisible exits should be included.</param>
        /// <returns>If there is a locked exit in the specified direction.</returns>
        public bool HasLockedExitInDirection(Direction direction, bool includeInvisibleExits = false)
        {
            return Exits.Any(x => x.Direction == direction && x.IsLocked && (includeInvisibleExits || x.IsPlayerVisible));
        }

        /// <summary>
        /// Get if this room has a visible unlocked exit in a specified direction.
        /// </summary>
        /// <param name="direction">The direction to check.</param>
        /// <param name="includeInvisibleExits">Specify if invisible exits should be included.</param>
        /// <returns>If there is a unlocked exit in the specified direction.</returns>
        public bool HasUnlockedExitInDirection(Direction direction, bool includeInvisibleExits = false)
        {
            return Exits.Any(x => x.Direction == direction && !x.IsLocked && (includeInvisibleExits || x.IsPlayerVisible));
        }

        /// <summary>
        /// Get if this Room contains an exit.
        /// </summary>
        /// <param name="includeInvisibleExits">Specify if invisible exits should be included.</param>
        /// <returns>True if the exit exists, else false.</returns>
        public bool ContainsExit(bool includeInvisibleExits = false)
        {
            return Exits.Any(exit => includeInvisibleExits || exit.IsPlayerVisible);
        }

        /// <summary>
        /// Get if this Room contains an exit.
        /// </summary>
        /// <param name="direction">The direction of the exit to check for.</param>
        /// <param name="includeInvisibleExits">Specify if invisible exits should be included.</param>
        /// <returns>True if the exit exists, else false.</returns>
        public bool ContainsExit(Direction direction, bool includeInvisibleExits = false)
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
        public bool FindExit(Direction direction, bool includeInvisibleExits, out Exit exit)
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
        /// <param name="itemName">The item name to check for.</param>
        /// <param name="includeInvisibleItems">Specify if invisible items should be included.</param>
        /// <returns>True if the item is in this room, else false.</returns>
        public bool ContainsItem(string itemName, bool includeInvisibleItems = false)
        {
            return Items.Any(item => itemName.EqualsExaminable(item) && (includeInvisibleItems || item.IsPlayerVisible));
        }

        /// <summary>
        /// Get if this Room contains an interaction target.
        /// </summary>
        /// <param name="targetName">The name of the target to check for.</param>
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
        /// <param name="itemName">The items name.</param>
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
        /// <param name="targetName">The targets name.</param>
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
        /// <param name="character">The character.</param>
        /// <param name="includeInvisibleCharacters">Specify if invisible characters should be included.</param>
        /// <returns>True if the item is in this room, else false.</returns>
        public bool ContainsCharacter(NonPlayableCharacter character, bool includeInvisibleCharacters = false)
        {
            return Characters.Contains(character) && (includeInvisibleCharacters || character.IsPlayerVisible);
        }

        /// <summary>
        /// Get if this Room contains a character.
        /// </summary>
        /// <param name="characterName">The character name to check for.</param>
        /// <param name="includeInvisibleCharacters">Specify if invisible characters should be included.</param>
        /// <returns>True if the item is in this room, else false.</returns>
        public bool ContainsCharacter(string characterName, bool includeInvisibleCharacters = false)
        {
            return Characters.Any(character => characterName.EqualsExaminable(character) && (includeInvisibleCharacters || character.IsPlayerVisible));
        }

        /// <summary>
        /// Find a character. This will not include characters whose ExaminableObject.IsPlayerVisible property is set to false.
        /// </summary>
        /// <param name="character">The character name.</param>
        /// <param name="characterName">The character.</param>
        /// <returns>True if the character was found.</returns>
        public bool FindCharacter(string characterName, out NonPlayableCharacter character)
        {
            return FindCharacter(characterName, out character, false);
        }

        /// <summary>
        /// Find a character.
        /// </summary>
        /// <param name="characterName">The character name.</param>
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
        /// Handle movement into this GameLocation.
        /// </summary>
        /// <param name="fromDirection">The direction movement into this Room is from. Use null if there is no direction.</param>
        public void MovedInto(Direction? fromDirection)
        {
            EnteredFrom = fromDirection;
            HasBeenVisited = true;
        }

        #endregion

        #region IInteractWithItem Members

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