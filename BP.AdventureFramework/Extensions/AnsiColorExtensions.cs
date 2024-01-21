using System;
using BP.AdventureFramework.Rendering;

namespace BP.AdventureFramework.Extensions
{
    /// <summary>
    /// Provides extension versions for AnsiColor.
    /// </summary>
    public static class AnsiColorExtensions
    {
        /// <summary>
        /// Convert this color to a console color.
        /// </summary>
        /// <param name="value">The color.</param>
        /// <returns>The console color.</returns>
        public static ConsoleColor ToConsoleColor(this AnsiColor value)
        {
            switch (value)
            {
                case AnsiColor.White:
                    return ConsoleColor.White;
                case AnsiColor.Black:
                    return ConsoleColor.Black;
                case AnsiColor.Gray:
                    return ConsoleColor.Gray;
                case AnsiColor.DarkGray:
                    return ConsoleColor.DarkGray;
                case AnsiColor.Blue:
                    return ConsoleColor.Blue;
                case AnsiColor.Red:
                    return ConsoleColor.Red;
                case AnsiColor.Yellow:
                    return ConsoleColor.Yellow;
                case AnsiColor.Green:
                    return ConsoleColor.Green;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }
    }
}
