using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Conversations;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Regions.Flat.NPCs
{
    internal class Beth : NonPlayableCharacterTemplate<Beth>
    {
        #region Constants

        internal const string Name = "Beth";
        private const string Description = "Beth is very pretty, she has blue eyes and ginger hair. She is sat on the sofa watching the TV.";

        #endregion

        #region Overrides of NonPlayableCharacterTemplate<Beth>

        /// <summary>
        /// Create a new instance of the non-playable character.
        /// </summary>
        /// <param name="pC">The playable character.</param>
        /// <param name="room">The room.</param>
        /// <returns>The non-playable character.</returns>
        protected override NonPlayableCharacter OnCreate(PlayableCharacter pC, Room room)
        {
            return new NonPlayableCharacter(Name, Description)
            {
                Conversation = new Conversation(new Paragraph("Hello Ben."), new Paragraph("How are you?", null, 0))
            };
        }

        #endregion
    }
}
