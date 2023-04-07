# Introduction 
.NET 4.6.1 implementation of a framework for building text based adventures. This was originally developed in 2011 but has had some quality of life updates.

![image](https://user-images.githubusercontent.com/129943363/230678655-a1c76828-997c-4bce-913e-70fc83889029.png)

Provides simple classes for developing game elements:
  * Hierarchical environments with the following elements:
    * Overworld
    * Region
    * Room
  * NPC's which can be interacted with either with items or conversation.
  * Items which can be interacted with.
    * Items can morph in to other items. For example, using item A on item B may cause item B to morph in to item C.
  
Provides keywords for interacting with game elements:
  * Drop - drop an item.
  * Examine - allows items, characters and environments to be examined.
  * Use - use an item.
  * On - used in conjunction with the Use keyword to use an item on a character, another item or environment.
  * Talk to - talk to a NPC.
  * Take - take an item.
  
Provides global commands to help with game flow:
  * About - display version information.
  * CommandsOn / CommandsOff - toggle commands on/off.
  * Exit - exit the game.
  * Help - display help.
  * Invert - invert the console colours.
  * KeyOn / KeyOff - turn the Key on/off.
  * Map - display the map.
  * New - start a new game.
  
Maps are automatically generated for regions:

![image](https://user-images.githubusercontent.com/129943363/230676860-4bb57929-61a3-43d4-9c24-9b824b45bafc.png)

A simple ASCII art engine supports conversion of images to ASCII art that can be displayed when an element is examined. This also supports basic animation.

![image](https://user-images.githubusercontent.com/129943363/230678519-6ff42c27-7322-43ca-8151-977417102b7f.png)

# Prerequisites
 * Windows
   * Download free IDE Visual Studio 2022 Community ( >> https://visualstudio.microsoft.com/de/vs/community/ ), or use commercial Visual Studio 2022 Version.

# Getting Started
 * Clone the repo
 * Build all projects
 * Run the BP.AdventureFramework.Demo project

# Contribute
It´s Open Source (License >> MIT), please feel free to use or contribute. To raise a pull request visit https://github.com/ben-pollard-uk/adventure-framework/pulls.

# For Open Questions
Visit https://github.com/ben-pollard-uk/adventure-framework/issues
