using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventureFramework.Rendering;
using AdventureFramework.Locations;
using AdventureFramework.IO;
using AdventureFramework.Rendering.Frames;
using AdventureFramework.Interaction;

namespace AdventureFramework.Structure
{
    /// <summary>
    /// Represents a class that helps to build games
    /// </summary>
    public class GameCreationHelper
    {
        #region Properties

        /// <summary>
        /// Get the game creator 
        /// </summary>
        public GameCreator Creator
        {
            get { return this.creator; }
            protected set { this.creator = value; }
        }

        /// <summary>
        /// Get or set the game creator 
        /// </summary>
        private GameCreator creator;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the GameCreationHelper class
        /// </summary>
        private GameCreationHelper()
        {
        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Create a new GameCreationHelper
        /// </summary>
        /// <param name="name">The name of the game</param>
        /// <param name="description">A description of the game</param>
        /// <param name="overworldGenerator">The callback for generating the Overworld</param>
        /// <param name="playerGenerator">The callback for generating the Player</param>
        /// <param name="completionCondition">The callback used to to check game completion</param>
        /// <returns>A new GameCreationHelper that will create a GameCreator with the parameters specified</returns>
        public static GameCreationHelper Create(String name, String description, OverworldGeneration overworldGenerator, PlayerGeneration playerGenerator, CompletionCheck completionCondition)
        {
            // create new helper
            return GameCreationHelper.Create(name, description, overworldGenerator, playerGenerator, completionCondition, new TitleFrame(name, description));
        }

        /// <summary>
        /// Create a new GameCreationHelper
        /// </summary>
        /// <param name="name">The name of the game</param>
        /// <param name="description">A description of the game</param>
        /// <param name="overworldGenerator">The callback for generating the Overworld</param>
        /// <param name="playerGenerator">The callback for generating the Player</param>
        /// <param name="completionCondition">The callback used to to check game completion</param>
        /// <param name="titleFrame">The title Frame</param>
        /// <returns>A new GameCreationHelper that will create a GameCreator with the parameters specified</returns>
        public static GameCreationHelper Create(String name, String description, OverworldGeneration overworldGenerator, PlayerGeneration playerGenerator, CompletionCheck completionCondition, TitleFrame titleFrame)
        {
            // create new helper
            return GameCreationHelper.Create(name, description, overworldGenerator, playerGenerator, completionCondition, titleFrame, new TitleFrame("You have compleated " + name + "!!!", "Well done you have compleated the game. Thanks for playing"));
        }

        /// <summary>
        /// Create a new GameCreationHelper
        /// </summary>
        /// <param name="name">The name of the game</param>
        /// <param name="description">A description of the game</param>
        /// <param name="overworldGenerator">A function to generate the Overworld with</param>
        /// <param name="playerGenerator">The function to generate the Player with</param>
        /// <param name="completionCondition">The callback used to to check game completion</param>
        /// <param name="titleFrame">The title Frame</param>
        /// <param name="completionFrame">The completion Frame</param>
        /// <returns>A new GameCreationHelper that will create a GameCreator with the parameters specified</returns>
        public static GameCreationHelper Create(String name, String description, OverworldGeneration overworldGenerator, PlayerGeneration playerGenerator, CompletionCheck completionCondition, TitleFrame titleFrame, Frame completionFrame)
        {
            // create new helper
            return GameCreationHelper.Create(name, description, overworldGenerator, playerGenerator, completionCondition, titleFrame, completionFrame, new HelpFrame());
        }

        /// <summary>
        /// Create a new GameCreationHelper
        /// </summary>
        /// <param name="name">The name of the game</param>
        /// <param name="description">A description of the game</param>
        /// <param name="overworldGenerator">A function to generate the Overworld with</param>
        /// <param name="playerGenerator">The function to generate the Player with</param>
        /// <param name="completionCondition">The callback used to to check game completion</param>
        /// <param name="titleFrame">The title Frame</param>
        /// <param name="completionFrame">The completion Frame</param>
        /// <param name="help">The games help screen</param>
        /// <returns>A new GameCreationHelper that will create a GameCreator with the parameters specified</returns>
        public static GameCreationHelper Create(String name, String description, OverworldGeneration overworldGenerator, PlayerGeneration playerGenerator, CompletionCheck completionCondition, TitleFrame titleFrame, Frame completionFrame, HelpFrame help)
        {
            // create new helper
            return GameCreationHelper.Create(name, description, overworldGenerator, playerGenerator, completionCondition, titleFrame, completionFrame, help, new TextParser());
        }

        /// <summary>
        /// Create a new GameCreationHelper
        /// </summary>
        /// <param name="name">The name of the game</param>
        /// <param name="description">A description of the game</param>
        /// <param name="overworldGenerator">A function to generate the Overworld with</param>
        /// <param name="playerGenerator">The function to generate the Player with</param>
        /// <param name="completionCondition">The callback used to to check game completion</param>
        /// <param name="titleFrame">The title Frame</param>
        /// <param name="completionFrame">The completion Frame</param>
        /// <param name="help">The help Frame</param>
        /// <param name="parser">The parser for all input parsing</param>
        /// <returns>A new GameCreationHelper that will create a GameCreator with the parameters specified</returns>
        public static GameCreationHelper Create(String name, String description, OverworldGeneration overworldGenerator, PlayerGeneration playerGenerator, CompletionCheck completionCondition, TitleFrame titleFrame, Frame completionFrame, HelpFrame help, TextParser parser)
        {
            // create new helper
            GameCreationHelper helper = new GameCreationHelper();

            // set creator
            helper.Creator = new GameCreator(() =>
            {
                // invoke generation
                PlayableCharacter pC = playerGenerator.Invoke();

                // generate overworld
                Game game = new Game(name, description, pC, overworldGenerator.Invoke(pC));

                // set title screen
                game.TitleFrame = titleFrame;

                // set completion screen
                game.CompletionFrame = completionFrame;

                // set completion codition
                game.CompletionCondition = completionCondition;

                // set parser
                game.Parser = parser;

                // set help
                game.HelpFrame = help;

                // return game
                return game;
            });

            // return
            return helper;
        }

        /// <summary>
        /// Create a new GameCreationHelper
        /// </summary>
        /// <param name="name">The name of the game</param>
        /// <param name="description">A description of the game</param>
        /// <param name="overworldGenerator">A function to generate the Overworld with</param>
        /// <param name="playerGenerator">The function to generate the Player with</param>
        /// <param name="completionCondition">The callback used to to check game completion</param>
        /// <returns>A new GameCreationHelper that will create a GameCreator with the parameters specified</returns>
        public static GameCreationHelper Create(String name, String description, Func<Overworld> overworldGenerator, Func<PlayableCharacter> playerGenerator, CompletionCheck completionCondition)
        {
            // create new helper
            return GameCreationHelper.Create(name, description, overworldGenerator, playerGenerator, completionCondition, new TitleFrame(name, description));
        }

        /// <summary>
        /// Create a new GameCreationHelper
        /// </summary>
        /// <param name="name">The name of the game</param>
        /// <param name="description">A description of the game</param>
        /// <param name="overworldGenerator">A function to generate the Overworld with</param>
        /// <param name="playerGenerator">The function to generate the Player with</param>
        /// <param name="completionCondition">The callback used to to check game completion</param>
        /// <param name="titleFrame">The title Frame</param>
        /// <returns>A new GameCreationHelper that will create a GameCreator with the parameters specified</returns>
        public static GameCreationHelper Create(String name, String description, Func<Overworld> overworldGenerator, Func<PlayableCharacter> playerGenerator, CompletionCheck completionCondition, TitleFrame titleFrame)
        {
            // create new helper
            return GameCreationHelper.Create(name, description, overworldGenerator, playerGenerator, completionCondition, titleFrame, new TitleFrame("You have compleated " + name + "!!!", "Well done you have compleated the game. Thanks for playing"));
        }

        /// <summary>
        /// Create a new GameCreationHelper
        /// </summary>
        /// <param name="name">The name of the game</param>
        /// <param name="description">A description of the game</param>
        /// <param name="overworldGenerator">A function to generate the Overworld with</param>
        /// <param name="playerGenerator">The function to generate the Player with</param>
        /// <param name="completionCondition">The callback used to to check game completion</param>
        /// <param name="titleFrame">The title Frame</param>
        /// <param name="completionFrame">The completion Frame</param>
        /// <returns>A new GameCreationHelper that will create a GameCreator with the parameters specified</returns>
        public static GameCreationHelper Create(String name, String description, Func<Overworld> overworldGenerator, Func<PlayableCharacter> playerGenerator, CompletionCheck completionCondition, TitleFrame titleFrame, Frame completionFrame)
        {
            // create new helper
            return GameCreationHelper.Create(name, description, overworldGenerator, playerGenerator, completionCondition, titleFrame, completionFrame, new HelpFrame());
        }

        /// <summary>
        /// Create a new GameCreationHelper
        /// </summary>
        /// <param name="name">The name of the game</param>
        /// <param name="description">A description of the game</param>
        /// <param name="overworldGenerator">A function to generate the Overworld with</param>
        /// <param name="playerGenerator">The function to generate the Player with</param>
        /// <param name="completionCondition">The callback used to to check game completion</param>
        /// <param name="titleFrame">The title Frame</param>
        /// <param name="help">The help Frame</param>
        /// <param name="completionFrame">The completion Frame</param>
        /// <returns>A new GameCreationHelper that will create a GameCreator with the parameters specified</returns>
        public static GameCreationHelper Create(String name, String description, Func<Overworld> overworldGenerator, Func<PlayableCharacter> playerGenerator, CompletionCheck completionCondition, TitleFrame titleFrame, Frame completionFrame, HelpFrame help)
        {
            // create new helper
            return GameCreationHelper.Create(name, description, overworldGenerator, playerGenerator, completionCondition, titleFrame, completionFrame, help, new TextParser());
        }

        /// <summary>
        /// Create a new GameCreationHelper
        /// </summary>
        /// <param name="name">The name of the game</param>
        /// <param name="description">A description of the game</param>
        /// <param name="overworldGenerator">A function to generate the Overworld with</param>
        /// <param name="playerGenerator">The function to generate the Player with</param>
        /// <param name="completionCondition">The callback used to to check game completion</param>
        /// <param name="titleFrame">The title Frame</param>
        /// <param name="completionFrame">The completion Frame</param>
        /// <param name="help">The help Frame</param>
        /// <param name="parser">The parser for all input parsing</param>
        /// <returns>A new GameCreationHelper that will create a GameCreator with the parameters specified</returns>
        public static GameCreationHelper Create(String name, String description, Func<Overworld> overworldGenerator, Func<PlayableCharacter> playerGenerator, CompletionCheck completionCondition, TitleFrame titleFrame, Frame completionFrame, HelpFrame help, TextParser parser)
        {
            // create new helper
            GameCreationHelper helper = new GameCreationHelper();

            // set creator
            helper.Creator = new GameCreator(() =>
            {
                // generate overworld
                Game game = new Game(name, description, playerGenerator.Invoke(), overworldGenerator.Invoke());

                // set title screen
                game.TitleFrame = titleFrame;

                // set completion screen
                game.CompletionFrame = completionFrame;

                // set completion codition
                game.CompletionCondition = completionCondition;

                // set help
                game.HelpFrame = help;

                // return game
                return game;
            });

            // return
            return helper;
        }

        #endregion
    }

    /// <summary>
    /// Represents a callback for Overworld generation
    /// </summary>
    /// <param name="pC">The playable character that will appear in the overworld</param>
    /// <returns>A generated Overworld</returns>
    public delegate Overworld OverworldGeneration(PlayableCharacter pC);

    /// <summary>
    /// Represents a callback for Player generation
    /// </summary>
    /// <returns>A generated Player</returns>
    public delegate PlayableCharacter PlayerGeneration();
}
