using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Examples.Assets.Items;
using BP.AdventureFramework.Examples.Assets.Regions.Flat.Items;
using BP.AdventureFramework.Extensions;

namespace BP.AdventureFramework.Examples.Assets.Player
{
    public static class Player
    {
        #region Constants

        private const string Name = "Ben";
        private const string Description = "You are a 25 year old man, dressed in shorts, a t-shirt and flip-flops.";

        #endregion

        #region StaticMethods

        public static PlayableCharacter Create()
        {
            var player = new PlayableCharacter(Name, Description, Knife.Create())
            {
                Interaction = (i, target) =>
                {
                    if (i == null)
                        return new InteractionResult(InteractionEffect.NoEffect, null);

                    if (Knife.Name.EqualsExaminable(i))
                        return new InteractionResult(InteractionEffect.FatalEffect, i, "You slash wildly at your own throat. You are dead.");

                    if (EmptyCoffeeMug.Name.EqualsIdentifier(i.Identifier))
                        return new InteractionResult(InteractionEffect.NoEffect, i, "If there was some coffee in the mug you could drink it.");

                    if (Guitar.Name.EqualsIdentifier(i.Identifier))
                        return new InteractionResult(InteractionEffect.NoEffect, i, "You bust out some Bad Religion. Cracking, shame the guitar isn't plugged in to an amplified though...");

                    return new InteractionResult(InteractionEffect.NoEffect, i);
                }
            };

            return player;
        }

        #endregion
    }
}
