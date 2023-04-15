using BP.AdventureFramework.Assets.Interaction;

namespace BP.AdventureFramework.Commands.Global
{
    /// <summary>
    /// Represents the About command.
    /// </summary>
    internal class About : ICommand
    {
        #region Properties

        /// <summary>
        /// Get the game.
        /// </summary>
        public Logic.Game Game { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the About class.
        /// </summary>
        /// <param name="game">The game.</param>
        public About(Logic.Game game)
        {
            Game = game;
        }

        #endregion

        #region Implementation of ICommand

        /// <summary>
        /// Invoke the command.
        /// </summary>
        /// <returns>The reaction.</returns>
        public Reaction Invoke()
        {
            if (Game == null)
                return new Reaction(ReactionResult.None, "No game specified.");

            var defaultString = "BP.AdventureFramework by Ben Pollard 2011 - 2023";

            if (!string.IsNullOrEmpty(Game.Description))
                Game.Refresh(Game.Description + $"\n\n{defaultString}");
            else
                Game.Refresh(defaultString);

            return new Reaction(ReactionResult.SelfContained, string.Empty);
        }

        #endregion
    }
}