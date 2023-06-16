using System;
using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Locations;

namespace BP.AdventureFramework.Utilities.Templates
{
    /// <summary>
    /// Provides a template class to help with the creation of non-playable characters.
    /// </summary>
    /// <typeparam name="TDerived">The derived type.</typeparam>
    public class NonPlayableCharacterTemplate<TDerived> where TDerived : NonPlayableCharacterTemplate<TDerived>
    {
        #region Methods

        /// <summary>
        /// Create a new instance of the non-playable character.
        /// </summary>
        /// <param name="pC">The playable character.</param>
        /// <param name="room">The room.</param>
        /// <returns>The non-playable character.</returns>
        protected virtual NonPlayableCharacter OnCreate(PlayableCharacter pC, Room room)
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
        /// Create a new instance of the non-playable character.
        /// </summary>
        /// <param name="pC">The playable character.</param>
        /// <param name="room">The room.</param>
        /// <returns>The non-playable character.</returns>
        public static NonPlayableCharacter Create(PlayableCharacter pC, Room room)
        {
            return GetInstance().OnCreate(pC, room);
        }

        /// <summary>
        /// Get an identifier for the templated non-playable character.
        /// </summary>
        /// <param name="pC">The playable character.</param>
        /// <param name="room">The room.</param>
        /// <returns>The identifier for the templated room.</returns>
        public static Identifier GetIdentifier(PlayableCharacter pC, Room room)
        {
            return Create(pC, room).Identifier;
        }

        #endregion
    }
}
