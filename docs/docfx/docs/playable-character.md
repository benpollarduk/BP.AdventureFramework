# PlayableCharacter

## Overview

A PlayableCharacter represents the character that the player plays as throughout the game. Each game has only a single PlayableCharacter.

## Use

A PlayableCharacter can be simply instantiated with a name and description.

```csharp
var player = new PlayableCharacter("Ben", "A 39 year old man.");
```

A PlayableCharacter can be also be instantiated with a list of Items.

```csharp
var player = new PlayableCharacter("Ben", "A 39 year old man.");
```

Rooms can contains custom commands that allow the user to directly interact with the Room:

```csharp
room.Commands =
[
    new CustomCommand(new CommandHelp("Pull lever", "Pull the lever."), true, (game, args) =>
    {
        room.FindExit(Direction.East, true, out var exit);
        exit.Unlock();
        return new Reaction(ReactionResult.OK, "The exit was unlocked.");
    })
];
```
