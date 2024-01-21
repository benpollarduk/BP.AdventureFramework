using System;
using System.Linq;
using BP.AdventureFramework.Extensions;

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
        /// Get the regions in this overworld.
        /// </summary>
        public Region[] Regions { get; private set; } = Array.Empty<Region>();

        /// <summary>
        /// Get the current region.
        /// </summary>
        public Region CurrentRegion
        {
            get { return currentRegion ?? (Regions.Length > 0 ? Regions[0] : null); }
            private set { currentRegion = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the overworld class.
        /// </summary>
        /// <param name="identifier">The identifier for this overworld.</param>
        /// <param name="description">A description of this overworld.</param>
        public Overworld(string identifier, string description) : this(new Identifier(identifier), new Description(description))
        {
        }

        /// <summary>
        /// Initializes a new instance of the overworld class.
        /// </summary>
        /// <param name="identifier">The identifier for this overworld.</param>
        /// <param name="description">A description of this overworld.</param>
        public Overworld(Identifier identifier, Description description)
        {
            Identifier = identifier;
            Description = description;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Add a region to this overworld.
        /// </summary>
        /// <param name="region">The region to add.</param>
        public void AddRegion(Region region)
        {
            Regions = Regions.Add(region);
        }

        /// <summary>
        /// Remove a region from this overworld.
        /// </summary>
        /// <param name="region">The region to remove.</param>
        public void RemoveRegion(Region region)
        {
            Regions = Regions.Remove(region);
        }

        /// <summary>
        /// Find a region.
        /// </summary>
        /// <param name="regionName">The regions name.</param>
        /// <param name="region">The region.</param>
        /// <returns>True if the region was found.</returns>
        public bool FindRegion(string regionName, out Region region)
        {
            var regions = Regions.Where(regionName.EqualsExaminable).ToArray();

            if (regions.Length > 0)
            {
                region = regions[0];
                return true;
            }

            region = null;
            return false;
        }

        /// <summary>
        /// Move to a region.
        /// </summary>
        /// <param name="region">The region to move to.</param>
        /// <returns>True if the region could be moved to, else false.</returns>
        public bool Move(Region region)
        {
            if (!Regions.Contains(region))
                return false;

            CurrentRegion = region;
            return true;
        }
    
        #endregion

        #region Overrides of ExaminableObject

        /// <summary>
        /// Examine this object.
        /// </summary>
        /// <returns>A ExaminationResult detailing the examination of this object.</returns>
        public override ExaminationResult Examine()
        {
            return new ExaminationResult(Description.GetDescription());
        }

        #endregion
    }
}