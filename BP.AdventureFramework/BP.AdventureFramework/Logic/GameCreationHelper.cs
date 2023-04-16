using BP.AdventureFramework.Commands.Global;
using BP.AdventureFramework.Interpretation;
using BP.AdventureFramework.Rendering.Frames;

namespace BP.AdventureFramework.Logic
{
    /// <summary>
    /// Provides helper functionality for creating games.
    /// </summary>
    public static class GameCreationHelper
    {
        #region StaticMethods

        /// <summary>
        /// Create a new GameCreationHelper.
        /// </summary>
        /// <param name="name">The name of the game.</param>
        /// <param name="description">A description of the game.</param>
        /// <param name="overworldGenerator">A function to generate the Overworld with.</param>
        /// <param name="playerGenerator">The function to generate the Player with.</param>
        /// <param name="completionCondition">The callback used to check game completion.</param>
        /// <returns>A new GameCreationHelper that will create a GameCreator with the parameters specified.</returns>
        public static GameCreationCallback Create(string name, string description, OverworldCreationCallback overworldGenerator, PlayerCreationCallback playerGenerator, CompletionCheck completionCondition, Frame titleFrame, Frame completionFrame, Frame helpFrame, IInterpreter interpreter)
        {
            return () =>
            {
                var pC = playerGenerator.Invoke();
                var game = new Game(name, description, pC, overworldGenerator.Invoke(pC))
                {
                    TitleFrame = new TitleFrame(name, description),
                    CompletionFrame = new TitleFrame("You have completed " + name + "!!!", "Well done you have completed the game. Thanks for playing."),
                    CompletionCondition = completionCondition
                };

                return game;
            };
        }

        #endregion
    }
}