using System;

namespace BP.AdventureFramework.Interaction
{
    /// <summary>
    /// Provides a class that can be used as an identifier.
    /// </summary>
    public class Identifier : IEquatable<string>, IEquatable<Identifier>
    {
        #region Properties

        /// <summary>
        /// Get the name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Get the name as a case insensitive identifier.
        /// </summary>
        public string IdentifiableName => Name.ToUpper().Replace(" ", string.Empty);

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

        #region Implementation of IEquatable<string>

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
        public bool Equals(string other)
        {
            return Name == other || IdentifiableName == other;
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
    }
}
