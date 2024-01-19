using System;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Locations;

namespace BP.AdventureFramework.Utilities.Templates
{
    /// <summary>
    /// Provides a template class to help with the creation of non-playable characters.
    /// </summary>
    public class NonPlayableCharacterTemplate
    {
        #region Methods

        /// <summary>
        /// Instantiate a new instance of the non-playable character.
        /// </summary>
        /// <returns>The non-playable character.</returns>
        public virtual NonPlayableCharacter Instantiate()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
