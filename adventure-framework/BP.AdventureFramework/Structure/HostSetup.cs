using System;
using System.Drawing;
using BP.AdventureFramework.Rendering.Frames;

namespace BP.AdventureFramework.Structure
{
    /// <summary>
    /// Represents generic setup protocols for various Windows program types.
    /// </summary>
    public static class HostSetup
    {
        #region StaticMethods

        /// <summary>
        /// Setup the windows console for a new GameFlow.
        /// </summary>
        /// <param name="flow">The flow to prepare the windows Console for.</param>
        /// <param name="title">The title to display as the Console.Title property.</param>
        public static void SetupWindowsConsole(GameFlow flow, string title)
        {
            Console.Title = title;
            flow.Input = Console.In;
            flow.Output = Console.Out;
            flow.Error = Console.Error;
            flow.WaitForKeyPressCallback = key => Console.ReadKey().KeyChar == key;
            flow.DisplaySize = new Size(Console.WindowWidth, Console.WindowHeight);
            flow.DisplayInverted += ConsoleFlow_DisplayInverted;
            flow.FinishingFrameDraw += ConsoleFlow_FinishingFrameDraw;
            flow.StartingFrameDraw += ConsoleFlow_StartingFrameDraw;
        }

        #endregion

        #region EventHandlers

        private static void ConsoleFlow_DisplayInverted(object sender, EventArgs e)
        {
            var background = Console.BackgroundColor;
            Console.BackgroundColor = Console.ForegroundColor;
            Console.ForegroundColor = background;
        }

        private static void ConsoleFlow_FinishingFrameDraw(object sender, FrameEventArgs e)
        {
            Console.CursorVisible = e.Frame.ShowCursor;
            Console.SetCursorPosition(e.Frame.CursorLeft, e.Frame.CursorTop);
        }

        private static void ConsoleFlow_StartingFrameDraw(object sender, FrameEventArgs e)
        {
            Console.Clear();
        }

        #endregion
    }
}