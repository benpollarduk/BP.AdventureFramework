using System;
using BP.AdventureFramework.GameStructure;
using BP.AdventureFramework.Rendering;

namespace BP.AdventureFramework.Tutorial
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                InGameGraphics.BufferGraphics();

                SetupConsole();

                var helper = GameCreationHelper.Create("The Legend Of Zelda: Links Texting!",
                    "It's a sunny day in Hyrule and Link is in his tree hut...",
                    Zelda.GenerateOverworld,
                    Zelda.GeneratePC,
                    DetermineIfGameHasCompleted);


                using (var flow = new GameFlow(helper))
                {
                    HostSetup.SetupWindowsConsole(flow, "The Legend Of Zelda: Links Texting!");
                    flow.Begin();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught running Tutorial: {0}\nPress enter to close", e.Message);
                Console.ReadLine();
                Console.WriteLine("Closing...");
            }
        }

        /// <summary>
        /// Setup the console.
        /// </summary>
        private static void SetupConsole()
        {
            try
            {
                // try and set desired size
                Console.SetWindowSize(80, 50);
                Console.SetBufferSize(80, 50);
            }
            catch (ArgumentOutOfRangeException)
            {
                // let console size itself
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        /// <summary>
        /// Determine if the game has completed.
        /// </summary>
        /// <param name="game">The Game to check for completion.</param>
        /// <returns>True if the Game is complete, else false.</returns>
        private static bool DetermineIfGameHasCompleted(Game game)
        {
            return game.Overworld.CurrentRegion.CurrentRoom.Name.ToUpper() == "TAIL CAVE";
        }
    }
}