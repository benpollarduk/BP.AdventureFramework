using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Conversations;
using BP.AdventureFramework.Conversations.Instructions;
using BP.AdventureFramework.Examples.Assets.Regions.Zelda.Items;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Utilities;

namespace BP.AdventureFramework.Examples.Assets.Regions.Zelda.NPCs
{
    internal class Saria : IAssetTemplate<NonPlayableCharacter>
    {
        #region Constants

        internal const string Name = "Saria";
        private const string Description = "A pretty Kokiri elf, dresse.";

        #endregion

        #region Fields

        private readonly Room room;

        #endregion

        #region Constructors

        internal Saria(Room room)
        {
            this.room = room;
        }

        #endregion

        #region Overrides of NonIAssetTemplate<PlayableCharacter>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The asset.</returns>
        public NonPlayableCharacter Instantiate()
        {
            var saria = new NonPlayableCharacter(Name, Description);

            saria.AcquireItem(new TailKey().Instantiate());

            saria.Conversation = new Conversation
            (
                new Paragraph("Hi Link, how's it going?"),
                new Paragraph("I lost my red rupee, if you find it will you please bring it to me?"),
                new Paragraph("Oh Link you are so adorable."),
                new Paragraph("OK Link your annoying me now, I'm just going to ignore you.", new First())
            );

            saria.Interaction = item =>
            {
                saria.FindItem(TailKey.Name, out var key);

                if (Rupee.Name.EqualsIdentifier(item.Identifier))
                {
                    item.Morph(key);
                    return new InteractionResult(InteractionEffect.SelfContained, item, $"{saria.Identifier.Name} looks excited! \"Thanks Link, here take the Tail Key!\" Saria gave you the Tail Key, awesome!");
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
