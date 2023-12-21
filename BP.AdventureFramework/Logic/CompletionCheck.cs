namespace BP.AdventureFramework.Logic
{
    /// <summary>
    /// Represents the callback used for completion checks.
    /// </summary>
    /// <param name="game">The Game to check for completion.</param>
    /// <returns>Returns a result from the check.</returns>
    public delegate CompletionCheckResult CompletionCheck(Game game);
}