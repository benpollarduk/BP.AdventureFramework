# Global Commands

## Overview
There are three main types of Command.
* **Game Commands** are used to interact with the game.
* **Global Commands** are used to interact with the program running the game.
* **Custom Commands** allow developers to add custom commands to the game without having to worry about extended the games interpreters.

## Game Commands

### Drop
Allows players to drop an item. **R** can be used as a shortcut.

```
drop sword
```

The player can also drop **all** items.

```
drop all
```

### Examine
Allows players to examine any asset. **X** can be used as a shortcut.

Examine will examine the current room.

```
examine
```

The player themselves can be examined with **me** or the players name.

```
examine me
```

or

```
examine ben
```

The same is true for Regions, Overworlds, Items and Exits.

### Take
Allows the player to take an Item. **T** can be used as a shortcut.

```
take sword
```

Take **all** allows the player to take all takeables Items in the current Room.

```
take all
```

### Talk
Talk allows the player to start a conversation with a NonPlayableCharacter. **L** can be used as a shortcut.

If only a single NonPlayableCharacter is in the current Room no argument needs to be specified.

```
talk
```

However, if the current Room contains two or more NonPlayableCharacters then **to** and the NonPlayableCharacters name must be specified.

```
talk to dave
```

### Use
Use allows the player to use the Items that the player has or that are in the current Room.

```
use sword
```

Items can be used on the Player, the Room, an Exit, a NonPlayableCharacter or another Item. The target must be specified with the **on** keyword.

```
use sword on me
```

Or

```
use sword on bush
```

### Move
Regions are traversed with direction commands.

* **North** or **N** moves north.
* **East** or **E** moves east.
* **South** or **S** moves south.
* **West** or **W** moves west.
* **Down** or **D** moves down.
* **Up** or **U** moves up.

### End
Only valid during a conversation with a NonPlayableCharacter, the End command will end the conversation.

```
end
```

## Global Commands

### About
Displays a screen containing information about the game.

```
about
```

### CommandsOn / CommandsOff
Toggles the display of the contextual commands on the screen on and off.

```
commandson
```

Or

```
commandsoff
```

### Exit
Exit the current game.

```
exit
```

### Help
Displays a Help screen listing all available commands.

```
help
```

### KeyOn / KeyOff
Toggles the display of the map key on and off.

```
keyon
```

Or

```
keyoff
```

### Map
Displays the Region map screen.

```
map
```

### New
Starts a new game.

```
new
```

## Custom Commands
Custom commands can be added to many of the assets, including Room, PlayableCharacter, NonPlayableCharacter, Item and Exit.
