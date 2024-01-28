# Exit

## Overview

An Exit is essentially a connector between to adjoining rooms.

## Use

An Exit can be simply instantiated with a direction.

```csharp
var exit = new Exit(Direction.North);
```

An Exit can be hidden from the player by setting its **IsPlayerVisible** property to false, this can be set in the constructor.

```csharp
var exit = new Exit(Direction.North, false);
```

Or set explicitly.

```csharp
exit.IsPlayerVisible = false;
```

Optionally, a description of the Exit can be specified.

```csharp
var exit = new Exit(Direction.North, true, new Description("A door covered in ivy."));
```

This will be returned if the player examines the Exit.

Like all Examinable objects, an Exit can be assigned custom commands.

```csharp
exit.Commands =
[
    new CustomCommand(new CommandHelp("Shove", "Shove the door."), true, (game, args) =>
    {
        exit.Unlock();
        return new Reaction(ReactionResult.OK, "The door swung open.");
    })
];
```