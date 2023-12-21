using System;
using BP.AdventureFramework.Rendering.FrameBuilders.Grid.Color;

namespace BP.AdventureFramework.Extensions
{
    /// <summary>
    /// Provides extension versions for RenderColor.
    /// </summary>
    public static class RenderColorExtensions
    {
        /// <summary>
        /// Convert this color to a console color.
        /// </summary>
        /// <param name="value">The color.</param>
        /// <returns>The console color.</returns>
        public static ConsoleColor ToConsoleColor(this RenderColor value)
        {
            switch (value)
            {
                case RenderColor.White:
                    return ConsoleColor.White;
                case RenderColor.Black:
                    return ConsoleColor.Black;
                case RenderColor.Gray:
                    return ConsoleColor.Gray;
                case RenderColor.DarkGray:
                    return ConsoleColor.DarkGray;
                case RenderColor.Blue:
                    return ConsoleColor.Blue;
                case RenderColor.Red:
                    return ConsoleColor.Red;
                case RenderColor.Yellow:
                    return ConsoleColor.Yellow;
                case RenderColor.Green:
                    return ConsoleColor.Green;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }
    }
}
