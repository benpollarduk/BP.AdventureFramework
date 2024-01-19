using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Regions.Zelda.Items
{
    public class TailKey : ItemTemplate
    {
        #region Constants

        internal const string Name = "Tail Key";
        private const string Description = "A small key, with a complex handle in the shape of a worm like creature.";

        #endregion

        #region Overrides of ItemTemplate

        /// <summary>
        /// Instantiate a new instance of the item.
        /// </summary>
        /// <returns>The item.</returns>
        public override Item Instantiate()
        {
            return new Item(Name, Description, true);
        }

        #endregion
    }
}
