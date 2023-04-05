namespace BP.AdventureFramework.Interaction
{
    /// <summary>
    /// Represents a description of an object.
    /// </summary>
    public class Description
    {
        #region Properties

        /// <summary>
        /// Get the description.
        /// </summary>
        public string DefaultDescription { get; protected set; }

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
    }
}