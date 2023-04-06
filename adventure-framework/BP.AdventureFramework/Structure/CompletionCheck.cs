namespace BP.AdventureFramework.Structure
{
    /// <summary>
    /// Represents the callback used for completion checks.
    /// </summary>
    /// <param name="game">The Game to check for completion.</param>
    /// <returns>Returns if the condition if fulfilled.</returns>
    public delegate bool CompletionCheck(Game game);
}