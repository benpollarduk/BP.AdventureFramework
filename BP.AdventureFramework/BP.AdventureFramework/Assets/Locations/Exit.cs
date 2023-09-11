﻿namespace BP.AdventureFramework.Assets.Locations
{
    /// <summary>
    /// Represents an exit from a GameLocation.
    /// </summary>
    public sealed class Exit : ExaminableObject
    {
        #region Properties

        /// <summary>
        /// Get the direction of the exit.
        /// </summary>
        public Direction Direction { get; }

        /// <summary>
        /// Get if this Exit is locked.
        /// </summary>
        public bool IsLocked { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Exit class.
        /// </summary>
        /// <param name="direction">The direction of the exit.</param>
        /// <param name="isLocked">If this exit is locked.</param>
        /// <param name="description">A description of the exit.</param>
        public Exit(Direction direction, bool isLocked = false, Description description = null)
        {
            Direction = direction;
            Description = description ?? GenerateDescription();
            IsLocked = isLocked;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Generate a description for this exit.
        /// </summary>
        /// <returns>The completed Description.</returns>
        private Description GenerateDescription()
        {
            return new ConditionalDescription($"The exit {Direction.ToString().ToLower()} is locked.", $"The exit {Direction.ToString().ToLower()} is unlocked.", () => IsLocked);
        }

        /// <summary>
        /// Unlock this exit.
        /// </summary>
        public void Unlock()
        {
            IsLocked = false;
        }

        /// <summary>
        /// Lock this exit.
        /// </summary>
        public void Lock()
        {
            IsLocked = true;
        }

        #endregion
    }
}