using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Conversations;
using BP.AdventureFramework.Examples.Assets.Items;
using BP.AdventureFramework.Examples.Assets.Regions.Flat.Items;
using BP.AdventureFramework.Extensions;

namespace BP.AdventureFramework.Examples.Assets.Regions.Flat.NPCs
{
    public static class Beth
    {
        #region Constants

        internal const string Name = "Beth";
        private const string Description = "Beth is very pretty, she has blue eyes and ginger hair. She is sat on the sofa watching the TV.";

        #endregion

        #region StaticMethods

        public static NonPlayableCharacter Create()
        {
            return new NonPlayableCharacter(Name, Description)
            {
                Conversation = new Conversation(new Paragraph("Hello Ben."), new Paragraph("How are you?", null, 0))
            };
        }

        #endregion
    }
}
