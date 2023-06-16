using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Conversations;
using BP.AdventureFramework.Examples.Assets.Regions.Zelda.Items;

namespace BP.AdventureFramework.Examples.Assets.Regions.Zelda.NPCs
{
    public static class Saria
    {
        #region Constants

        internal const string Name = "Saria";
        private const string Description = "A very annoying, but admittedly quite pretty elf, dressed, like you, completely in green.";

        #endregion

        #region StaticMethods

        public static NonPlayableCharacter Create()
        {
            var saria = new NonPlayableCharacter(Name, Description);

            saria.AquireItem(TailKey.Create());

            saria.Conversation = new Conversation
            (
                new Paragraph("Hi Link, how's it going?"),
                new Paragraph("I lost my red rupee, if you find it will you please bring it to me?"),
                new Paragraph("Oh Link you are so adorable."),
                new Paragraph("OK Link your annoying me now, I'm just going to ignore you.", 0)
            );

            return saria;
        }

        #endregion
    }
}
