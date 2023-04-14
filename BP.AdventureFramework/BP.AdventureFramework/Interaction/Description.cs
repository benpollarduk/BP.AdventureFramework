namespace BP.AdventureFramework.Interaction
{
    /// <summary>
    /// Represents a description of an object.
    /// </summary>
    public class Description
    {
        #region StaticProperties

        /// <summary>
        /// Get an empty description.
        /// </summary>
        public static Description Empty { get; } = new Description(string.Empty);

        #endregion

        #region Properties

        /// <summary>
        /// Get or set the description.
        /// </summary>
        protected string DefaultDescription { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Description class
        /// </summary>
        /// <param name="description">The description</param>
        public Description(string description)
        {
            DefaultDescription = description;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get the description.
        /// </summary>
        /// <returns>The description.</returns>
        public virtual string GetDescription()
        {
            return DefaultDescription;
        }

        #endregion
    }
}