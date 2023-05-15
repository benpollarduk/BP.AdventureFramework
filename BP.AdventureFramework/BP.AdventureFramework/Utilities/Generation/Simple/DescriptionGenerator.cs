using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Extensions;

namespace BP.AdventureFramework.Utilities.Generation.Simple
{
    /// <summary>
    /// Provides a description generator.
    /// </summary>
    internal sealed class DescriptionGenerator : IDescriptionGenerator
    {
        #region Implementation of IDescriptionGenerator

        /// <summary>
        /// Generate a description.
        /// </summary>
        /// <param name="identifier">The identifier to generate the description for.</param>
        /// <returns>The description.</returns>
        public Description Generate(Identifier identifier)
        {
            if (identifier == null || string.IsNullOrEmpty(identifier.Name))
                return new Description("An empty void.");

            return new Description($"{identifier.Name.GetObjectifier().ToSentenceCase()} {identifier.Name.ToLower()}.");
        }

        #endregion
    }
}
