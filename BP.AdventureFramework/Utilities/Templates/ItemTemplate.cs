using System;
using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Locations;

namespace BP.AdventureFramework.Utilities.Templates
{
    /// <summary>
    /// Provides a template class to help with the creation of items.
    /// </summary>
    public class ItemTemplate
    {
        #region Methods

        /// <summary>
        /// Instantiate a new instance of the item.
        /// </summary>
        /// <returns>The item.</returns>
        public virtual Item Instantiate()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
