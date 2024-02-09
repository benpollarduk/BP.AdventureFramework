using System;
using System.Drawing;
using System.IO;
using BP.AdventureFramework.Rendering.Frames;

namespace BP.AdventureFramework.Logic
{
    /// <summary>
    /// Provides an adapter for the System.Console.
    /// </summary>
    internal class SystemConsoleAdapter : IConsoleAdapter
    {
        #region Implementation of IConsoleAdapter

        /// <summary>
        /// Get the input stream.
        /// </summary>
        public TextReader In => Console.In;

        /// <summary>
        /// Get the output stream.
        /// </summary>
        public TextWriter Out => Console.Out;

        /// <summary>
        /// Get the error output stream.
        /// </summary>
        public TextWriter Error => Console.Error;

        /// <summary>
        /// Wait for a key press.
        /// </summary>
        /// <param name="key">The ASCII code of the key to wait for.</param>
        /// <returns>True if the key pressed returned the same ASCII character as the key property, else false.</returns>
        public bool WaitForKeyPress(char key)
        {
            return Console.ReadKey().KeyChar == key;
        }

        /// <summary>
        /// Handle a game started a frame draw.
        /// </summary>
        /// <param name="frame">The frame the game started to draw.</param>
        public void OnGameStartedFrameDraw(IFrame frame)
        {
            Console.Clear();
        }

        /// <summary>
        /// Handle a game finished a frame draw.
        /// </summary>
        /// <param name="frame">The frame the game finished drawing.</param>
        public void OnGameFinishedFrameDraw(IFrame frame)
        {
            Console.CursorVisible = frame.ShowCursor;
            Console.SetCursorPosition(frame.CursorLeft, frame.CursorTop);
        }

        /// <summary>
        /// Setup for a game.
        /// </summary>
        /// <param name="game">The game to set up for.</param>
        public void Setup(Game game)
        {
            Console.Title = game.Name;
            var actualDisplaySize = new Size(game.DisplaySize.Width + 1, game.DisplaySize.Height);
            Console.SetWindowSize(actualDisplaySize.Width, actualDisplaySize.Height);
            Console.SetBufferSize(actualDisplaySize.Width, actualDisplaySize.Height);
        }

        #endregion
    }
}
