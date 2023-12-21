using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Regions.Zelda.Items
{
    public class TailDoor : ItemTemplate<TailDoor>
    {
        #region Constants

        internal const string Name = "Tail Door";
        private const string Description = "The doorway to the tail cave.";

        #endregion

        #region Overrides of ItemTemplate<TailDoor>

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
