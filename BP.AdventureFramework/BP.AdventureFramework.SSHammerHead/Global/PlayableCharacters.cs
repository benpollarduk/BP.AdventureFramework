using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Interaction;

namespace BP.AdventureFramework.SSHammerHead.Global
{
    /// <summary>
    /// Provides all playable characters.
    /// </summary>
    internal static class PlayableCharacters
    {
        internal static PlayableCharacter Generate()
        {
            var player = new PlayableCharacter("Naomi", "You, Naomi Watts, are a 45 year old shuttle mechanic.", GlobalItems.Hammer, GlobalItems.Mirror)
            {
                Interaction = (i, target) =>
                {
                    if (i == null)
                        return new InteractionResult(InteractionEffect.NoEffect, null);

                    if (i == GlobalItems.Hammer)
                        return new InteractionResult(InteractionEffect.FatalEffect, i, "You swing wildly at your own head. The first few blows connect and knock you down. You are dead.");

                    if (i == GlobalItems.Mirror)
                        return new InteractionResult(InteractionEffect.NoEffect, i, "Peering in to the mirror you can see yourself looking back through your helmets visor.");

                    return new InteractionResult(InteractionEffect.NoEffect, i);
                }
            };

            return player;
        }
    }
}
