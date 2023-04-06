using System.Collections.Generic;

namespace BP.AdventureFramework.Interaction
{
    /// <summary>
    /// Represents any object that can implement its own actions.
    /// </summary>
    public interface IImplementOwnActions
    {
        /// <summary>
        /// Get or set the ActionableCommands this object can interact with.
        /// </summary>
        List<ActionableCommand> AdditionalCommands { get; set; }
        /// <summary>
        /// React to an ActionableCommand.
        /// </summary>
        /// <param name="command">The command to react to.</param>
        /// <returns>The result of the interaction.</returns>
        InteractionResult ReactToAction(ActionableCommand command);
        /// <summary>
        /// Find a command by its name.
        /// </summary>
        /// <param name="command">The name of the command to find.</param>
        /// <returns>The ActionableCommand.</returns>
        ActionableCommand FindCommand(string command);
    }
}