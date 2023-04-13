using BP.AdventureFramework.Interaction;

namespace BP.AdventureFramework.Extensions
{
    /// <summary>
    /// Provides extension methods for strings.
    /// </summary>
    public static class StringExtensions
    {
        #region Extensions

        /// <summary>
        /// Returns this string as a Description.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This string as a description.</returns>
        public static Description ToDescription(this string value)
        {
            return new Description(value);
        }

        /// <summary>
        /// Returns this string as an Identifier.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This string as an identifier.</returns>
        public static Identifier ToIdentifier(this string value)
        {
            return new Identifier(value);
        }

        /// <summary>
        /// Determine if this string equals an identifier.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="identifier">The identifier.</param>
        /// <returns>True if this string equals the identifier, else false.</returns>
        public static bool EqualsIdentifier(this string value, Identifier identifier)
        {
            return identifier.Equals(value);
        }

        /// <summary>
        /// Determine if this string equals an IExaminable.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="examinable">The examinable.</param>
        /// <returns>True if this string equals the identifier, else false.</returns>
        public static bool EqualsExaminable(this string value, IExaminable examinable)
        {
            return examinable.Identifier.Equals(value);
        }

        #endregion
    }
}
