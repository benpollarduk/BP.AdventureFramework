using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventureFramework.Interaction
{
    /// <summary>
    /// Represents an object that can talk
    /// </summary>
    public interface ITalkative
    {
        #region Methods

        /// <summary>
        /// Talk to this object
        /// </summary>
        /// <returns>A string representing the conversation</returns>
        String Talk();

        #endregion
    }
}
