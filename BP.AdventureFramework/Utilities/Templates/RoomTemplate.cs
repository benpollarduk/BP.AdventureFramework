using System;
using BP.AdventureFramework.Assets.Locations;

namespace BP.AdventureFramework.Utilities.Templates
{
    /// <summary>
    /// Provides a template class to help with the creation of rooms.
    /// </summary>
    public class RoomTemplate
    {
        #region Methods

        /// <summary>
        /// Instantiate a new instance of the room.
        /// </summary>
        /// <returns>The room.</returns>
        public virtual Room Instantiate()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
