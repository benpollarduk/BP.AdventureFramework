using System;
using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Commands;

namespace BP.AdventureFramework.Utilities.Generation
{
    /// <summary>
    /// Represents a generated examinable.
    /// </summary>
    internal sealed class GeneratedExaimnable : IExaminable
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the GeneratedExaimnable class.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <param name="description">The description of the player.</param>
        internal GeneratedExaimnable(string identifier, string description) : this(new Identifier(identifier), new Description(description))
        {
        }

        /// <summary>
        /// Initializes a new instance of the GeneratedExaimnable class.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <param name="description">The description of the player.</param>
        internal GeneratedExaimnable(Identifier identifier, Description description)
        {
            Identifier = identifier;
            Description = description;
        }

        #endregion

        #region Implementation of IExaminable

        /// <summary>
        /// Get this objects identifier.
        /// </summary>
        public Identifier Identifier { get; }

        /// <summary>
        /// Get or set a description of this object.
        /// </summary>
        public Description Description { get; set; }

        /// <summary>
        /// Get if this is visible to the player.
        /// </summary>
        public bool IsPlayerVisible { get; set; }

        /// <summary>
        /// Get or set this objects commands.
        /// </summary>
        public CustomCommand[] Commands { get; set; }

        /// <summary>
        /// Examine this object.
        /// </summary>
        /// <returns>A ExaminationResult detailing the examination of this object.</returns>
        public ExaminationResult Examine()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
