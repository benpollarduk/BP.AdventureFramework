namespace BP.AdventureFramework.Assets.Attributes
{
    /// <summary>
    /// Provides a description of an attribute.
    /// </summary>
    public class Attribute
    {
        #region Properties

        /// <summary>
        /// Get the name of the attribute.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Get the description of the attribute.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Get the minimum limit of the attribute.
        /// </summary>
        public double Minimum { get; }

        /// <summary>
        /// Get the maximum limit of the attribute.
        /// </summary>
        public double Maximum { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initailizes a new instance of the Attribute class.
        /// </summary>
        /// <param name="name">Specify the name of the attibute.</param>
        /// <param name="description">Specify the description of the attibute.</param>
        /// <param name="minimum">Specify the minimum limit of the attibute.</param>
        /// <param name="maximum">Specify the maximum limit of the attibute.</param>
        public Attribute(string name, string description, double minimum, double maximum)
        {
            Name = name;
            Description = description;
            Minimum = minimum;
            Maximum = maximum;
        }

        #endregion
    }
}
