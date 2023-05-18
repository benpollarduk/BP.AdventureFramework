namespace BP.AdventureFramework.Assets
{
    /// <summary>
    /// Represents any object that is visible to a player.
    /// </summary>
    public interface IPlayerVisible
    {
        /// <summary>
        /// Get or set if this is visible to the player.
        /// </summary>
        bool IsPlayerVisible { get; set; }
    }
}
