using System;
using BP.AdventureFramework.Assets;

namespace BP.AdventureFramework.Utils.Generation.Simple
{
    /// <summary>
    /// Provides an item generator.
    /// </summary>
    public sealed class ItemGenerator : IItemGenerator
    {
        #region Properties

        /// <summary>
        /// Get or set the examinable generator.
        /// </summary>
        private IExaminableGenerator ExaminableGenerator { get; }

        /// <summary>
        /// Get or set if the items generated are takeable.
        /// </summary>
        private bool IsTakeable { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ItemGenerator class.
        /// </summary>
        /// <param name="examinableGenerator">An examinable generator.</param>
        /// <param name="isTakeable">True if the generated items are takeable, else false.</param>
        public ItemGenerator(IExaminableGenerator examinableGenerator, bool isTakeable)
        {
            ExaminableGenerator = examinableGenerator;
            IsTakeable = isTakeable;
        }

        #endregion

        #region Implementation of IItemGenerator

        /// <summary>
        /// Generate an item.
        /// </summary>
        /// <param name="generator">The generator.</param>
        /// <returns>The generated item.</returns>
        public Item Generate(Random generator)
        {
            var item = ExaminableGenerator.Generate(generator);
            return item == null ? null : new Item(item.Identifier, item.Description, IsTakeable);
        }

        #endregion
    }
}
