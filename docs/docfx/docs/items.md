# Item

## Overview

Items can be used to add interactivity with a game. Items can be something that a player can take with them, or they may be static in a Room.

## Use

An Item can be simply instantiated with a name and description.

```csharp
var sword = new Item("Sword", "A heroes sword.");
```

By default, an Item is not takeable and is tied to a Room. If it is takeable this can be specified in the constructor.

```csharp
var sword = new Item("Sword", "A heroes sword.", true);
```

An Item can morph in to another Item. This is useful in situations where the Item changes state. Morphing is invoked with the **Morph** method. The Item that Morph is invoked on takes on the properties of the Item being morphed into.

```csharp
var brokenSword = new Item("Broken Sword", "A broken sword");
sword.Morph(brokenSword);
```

Like all Examinable objects, an Item can be assigned custom commands.

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

## Interaction

Interactions can be set up between different assets in the game. The **InteractionResult** contains the result of the interaction, and allows the game to react to the interaction.

```csharp
var dartsBoard = new Item("Darts board", "A darts board.");

var dart = new Item("Dart", "A dart")
{
    Interaction = item =>
    {
        if (item == dartsBoard)
            return new InteractionResult(InteractionEffect.SelfContained, item, "The dart stuck in the darts board.");

        return new InteractionResult(InteractionEffect.NoEffect, item);
    }
};
```