using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Interpretation;

namespace BP.AdventureFramework.Commands
{
    /// <summary>
    /// Provides a custom command.
    /// </summary>
    public class CustomCommand : ICommand
    {
        #region Properties

        /// <summary>
        /// Get or set the callback.
        /// </summary>
        private CustomCommandCallback Callback { get; }

        /// <summary>
        /// Get the help for this command.
        /// </summary>
        public CommandHelp Help { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CustomCommand class.
        /// </summary>
        /// <param name="help">The help for this command.</param>
        /// <param name="callback">The callback to invoke when this command is invoked.</param>
        public CustomCommand(CommandHelp help, CustomCommandCallback callback)
        {
            Help = help;
            Callback = callback;
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
            return Callback.Invoke(game);
        }

        #endregion
    }
}
