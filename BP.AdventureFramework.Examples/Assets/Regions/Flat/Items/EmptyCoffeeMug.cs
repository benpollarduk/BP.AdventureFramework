﻿using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Regions.Flat.Items
{
    public class EmptyCoffeeMug : ItemTemplate<EmptyCoffeeMug>
    {
        #region Constants

        internal const string Name = "Empty Coffee Mug";
        private const string Description = "A coffee mug. It has an ugly hand painted picture of a man with green hair and enormous sideburns painted on the side of it. Underneath it says 'The Sideburn Monster Rides again'. Strange.";

        #endregion

        #region Overrides of ItemTemplate<EmptyCoffeeMug>

        /// <summary>
        /// Create a new instance of the item.
        /// </summary>
        /// <returns>The region.</returns>
        protected override Item OnCreate()
        {
            return new Item(Name, Description, true)
            {
                Interaction = (i, target) =>
                {
                    if (Kettle.Name.EqualsIdentifier(i.Identifier))
                    {
                        var item = target as Item;
                        item?.Morph(MugOfCoffee.Create());
                        return new InteractionResult(InteractionEffect.ItemMorphed, i, "You put some instant coffee graduals into the mug and add some freshly boiled water from the Kettle. The coffee smells amazing!");
                    }

                    return new InteractionResult(InteractionEffect.NoEffect, i);
                }
            };
        }

        #endregion
    }
}
