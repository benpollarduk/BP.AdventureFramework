using BP.AdventureFramework.Interaction;

namespace BP.AdventureFramework.Locations
{
    /// <summary>
    /// Represents any location within the game.
    /// </summary>
    public class GameLocation : ExaminableObject
    {
        #region Properties

        /// <summary>
        /// Get or set the row of this Room within it's parent location.
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// Get or set the column of this Room within it's parent location.
        /// </summary>
        public int Column { get; set; }

        /// <summary>
        /// Get if this location has been visited.
        /// </summary>
        public bool HasBeenVisited { get; protected set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the GameLocation class
        /// </summary>
        public GameLocation()
        {
        }

        /// <summary>
        /// Initializes a new instance of the GameLocation class.
        /// </summary>
        /// <param name="columnWithinParnt">The column this GameLocation resides within its parent.</param>
        /// <param name="rowWithinParent">The row this GameLocation resides within it's parent.</param>
        public GameLocation(int columnWithinParnt, int rowWithinParent)
        {
            Column = columnWithinParnt;
            Row = rowWithinParent;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handle movement into this GameLocation.
        /// </summary>
        /// <param name="fromDirection">The direction movement into this GameLocation is from. Use null if there should be no direction.</param>
        public virtual void MovedInto(CardinalDirection? fromDirection)
        {
            HasBeenVisited = true;
        }

        #endregion
    }
}