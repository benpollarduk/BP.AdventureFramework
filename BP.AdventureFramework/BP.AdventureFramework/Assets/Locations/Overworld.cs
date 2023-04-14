using System.Collections.Generic;
using System.Linq;

namespace BP.AdventureFramework.Assets.Locations
{
    /// <summary>
    /// Represents an entire overworld.
    /// </summary>
    public class Overworld : GameLocation
    {
        #region Fields

        private Region currentRegion;

        #endregion

        #region Properties

        /// <summary>
        /// Get the Regions in this Overworld.
        /// </summary>
        public List<Region> Regions { get; } = new List<Region>();

        /// <summary>
        /// Get the current Region.
        /// </summary>
        public Region CurrentRegion
        {
            get { return currentRegion ?? (Regions.Count > 0 ? Regions[0] : null); }
            protected set { currentRegion = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Overworld class.
        /// </summary>
        /// <param name="identifier">The identifier for this Overworld.</param>
        /// <param name="description">A description of this Overworld.</param>
        public Overworld(Identifier identifier, Description description)
        {
            Identifier = identifier;
            Description = description;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Create a Region in this Overworld.
        /// </summary>
        /// <param name="region">The Region to create.</param>
        /// <param name="columnInOverworld">The column of the Region with this Overworld.</param>
        /// <param name="rowInOverworld">The row of the Region within this Overworld.</param>
        public bool CreateRegion(Region region, int columnInOverworld, int rowInOverworld)
        {
            region.Column = columnInOverworld;
            region.Row = rowInOverworld;

            var addable = Regions.All(r => r.Column != region.Column || r.Row != region.Row);

            if (addable)
                Regions.Add(region);

            return addable;
        }

        #endregion

        #region Overrides of ExaminableObject

        /// <summary>
        /// Examine this object.
        /// </summary>
        /// <returns>A ExaminationResult detailing the examination of this object.</returns>
        public override ExaminationResult Examime()
        {
            return new ExaminationResult(Description.GetDescription());
        }

        #endregion
    }
}