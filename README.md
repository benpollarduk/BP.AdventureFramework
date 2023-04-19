# Introduction 
.NET Standard 2.0 implementation of a framework for building text based adventures. This was originally developed in 2011 but has had some quality of life updates.

![image](https://user-images.githubusercontent.com/129943363/232739390-228b490d-fda2-4d1c-abef-b968980616c0.png)

Provides simple classes for developing game elements:
  * Interface and base class for examinable objects:
    * Examination returns a description of the object.
    * Descriptions can be conditional, with different results generated from the game state
  * Hierarchical environments:
    * Overworld
      * Acts as a container of regions.
    * Region
      * Acts as a container of rooms.
    * Room
      * The player traverses through the rooms.
      * Provides a description of itself.
      * Supports up to 4 exits. Each exit can be locked until a condition is met.
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
  * N, S, E, W - traverse through the rooms in a region.
  
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
  * Input parsing.
  * State management.
  * Game creation.
  
Maps are automatically generated for regions:

![image](https://user-images.githubusercontent.com/129943363/232739597-cece8804-095b-4ee8-a945-dc66d1888e7f.png)

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
var player = new PlayableCharacter(new Identifier("Link"), new Description("A young Kokiri boy on a quest to save Princess Zelda."));

// create region maker and add room
var regionMaker = new RegionMaker(new Identifier("Death Mountain"), new Description("An imposing volcano just East of Castle Town."))
{
    [0, 0] = new Room(new Identifier("Cavern"), new Description("A dark cavern set in to the base of the mountain."))
};

// create overworld maker
var overworldMaker = new OverworldMaker(new Identifier("Hyrule"), new Description("An ancient kingdom."), regionMaker);

// create callback for generating games
var gameCreator = Game.Create("Zelda",
    "A very low budget Zelda adventure.",
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
