# Introduction 
.NET Standard 2.0 implementation of a framework for building text based adventures. This was originally developed in 2011 but has had some quality of life updates.

![image](https://user-images.githubusercontent.com/129943363/230678655-a1c76828-997c-4bce-913e-70fc83889029.png)

Provides simple classes for developing game elements:
  * Hierarchical environments constructed from:
    * Overworld
    * Region
    * Room
  * NPC's that support interaction with items and the player through conversation.
  * Items
    * Support interaction with the player, rooms, other items, NPC's.
    * Items can morph in to other items. For example, using item A on item B may cause item B to morph into item C.
  
Provides keywords for interacting with game elements:
  * Drop - drop an item.
  * Examine - allows items, characters and environments to be examined.
  * On - used in conjunction with the Use keyword to use an item on a character, another item or environment.
  * Take - take an item.
  * Talk to - talk to a NPC.
  * Use - use an item.
  
Provides global commands to help with game flow:
  * About - display version information.
  * CommandsOn / CommandsOff - toggle commands on/off.
  * Exit - exit the game.
  * Help - display help.
  * KeyOn / KeyOff - turn the Key on/off.
  * Map - display the map.
  * New - start a new game.

All game management is provided by the framework, including title screens, completion screens and parsing of user input.
  
Maps are automatically generated for regions:

![image](https://user-images.githubusercontent.com/129943363/230676860-4bb57929-61a3-43d4-9c24-9b824b45bafc.png)

# Prerequisites
 * Windows
   * Download free IDE Visual Studio 2022 Community ( >> https://visualstudio.microsoft.com/de/vs/community/ ), or use commercial Visual Studio 2022 Version.

# Getting Started
 * Clone the repo
 * Build all projects
 * Run the BP.AdventureFramework.Tutorial project

# Contribute
It´s Open Source (License >> MIT), please feel free to use or contribute. To raise a pull request visit https://github.com/ben-pollard-uk/adventure-framework/pulls.

# For Open Questions
Visit https://github.com/ben-pollard-uk/adventure-framework/issues
