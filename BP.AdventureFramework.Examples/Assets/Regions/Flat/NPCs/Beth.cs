using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Conversations;
using BP.AdventureFramework.Utilities;

namespace BP.AdventureFramework.Examples.Assets.Regions.Flat.NPCs
{
    internal class Beth : IAssetTemplate<NonPlayableCharacter>
    {
        #region Constants

        internal const string Name = "Beth";
        private const string Description = "Beth is very pretty, she has blue eyes and ginger hair. She is sat on the sofa watching the TV.";

        #endregion

        #region Overrides of NonIAssetTemplate<PlayableCharacter>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The asset.</returns>
        public NonPlayableCharacter Instantiate()
        {
            return new NonPlayableCharacter(Name, Description)
            {
                Conversation = new Conversation(new Paragraph("Hello Ben."), new Paragraph("How are you?", null, 0))
            };
        }

        #endregion
    }
}
