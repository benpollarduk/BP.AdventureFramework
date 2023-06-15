using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Regions.Flat.Items
{
    public class Toilet : ItemTemplate<Toilet>
    {
        #region Constants

        internal const string Name = "Toilet";
        private const string Description = "A clean looking toilet. You lift the lid to take a look inside... ergh a floater! You flush the toilet but it just churns around! You close the lid and pretend it isn't there.";

        #endregion

        #region Overrides of ItemTemplate<ConchShell>

        /// <summary>
        /// Create a new instance of the item.
        /// </summary>
        /// <returns>The region.</returns>
        protected override Item OnCreate()
        {
            return new Item(Name, Description);
        }

        #endregion
    }
}
