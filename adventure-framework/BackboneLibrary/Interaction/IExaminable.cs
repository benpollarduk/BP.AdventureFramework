using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventureFramework.Interaction
{
    /// <summary>
    /// Represents anything that is examinable
    /// </summary>
    public interface IExaminable
    {         
        #region Properties

        /// <summary>
        /// Get the name of this object
        /// </summary>
        String Name
        {
            get;
        }

        /// <summary>
        /// Get or set a description of this object
        /// </summary>
        Description Description
        {
            get;
            set;
        }

        /// <summary>
        /// Get if this is player visible
        /// </summary>
        Boolean IsPlayerVisible
        {
            get;
            set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Examine this object
        /// </summary>
        /// <returns>A ExaminationResult detailing the examination of this object</returns>
        ExaminationResult Examime();
        
        #endregion
    }
}
