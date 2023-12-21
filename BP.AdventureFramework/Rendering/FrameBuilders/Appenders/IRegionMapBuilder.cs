using BP.AdventureFramework.Assets.Locations;

namespace BP.AdventureFramework.Rendering.FrameBuilders.Appenders
{
    /// <summary>
    /// Represents any object that can build region maps.
    /// </summary>
    public interface IRegionMapBuilder
    {
        /// <summary>
        /// Build a map of a region.
        /// </summary>
        /// <param name="lineStringBuilder">The string builder to use.</param>
        /// <param name="region">The region.</param>
        /// <param name="availableColumns">The available horizontal space, in columns, to build the map within.</param>
        /// <param name="availableRows">The available vertical space, in rows, to build the map within.</param>
        /// <returns>A map of the region in a string.</returns>
        string BuildRegionMap(LineStringBuilder lineStringBuilder, Region region, int availableColumns, int availableRows);
    }
}
