using BP.AdventureFramework.GameAssets.Interaction;
using BP.AdventureFramework.Parsing.Commands;

namespace BP.AdventureFramework.Commands.Game
{
    /// <summary>
    /// Represents the Examine command.
    /// </summary>
    public class Examine : ICommand
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
        /// <returns>The reaction.</returns>
        public Reaction Invoke()
        {
            if (Examinable == null)
                return new Reaction(ReactionResult.None, "Nothing to examine.");

            return new Reaction(ReactionResult.Reacted, Examinable.Examime().Desciption);
        }

        #endregion
    }
}