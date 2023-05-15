# Introduction 
.NET Standard 2.0 implementation of a framework for building text based adventures. This was originally developed in 2011 but has had some quality of life updates.

![image](https://github.com/ben-pollard-uk/adventure-framework/assets/129943363/57a5ce48-b35a-402b-bfbc-f4840e249211)

Provides simple classes for developing game elements:
  * Interface and base class for examinable objects:
    * Examination returns a description of the object.
    * Descriptions can be conditional, with different results generated from the game state.
    * All items can contain custom commands.
  * Hierarchical environments:
    * Overworld
      * Acts as a container of regions.
    * Region
      * Acts as a container of rooms.
    * Room
      * The player traverses through the rooms.
      * Provides a description of itself.
      * Supports up to 6 exits. Each exit can be locked until a condition is met.
      * Can contain multiple items.
  * NPC's:
    * Support provided for conversations with the player.
    * Can interact with items.
    * Can contain multiple items.
  * Items
    * Support interaction with the player, rooms, other items and NPC's.
    * Items can morph in to other items. For example, using item A on item B may cause item B to morph into item C.
  
The framework provides keywords for interacting with game elements:
  * Drop - drop an item.
  * Examine - allows items, characters and environments to be examined.
  * Take - take an item.
  * Talk to - talk to a NPC.
  * Use on - use an item. Items can be used on a variety of targets.
  * N, S, E, W, U, D - traverse through the rooms in a region.

Conversations with NPC's can be entered in to with an easy to use interface to display dialogue and provide responses:

![image](https://github.com/ben-pollard-uk/adventure-framework/assets/129943363/5ed1afc0-1ab8-4d35-9c90-dd848f18bfda)
  
The framework also provides global commands to help with game flow:
  * About - display version information.
  * CommandsOn / CommandsOff - toggle commands on/off.
  * Exit - exit the game.
  * Help - display the help screen.
  * KeyOn / KeyOff - turn the Key on/off.
  * Map - display the map.
  * New - start a new game.

All game management is provided by the framework, including:
  * Rendering of game screens:
    * Default frame.
    * Help frame.
    * Map frame.
    * Title frame.
    * Completion frame.
    * Game over frame.
    * Transition frame.
    * Conversation frame.
  * Input parsing.
  * State management.
  * Game creation.
  
Maps are automatically generated for regions:

![image](https://github.com/ben-pollard-uk/adventure-framework/assets/129943363/b6c05233-6856-4103-be44-be1c73a85874)

# Prerequisites
 * Windows
   * Download free IDE Visual Studio 2022 Community ( >> https://visualstudio.microsoft.com/de/vs/community/ ), or use commercial Visual Studio 2022 Version.

# Getting Started
 * Clone the repo
 * Build all projects
 * Run the BP.AdventureFramework.Examples project

# Hello World

```csharp
// create player
var player = new PlayableCharacter(new Identifier("Dave"), new Description("A young boy on a quest to find the meaning of life."));

// create region maker and add room
var regionMaker = new RegionMaker(new Identifier("Mountain"), new Description("An imposing volcano just East of town."))
{
    [0, 0] = new Room(new Identifier("Cavern"), new Description("A dark cavern set in to the base of the mountain."))
};

// create overworld maker
var overworldMaker = new OverworldMaker(new Identifier("Daves World"), new Description("An ancient kingdom."), regionMaker);

// create callback for generating games
var gameCreator = Game.Create("The Life Of Dave",
    "A very low budget adventure.",
    x => overworldMaker.Make(),
    () => player,
    x => false);

// execute game
Game.Execute(gameCreator);
```

# Contribute
It´s Open Source (License >> MIT), please feel free to use or contribute. To raise a pull request visit https://github.com/ben-pollard-uk/adventure-framework/pulls.

# For Open Questions
Visit https://github.com/ben-pollard-uk/adventure-framework/issues
