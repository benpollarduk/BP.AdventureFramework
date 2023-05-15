using BP.AdventureFramework.Assets.Interaction;

namespace BP.AdventureFramework.Commands
{
    /// <summary>
    /// Represents a command.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Invoke the command.
        /// </summary>
        /// <param name="game">The game to invoke the command on.</param>
        /// <returns>The reaction.</returns>
        Reaction Invoke(Logic.Game game);
    }
}
