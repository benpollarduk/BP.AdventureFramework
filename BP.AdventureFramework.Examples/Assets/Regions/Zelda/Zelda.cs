using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Conversations;
using BP.AdventureFramework.Examples.Assets.Regions.Zelda.Items;
using BP.AdventureFramework.Examples.Assets.Regions.Zelda.Rooms;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Utilities;

namespace BP.AdventureFramework.Examples.Assets.Regions.Zelda
{
    internal class Zelda : IAssetTemplate<Region>
    {
        #region Constants

        private const string Name = "Kokiri Forest";
        private const string Description = "The home of the Kokiri tree folk.";

        #endregion

        #region Fields

        private readonly PlayableCharacter player;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Zelda class.
        /// </summary>
        /// <param name="pc">The playable character.</param>
        internal Zelda(PlayableCharacter pc)
        {
            player = pc;
        }

        #endregion

        #region Implementation of IAssetTemplate<Region>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The asset.</returns>
        public Region Instantiate()
        {
            var regionMaker = new RegionMaker(Name, Description)
            {
                [0, 0, 0] = new LinksHouse().Instantiate(),
                [0, 1, 0] = new OutsideLinksHouse().Instantiate(),
                [1, 1, 0] = new TailCave().Instantiate(),
                [0, 2, 0] = new Stream().Instantiate()
            };

            var saria = new NonPlayableCharacter(Name, Description);

            saria.AcquireItem(new TailKey().Instantiate());

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
                    player.Give(item, saria);
                    saria.Give(key, player);
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
                    regionMaker[0, 1, 0].AddItem(key);

                    return new InteractionResult(InteractionEffect.SelfContained, item, $"You strike {saria.Identifier.Name} in the face with the sword and she falls down dead. When she fell you saw something drop to out of her hand, it looked like a key...");
                }

                return new InteractionResult(InteractionEffect.NoEffect, item);
            };

            regionMaker[0, 1, 0].AddCharacter(saria);

            return regionMaker.Make(0, 0, 0);
        }

        #endregion
    }
}
