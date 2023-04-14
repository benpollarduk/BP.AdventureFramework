using System;

namespace BP.AdventureFramework.GameAssets
{
    /// <summary>
    /// Provides a class that can be used as an identifier.
    /// </summary>
    public class Identifier : IEquatable<string>, IEquatable<Identifier>
    {
        #region StaticProperties

        /// <summary>
        /// Get an empty identifier.
        /// </summary>
        public static Identifier Empty { get; } = new Identifier(string.Empty);

        #endregion

        #region Properties

        /// <summary>
        /// Get the name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Get the name as a case insensitive identifier.
        /// </summary>
        public string IdentifiableName => ToIdentifiableString(Name);

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of the Identifier class.
        /// </summary>
        /// <param name="name">The name.</param>
        public Identifier(string name)
        {
            Name = name;
        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Convert a string to an identifiable string.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The identifiable string.</returns>
        protected string ToIdentifiableString(string value)
        {
            return value.ToUpper().Replace(" ", string.Empty);
        }

        #endregion

        #region Implementation of IEquatable<string>

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
        public bool Equals(string other)
        {
            return Name == other || IdentifiableName == ToIdentifiableString(other);
        }

        #endregion

        #region Implementation of IEquatable<ExaminableIdentifier>

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
        public bool Equals(Identifier other)
        {
            return IdentifiableName == other?.IdentifiableName;
        }

        #endregion

        #region Overrides of Object

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return Name;
        }

        #endregion
    }
}
