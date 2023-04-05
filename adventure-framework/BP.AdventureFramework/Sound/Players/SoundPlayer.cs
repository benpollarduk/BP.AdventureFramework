namespace AdventureFramework.Sound.Players
{
    /// <summary>
    /// Represent a sgeneric sound player
    /// </summary>
    public abstract class SoundPlayer
    {
        #region StaticProperties

        /// <summary>
        /// Get or set if souds should be used
        /// </summary>
        public static bool UseSounds
        {
            get { return useSounds; }
            set { useSounds = value; }
        }

        /// <summary>
        /// Get or set if souds should be used
        /// </summary>
        private static bool useSounds = true;

        #endregion
    }
}