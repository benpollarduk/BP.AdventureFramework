using System;
using BP.AdventureFramework.Assets.Locations;

namespace BP.AdventureFramework.Utilities.Templates
{
    /// <summary>
    /// Provides a template class to help with the creation of regions.
    /// </summary>
    public class RegionTemplate
    {
        #region Methods

        /// <summary>
        /// Instantiate a new instance of the region.
        /// </summary>
        /// <returns>The region.</returns>
        public virtual Region Instantiate()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
