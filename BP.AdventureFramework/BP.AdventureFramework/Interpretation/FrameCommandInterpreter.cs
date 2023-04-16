using System;
using BP.AdventureFramework.Commands.Frame;
using BP.AdventureFramework.Logic;
using BP.AdventureFramework.Rendering;

namespace BP.AdventureFramework.Interpretation
{
    /// <summary>
    /// Represents an object that can be used for interpreting frame commands.
    /// </summary>
    internal class FrameCommandInterpreter : IInterpreter
    {
        #region Constants

        /// <summary>
        /// Get the commands off command.
        /// </summary>
        public const string CommandsOff = "CommandsOff";

        /// <summary>
        /// Get the commands on command.
        /// </summary>
        public const string CommandsOn = "CommandsOn";

        /// <summary>
        /// Get the keys on command.
        /// </summary>
        public const string KeyOn = "KeyOn";

        /// <summary>
        /// Get the commands off command.
        /// </summary>
        public const string KeyOff = "KeyOff";

        #endregion

        #region Properties

        /// <summary>
        /// Get the map drawer.
        /// </summary>
        public MapDrawer MapDrawer { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the FrameCommandInterpreter class.
        /// </summary>
        /// <param name="mapDrawer">The map drawer.</param>
        public FrameCommandInterpreter(MapDrawer mapDrawer)
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
            if (input.Equals(CommandsOff, StringComparison.CurrentCultureIgnoreCase))
                return new InterpretationResult(true, new CommandsOff());

            if (input.Equals(CommandsOn, StringComparison.CurrentCultureIgnoreCase))
                return new InterpretationResult(true, new CommandsOn());

            if (input.Equals(KeyOff, StringComparison.CurrentCultureIgnoreCase))
                return new InterpretationResult(true, new KeyOff(MapDrawer));

            if (input.Equals(KeyOn, StringComparison.CurrentCultureIgnoreCase))
                return new InterpretationResult(true, new KeyOn(MapDrawer));

            return InterpretationResult.Fail;
        }

        #endregion
    }
}
