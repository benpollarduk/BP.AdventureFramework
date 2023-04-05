using System;
using System.Drawing;
using AdventureFramework.Rendering.Frames;

namespace AdventureFramework.Structure
{
    /// <summary>
    /// Represents generic setup protcols for various Windows program types
    /// </summary>
    public static class HostSetup
    {
        #region StaticMethods

        /// <summary>
        /// Setup the windows console for a new GameFlow
        /// </summary>
        /// <param name="flow">The flow to prepare the windows Console for</param>
        /// <param name="title">The title to display as the Console.Title property</param>
        public static void SetupWindowsConsole(GameFlow flow, string title)
        {
            // set title
            Console.Title = title;

            // set input
            flow.Input = Console.In;

            // set output
            flow.Output = Console.Out;

            // set error
            flow.Error = Console.Error;

            // set callback for console key presses
            flow.WaitForKeyPressCallback = key =>
            {
                // check characters
                return Console.ReadKey().KeyChar == key;
            };

            // set standard size of window
            flow.DisplaySize = new Size(Console.WindowWidth, Console.WindowHeight);

            // handle inversion
            flow.DisplayInverted += consoleFlow_DisplayInverted;

            // handle exit drawing
            flow.FinishingFrameDraw += consoleFlow_FinishingFrameDraw;

            // handle enter drawing
            flow.StartingFrameDraw += consoleFlow_StartingFrameDraw;
        }

        #endregion

        #region EventHandlers

        private static void consoleFlow_DisplayInverted(object sender, EventArgs e)
        {
            // hold background
            var background = Console.BackgroundColor;

            // set background
            Console.BackgroundColor = Console.ForegroundColor;

            // set foreground
            Console.ForegroundColor = background;
        }

        private static void consoleFlow_FinishingFrameDraw(object sender, FrameEventArgs e)
        {
            // if cursor should be shown
            if (e.Frame.ShowCursor)
                // show cursor
                Console.CursorVisible = true;
            else
                // hide cursor
                Console.CursorVisible = false;

            // set cursor position
            Console.SetCursorPosition(e.Frame.CursorLeft, e.Frame.CursorTop);
        }

        private static void consoleFlow_StartingFrameDraw(object sender, FrameEventArgs e)
        {
            // clear
            Console.Clear();
        }

        #endregion
    }
}