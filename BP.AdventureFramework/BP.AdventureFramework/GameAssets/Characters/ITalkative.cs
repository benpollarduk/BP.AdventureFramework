namespace BP.AdventureFramework.GameAssets.Characters
{
    /// <summary>
    /// Represents an object that can talk.
    /// </summary>
    public interface ITalkative
    {
        /// <summary>
        /// Talk to this object.
        /// </summary>
        /// <returns>A string representing the conversation.</returns>
        string Talk();
    }
}