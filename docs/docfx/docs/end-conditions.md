# End Conditions

## Overview
The **EndCheck** class allows the game to determine if it has come to an end. Each game has two end conditions
* **GameOverCondition** when the game is over, but has not been won.
* **CompletionCondition** when the game is over because it has been won.

## Use
When an **EndCheck** is invoked it returns an **EndCheckResult**. The **EndCheckResult** details the result of the check to see if the game has ended.

```csharp
private static EndCheckResult IsGameOver(Game game)
{
    if (game.Player.IsAlive)
        return EndCheckResult.NotEnded;

    return new EndCheckResult(true, "Game Over", "You died!");
}
```

This method can be used as an **EndCheck**:

```csharp
EndCheck gameOverCheck = IsGameOver;
```

The **GameOverCondition** and **CompletionCondition** are passed in to the game as arguments when a game is created.
