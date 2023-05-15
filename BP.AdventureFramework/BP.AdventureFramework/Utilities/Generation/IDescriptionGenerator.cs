using BP.AdventureFramework.Assets;

namespace BP.AdventureFramework.Utilities.Generation
{
    /// <summary>
    /// Represents a generator for descriptions.
    /// </summary>
    public interface IDescriptionGenerator
    {
        /// <summary>
        /// Generate a description.
        /// </summary>
        /// <param name="identifier">The identifier to generate the description for.</param>
        /// <returns>The description.</returns>
        Description Generate(Identifier identifier);
    }
}
