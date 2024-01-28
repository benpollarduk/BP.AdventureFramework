# NonPlayableCharacter

## Overview

A NonPlayableCharacter represents any character that the player may meet throughout the game.

## Use

A NonPlayableCharacter can be simply instantiated with a name and description.

```csharp
var goblin = new NonPlayableCharacter("Goblin", "A vile goblin.");
```

A NonPlayableCharacter can give an item to another NonPlayableCharacter.

```csharp
var daisy = new Item("Daisy", "A beautiful daisy that is sure to cheer up even the most miserable creature.");
npc.Give(daisy, goblin);
```

NonPlayableCharacters can contain custom commands that allow the user to directly interact with the character or other assets.

```csharp
goblin.Commands =
[
    new CustomCommand(new CommandHelp("Smile", "Crack a smile."), true, (game, args) =>
    {
        return new Reaction(ReactionResult.OK, "Well that felt weird.");
    })
];
```

## Conversations

A NonPlayableCharacter can hold a conversation with the player. 
* A Conversation contains **Paragraphs**. 
* A Paragraph can contain one or more **Responses**.
* A **Response** can contain a delta to shift the conversation by, which will cause the conversation to jump paragraphs by the specified value.
* A **Response** can also contain a callback to perform some action when the player selects that option.

```csharp
goblin.Conversation = new Conversation(
    new Paragraph("This is a the first line."),
    new Paragraph("This is a question.")
    {
        Responses =
        [
            new Response("This is the first response." 1),
            new Response("This is the second response.", 2),
            new Response("This is the third response.", 2)
        ]
    },
    new Paragraph("You picked first response, return to start of conversation.", -2),
    new Paragraph("You picked second response, return to start of conversation., -2),
    new Paragraph("You picked third response, you are dead., game => game.Player.Kill())
);
```