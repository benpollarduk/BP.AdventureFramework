using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.SSHammerHead.Assets.Regions.SSHammerHead.Items
{
    public class BrokenControlPanel : ItemTemplate<BrokenControlPanel>
    {
        #region Constants

        internal const string Name = "Broken Control Panel";
        private const string Description = "The beaten up and broken remains of a control panel.";

        #endregion

        #region Overrides of ItemTemplate<BrokenControlPanel>

        /// <summary>
        /// Create a new instance of the item.
        /// </summary>
        /// <returns>The region.</returns>
        protected override Item OnCreate()
        {
            var brokenControlPanel = new Item(Name, Description);

            brokenControlPanel.Interaction = (item, target) =>
            {
                if (Name.EqualsExaminable(brokenControlPanel))
                {
                    if (Hammer.Name.EqualsIdentifier(item.Identifier))
                    {
                        return new InteractionResult(InteractionEffect.FatalEffect, item, $"Once again you swing the {Hammer.Name} in to the remains of the control panel. You must have hit a high voltage wire inside because you are suddenly electrocuted. You are electrocuted to death.");
                    }
                }

                return new InteractionResult(InteractionEffect.NoEffect, item);
            };

            return brokenControlPanel;
        }

        #endregion
    }
}
