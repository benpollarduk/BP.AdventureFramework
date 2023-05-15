using System;
using BP.AdventureFramework.Assets;

namespace BP.AdventureFramework.Utilities.Generation
{
    /// <summary>
    /// Represents any object that provides examinable generation.
    /// </summary>
    public interface IExaminableGenerator
    {
        /// <summary>
        /// Generate an examinable.
        /// </summary>
        /// <param name="generator">The generator.</param>
        /// <returns>The generated examinable.</returns>
        IExaminable Generate(Random generator);
    }
}
