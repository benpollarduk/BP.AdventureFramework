using System;
using BP.AdventureFramework.Assets;

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
