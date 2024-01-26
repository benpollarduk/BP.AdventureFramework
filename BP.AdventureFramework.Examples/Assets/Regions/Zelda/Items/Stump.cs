using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Utilities;

namespace BP.AdventureFramework.Examples.Assets.Regions.Zelda.Items
{
    public class Stump : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Stump";
        private const string Description = "A small stump of wood.";

        #endregion

        #region Implementation of IAssetTemplate<Item>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The item.</returns>
        public Item Instantiate()
        {
            var stump = new Item(Name, Description);

            stump.Interaction = item =>
            {
                if (Shield.Name.EqualsExaminable(item))
                {
                    return new InteractionResult(InteractionEffect.NoEffect, item, "You hit the stump, and it makes a solid knocking noise.");
                }

                if (Sword.Name.EqualsExaminable(item))
                {
                    stump.Morph(new SplintersOfWood().Instantiate());
                    return new InteractionResult(InteractionEffect.ItemMorphed, item, "You chop the stump into tiny pieces in a mad rage. All that is left is some splinters of wood.");
                }

                return new InteractionResult(InteractionEffect.NoEffect, item);
            };

            return stump;
        }

        #endregion
    }
}
