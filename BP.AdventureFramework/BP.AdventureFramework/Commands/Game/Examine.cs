using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Interaction;

namespace BP.AdventureFramework.Commands.Game
{
    /// <summary>
    /// Represents the Examine command.
    /// </summary>
    internal class Examine : ICommand
    {
        #region Properties

        /// <summary>
        /// Get the examinable.
        /// </summary>
        public IExaminable Examinable { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Examine command.
        /// </summary>
        /// <param name="examinable">The examinable.</param>
        public Examine(IExaminable examinable)
        {
            Examinable = examinable;
        }

        #endregion

        #region Implementation of ICommand

        /// <summary>
        /// Invoke the command.
        /// </summary>
        /// <param name="game">The game to invoke the command on.</param>
        /// <returns>The reaction.</returns>
        public Reaction Invoke(Logic.Game game)
        {
            if (Examinable == null)
                return new Reaction(ReactionResult.Error, "Nothing to examine.");

            return new Reaction(ReactionResult.OK, Examinable.Examime().Description);
        }

        #endregion
    }
}