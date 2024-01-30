# Attributes

## Overview
All examinable objects can have attributes. Attributes provide a way of adding a lot of depth to games. For example, attributes could be used to buy and sell items, contain a characters XP or HP or even provide a way to add durability to items.

## Use
To add to an existing attribute or to create a new one use the **Add** method.

```csharp
var player = new PlayableCharacter("Player", string.Empty);
player.Attributes.Add("$", 10);
```

To subtract from an existing attribute use the **Subtract** method.

```csharp
player.Attributes.Subtract("$", 10);
```

Attributes values can be capped. In this example the $ attribute is limited to a range of 0 - 100. Adding or subtracting will not cause the value of the attribute to change outside of this range.

```csharp
var cappedAttribute = new Attribute("$", "Dollars.", 0, 100);
player.Attributes.Add(cappedAttribute, 50);
```

## An example - buying an Item from a NonPlayableCharacter.
The following is an example of buying an Item from NonPlayableCharacter. Here a trader has a spade. The player can only buy the spade if they have at least $5. The conversation will jump to the correct paragraph based on if they choose to buy the spade or not. If the player chooses to buy the spade and has enough $ the transaction is made and the spade changes hands.

```csharp
const string currency = "$";

var player = new PlayableCharacter("Player", string.Empty);
player.Attributes.Add(currency, 10);

var trader = new NonPlayableCharacter("Trader", string.Empty);
var spade = new Item("Spade", string.Empty);
trader.AcquireItem(spade);

trader.Conversation = new Conversation(
    new Paragraph("What will you buy?")
    {
        Responses =
        [
            new Response("Spade", new ByCallback(() =>
                player.Attributes.GetValue(currency) >= 5
                    ? new ToName("BoughtSpade")
                    : new ToName("NotEnough"))),
            new Response("Nothing", new Last())
        ]
    },
    new Paragraph("Here it is.", _ =>
    {
        player.Attributes.Subtract(currency, 5);
        trader.Attributes.Add(currency, 5);
        trader.Give(spade, player);
    }, new GoTo(0), "BoughtSpade"),
    new Paragraph("You don't have enough money.", new First(), "NotEnough"),
    new Paragraph("Fine.")
);
```
This is just one example of using attributes to add depth to a game.