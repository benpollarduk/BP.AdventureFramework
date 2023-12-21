using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Regions.Flat.Items
{
    public class GameCube : ItemTemplate<GameCube>
    {
        #region Constants

        internal const string Name = "GameCube";
        private const string Description = "A Nintendo Gamecube.You pop the disk cover, it looks like someone has been playing Killer7.";

        #endregion

        #region Overrides of ItemTemplate<GameCube>

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
