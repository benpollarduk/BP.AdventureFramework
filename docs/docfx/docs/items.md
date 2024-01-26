# Item

## Overview

Items can be used to add interactivity with a game. Items can be something that a player can take with them, or they may be static in a Room.

## Use

An Item can be simply instantiated with a name and description.

```csharp
var exit = new Item("Sword", "A heroes sword.");
```

By default an Item is not takeable and is tied to a Room. If it is takeable this can be specified in the constructor:

```csharp
var item = new Item("Sword", "A heroes sword.", true);
```

Like all Examinable objects, an Item can be assigned custom commands:

```csharp
bomb.Commands =
[
    new CustomCommand(new CommandHelp("Cut wire", "Cut the red wire."), true, (game, args) =>
    {
        game.Player.Kill();
        return new Reaction(ReactionResult.Fatal, "Boom!");
    })
];
```