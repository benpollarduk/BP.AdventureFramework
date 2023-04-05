using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public static Boolean UseSounds
        {
            get { return SoundPlayer.useSounds; }
            set { SoundPlayer.useSounds = value; }
        }

        /// <summary>
        /// Get or set if souds should be used
        /// </summary>
        private static Boolean useSounds = true;

        #endregion
    }
}
