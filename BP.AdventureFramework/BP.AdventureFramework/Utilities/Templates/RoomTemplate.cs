using System;
using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Locations;

namespace BP.AdventureFramework.Utilities.Templates
{
    /// <summary>
    /// Provides a template class to help with the creation of rooms.
    /// </summary>
    /// <typeparam name="TDerived">The derived type.</typeparam>
    public class RoomTemplate<TDerived> where TDerived : RoomTemplate<TDerived>
    {
        #region Methods

        /// <summary>
        /// Create a new instance of the room.
        /// </summary>
        /// <returns>The room.</returns>
        protected virtual Room OnCreate()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create a new instance of the room.
        /// </summary>
        /// <param name="pC">The playable character.</param>
        /// <returns>The room.</returns>
        protected virtual Room OnCreate(PlayableCharacter pC)
        {
            return OnCreate();
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
        /// Create a new instance of the room.
        /// </summary>
        /// <returns>The room.</returns>
        public static Room Create()
        {
            return GetInstance().OnCreate();
        }

        /// <summary>
        /// Create a new instance of the room.
        /// </summary>
        /// <param name="pC">The playable character.</param>
        /// <returns>The room.</returns>
        public static Room Create(PlayableCharacter pC)
        {
            return GetInstance().OnCreate(pC);
        }

        #endregion
    }
}
