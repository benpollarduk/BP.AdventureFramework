using System;
using BP.AdventureFramework.GameStructure;
using BP.AdventureFramework.Rendering;

namespace BP.AdventureFramework.DemoQuest
{
    internal class Program
    {
        #region StaticMethods

        private static void Main(string[] args)
        {
            try
            {
                SetupConsole();
                InGameGraphics.BufferGraphics();
                GameCreationHelper creationHelper = null;

                while (creationHelper == null)
                {
                    Console.Clear();

                    Console.WriteLine("Select Demo Game:");
                    Console.WriteLine("1. Everglades");
                    Console.WriteLine("2. Flat");

                    switch (Console.ReadKey().Key)
                    {
                        case ConsoleKey.NumPad1:
                        case ConsoleKey.D1:
                            
                            creationHelper = GameCreationHelper.Create("A Strange World",
                                "You wake up at the entrance to a small clearing...",
                                Everglades.GenerateOverworld,
                                Everglades.GeneratePC,
                                g => false);

                            break;

                        case ConsoleKey.NumPad2:
                        case ConsoleKey.D2:
                            
                            creationHelper = GameCreationHelper.Create("Escape From Bagley House!",
                                "You wake up in the bedroom of your flat in Bagley house. Your a little disorientated, but then again you are most mornings! Your itching for some punk rock!",
                                Flat.GenerateOverworld,
                                Flat.GeneratePC,
                                g => false);

                            break;
                    }
                }

                using (var flow = new GameFlow(creationHelper))
                {
                    HostSetup.SetupWindowsConsole(flow, "BP.AdventureFramework Demo");
                    flow.Begin();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught running demo: {0}", e.Message);
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Setup the console
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

        #endregion
    }
}