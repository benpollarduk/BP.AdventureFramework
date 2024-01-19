using System;
using BP.AdventureFramework.Assets.Characters;

namespace BP.AdventureFramework.Utilities.Templates
{
    /// <summary>
    /// Provides a template class to help with the creation of playable characters.
    /// </summary>
    public class PlayableCharacterTemplate
    {
        #region Methods

        /// <summary>
        /// Instantiate a new instance of the playable character.
        /// </summary>
        /// <returns>The playable character.</returns>
        public virtual PlayableCharacter Instantiate()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
