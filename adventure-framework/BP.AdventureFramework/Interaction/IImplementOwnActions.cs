using System.Collections.Generic;

namespace BP.AdventureFramework.Interaction
{
    /// <summary>
    /// Represents any object that can implement it's own actions
    /// </summary>
    public interface IImplementOwnActions
    {
        #region Properties

        /// <summary>
        /// Get or set the ActionableCommands this object can interact with
        /// </summary>
        List<ActionableCommand> AdditionalCommands { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// React to an ActionableCommand
        /// </summary>
        /// <param name="command">The command to react to</param>
        /// <returns>The result of the interaction</returns>
        InteractionResult ReactToAction(ActionableCommand command);

        /// <summary>
        /// Find a command by it's name
        /// </summary>
        /// <param name="command">The name of the command to find</param>
        /// <returns>The ActionableCommand (if it is found)</returns>
        ActionableCommand FindCommand(string command);

        #endregion
    }
}