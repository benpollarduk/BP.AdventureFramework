using System;
using System.IO;
using BP.AdventureFramework.Logic;
using BP.AdventureFramework.Rendering.Frames;

namespace BP.AdventureFramework.Tests.Logic
{
    /// <summary>
    /// Provides a console adapter for tests.
    /// </summary>
    internal class TestConsoleAdapter : IConsoleAdapter
    {
        #region Properties

        /// <summary>
        /// Get or set the input bytes.
        /// </summary>
        public byte[] InBytes { get; set; } = Array.Empty<byte>();

        /// <summary>
        /// Get or set the output bytes.
        /// </summary>
        public byte[] OutBytes { get; set; } = Array.Empty<byte>();

        /// <summary>
        /// Get or set the output error bytes.
        /// </summary>
        public byte[] ErrorBytes { get; set; } = Array.Empty<byte>();

        #endregion

        #region Implementation of IConsoleAdapter

        /// <summary>
        /// Get the input stream.
        /// </summary>
        public TextReader In
        {
            get
            {
                var memoryStream = new MemoryStream(InBytes);
                return new StreamReader(memoryStream);
            }
        }

        /// <summary>
        /// Get the output stream.
        /// </summary>
        public TextWriter Out
        {
            get
            {
                var memoryStream = new MemoryStream(OutBytes);
                return new StreamWriter(memoryStream);
            }
        }

        /// <summary>
        /// Get the error output stream.
        /// </summary>
        public TextWriter Error
        {
            get
            {
                var memoryStream = new MemoryStream(ErrorBytes);
                return new StreamWriter(memoryStream);
            }
        }

        /// <summary>
        /// Wait for a key press.
        /// </summary>
        /// <param name="key">The ASCII code of the key to wait for.</param>
        /// <returns>True if the key pressed returned the same ASCII character as the key property, else false.</returns>
        public bool WaitForKeyPress(char key)
        {
            return true;
        }

        /// <summary>
        /// Handle a game started a frame draw.
        /// </summary>
        /// <param name="frame">The frame the game started to draw.</param>
        public void OnGameStartedFrameDraw(IFrame frame)
        {
        }

        /// <summary>
        /// Handle a game finished a frame draw.
        /// </summary>
        /// <param name="frame">The frame the game finished drawing.</param>
        public void OnGameFinishedFrameDraw(IFrame frame)
        {
        }

        /// <summary>
        /// Setup for a game.
        /// </summary>
        /// <param name="game">The game to set up for.</param>
        public void Setup(Game game)
        {
        }

        #endregion
    }
}
