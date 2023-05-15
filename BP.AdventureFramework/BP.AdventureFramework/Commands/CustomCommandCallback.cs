using BP.AdventureFramework.Assets.Interaction;

namespace BP.AdventureFramework.Commands
{
    /// <summary>
    /// Provides a callback for custom commands.
    /// </summary>
    /// <param name="game">The game to invoke the command on.</param>
    /// <returns>The reaction to the command.</returns>
    public delegate Reaction CustomCommandCallback(Logic.Game game);
}
