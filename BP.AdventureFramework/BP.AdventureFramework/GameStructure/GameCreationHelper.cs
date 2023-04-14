using BP.AdventureFramework.Rendering.Frames;

namespace BP.AdventureFramework.GameStructure
{
    /// <summary>
    /// Represents a class that helps to build games.
    /// </summary>
    public class GameCreationHelper
    {
        #region StaticMethods

        /// <summary>
        /// Create a new GameCreationHelper.
        /// </summary>
        /// <param name="name">The name of the game.</param>
        /// <param name="description">A description of the game.</param>
        /// <param name="overworldGenerator">The callback for generating the Overworld.</param>
        /// <param name="playerGenerator">The callback for generating the Player.</param>
        /// <param name="completionCondition">The callback used to check game completion.</param>
        /// <returns>A new GameCreationHelper that will create a GameCreator with the parameters specified.</returns>
        public static GameCreationCallback Create(string name, string description, OverworldCreationCallback overworldGenerator, PlayerCreationCallback playerGenerator, CompletionCheck completionCondition)
        {
            return Create(name, description, overworldGenerator, playerGenerator, completionCondition, new TitleFrame(name, description));
        }

        /// <summary>
        /// Create a new GameCreationHelper.
        /// </summary>
        /// <param name="name">The name of the game.</param>
        /// <param name="description">A description of the game.</param>
        /// <param name="overworldGenerator">The callback for generating the Overworld.</param>
        /// <param name="playerGenerator">The callback for generating the Player.</param>
        /// <param name="completionCondition">The callback used to check game completion.</param>
        /// <param name="titleFrame">The title Frame.</param>
        /// <returns>A new GameCreationHelper that will create a GameCreator with the parameters specified.</returns>
        public static GameCreationCallback Create(string name, string description, OverworldCreationCallback overworldGenerator, PlayerCreationCallback playerGenerator, CompletionCheck completionCondition, TitleFrame titleFrame)
        {
            return Create(name, description, overworldGenerator, playerGenerator, completionCondition, titleFrame, new TitleFrame("You have completed " + name + "!!!", "Well done you have completed the game. Thanks for playing"));
        }

        /// <summary>
        /// Create a new GameCreationHelper.
        /// </summary>
        /// <param name="name">The name of the game.</param>
        /// <param name="description">A description of the game.</param>
        /// <param name="overworldGenerator">A function to generate the Overworld with.</param>
        /// <param name="playerGenerator">The function to generate the Player with.</param>
        /// <param name="completionCondition">The callback used to to check game completion.</param>
        /// <param name="titleFrame">The title Frame.</param>
        /// <param name="completionFrame">The completion Frame.</param>
        /// <returns>A new GameCreationHelper that will create a GameCreator with the parameters specified.</returns>
        public static GameCreationCallback Create(string name, string description, OverworldCreationCallback overworldGenerator, PlayerCreationCallback playerGenerator, CompletionCheck completionCondition, TitleFrame titleFrame, Frame completionFrame)
        {
            return Create(name, description, overworldGenerator, playerGenerator, completionCondition, titleFrame, completionFrame, new HelpFrame());
        }

        /// <summary>
        /// Create a new GameCreationHelper.
        /// </summary>
        /// <param name="name">The name of the game.</param>
        /// <param name="description">A description of the game.</param>
        /// <param name="overworldGenerator">A function to generate the Overworld with.</param>
        /// <param name="playerGenerator">The function to generate the Player with.</param>
        /// <param name="completionCondition">The callback used to check game completion.</param>
        /// <param name="titleFrame">The title Frame.</param>
        /// <param name="completionFrame">The completion Frame.</param>
        /// <param name="help">The help Frame.</param>
        /// <returns>A new GameCreationHelper that will create a GameCreator with the parameters specified.</returns>
        public static GameCreationCallback Create(string name, string description, OverworldCreationCallback overworldGenerator, PlayerCreationCallback playerGenerator, CompletionCheck completionCondition, TitleFrame titleFrame, Frame completionFrame, HelpFrame help)
        {
            return () =>
            {
                var pC = playerGenerator.Invoke();
                var game = new Game(name, description, pC, overworldGenerator.Invoke(pC))
                {
                    TitleFrame = titleFrame,
                    CompletionFrame = completionFrame,
                    CompletionCondition = completionCondition,
                    HelpFrame = help
                };

                return game;
            };
        }

        #endregion
    }
}