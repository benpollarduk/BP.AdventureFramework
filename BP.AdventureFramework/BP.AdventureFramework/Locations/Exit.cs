using System;
using System.Xml;
using BP.AdventureFramework.Interaction;

namespace BP.AdventureFramework.Locations
{
    /// <summary>
    /// Represents an exit from a GameLocation.
    /// </summary>
    public class Exit : ExaminableObject
    {
        #region Properties

        /// <summary>
        /// Get the direction of the exit.
        /// </summary>
        public CardinalDirection Direction { get; protected set; }

        /// <summary>
        /// Get if this Exit is locked.
        /// </summary>
        public bool IsLocked { get; protected set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Exit class.
        /// </summary>
        /// <param name="direction">The direction of the exit.</param>
        public Exit(CardinalDirection direction)
        {
            Direction = direction;
            IsLocked = false;
            Description = GenerateDescription();
        }

        /// <summary>
        /// Initializes a new instance of the Exit class.
        /// </summary>
        /// <param name="direction">The direction of the exit.</param>
        /// <param name="isLocked">If this exit is locked.</param>
        public Exit(CardinalDirection direction, bool isLocked) : this(direction)
        {
            IsLocked = isLocked;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Generate a description for this exit.
        /// </summary>
        /// <returns>The completed Description.</returns>
        protected Description GenerateDescription()
        {
            return new ConditionalDescription($"The exit {Direction.ToString().ToLower()} is locked", $"The exit {Direction.ToString().ToLower()} is unlocked", () => IsLocked);
        }

        /// <summary>
        /// Set if this exit is locked.
        /// </summary>
        public void Unlock()
        {
            IsLocked = false;
        }

        #endregion
    }
}