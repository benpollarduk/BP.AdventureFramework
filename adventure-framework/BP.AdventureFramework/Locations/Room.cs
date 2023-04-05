using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using BP.AdventureFramework.Interaction;

namespace AdventureFramework.Locations
{
    /// <summary>
    /// Represents a room
    /// </summary>
    public class Room : GameLocation, IInteractWithItem, IImplementOwnActions
    {
        #region IInteractWithInGameItem Members

        /// <summary>
        /// Interact with an item
        /// </summary>
        /// <param name="item">The item to interact with</param>
        /// <returns>The result of the interaction</returns>
        public InteractionResult Interact(Item item)
        {
            return InteractWithItem(item);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Get the exits
        /// </summary>
        public Exit[] Exits
        {
            get { return exits.ToArray<Exit>(); }
            protected set { exits = new List<Exit>(value); }
        }

        /// <summary>
        /// Get o set the exits
        /// </summary>
        protected List<Exit> exits = new List<Exit>();

        /// <summary>
        /// Get all unlocked exits
        /// </summary>
        public Exit[] UnlockedExits
        {
            get { return exits.Where(x => !x.IsLocked).ToArray(); }
        }

        /// <summary>
        /// Get the characters of this Room
        /// </summary>
        public NonPlayableCharacter[] Characters
        {
            get { return characters.ToArray<NonPlayableCharacter>(); }
            protected set { characters = new List<NonPlayableCharacter>(value); }
        }

        /// <summary>
        /// Get or set the characters of this Room
        /// </summary>
        protected List<NonPlayableCharacter> characters = new List<NonPlayableCharacter>();

        /// <summary>
        /// Get the items in this Room
        /// </summary>
        public Item[] Items
        {
            get { return items.ToArray<Item>(); }
            protected set { items = new List<Item>(value); }
        }

        /// <summary>
        /// Get or set the items in this Room
        /// </summary>
        protected List<Item> items = new List<Item>();

        /// <summary>
        /// Get or set the interaction
        /// </summary>
        public InteractionCallback Interaction
        {
            get { return interaction; }
            set { interaction = value; }
        }

        /// <summary>
        /// Get or set the interaction
        /// </summary>
        private InteractionCallback interaction = (i, target) =>
        {
            // return default
            return new InteractionResult(InteractionEffect.NoEffect, i);
        };

        /// <summary>
        /// Get an exit
        /// </summary>
        /// <param name="direction">The direction of an exit</param>
        /// <returns>The exit</returns>
        public Exit this[ECardinalDirection direction]
        {
            get
            {
                // itterate all exits
                foreach (var e in Exits)
                    // if directions match
                    if (e.Direction == direction)
                        // return matching exit
                        return e;

                // no such exit
                return null;
            }
        }

        /// <summary>
        /// Get or set the additional actionable commands
        /// </summary>
        private List<ActionableCommand> additionalCommands = new List<ActionableCommand>();

        /// <summary>
        /// Get which direction this Room was entered from
        /// </summary>
        public ECardinalDirection? EnteredFrom
        {
            get { return enteredFrom; }
            protected set { enteredFrom = value; }
        }

        /// <summary>
        /// Get or set which direction this Room was entered from
        /// </summary>
        private ECardinalDirection? enteredFrom;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the Room class
        /// </summary>
        protected Room()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Room class
        /// </summary>
        /// <param name="name">This rooms name</param>
        /// <param name="description">This rooms description</param>
        public Room(string name, Description description)
        {
            // set name
            this.name = name;

            // set description
            this.Description = description;
        }

        /// <summary>
        /// Initializes a new instance of the Room class
        /// </summary>
        /// <param name="name">This rooms name</param>
        /// <param name="description">This rooms description</param>
        /// <param name="exits">The exits from this room</param>
        public Room(string name, Description description, params Exit[] exits)
        {
            // set name
            this.name = name;

            // set description
            this.Description = description;

            // set exits
            this.exits.AddRange(exits);
        }

        /// <summary>
        /// Initializes a new instance of the Room class
        /// </summary>
        /// <param name="name">This rooms name</param>
        /// <param name="description">This rooms description</param>
        /// <param name="interaction">This rooms interaction</param>
        /// <param name="exits">The exits from this room</param>
        public Room(string name, Description description, InteractionCallback interaction, params Exit[] exits)
        {
            // set name
            this.name = name;

            // set description
            this.Description = description;

            // set interaction
            Interaction = interaction;

            // set exits
            this.exits.AddRange(exits);
        }

        /// <summary>
        /// Initializes a new instance of the Room class
        /// </summary>
        /// <param name="name">This rooms name</param>
        /// <param name="description">This rooms description</param>
        /// <param name="exit">The exit from this room</param>
        /// <param name="items">The items in this room</param>
        public Room(string name, Description description, Exit exit, params Item[] items)
        {
            // set name
            this.name = name;

            // set description
            this.Description = description;

            // set exit
            exits.Add(exit);

            // set items
            this.items.AddRange(items);
        }

        /// <summary>
        /// Initializes a new instance of the Room class
        /// </summary>
        /// <param name="name">This rooms name</param>
        /// <param name="description">This rooms description</param>
        /// <param name="exits">The exits from this room</param>
        /// <param name="items">The items in this room</param>
        public Room(string name, Description description, Exit[] exits, params Item[] items)
        {
            // set name
            this.name = name;

            // set description
            this.Description = description;

            // set exits
            this.exits.AddRange(exits);

            // set items
            this.items.AddRange(items);
        }

        /// <summary>
        /// Initializes a new instance of the Room class
        /// </summary>
        /// <param name="name">This rooms name</param>
        /// <param name="description">This rooms description</param>
        /// <param name="interaction">This rooms interaction</param>
        /// <param name="exit">The exit from this room</param>
        /// <param name="items">The items in this room</param>
        public Room(string name, Description description, InteractionCallback interaction, Exit exit, params Item[] items)
        {
            // set name
            this.name = name;

            // set description
            this.Description = description;

            // set interaction
            Interaction = interaction;

            // set exit
            exits.Add(exit);

            // set items
            this.items.AddRange(items);
        }

        /// <summary>
        /// Initializes a new instance of the Room class
        /// </summary>
        /// <param name="name">This rooms name</param>
        /// <param name="description">This rooms description</param>
        /// <param name="interaction">This rooms interaction</param>
        /// <param name="exits">The exits from this room</param>
        /// <param name="items">The items in this room</param>
        public Room(string name, Description description, InteractionCallback interaction, Exit[] exits, params Item[] items)
        {
            // set name
            this.name = name;

            // set description
            this.Description = description;

            // set interaction
            Interaction = interaction;

            // set exits
            this.exits.AddRange(exits);

            // set items
            this.items.AddRange(items);
        }

        /// <summary>
        /// Initializes a new instance of the Room class
        /// </summary>
        /// <param name="name">This rooms name</param>
        /// <param name="description">This rooms description</param>
        /// <param name="exit">The exit from this room</param>
        /// <param name="items">The items in this room</param>
        /// <param name="characters">The characters in this room</param>
        public Room(string name, Description description, Exit exit, Item[] items, params NonPlayableCharacter[] characters)
        {
            // set name
            this.name = name;

            // set description
            this.Description = description;

            // set exit
            exits.Add(exit);

            // set items
            this.items.AddRange(items);

            // set characters
            this.characters.AddRange(characters);
        }

        /// <summary>
        /// Initializes a new instance of the Room class
        /// </summary>
        /// <param name="name">This rooms name</param>
        /// <param name="description">This rooms description</param>
        /// <param name="exit">The exit from this room</param>
        /// <param name="item">The item in this room</param>
        /// <param name="characters">The characters in this room</param>
        public Room(string name, Description description, Exit exit, Item item, params NonPlayableCharacter[] characters)
        {
            // set name
            this.name = name;

            // set description
            this.Description = description;

            // set exit
            exits.Add(exit);

            // set item
            items.Add(item);

            // set characters
            this.characters.AddRange(characters);
        }

        /// <summary>
        /// Initializes a new instance of the Room class
        /// </summary>
        /// <param name="name">This rooms name</param>
        /// <param name="description">This rooms description</param>
        /// <param name="exits">The exits from this room</param>
        /// <param name="items">The items in this room</param>
        /// <param name="characters">The characters in this room</param>
        public Room(string name, Description description, Exit[] exits, Item[] items, params NonPlayableCharacter[] characters)
        {
            // set name
            this.name = name;

            // set description
            this.Description = description;

            // set exits
            this.exits.AddRange(exits);

            // set items
            this.items.AddRange(items);

            // set characters
            this.characters.AddRange(characters);
        }

        /// <summary>
        /// Initializes a new instance of the Room class
        /// </summary>
        /// <param name="name">This rooms name</param>
        /// <param name="description">This rooms description</param>
        /// <param name="interaction">This rooms interaction</param>
        /// <param name="exit">The exit from this room</param>
        /// <param name="items">The items in this room</param>
        /// <param name="characters">The characters in this room</param>
        public Room(string name, Description description, InteractionCallback interaction, Exit exit, Item[] items, params NonPlayableCharacter[] characters)
        {
            // set name
            this.name = name;

            // set description
            this.Description = description;

            // set interaction
            Interaction = interaction;

            // set exit
            exits.Add(exit);

            // set items
            this.items.AddRange(items);

            // set characters
            this.characters.AddRange(characters);
        }

        /// <summary>
        /// Initializes a new instance of the Room class
        /// </summary>
        /// <param name="name">This rooms name</param>
        /// <param name="description">This rooms description</param>
        /// <param name="interaction">This rooms interaction</param>
        /// <param name="exit">The exit from this room</param>
        /// <param name="item">The item in this room</param>
        /// <param name="characters">The characters in this room</param>
        public Room(string name, Description description, InteractionCallback interaction, Exit exit, Item item, params NonPlayableCharacter[] characters)
        {
            // set name
            this.name = name;

            // set description
            this.Description = description;

            // set interaction
            Interaction = interaction;

            // set exit
            exits.Add(exit);

            // set item
            items.Add(item);

            // set characters
            this.characters.AddRange(characters);
        }

        /// <summary>
        /// Initializes a new instance of the Room class
        /// </summary>
        /// <param name="name">This rooms name</param>
        /// <param name="description">This rooms description</param>
        /// <param name="interaction">This rooms interaction</param>
        /// <param name="exits">The exits from this room</param>
        /// <param name="items">The items in this room</param>
        /// <param name="characters">The characters in this room</param>
        public Room(string name, Description description, InteractionCallback interaction, Exit[] exits, Item[] items, params NonPlayableCharacter[] characters)
        {
            // set name
            this.name = name;

            // set description
            this.Description = description;

            // set interaction
            Interaction = interaction;

            // set exits
            this.exits.AddRange(exits);

            // set items
            this.items.AddRange(items);

            // set characters
            this.characters.AddRange(characters);
        }

        /// <summary>
        /// Initializes a new instance of the Room class
        /// </summary>
        /// <param name="name">This rooms name</param>
        /// <param name="description">This rooms description</param>
        public Room(string name, string description)
        {
            // set name
            this.name = name;

            // set description
            this.Description = new Description(description);
        }

        /// <summary>
        /// Initializes a new instance of the Room class
        /// </summary>
        /// <param name="name">This rooms name</param>
        /// <param name="description">This rooms description</param>
        /// <param name="exits">The exits from this room</param>
        public Room(string name, string description, params Exit[] exits)
        {
            // set name
            this.name = name;

            // set description
            this.Description = new Description(description);

            // set exits
            this.exits.AddRange(exits);
        }

        /// <summary>
        /// Initializes a new instance of the Room class
        /// </summary>
        /// <param name="name">This rooms name</param>
        /// <param name="description">This rooms description</param>
        /// <param name="interaction">This rooms interaction</param>
        /// <param name="exits">The exits from this room</param>
        public Room(string name, string description, InteractionCallback interaction, params Exit[] exits)
        {
            // set name
            this.name = name;

            // set description
            this.Description = new Description(description);

            // set interaction
            Interaction = interaction;

            // set exits
            this.exits.AddRange(exits);
        }

        /// <summary>
        /// Initializes a new instance of the Room class
        /// </summary>
        /// <param name="name">This rooms name</param>
        /// <param name="description">This rooms description</param>
        /// <param name="exit">The exit from this room</param>
        /// <param name="items">The items in this room</param>
        public Room(string name, string description, Exit exit, params Item[] items)
        {
            // set name
            this.name = name;

            // set description
            this.Description = new Description(description);

            // set exits
            exits.Add(exit);

            // set items
            this.items.AddRange(items);
        }

        /// <summary>
        /// Initializes a new instance of the Room class
        /// </summary>
        /// <param name="name">This rooms name</param>
        /// <param name="description">This rooms description</param>
        /// <param name="exits">The exits from this room</param>
        /// <param name="items">The items in this room</param>
        public Room(string name, string description, Exit[] exits, params Item[] items)
        {
            // set name
            this.name = name;

            // set description
            this.Description = new Description(description);

            // set exits
            this.exits.AddRange(exits);

            // set items
            this.items.AddRange(items);
        }

        /// <summary>
        /// Initializes a new instance of the Room class
        /// </summary>
        /// <param name="name">This rooms name</param>
        /// <param name="description">This rooms description</param>
        /// <param name="interaction">This rooms interaction</param>
        /// <param name="exit">The exit from this room</param>
        /// <param name="items">The items in this room</param>
        public Room(string name, string description, InteractionCallback interaction, Exit exit, params Item[] items)
        {
            // set name
            this.name = name;

            // set description
            this.Description = new Description(description);

            // set interaction
            Interaction = interaction;

            // set exit
            exits.Add(exit);

            // set items
            this.items.AddRange(items);
        }

        /// <summary>
        /// Initializes a new instance of the Room class
        /// </summary>
        /// <param name="name">This rooms name</param>
        /// <param name="description">This rooms description</param>
        /// <param name="interaction">This rooms interaction</param>
        /// <param name="exits">The exits from this room</param>
        /// <param name="items">The items in this room</param>
        public Room(string name, string description, InteractionCallback interaction, Exit[] exits, params Item[] items)
        {
            // set name
            this.name = name;

            // set description
            this.Description = new Description(description);

            // set interaction
            Interaction = interaction;

            // set exits
            this.exits.AddRange(exits);

            // set items
            this.items.AddRange(items);
        }

        /// <summary>
        /// Initializes a new instance of the Room class
        /// </summary>
        /// <param name="name">This rooms name</param>
        /// <param name="description">This rooms description</param>
        /// <param name="exit">The exit from this room</param>
        /// <param name="item">The item in this room</param>
        /// <param name="characters">The characters in this room</param>
        public Room(string name, string description, Exit exit, Item item, params NonPlayableCharacter[] characters)
        {
            // set name
            this.name = name;

            // set description
            this.Description = new Description(description);

            // set exit
            exits.Add(exit);

            // set item
            items.Add(item);

            // set characters
            this.characters.AddRange(characters);
        }

        /// <summary>
        /// Initializes a new instance of the Room class
        /// </summary>
        /// <param name="name">This rooms name</param>
        /// <param name="description">This rooms description</param>
        /// <param name="exit">The exit from this room</param>
        /// <param name="items">The items in this room</param>
        /// <param name="characters">The characters in this room</param>
        public Room(string name, string description, Exit exit, Item[] items, params NonPlayableCharacter[] characters)
        {
            // set name
            this.name = name;

            // set description
            this.Description = new Description(description);

            // set exits
            exits.Add(exit);

            // set items
            this.items.AddRange(items);

            // set characters
            this.characters.AddRange(characters);
        }

        /// <summary>
        /// Initializes a new instance of the Room class
        /// </summary>
        /// <param name="name">This rooms name</param>
        /// <param name="description">This rooms description</param>
        /// <param name="exits">The exits from this room</param>
        /// <param name="items">The items in this room</param>
        /// <param name="characters">The characters in this room</param>
        public Room(string name, string description, Exit[] exits, Item[] items, params NonPlayableCharacter[] characters)
        {
            // set name
            this.name = name;

            // set description
            this.Description = new Description(description);

            // set exits
            this.exits.AddRange(exits);

            // set items
            this.items.AddRange(items);

            // set characters
            this.characters.AddRange(characters);
        }

        /// <summary>
        /// Initializes a new instance of the Room class
        /// </summary>
        /// <param name="name">This rooms name</param>
        /// <param name="description">This rooms description</param>
        /// <param name="interaction">This rooms interaction</param>
        /// <param name="exit">The exit from this room</param>
        /// <param name="items">The items in this room</param>
        /// <param name="characters">The characters in this room</param>
        public Room(string name, string description, InteractionCallback interaction, Exit exit, Item[] items, params NonPlayableCharacter[] characters)
        {
            // set name
            this.name = name;

            // set description
            this.Description = new Description(description);

            // set interaction
            Interaction = interaction;

            // set exit
            exits.Add(exit);

            // set items
            this.items.AddRange(items);

            // set characters
            this.characters.AddRange(characters);
        }

        /// <summary>
        /// Initializes a new instance of the Room class
        /// </summary>
        /// <param name="name">This rooms name</param>
        /// <param name="description">This rooms description</param>
        /// <param name="interaction">This rooms interaction</param>
        /// <param name="exit">The exit from this room</param>
        /// <param name="item">The item in this room</param>
        /// <param name="characters">The characters in this room</param>
        public Room(string name, string description, InteractionCallback interaction, Exit exit, Item item, params NonPlayableCharacter[] characters)
        {
            // set name
            this.name = name;

            // set description
            this.Description = new Description(description);

            // set interaction
            Interaction = interaction;

            // set exit
            exits.Add(exit);

            // set item
            items.Add(item);

            // set characters
            this.characters.AddRange(characters);
        }

        /// <summary>
        /// Initializes a new instance of the Room class
        /// </summary>
        /// <param name="name">This rooms name</param>
        /// <param name="description">This rooms description</param>
        /// <param name="interaction">This rooms interaction</param>
        /// <param name="exits">The exits from this room</param>
        /// <param name="items">The items in this room</param>
        /// <param name="characters">The characters in this room</param>
        public Room(string name, string description, InteractionCallback interaction, Exit[] exits, Item[] items, params NonPlayableCharacter[] characters)
        {
            // set name
            this.name = name;

            // set description
            this.Description = new Description(description);

            // set interaction
            Interaction = interaction;

            // set exits
            this.exits.AddRange(exits);

            // set items
            this.items.AddRange(items);

            // set characters
            this.characters.AddRange(characters);
        }

        /// <summary>
        /// Add a character to this room
        /// </summary>
        /// <param name="character">The character to add</param>
        public void AddCharacter(NonPlayableCharacter character)
        {
            // add the character
            characters.Add(character);
        }

        /// <summary>
        /// Add an item to this room
        /// </summary>
        /// <param name="item">The item to add</param>
        public void AddItem(Item item)
        {
            // add the item
            items.Add(item);
        }


        /// <summary>
        /// Add an exit to this room
        /// </summary>
        /// <param name="exit">The exit to add</param>
        public void AddExit(Exit exit)
        {
            // add the exit
            exits.Add(exit);
        }

        /// <summary>
        /// Remove an item from the room
        /// </summary>
        /// <param name="item">The item to remove</param>
        /// <returns>The item removed from this room</returns>
        public Item RemoveItemFromRoom(Item item)
        {
            // remove
            items.Remove(item);

            // return item
            return item;
        }

        /// <summary>
        /// Remove an item from the room
        /// </summary>
        /// <param name="itemName">The name of the item to remove</param>
        /// <returns>If the item was removed correctly</returns>
        public Decision RemoveItemFromRoom(string itemName)
        {
            // holds item
            Item item;

            // remove
            return RemoveItemFromRoom(itemName, out item);
        }

        /// <summary>
        /// Remove an item from the room
        /// </summary>
        /// <param name="itemName">The name of the item to remove</param>
        /// <param name="removedItem">The item removed from this room</param>
        /// <returns>If the item was removed correctly</returns>
        public Decision RemoveItemFromRoom(string itemName, out Item removedItem)
        {
            // hold all items that match
            var matchingItems = Items.Where(x => x.Name.ToUpper() == itemName.ToUpper()).ToArray();

            // if found
            if (matchingItems.Length > 0)
            {
                // if visible
                if (matchingItems[0].IsPlayerVisible)
                {
                    // takeable
                    if (matchingItems[0].IsTakeable)
                    {
                        // get item
                        removedItem = matchingItems[0];

                        // remove
                        items.Remove(removedItem);

                        // return pass
                        return new Decision(EReactionToInput.CouldReact, "Took " + removedItem.Name);
                    }

                    // no item removed
                    removedItem = null;

                    // return fail
                    return new Decision(EReactionToInput.CouldntReact, matchingItems[0].Name + " is not takeable");
                }

                // no item
                removedItem = null;

                // failed
                return new Decision(EReactionToInput.CouldntReact, "That is not an item");
            }

            // no item
            removedItem = null;

            // failed
            return new Decision(EReactionToInput.CouldntReact, "That is not an item");
        }

        /// <summary>
        /// Remove an character from the room
        /// </summary>
        /// <param name="character">The character to remove</param>
        /// <returns>The character removed from this room</returns>
        public Character RemoveCharacterFromRoom(NonPlayableCharacter character)
        {
            // remove
            characters.Remove(character);

            // return item
            return character;
        }

        /// <summary>
        /// Remove an character from the room
        /// </summary>
        /// <param name="characterName">The name of the character to remove</param>
        /// <returns>If the character was removed correctly</returns>
        public Decision RemoveCharacterFromRoom(string characterName)
        {
            // holds removed character
            NonPlayableCharacter character;

            // remove
            return RemoveCharacterFromRoom(characterName, out character);
        }

        /// <summary>
        /// Remove an character from the room
        /// </summary>
        /// <param name="characterName">The name of the character to remove</param>
        /// <param name="removedCharacter">The character removed from this room</param>
        /// <returns>If the character was removed correctly</returns>
        public Decision RemoveCharacterFromRoom(string characterName, out NonPlayableCharacter removedCharacter)
        {
            // hold all characters that match
            var matchingCharacters = Characters.Where(x => x.Name.ToUpper() == characterName.ToUpper()).ToArray();

            // if found
            if (matchingCharacters.Length > 0)
            {
                // get item
                removedCharacter = matchingCharacters[0];

                // remove
                characters.Remove(removedCharacter);

                // return pass
                return new Decision(EReactionToInput.CouldReact, "Removed " + removedCharacter.Name);
            }

            // no item
            removedCharacter = null;

            // failed
            return new Decision(EReactionToInput.CouldntReact, "That is not an character");
        }

        /// <summary>
        /// Remove an interaction target from the room
        /// </summary>
        /// <param name="target">The target to remove</param>
        /// <returns>The target removed from this room</returns>
        public virtual IInteractWithItem RemoveInteractionTargetFromRoom(IInteractWithItem target)
        {
            // if in items
            if (Items.Contains(target))
            {
                // remove
                items.Remove(target as Item);

                // return target
                return target;
            }

            if (Characters.Contains(target))
            {
                // remove
                characters.Remove(target as NonPlayableCharacter);

                // return target
                return target;
            }

            // not removed
            return null;
        }

        /// <summary>
        /// Test if a move is possible
        /// </summary>
        /// <param name="direction">The direction to test</param>
        /// <returns>If a move in the specified direction is possible</returns>
        public virtual bool CanMove(ECardinalDirection direction)
        {
            // return if an unlocked exit in that directin
            return UnlockedExits.Where(x => x.Direction == direction).Count() > 0;
        }

        /// <summary>
        /// Interact with a specified item
        /// </summary>
        /// <param name="item">The item to interact with</param>
        /// <returns>The result of the interaction</returns>
        protected virtual InteractionResult InteractWithItem(Item item)
        {
            // handle interaction
            return interaction.Invoke(item, this);
        }

        /// <summary>
        /// Handle examination this Room
        /// </summary>
        /// <returns>The result of this examination</returns>
        protected override ExaminationResult OnExamined()
        {
            // hold items in the room
            var itemsInRoom = string.Empty;

            // if no items
            if (Items.Where(i => i.IsPlayerVisible).ToArray().Count() == 0)
                // nothing to examine
                return new ExaminationResult("There is nothing to examine");

            if (Items.Where(i => i.IsPlayerVisible).ToArray().Count() == 1)
            {
                // one item

                // get single item
                var singularItem = Items.Where(i => i.IsPlayerVisible).ToArray()[0];

                // return compiled string
                return new ExaminationResult(string.Format("There {0} {1} {2}", isPlural(singularItem.Name) ? "are" : "is", getPrexingGrammer(singularItem.Name), singularItem.Name));
            }
            // multiple items

            // get items in a list
            var itemsInList = getItemsAndPuzzlesAsList();

            // hold sentance
            var sentance = itemsInList;

            // hold if something left to check
            var somethingLeftToCheck = true;

            // hold index
            var index = 0;

            // hold current item name
            var currentItemName = string.Empty;

            // while something left to check
            while (somethingLeftToCheck)
            {
                // find index of next
                index = sentance.IndexOf(",", index);

                // if last index
                if (index == sentance.LastIndexOf(", "))
                {
                    // remove ","
                    sentance = sentance.Remove(index, 2);

                    // get current item name
                    currentItemName = sentance.Substring(index).Trim(Convert.ToChar(" "));

                    // add "and ?"
                    sentance = sentance.Insert(index, string.Format(" and {0} ", getPrexingGrammer(currentItemName)));

                    // reached end
                    somethingLeftToCheck = false;
                }
                else
                {
                    // remove ","
                    sentance = sentance.Remove(index, 2);

                    // get current item name
                    currentItemName = sentance.Substring(index, sentance.IndexOf(", ", index) - index).Trim(Convert.ToChar(" "));

                    // add ", ? " grammer
                    sentance = sentance.Insert(index, string.Format(", {0} ", getPrexingGrammer(currentItemName)));

                    // increase index so we miss the current ","
                    index += 1;
                }
            }

            // get first item
            if (sentance.Contains(", "))
                // get first item
                currentItemName = sentance.Substring(0, sentance.IndexOf(", "));
            else
                // get first item
                currentItemName = sentance.Substring(0, sentance.IndexOf(" and "));

            // return compiled string
            return new ExaminationResult(string.Format("There {0} {1} {2}", isPlural(currentItemName) ? "are" : "is", getPrexingGrammer(currentItemName), sentance));
        }

        /// <summary>
        /// Get prefixing grammer for a string
        /// </summary>
        /// <param name="word">The word to get grammer for</param>
        /// <returns>The grammer prefixing this word</returns>
        private string getPrexingGrammer(string word)
        {
            // if no string
            if (string.IsNullOrEmpty(word))
                // throw excpetion
                throw new ArgumentException("Parameter 'word' must have a value");

            // if a plural
            if (isPlural(word))
                // multiple
                return "some";
            if (isVowel(word[0]) &&
                word[0].ToString().ToUpper() != "U")
                // singular an
                return "an";
            return "a";
        }

        /// <summary>
        /// Get if a character is a vowel
        /// </summary>
        /// <param name="c">The character to check</param>
        /// <returns>True if the character is a vowel</returns>
        private bool isVowel(char c)
        {
            // get vowel
            var vowel = c.ToString().ToUpper();

            // get if vowel
            return vowel == "A" ||
                   vowel == "E" ||
                   vowel == "I" ||
                   vowel == "O" ||
                   vowel == "U";
        }

        /// <summary>
        /// Get if a word is plural
        /// </summary>
        /// <param name="word">The word to check</param>
        /// <returns>True if the word is plural</returns>
        private bool isPlural(string word)
        {
            // if no string
            if (string.IsNullOrEmpty(word))
                // throw excpetion
                throw new ArgumentException("Parameter 'word' must have a value");

            // trim all trailing white
            word = word.Trim(Convert.ToChar(" "));

            // may have a space
            if (word.Contains(" "))
                // get subsrting uptill " "
                word = word.Substring(0, word.IndexOf(" "));

            // return if a plural
            return word.Substring(word.Length - 1).ToUpper() == "S";
        }

        /// <summary>
        /// Get all Items and Puzzles as a list in a string
        /// </summary>
        /// <returns>A list of all</returns>
        private string getItemsAndPuzzlesAsList()
        {
            // if some items
            if (Items.Length > 0)
            {
                // hold entrace
                var itemsInRoom = string.Empty;

                // hold item names
                var itemNames = new List<string>();

                // itterate all items
                foreach (var i in Items)
                    // if player visible
                    if (i.IsPlayerVisible)
                        // add name
                        itemNames.Add(i.Name);

                // sort
                itemNames.Sort();

                // itterate names
                foreach (var name in itemNames)
                    // add item
                    itemsInRoom += name + ", ";

                // remove last ", "
                itemsInRoom = itemsInRoom.Remove(itemsInRoom.Length - 2);

                // return items
                return itemsInRoom;
            }

            // none
            return string.Empty;
        }

        /// <summary>
        /// Get everything that can be examined within this room
        /// </summary>
        /// <returns>An array of everything that can be examined in this room</returns>
        public virtual IExaminable[] GetExaminableObjects()
        {
            // create list
            var examinable = new List<IExaminable>();

            // add characters
            examinable.AddRange(Characters.Where<Character>(x => x.IsPlayerVisible).ToArray());

            // add items
            examinable.AddRange(Items.Where(x => x.IsPlayerVisible).ToArray());

            // return
            return examinable.ToArray<IExaminable>();
        }

        /// <summary>
        /// Get if this room has a visible locked exit in a specified direction. This will not include exits whose ExaminableObject.IsPlayerVisible property is set to false
        /// </summary>
        /// <param name="direction">The directon to check</param>
        /// <returns>If there is a locked exit in the specified direction</returns>
        public bool HasLockedExitInDirection(ECardinalDirection direction)
        {
            // check
            return HasLockedExitInDirection(direction, false);
        }

        /// <summary>
        /// Get if this room has a visible locked exit in a specified direction
        /// </summary>
        /// <param name="direction">The directon to check</param>
        /// <param name="includeInvisibleExits">Specifiy if invisible exits should be included</param>
        /// <returns>If there is a locked exit in the specified direction</returns>
        public bool HasLockedExitInDirection(ECardinalDirection direction, bool includeInvisibleExits)
        {
            // check
            return Exits.Where(x => x.Direction == direction && x.IsLocked && (includeInvisibleExits || x.IsPlayerVisible)).Count() > 0;
        }

        /// <summary>
        /// Get if this room has a visible unlocked exit in a specified direction. This will not include exits whose ExaminableObject.IsPlayerVisible property is set to false
        /// </summary>
        /// <param name="direction">The directon to check</param>
        /// <returns>If there is a unlocked exit in the specified direction</returns>
        public bool HasUnlockedExitInDirection(ECardinalDirection direction)
        {
            // check
            return HasUnlockedExitInDirection(direction, false);
        }

        /// <summary>
        /// Get if this room has a visible unlocked exit in a specified direction
        /// </summary>
        /// <param name="direction">The directon to check</param>
        /// <param name="includeInvisibleExits">Specify if invisible exits should be included</param>
        /// <returns>If there is a unlocked exit in the specified direction</returns>
        public bool HasUnlockedExitInDirection(ECardinalDirection direction, bool includeInvisibleExits)
        {
            // check
            return Exits.Where(x => x.Direction == direction && !x.IsLocked && (includeInvisibleExits || x.IsPlayerVisible)).Count() > 0;
        }

        /// <summary>
        /// Get if this Room contains an exit. This will not include exits whose ExaminableObject.IsPlayerVisible property is set to false
        /// </summary>
        /// <param name="exit">The exit to check for</param>
        /// <returns>True if the exit exists, else false</returns>
        public bool ContainsExit(Exit exit)
        {
            // return
            return ContainsExit(exit, false);
        }

        /// <summary>
        /// Get if this Room contains an exit
        /// </summary>
        /// <param name="exit">The exit to check for</param>
        /// <param name="includeInvisibleExits">Specify if invisible exits should be included</param>
        /// <returns>True if the exit exists, else false</returns>
        public bool ContainsExit(Exit exit, bool includeInvisibleExits)
        {
            // return
            return Exits.Contains(exit) && (includeInvisibleExits || exit.IsPlayerVisible);
        }

        /// <summary>
        /// Get if this Room contains an exit. This will not include exits whose ExaminableObject.IsPlayerVisible property is set to false
        /// </summary>
        /// <param name="direction">The direction of the exit to check for</param>
        /// <returns>True if the exit exists, else false</returns>
        public bool ContainsExit(ECardinalDirection direction)
        {
            return ContainsExit(direction, false);
        }

        /// <summary>
        /// Get if this Room contains an exit
        /// </summary>
        /// <param name="direction">The direction of the exit to check for</param>
        /// <param name="includeInvisibleExits">Specify if invisible exits should be included</param>
        /// <returns>True if the exit exists, else false</returns>
        public bool ContainsExit(ECardinalDirection direction, bool includeInvisibleExits)
        {
            return Exits.Where(exit => exit.Direction == direction && (includeInvisibleExits || exit.IsPlayerVisible)).Count() > 0;
        }

        /// <summary>
        /// Find an exit. This will not include exits whose ExaminableObject.IsPlayerVisible property is set to false
        /// </summary>
        /// <param name="direction">The exits direction</param>
        /// <param name="exit">The exit</param>
        /// <returns>True if the exit was found</returns>
        public bool FindExit(ECardinalDirection direction, out Exit exit)
        {
            return FindExit(direction, out exit, false);
        }

        /// <summary>
        /// Find an exit
        /// </summary>
        /// <param name="direction">The exits direction</param>
        /// <param name="exit">The exit</param>
        /// <param name="includeInvisibleExits">Specify if invisible exists should be included</param>
        /// <returns>True if the exit was found</returns>
        public bool FindExit(ECardinalDirection direction, out Exit exit, bool includeInvisibleExits)
        {
            // hold items
            var exits = Exits.Where(x => x.Direction == direction && (includeInvisibleExits || x.IsPlayerVisible)).ToArray();

            // if found
            if (exits.Length > 0)
            {
                // set exit
                exit = exits[0];

                // item found
                return true;
            }

            // no exit
            exit = null;

            // fail
            return false;
        }

        /// <summary>
        /// Get if this Room contains an item. This will not include items whose ExaminableObject.IsPlayerVisible property is set to false
        /// </summary>
        /// <param name="item">The item to check for</param>
        /// <returns>True if the item is in this room, else false</returns>
        public bool ContainsItem(Item item)
        {
            return Items.Contains(item);
        }

        /// <summary>
        /// Get if this Room contains an item
        /// </summary>
        /// <param name="item">The item to check for</param>
        /// <param name="includeInvisibleItems">Specify if invisible items should be included</param>
        /// <returns>True if the item is in this room, else false</returns>
        public bool ContainsItem(Item item, bool includeInvisibleItems)
        {
            return Items.Contains(item) && (includeInvisibleItems || item.IsPlayerVisible);
        }

        /// <summary>
        /// Get if this Room contains an item. This will not include items whose ExaminableObject.IsPlayerVisible property is set to false
        /// </summary>
        /// <param name="itemName">The item name to check for. This is case insensitive</param>
        /// <returns>True if the item is in this room, else false</returns>
        public bool ContainsItem(string itemName)
        {
            return ContainsItem(itemName, false);
        }

        /// <summary>
        /// Get if this Room contains an item
        /// </summary>
        /// <param name="itemName">The item name to check for. This is case insensitive</param>
        /// <param name="includeInvisibleItems">Specify if invisible items should be included</param>
        /// <returns>True if the item is in this room, else false</returns>
        public bool ContainsItem(string itemName, bool includeInvisibleItems)
        {
            return Items.Where(item => item.Name.ToUpper() == itemName.ToUpper() && (includeInvisibleItems || item.IsPlayerVisible)).Count() > 0;
        }

        /// <summary>
        /// Get if this Room contains an interaction target
        /// </summary>
        /// <param name="target">The target to check for</param>
        /// <returns>True if the target is in this room, else false</returns>
        public virtual bool ContainsInteractionTarget(IInteractWithItem target)
        {
            return Items.Contains(target) || Characters.Contains(target);
        }

        /// <summary>
        /// Get if this Room contains an interaction target
        /// </summary>
        /// <param name="targetName">The name of the target to check for. This is case insensitive</param>
        /// <returns>True if the target is in this room, else false</returns>
        public virtual bool ContainsInteractionTarget(string targetName)
        {
            return Items.Where(i => i.Name.ToUpper() == targetName.ToUpper()).Count() > 0 ||
                   Characters.Where(nPC => nPC.Name.ToUpper() == targetName.ToUpper()).Count() > 0;
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
        /// Find an item
        /// </summary>
        /// <param name="itemName">The items name. This is case insensitive</param>
        /// <param name="item">The item</param>
        /// <param name="includeInvisibleItems">Specify is invisible items should be included</param>
        /// <returns>True if the item was found</returns>
        public bool FindItem(string itemName, out Item item, bool includeInvisibleItems)
        {
            // hold items
            var items = Items.Where(x => x.Name.ToUpper() == itemName.ToUpper() && (includeInvisibleItems || x.IsPlayerVisible)).ToArray();

            // if found
            if (items.Length > 0)
            {
                // set item
                item = items[0];

                // item found
                return true;
            }

            // no item
            item = null;

            // fail
            return false;
        }

        /// <summary>
        /// Find an item. This will not include items whose ExaminableObject.IsPlayerVisible property is set to false
        /// </summary>
        /// <param name="itemID">The items ID</param>
        /// <param name="item">The item</param>
        /// <returns>True if the item was found</returns>
        internal bool FindItemByID(string itemID, out Item item)
        {
            return FindItemByID(itemID, out item, false);
        }

        /// <summary>
        /// Find an item
        /// </summary>
        /// <param name="itemID">The items ID</param>
        /// <param name="item">The item</param>
        /// <param name="includeInvisibleItems">Specifiy if invisible items should be included</param>
        /// <returns>True if the item was found</returns>
        internal bool FindItemByID(string itemID, out Item item, bool includeInvisibleItems)
        {
            // hold items
            var items = Items.Where(x => x.ID == itemID && (includeInvisibleItems || x.IsPlayerVisible)).ToArray();

            // if found
            if (items.Length > 0)
            {
                // set item
                item = items[0];

                // item found
                return true;
            }

            // no item
            item = null;

            // fail
            return false;
        }

        /// <summary>
        /// Find an interaction target
        /// </summary>
        /// <param name="targetName">The targets name. This is case insensitive</param>
        /// <param name="target">The target</param>
        /// <returns>True if the target was found</returns>
        public virtual bool FindInteractionTarget(string targetName, out IInteractWithItem target)
        {
            // hold items
            var items = Items.Where(x => x.Name.ToUpper() == targetName.ToUpper()).ToArray();

            // hold npcs
            var nPCS = Characters.Where(n => n.Name.ToUpper() == targetName.ToUpper()).ToArray();

            // create combined list
            var interactions = new List<IInteractWithItem>(items);

            // add npcs
            interactions.AddRange(nPCS);

            // if found
            if (interactions.Count > 0)
            {
                // set target
                target = interactions[0];

                // item found
                return true;
            }

            // no target
            target = null;

            // fail
            return false;
        }

        /// <summary>
        /// Get if this Room contains a character. This will not include characters whose ExaminableObject.IsPlayerVisible property is set to false
        /// </summary>
        /// <param name="character">The character to check for</param>
        /// <returns>True if the character is in this room, else false</returns>
        public bool ContainsCharacter(Character character)
        {
            return ContainsCharacter(character, false);
        }

        /// <summary>
        /// Get if this Room contains a character
        /// </summary>
        /// <param name="character">The character to check for</param>
        /// <param name="includeInvisibleCharacters">Specify is invisible characters should be included</param>
        /// <returns>True if the character is in this room, else false</returns>
        public bool ContainsCharacter(Character character, bool includeInvisibleCharacters)
        {
            return Characters.Contains(character) && (includeInvisibleCharacters || character.IsPlayerVisible);
        }

        /// <summary>
        /// Get if this Room contains a character. This will not include characters whose ExaminableObject.IsPlayerVisible property is set to false
        /// </summary>
        /// <param name="character">The item name to check for. This is case insensitive</param>
        /// <returns>True if the item is in this room, else false</returns>
        public bool ContainsCharacter(string character)
        {
            return ContainsCharacter(character, false);
        }

        /// <summary>
        /// Get if this Room contains a character
        /// </summary>
        /// <param name="characterName">The character name to check for. This is case insensitive</param>
        /// <param name="includeInvisibleCharacters">Specify if invisible characters should be included</param>
        /// <returns>True if the item is in this room, else false</returns>
        public bool ContainsCharacter(string characterName, bool includeInvisibleCharacters)
        {
            return Characters.Where(character => character.Name.ToUpper() == characterName.ToUpper() && (includeInvisibleCharacters || character.IsPlayerVisible)).Count() > 0;
        }

        /// <summary>
        /// Find a character. This will not include characters whose ExaminableObject.IsPlayerVisible property is set to false
        /// </summary>
        /// <param name="character">The character name. This is case insensitive</param>
        /// <param name="characterName">The character</param>
        /// <returns>True if the character was found</returns>
        public bool FindCharacter(string characterName, out NonPlayableCharacter character)
        {
            return FindCharacter(characterName, out character, false);
        }

        /// <summary>
        /// Find a character
        /// </summary>
        /// <param name="characterName">The character name. This is case insensitive</param>
        /// <param name="character">The character</param>
        /// <param name="includeInvisibleCharacters">Specify if inviisble characters should be included</param>
        /// <returns>True if the character was found</returns>
        public bool FindCharacter(string characterName, out NonPlayableCharacter character, bool includeInvisibleCharacters)
        {
            // hold characters
            var characters = Characters.Where(x => x.Name.ToUpper() == characterName.ToUpper() && (includeInvisibleCharacters || x.IsPlayerVisible)).ToArray();

            // if found
            if (characters.Length > 0)
            {
                // set character
                character = characters[0];

                // item found
                return true;
            }

            // no character
            character = null;

            // fail
            return false;
        }

        /// <summary>
        /// Sepecify a conditional description of this room
        /// </summary>
        /// <param name="description">The description of this room</param>
        public void SpecifyConditionalDescription(ConditionalDescription description)
        {
            // set description
            this.Description = description;
        }

        /// <summary>
        /// Get all IImplementOwnActions objects within this Room
        /// </summary>
        /// <returns>An array of all IImplementOwnActions objects within this rrom</returns>
        public virtual IImplementOwnActions[] GetAllObjectsWithAdditionalCommands()
        {
            // hold custom commandables
            var customCommands = new List<IImplementOwnActions>();

            // add room items
            customCommands.AddRange(Items.Where(i => i.IsPlayerVisible).ToArray());

            // add room characters
            customCommands.AddRange(Characters.Where<Character>(c => c.IsPlayerVisible).ToArray());

            // add room
            customCommands.Add(this);

            // return as array
            return customCommands.ToArray<IImplementOwnActions>();
        }

        /// <summary>
        /// Handle reactions to ActionableCommands
        /// </summary>
        /// <param name="command">The command to react to</param>
        /// <returns>The result of the command</returns>
        protected virtual InteractionResult OnReactToAction(ActionableCommand command)
        {
            // if command is found
            if (AdditionalCommands.Contains(command))
                // invoke action
                return command.Action.Invoke();
            throw new ArgumentException(string.Format("Command {0} was not found on object {1}", command.Command, Name));
        }

        /// <summary>
        /// Handle movement into this Room
        /// </summary>
        /// <param name="fromDirection">The direction movement into this Room is from. Use null if there should be no direction</param>
        public override void OnMovedInto(ECardinalDirection? fromDirection)
        {
            // hold from direction
            EnteredFrom = fromDirection;

            // handle at base level
            base.OnMovedInto(fromDirection);
        }

        /// <summary>
        /// Handle transferal of delegation to this Room from a source ITransferableDelegation object. This sould only concern top level properties and fields
        /// </summary>
        /// <param name="source">The source ITransferableDelegation object to transfer from</param>
        protected override void OnTransferFrom(ITransferableDelegation source)
        {
            // set interaction
            Interaction = ((Room)source).Interaction;
        }

        /// <summary>
        /// Handle registration of all child properties of this Room that are ITransferableDelegation
        /// </summary>
        /// <param name="children">A list containing all the ITransferableDelegation properties of this Room</param>
        protected override void OnRegisterTransferableChildren(ref List<ITransferableDelegation> children)
        {
            // itterate all items
            foreach (var i in Items)
            {
                // add
                children.Add(i);

                // register children
                i.RegisterTransferableChildren(ref children);
            }

            // itterate all characters
            foreach (var c in Characters)
            {
                // add
                children.Add(c);

                // register children
                c.RegisterTransferableChildren(ref children);
            }

            // itterate commands
            foreach (var aC in AdditionalCommands)
            {
                // add
                children.Add(aC);

                // register children
                aC.RegisterTransferableChildren(ref children);
            }

            base.OnRegisterTransferableChildren(ref children);
        }

        #region XmlSerialization

        /// <summary>
        /// Handle writing of Xml for this Room
        /// </summary>
        /// <param name="writer">The XmlWriter to write Xml with</param>
        protected override void OnWriteXml(XmlWriter writer)
        {
            // write start element
            writer.WriteStartElement("Room");

            // if an entrance value
            if (EnteredFrom.HasValue)
                // write where entered from
                writer.WriteAttributeString("EnteredFrom", EnteredFrom.Value.ToString());

            // write exits start element
            writer.WriteStartElement("Exits");

            // itterate all exits
            for (var index = 0; index < Exits.Length; index++)
                // write exit
                Exits[index].WriteXml(writer);

            // write end element
            writer.WriteEndElement();

            // write items start element
            writer.WriteStartElement("Items");

            // itterate all items
            for (var index = 0; index < Items.Length; index++)
                // write item
                Items[index].WriteXml(writer);

            // write end element
            writer.WriteEndElement();

            // write characters
            writer.WriteStartElement("Characters");

            // itterate all characters
            for (var index = 0; index < Characters.Length; index++)
                // write character
                Characters[index].WriteXml(writer);

            // write end element
            writer.WriteEndElement();

            // write start element
            writer.WriteStartElement("AdditionalActionableCommands");

            // itterate all custom commands
            foreach (var command in AdditionalCommands)
                // write
                command.WriteXml(writer);

            // write end element
            writer.WriteEndElement();

            // write base
            base.OnWriteXml(writer);

            // write end element
            writer.WriteEndElement();
        }

        /// <summary>
        /// Handle reading of Xml for this Room
        /// </summary>
        /// <param name="node">The node to read Xml from</param>
        protected override void OnReadXmlNode(XmlNode node)
        {
            // if attriburte exists
            if (AttributeExists(node, "EnteredFrom"))
                // set entered direction
                EnteredFrom = (ECardinalDirection)Enum.Parse(typeof(ECardinalDirection), GetAttribute(node, "EnteredFrom").Value);
            else
                // not entered
                EnteredFrom = null;

            // get exits node
            var exitsNode = GetNode(node, "Exits");

            // read items
            for (var index = 0; index < exitsNode.ChildNodes.Count; index++)
            {
                // get node for element
                var exitElementNode = exitsNode.ChildNodes[index];

                // hold exit
                Exit exit = null;

                // get direction
                var direction = (ECardinalDirection)Enum.Parse(typeof(ECardinalDirection), GetAttribute(exitElementNode, "Direction").Value);

                // if exit is not found
                if (!FindExit(direction, out exit, true))
                {
                    // create new exit
                    exit = new Exit(ECardinalDirection.East);

                    // add exit
                    exits.Add(exit);
                }

                // read element
                exit.ReadXmlNode(exitElementNode);
            }

            // get items node
            var itemsNode = GetNode(node, "Items");

            // hold all item id's that are in collection
            var itemIdsInCollection = new List<string>();

            // read items
            for (var index = 0; index < itemsNode.ChildNodes.Count; index++)
            {
                // get node for element
                var itemElementNode = itemsNode.ChildNodes[index];

                // find examinable object node
                var examinableObjectNode = GetNode(itemElementNode, "ExaminableObject");

                // add to collection
                itemIdsInCollection.Add(GetAttribute(examinableObjectNode, "ID").Value);

                // hold item
                Item item = null;

                // if item is not found
                if (!FindItemByID(GetAttribute(examinableObjectNode, "ID").Value, out item, true))
                {
                    // create new item
                    item = new Item(string.Empty, string.Empty, true);

                    // add item
                    items.Add(item);
                }

                // read node
                item.ReadXmlNode(itemElementNode);
            }

            // hold all items to remove
            var itemsToRemove = new List<Item>();

            // itterate each collectable in posession
            foreach (var i in items)
                // if not found in loaded items
                if (!itemIdsInCollection.Contains(i.ID))
                    // hold item
                    itemsToRemove.Add(i);

            // remove if not in list
            items.RemoveAll(i => itemsToRemove.Contains(i));

            // get characters node
            var charactersNode = GetNode(node, "Characters");

            // read characters
            for (var index = 0; index < charactersNode.ChildNodes.Count; index++)
            {
                // get npc node
                var npcNode = charactersNode.ChildNodes[index];

                // find examinable object node
                var characterElementNode = GetNode(npcNode, "Character");

                // find examinable object node
                var examinableObjectNode = GetNode(characterElementNode, "ExaminableObject");

                // hold character
                NonPlayableCharacter character = null;

                // if character is not found
                if (!FindCharacter(GetAttribute(examinableObjectNode, "Name").Value, out character, true))
                {
                    // create new character
                    character = new NonPlayableCharacter(string.Empty, string.Empty);

                    // add character
                    characters.Add(character);
                }

                // read element
                character.ReadXmlNode(npcNode);
            }

            // get custom commands node
            var customCommandsNode = GetNode(node, "AdditionalActionableCommands");

            // itterate all child nodes
            for (var index = 0; index < customCommandsNode.ChildNodes.Count; index++)
                // read from node
                AdditionalCommands[index].ReadXmlNode(customCommandsNode.ChildNodes[index]);

            base.OnReadXmlNode(GetNode(node, "GameLocation"));
        }

        #endregion

        #endregion

        #region IImplementOwnActions Members

        /// <summary>
        /// Get or set the ActionableCommands this object can interact with
        /// </summary>
        public List<ActionableCommand> AdditionalCommands
        {
            get { return additionalCommands; }
            set { additionalCommands = value; }
        }

        /// <summary>
        /// React to an ActionableCommand
        /// </summary>
        /// <param name="command">The command to react to</param>
        /// <returns>The result of the interaction</returns>
        public InteractionResult ReactToAction(ActionableCommand command)
        {
            return OnReactToAction(command);
        }

        /// <summary>
        /// Find a command by it's name
        /// </summary>
        /// <param name="command">The name of the command to find</param>
        /// <returns>The ActionableCommand (if it is found)</returns>
        public ActionableCommand FindCommand(string command)
        {
            // itterate all commands
            foreach (var c in AdditionalCommands)
                // check commands
                if (c.Command.ToUpper() == command.ToUpper())
                    // found
                    return c;

            // not found
            return null;
        }

        #endregion
    }

    /// <summary>
    /// Enumeration of cardinal directions
    /// </summary>
    public enum ECardinalDirection
    {
        /// <summary>
        /// North (up)
        /// </summary>
        North = 1,

        /// <summary>
        /// East (right)
        /// </summary>
        East = 2,

        /// <summary>
        /// South (down)
        /// </summary>
        South = -1,

        /// <summary>
        /// West (left)
        /// </summary>
        West = -2
    }
}