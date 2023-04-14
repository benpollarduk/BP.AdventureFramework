﻿using System;
using BP.AdventureFramework.GameStructure;
using BP.AdventureFramework.Parsing.Commands.Frame;
using BP.AdventureFramework.Rendering;

namespace BP.AdventureFramework.Parsing.Interpretation
{
    /// <summary>
    /// Represents an object that can be used for interpreting frame commands.
    /// </summary>
    public class FrameCommandInterpreter : IInterpreter
    {
        #region Constants

        private const string CommandsOff = "CommandsOff";
        private const string CommandsOn = "CommandsOn";
        private const string Invert = "Invert";
        private const string KeyOn = "KeyOn";
        private const string KeyOff = "KeyOff";

        #endregion

        #region Properties

        /// <summary>
        /// Get the map drawer.
        /// </summary>
        public MapDrawer MapDrawer { get; }

        /// <summary>
        /// Get the frame drawer.
        /// </summary>
        public FrameDrawer FrameDrawer { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the FrameCommandInterpreter class.
        /// </summary>
        /// <param name="mapDrawer">The map drawer.</param>
        /// <param name="frameDrawer">The frame drawer.</param>
        public FrameCommandInterpreter(MapDrawer mapDrawer, FrameDrawer frameDrawer)
        {
            MapDrawer = mapDrawer;
            FrameDrawer = frameDrawer;
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
                return new InterpretationResult(true, new CommandsOff(FrameDrawer));

            if (input.Equals(CommandsOn, StringComparison.CurrentCultureIgnoreCase))
                return new InterpretationResult(true, new CommandsOn(FrameDrawer));

            if (input.Equals(Invert, StringComparison.CurrentCultureIgnoreCase))
                return new InterpretationResult(true, new Invert());

            if (input.Equals(KeyOff, StringComparison.CurrentCultureIgnoreCase))
                return new InterpretationResult(true, new KeyOff(MapDrawer));

            if (input.Equals(KeyOn, StringComparison.CurrentCultureIgnoreCase))
                return new InterpretationResult(true, new KeyOn(MapDrawer));

            return InterpretationResult.Fail;
        }

        #endregion
    }
}