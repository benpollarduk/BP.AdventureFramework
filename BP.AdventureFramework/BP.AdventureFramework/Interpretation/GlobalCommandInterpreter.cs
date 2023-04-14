using System;
using BP.AdventureFramework.Commands.Global;
using BP.AdventureFramework.Logic;
using BP.AdventureFramework.Rendering;

namespace BP.AdventureFramework.Interpretation
{
    /// <summary>
    /// Represents an object that can be used for interpreting global commands.
    /// </summary>
    public class GlobalCommandInterpreter : IInterpreter
    {
        #region Constants

        /// <summary>
        /// Get the about command.
        /// </summary>
        public const string About = "About";

        /// <summary>
        /// Get the exit command.
        /// </summary>
        public const string Exit = "Exit";

        /// <summary>
        /// Get the help command.
        /// </summary>
        public const string Help = "Help";

        /// <summary>
        /// Get the map command.
        /// </summary>
        public const string Map = "Map";

        /// <summary>
        /// Get the new command.
        /// </summary>
        public const string New = "New";

        #endregion

        #region Properties

        /// <summary>
        /// Get the map drawer.
        /// </summary>
        public MapDrawer MapDrawer { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the GlobalCommandInterpreter class.
        /// </summary>
        /// <param name="mapDrawer">The map drawer.</param>
        public GlobalCommandInterpreter(MapDrawer mapDrawer)
        {
            MapDrawer = mapDrawer;
        }

        #endregion

        #region Implementation of IInterpreter

        /// <summary>
        /// Interpret a string.
        /// </summary>
        /// <param name="input">The string to interpret.</param>
        /// <param name="game">The game.</param>
        /// <returns>The result of the interpretation.</returns>
        public InterpretationResult Interpret(string input, Game game)
        {
            if (input.Equals(About, StringComparison.CurrentCultureIgnoreCase))
                return new InterpretationResult(true, new About(game));

            if (input.Equals(Exit, StringComparison.CurrentCultureIgnoreCase))
                return new InterpretationResult(true, new Exit(game));

            if (input.Equals(Help, StringComparison.CurrentCultureIgnoreCase))
                return new InterpretationResult(true, new Help(game));

            if (input.Equals(Map, StringComparison.CurrentCultureIgnoreCase))
                return new InterpretationResult(true, new Map(game, MapDrawer));

            if (input.Equals(New, StringComparison.CurrentCultureIgnoreCase))
                return new InterpretationResult(true, new New(game));

            return InterpretationResult.Fail;
        }

        #endregion
    }
}
