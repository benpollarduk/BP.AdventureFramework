using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Utilities;

namespace BP.AdventureFramework.Examples.Assets.Regions.Flat.Items
{
    public class EmptyCoffeeMug : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Empty Coffee Mug";
        private const string Description = "A coffee mug. It has an ugly hand painted picture of a man with green hair and enormous sideburns painted on the side of it. Underneath it says 'The Sideburn Monster Rides again'. Strange.";

        #endregion

        #region Implementation of IAssetTemplate<Item>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The item.</returns>
        public Item Instantiate()
        {
            return new Item(Name, Description, true)
            {
                Interaction = (i, target) =>
                {
                    if (Kettle.Name.EqualsIdentifier(i.Identifier))
                    {
                        var item = target as Item;
                        item?.Morph(new MugOfCoffee().Instantiate());
                        return new InteractionResult(InteractionEffect.ItemMorphed, i, "You put some instant coffee graduals into the mug and add some freshly boiled water from the Kettle. The coffee smells amazing!");
                    }

                    return new InteractionResult(InteractionEffect.NoEffect, i);
                }
            };
        }

        #endregion
    }
}
