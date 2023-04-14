using System;
using System.Drawing;
using BP.AdventureFramework.Rendering.Frames;

namespace BP.AdventureFramework.Logic
{
    /// <summary>
    /// Represents generic setup protocols for various Windows program types.
    /// </summary>
    public static class HostSetup
    {
        #region StaticMethods

        /// <summary>
        /// Setup the windows console for a new GameManager.
        /// </summary>
        /// <param name="gameManager">The game manager to prepare the windows Console for.</param>
        /// <param name="title">The title to display as the Console.Title property.</param>
        public static void SetupWindowsConsole(GameManager gameManager, string title)
        {
            Console.Title = title;
            gameManager.Input = Console.In;
            gameManager.Output = Console.Out;
            gameManager.Error = Console.Error;
            gameManager.WaitForKeyPressCallback = key => Console.ReadKey().KeyChar == key;
            gameManager.DisplaySize = new Size(Console.WindowWidth, Console.WindowHeight);
            gameManager.FinishingFrameDraw += ConsoleFlow_FinishingFrameDraw;
            gameManager.StartingFrameDraw += ConsoleFlow_StartingFrameDraw;
        }

        #endregion

        #region EventHandlers

        private static void ConsoleFlow_FinishingFrameDraw(object sender, Frame e)
        {
            Console.CursorVisible = e.ShowCursor;
            Console.SetCursorPosition(e.CursorLeft, e.CursorTop);
        }

        private static void ConsoleFlow_StartingFrameDraw(object sender, Frame e)
        {
            Console.Clear();
        }

        #endregion
    }
}