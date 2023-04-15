using BP.AdventureFramework.Logic;
using BP.AdventureFramework.Rendering;

namespace BP.AdventureFramework.Interpretation
{
    /// <summary>
    /// Represents an object that can be used for interpreting game input.
    /// </summary>
    internal class InputInterpreter : IInterpreter
    {
        #region Properties

        /// <summary>
        /// Get the game command interpreter.
        /// </summary>
        protected IInterpreter GameCommandInterpreter { get; }

        /// <summary>
        /// Get the frame command interpreter.
        /// </summary>
        protected IInterpreter FrameCommandInterpreter { get; }

        /// <summary>
        /// Get the global command interpreter.
        /// </summary>
        protected IInterpreter GlobalCommandInterpreter { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the InputInterpreter class.
        /// </summary>
        /// <param name="frameDrawer">The frame drawer.</param>
        /// <param name="mapDrawer">The map drawer.</param>
        public InputInterpreter(FrameDrawer frameDrawer, MapDrawer mapDrawer)
        {
            GameCommandInterpreter = new GameCommandInterpreter();
            FrameCommandInterpreter = new FrameCommandInterpreter(mapDrawer, frameDrawer);
            GlobalCommandInterpreter = new GlobalCommandInterpreter(mapDrawer);
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
            var result = GlobalCommandInterpreter.Interpret(input, game);

            if (result.WasInterpretedSuccessfully)
                return result;

            result = FrameCommandInterpreter.Interpret(input, game);

            return result.WasInterpretedSuccessfully ? result : GameCommandInterpreter.Interpret(input, game);
        }

        #endregion
    }
}
