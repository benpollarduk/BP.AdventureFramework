using System;
using BP.AdventureFramework.Assets;

namespace BP.AdventureFramework.Utilities.Generation
{
    /// <summary>
    /// Represents any object that can generate a region.
    /// </summary>
    public interface IRegionGenerator
    {
        /// <summary>
        /// Generate a region.
        /// </summary>
        /// <param name="identifier">The region identifier.</param>
        /// <param name="description">The region description.</param>
        /// <param name="generator">The generator.</param>
        /// <param name="roomGenerator">The room generator.</param>
        /// <param name="takeableItemGenerator">The item generator for takeable items.</param>
        /// <param name="nonTakeableItemGenerator">The item generator for non-takeable items.</param>
        /// <param name="options">The generation options.</param>
        /// <returns>The generated region maker.</returns>
        RegionMaker GenerateRegion(Identifier identifier, Description description, Random generator, IRoomGenerator roomGenerator, IItemGenerator takeableItemGenerator, IItemGenerator nonTakeableItemGenerator, GameGenerationOptions options);
    }
}
