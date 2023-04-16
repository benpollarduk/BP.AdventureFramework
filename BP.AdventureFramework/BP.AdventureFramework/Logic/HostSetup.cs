using System;
using System.Drawing;
using BP.AdventureFramework.Rendering.Frames;

namespace BP.AdventureFramework.Logic
{
    /// <summary>
    /// Represents generic setup protocols for various Windows program types.
    /// </summary>
    internal static class HostSetup
    {
        #region StaticMethods

        /// <summary>
        /// Setup the windows console for a game.
        /// </summary>
        /// <param name="game">The game to prepare the windows Console for.</param>
        internal static void SetupWindowsConsole(Game game)
        {
            Console.Title = game.Name;
            game.Input = Console.In;
            game.Output = Console.Out;
            game.Error = Console.Error;
            game.WaitForKeyPressCallback = key => Console.ReadKey().KeyChar == key;
            game.DisplaySize = new Size(Console.WindowWidth, Console.WindowHeight);
            game.FinishedFrameDraw += Game_FinishedFrameDraw;
            game.StartingFrameDraw += Game_StartingFrameDraw;
        }

        #endregion

        #region EventHandlers

        private static void Game_FinishedFrameDraw(object sender, Frame e)
        {
            Console.CursorVisible = e.ShowCursor;
            Console.SetCursorPosition(e.CursorLeft, e.CursorTop);
        }

        private static void Game_StartingFrameDraw(object sender, Frame e)
        {
            Console.Clear();
        }

        #endregion
    }
}