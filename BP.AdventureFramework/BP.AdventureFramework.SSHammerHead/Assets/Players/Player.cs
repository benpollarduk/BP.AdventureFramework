using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.SSHammerHead.Assets.Regions.SSHammerHead.Items;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.SSHammerHead.Assets.Players
{
    public class Player : PlayableCharacterTemplate<Player>
    {
        #region Constants

        private const string Name = "Naomi";
        private const string Description = "You, Naomi Watts, are a 45 year old shuttle mechanic.";

        #endregion

        #region Overrides of PlayableCharacterTemplate<Player>

        /// <summary>
        /// Create a new instance of the playable character.
        /// </summary>
        /// <returns>The playable character.</returns>
        protected override PlayableCharacter OnCreate()
        {
            var player = new PlayableCharacter(Name, Description, Hammer.Create(), Mirror.Create())
            {
                Interaction = (i, target) =>
                {
                    if (i == null)
                        return new InteractionResult(InteractionEffect.NoEffect, null);

                    if (Hammer.Name.EqualsIdentifier(i.Identifier))
                        return new InteractionResult(InteractionEffect.FatalEffect, i, "You swing wildly at your own head. The first few blows connect and knock you down. You are dead.");

                    if (Mirror.Name.EqualsIdentifier(i.Identifier))
                        return new InteractionResult(InteractionEffect.NoEffect, i, "Peering in to the mirror you can see yourself looking back through your helmets visor.");

                    return new InteractionResult(InteractionEffect.NoEffect, i);
                }
            };

            return player;
        }

        #endregion
    }
}
