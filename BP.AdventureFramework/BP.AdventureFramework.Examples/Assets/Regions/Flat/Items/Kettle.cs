using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Examples.Assets.Regions.Flat.Rooms;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Regions.Flat.Items
{
    public class Kettle : ItemTemplate<Kettle>
    {
        #region Constants

        internal const string Name = "Kettle";
        private const string Description = "The kettle has just boiled, you can tell because it is lightly steaming.";

        #endregion

        #region Overrides of ItemTemplate<ConchShell>

        /// <summary>
        /// Create a new instance of the item.
        /// </summary>
        /// <returns>The region.</returns>
        protected override Item OnCreate()
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
                            i.Morph(MugOfCoffee.Create());
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
