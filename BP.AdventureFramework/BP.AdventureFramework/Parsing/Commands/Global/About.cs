using BP.AdventureFramework.Interaction;

namespace BP.AdventureFramework.Parsing.Commands.Global
{
    /// <summary>
    /// Represents the About command.
    /// </summary>
    public class About : ICommand
    {
        #region Properties

        /// <summary>
        /// Get the game.
        /// </summary>
        public GameStructure.Game Game { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the About class.
        /// </summary>
        /// <param name="game">The game.</param>
        public About(GameStructure.Game game)
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

            var defaultString = "AdventureFramework by Ben Pollard 2011 - 2023";

            if (!string.IsNullOrEmpty(Game.Description))
                Game.Refresh(Game.Description + $"\n\n{defaultString}");
            else
                Game.Refresh(defaultString);

            return new Reaction(ReactionResult.SelfContained, string.Empty);
        }

        #endregion
    }
}