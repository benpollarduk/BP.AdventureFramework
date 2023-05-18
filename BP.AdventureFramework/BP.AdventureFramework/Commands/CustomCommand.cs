using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Interpretation;

namespace BP.AdventureFramework.Commands
{
    /// <summary>
    /// Provides a custom command.
    /// </summary>
    public class CustomCommand : ICommand, IPlayerVisible
    {
        #region Properties

        /// <summary>
        /// Get the callback.
        /// </summary>
        private CustomCommandCallback Callback { get; }

        /// <summary>
        /// Get or set the arguments.
        /// </summary>
        public string[] Arguments { get; set; }

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
        /// <param name="isPlayerVisible">If this is visible to the player.</param>
        public CustomCommand(CommandHelp help, CustomCommandCallback callback, bool isPlayerVisible = true)
        {
            Help = help;
            Callback = callback;
            IsPlayerVisible = isPlayerVisible;
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
            return Callback.Invoke(game, Arguments);
        }

        #endregion

        #region Implementation of IPlayerVisible

        /// <summary>
        /// Get or set if this is visible to the player.
        /// </summary>
        public bool IsPlayerVisible { get; set; }

        #endregion
    }
}
