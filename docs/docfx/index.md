---
_layout: landing
---
# BP.AdventureFramework
A C# library that provides a framework for building text adventures and interactive stories in .NET.

## Overview
BP.AdventureFramework is a .NET Standard 2.0 implementation of a framework for building text based adventures.

At its core BP.AdventureFramework provides simple classes for developing game elements:

### Environments
Environments are broken down in to three elements - Overworld, Region and Room. An Overworld contains one or more Regions. A Region contains one or more Rooms. 
A Room can contain up to six exits (north, south, east, west, up and down).

```
Overworld
├── Region
│   ├── Room
│   ├── Room
│   ├── Room
├── Region
│   ├── Room
│   ├── Room
```

### Exits
Rooms contain exits. Exits can be locked to block progress through the game.

```csharp
// create a test room
var room = new Room("Test Room", "A test room.");
        
// add an exit to the north
room.AddExit(new Exit(Direction.North));
```

### Items
Items add richness the game. Items support interaction with the player, rooms, other items and NPC's. Items can morph in to other items. 
For example, using item A on item B may cause item B to morph into item C.

```csharp
var sword = new Item("Sword", "The heroes sword.");
```

### Playable Character
Each BP.AdventureFramework game has a single playable charcter. This who the player controls.

```csharp
var player = new PlayableChracter("Dave", "The hero of the story.");
```

### Non-playable Characters
Non-playable characters (NPC's) can be added to rooms and can help drive the narrative. NPC's can hold conversations, contains items, 
and interact with items.

```csharp
var npc = new NonPlayableChracter("Gary", "The antagonist of the story.");
```
  
### Commands
  
BP.AdventureFramework provides commands for interacting with game elements:
  * **Drop X** - drop an item.
  * **Examine X** - allows items, characters and environments to be examined.
  * **Take X** - take an item.
  * **Talk to X** - talk to a NPC, where X is the NPC.
  * **Use X on Y** - use an item. Items can be used on a variety of targets. Where X is the item and Y is the target.
  * **N, S, E, W, U, D** - traverse through the rooms in a region.

BP.AdventureFramework also provides global commands to help with game flow and option management:
  * **About** - display version information.
  * **CommandsOn / CommandsOff** - toggle commands on/off.
  * **Exit** - exit the game.
  * **Help** - display the help screen.
  * **KeyOn / KeyOff** - turn the Key on/off.
  * **Map** - display the map.
  * **New** - start a new game.

Custom commands can be added to games without the need to extend the existing interpretation.

### Interpretation

BP.AdventureFramework provides classes for handling interpretation of input. Interpretation is extensible with the ability for custom interpreters to be added outside of the core BP.AdventureFramework library.

### Conversations

Conversations can be held between the player and a NPC. Conversations support multiple lines of dialogue and responses.
  
### Rendering

BP.AdventureFramework provides frames for rendering the various game screens. These are fully extensible and customisable. These include:
   * Scene frame.
   * Help frame.
   * Map frame.
   * Title frame.
   * Completion frame.
   * Game over frame.
   * Transition frame.
   * Conversation frame.

### Maps
  
Maps are automatically generated for regions and rooms, and can be viewed with the **map** command:

Maps display visited rooms, exits, player position, if an item is in a room, lower floors and more.
