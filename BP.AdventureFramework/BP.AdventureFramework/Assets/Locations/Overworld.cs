using System.Collections.Generic;

namespace BP.AdventureFramework.Assets.Locations
{
    /// <summary>
    /// Represents an entire overworld.
    /// </summary>
    public sealed class Overworld : ExaminableObject
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
            private set { currentRegion = value; }
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