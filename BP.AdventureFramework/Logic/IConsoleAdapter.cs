using System.IO;
using BP.AdventureFramework.Rendering.Frames;

namespace BP.AdventureFramework.Logic
{
    /// <summary>
    /// Represents any object that provides an apapter for a console.
    /// </summary>
    internal interface IConsoleAdapter
    {
        /// <summary>
        /// Get the input stream.
        /// </summary>
        TextReader In { get; }
        /// <summary>
        /// Get the output stream.
        /// </summary>
        TextWriter Out { get; }
        /// <summary>
        /// Get the error output stream.
        /// </summary>
        TextWriter Error { get; }
        /// <summary>
        /// Wait for a key press.
        /// </summary>
        /// <param name="key">The ASCII code of the key to wait for.</param>
        /// <returns>True if the key pressed returned the same ASCII character as the key property, else false.</returns>
        bool WaitForKeyPress(char key);
        /// <summary>
        /// Handle a game started a frame draw.
        /// </summary>
        /// <param name="frame">The frame the game started to draw.</param>
        void OnGameStartedFrameDraw(IFrame frame);
        /// <summary>
        /// Handle a game finished a frame draw.
        /// </summary>
        /// <param name="frame">The frame the game finished drawing.</param>
        void OnGameFinishedFrameDraw(IFrame frame);
        /// <summary>
        /// Setup for a game.
        /// </summary>
        /// <param name="game">The game to set up for.</param>
        void Setup(Game  game);
    }
}
