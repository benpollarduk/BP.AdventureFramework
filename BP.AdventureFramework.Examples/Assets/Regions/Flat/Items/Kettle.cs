using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Utilities;

namespace BP.AdventureFramework.Examples.Assets.Regions.Flat.Items
{
    public class Kettle : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Kettle";
        private const string Description = "The kettle has just boiled, you can tell because it is lightly steaming.";

        #endregion

        #region Implementation of IAssetTemplate<Item>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The item.</returns>
        public Item Instantiate()
        {
            return new Item(Name, Description)
            {
                Interaction = item =>
                {
                    if (item != null)
                    {
                        if (EmptyCoffeeMug.Name.EqualsIdentifier(item.Identifier))
                        {
                            item.Morph(new MugOfCoffee().Instantiate());
                            return new InteractionResult(InteractionEffect.ItemMorphed, item, "You put some instant coffee granuals into the mug and add some freshly boiled water from the Kettle. The coffee smells amazing!");
                        }
                    }

                    return new InteractionResult(InteractionEffect.NoEffect, item);
                }
            };
        }

        #endregion
    }
}
