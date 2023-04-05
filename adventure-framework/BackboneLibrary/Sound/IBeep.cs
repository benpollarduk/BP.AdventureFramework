using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        Int32 Duration
        {
            get;
            set;
        }

        /// <summary>
        /// Get or set the frequency in htZ
        /// </summary>
        Int32 Frequency
        {
            get;
            set;
        }
    }
}
