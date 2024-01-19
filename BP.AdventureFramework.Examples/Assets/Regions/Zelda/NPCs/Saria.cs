using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Conversations;
using BP.AdventureFramework.Examples.Assets.Regions.Zelda.Items;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Regions.Zelda.NPCs
{
    internal class Saria : NonPlayableCharacterTemplate<Saria>
    {
        #region Constants

        internal const string Name = "Saria";
        private const string Description = "A very annoying, but admittedly quite pretty elf, dressed, like you, completely in green.";

        #endregion

        #region Overrides of NonPlayableCharacterTemplate<Saria>

        /// <summary>
        /// Create a new instance of the non-playable character.
        /// </summary>
        /// <param name="pC">The playable character.</param>
        /// <param name="room">The room.</param>
        /// <returns>The non-playable character.</returns>
        protected override NonPlayableCharacter OnCreate(PlayableCharacter pC, Room room)
        {
            var saria = new NonPlayableCharacter(Name, Description);

            saria.AcquireItem(TailKey.Create());

            saria.Conversation = new Conversation
            (
                new Paragraph("Hi Link, how's it going?"),
                new Paragraph("I lost my red rupee, if you find it will you please bring it to me?"),
                new Paragraph("Oh Link you are so adorable."),
                new Paragraph("OK Link your annoying me now, I'm just going to ignore you.", 0)
            );

            saria.Interaction = (item, _) =>
            {
                saria.FindItem(TailKey.Name, out var key);

                if (Rupee.Name.EqualsIdentifier(item.Identifier))
                {
                    pC.Give(item, saria);
                    saria.Give(key, pC);
                    return new InteractionResult(InteractionEffect.SelfContained, item, $"{saria.Identifier.Name} looks excited! \"Thanks Link, here take the Tail Key!\" You've got the Tail Key, awesome!");
                }

                if (Shield.Name.EqualsIdentifier(item.Identifier))
                {
                    return new InteractionResult(InteractionEffect.NoEffect, item, $"{saria.Identifier.Name} looks at your shield, but seems pretty unimpressed.");
                }

                if (Sword.Name.EqualsIdentifier(item.Identifier))
                {
                    saria.Kill();

                    if (!saria.HasItem(key))
                        return new InteractionResult(InteractionEffect.SelfContained, item, $"You strike {saria.Identifier.Name} in the face with the sword and she falls down dead.");

                    saria.DequireItem(key);
                    room.AddItem(key);

                    return new InteractionResult(InteractionEffect.SelfContained, item, $"You strike {saria.Identifier.Name} in the face with the sword and she falls down dead. When she fell you saw something drop to out of her hand, it looked like a key...");
                }

                return new InteractionResult(InteractionEffect.NoEffect, item);
            };

            return saria;
        }

        #endregion
    }
}
