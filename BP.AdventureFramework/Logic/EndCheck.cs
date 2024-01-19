namespace BP.AdventureFramework.Logic
{
    /// <summary>
    /// Represents the callback used for end checks.
    /// </summary>
    /// <param name="game">The game to check for end.</param>
    /// <returns>Returns a result from the check.</returns>
    public delegate EndCheckResult EndCheck(Game game);
}