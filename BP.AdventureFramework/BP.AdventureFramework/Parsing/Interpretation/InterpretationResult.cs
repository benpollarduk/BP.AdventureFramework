using BP.AdventureFramework.Commands.Game;
using BP.AdventureFramework.Parsing.Commands;

namespace BP.AdventureFramework.Parsing.Interpretation
{
    /// <summary>
    /// Represents the result of an interpretation.
    /// </summary>
    public class InterpretationResult
    {
        #region StaticProperties

        /// <summary>
        /// Get a default result for failure.
        /// </summary>
        public static InterpretationResult Fail => new InterpretationResult(false, new Unactionable("Interpretation failed."));

        #endregion

        #region Properties

        /// <summary>
        /// Get if interpretation was successful.
        /// </summary>
        public bool WasInterpretedSuccessfully { get; }

        /// <summary>
        /// Get the command.
        /// </summary>
        public ICommand Command { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the InterpretationResult class.
        /// </summary>
        /// <param name="wasInterpretedSuccessfully">If interpretation was successful.</param>
        /// <param name="command">The command.</param>
        public InterpretationResult(bool wasInterpretedSuccessfully, ICommand command)
        {
            WasInterpretedSuccessfully = wasInterpretedSuccessfully;
            Command = command;
        }

        #endregion
    }
}
