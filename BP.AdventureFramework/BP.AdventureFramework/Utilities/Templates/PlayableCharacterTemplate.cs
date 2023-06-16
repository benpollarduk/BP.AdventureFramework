using System;
using BP.AdventureFramework.Assets.Characters;

namespace BP.AdventureFramework.Utilities.Templates
{
    /// <summary>
    /// Provides a template class to help with the creation of playable characters.
    /// </summary>
    /// <typeparam name="TDerived">The derived type.</typeparam>
    public class PlayableCharacterTemplate<TDerived> where TDerived : PlayableCharacterTemplate<TDerived>
    {
        #region Methods

        /// <summary>
        /// Create a new instance of the playable character.
        /// </summary>
        /// <returns>The playable character.</returns>
        protected virtual PlayableCharacter OnCreate()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Get an instance of the derived type.
        /// </summary>
        /// <returns>The instance.</returns>
        private static TDerived GetInstance()
        {
            var type = typeof(TDerived);
            return (TDerived)Activator.CreateInstance(type);
        }

        /// <summary>
        /// Create a new instance of the playable character.
        /// </summary>
        /// <returns>The playable character.</returns>
        public static PlayableCharacter Create()
        {
            return GetInstance().OnCreate();
        }

        #endregion
    }
}
