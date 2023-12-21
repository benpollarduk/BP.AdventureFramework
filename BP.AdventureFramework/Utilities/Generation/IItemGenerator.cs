using System;
using BP.AdventureFramework.Assets;

namespace BP.AdventureFramework.Utilities.Generation
{
    /// <summary>
    /// Represents any object that can generate items.
    /// </summary>
    public interface IItemGenerator
    {
        /// <summary>
        /// Generate an item.
        /// </summary>
        /// <param name="generator">The generator.</param>
        /// <returns>The generated item.</returns>
        Item Generate(Random generator);
    }
}
