using System;
using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Locations;

namespace BP.AdventureFramework.Utilities.Templates
{
    /// <summary>
    /// Provides a template class to help with the creation of regions.
    /// </summary>
    /// <typeparam name="TDerived">The derived type.</typeparam>
    public class RegionTemplate<TDerived> where TDerived : RegionTemplate<TDerived>
    {
        #region Methods

        /// <summary>
        /// Create a new instance of the region.
        /// </summary>
        /// <param name="pC">The playable character.</param>
        /// <returns>The region.</returns>
        protected virtual Region OnCreate(PlayableCharacter pC)
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
        /// Create a new instance of the region.
        /// </summary>
        /// <param name="pC">The playable character.</param>
        /// <returns>The region.</returns>
        public static Region Create(PlayableCharacter pC)
        {
            return GetInstance().OnCreate(pC);
        }

        /// <summary>
        /// Get an identifier for the templated room.
        /// </summary>
        /// <param name="pC">The playable character.</param>
        /// <returns>The identifier for the templated room.</returns>
        public static Identifier GetIdentifier(PlayableCharacter pC)
        {
            return Create(pC).Identifier;
        }

        #endregion
    }
}
