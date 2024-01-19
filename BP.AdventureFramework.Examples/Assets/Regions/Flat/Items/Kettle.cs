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
                Interaction = (i, target) =>
                {
                    var obj = target as Item;

                    if (obj != null)
                    {
                        if (EmptyCoffeeMug.Name.EqualsIdentifier(i.Identifier))
                        {
                            i.Morph(new MugOfCoffee().Instantiate());
                            return new InteractionResult(InteractionEffect.ItemMorphed, i, "You put some instant coffee granuals into the mug and add some freshly boiled water from the Kettle. The coffee smells amazing!");
                        }
                    }

                    return new InteractionResult(InteractionEffect.NoEffect, i);
                }
            };
        }

        #endregion
    }
}
