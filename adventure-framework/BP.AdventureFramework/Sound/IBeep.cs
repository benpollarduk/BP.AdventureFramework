namespace AdventureFramework.Sound
{
    /// <summary>
    /// Interface for mother board beeps
    /// </summary>
    public interface IBeep
    {
        /// <summary>
        /// Get or set the duration in ms
        /// </summary>
        int Duration { get; set; }

        /// <summary>
        /// Get or set the frequency in htZ
        /// </summary>
        int Frequency { get; set; }
    }
}