using System;

namespace BP.AdventureFramework.Utilities.Generation
{
    /// <summary>
    /// Represents any object that is a room generator.
    /// </summary>
    public interface IRoomGenerator
    {
        /// <summary>
        /// Generate the rooms.
        /// </summary>
        /// <param name="regionMaker">The region maker.</param>
        /// <param name="generator">The generator.</param>
        /// <param name="options">The game generation options.</param>
        void GenerateRooms(RegionMaker regionMaker, Random generator, GameGenerationOptions options);
    }
}
