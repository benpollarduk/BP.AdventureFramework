using System;
using BP.AdventureFramework.Logic;
using BP.AdventureFramework.Tutorial.Demos;

namespace BP.AdventureFramework.Tutorial
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                SetupConsole();
                GameCreationCallback creator = null;

                while (creator == null)
                {
                    Console.Clear();

                    Console.WriteLine("Select Demo Game:");
                    Console.WriteLine("1. Everglades");
                    Console.WriteLine("2. Flat");
                    Console.WriteLine("3. Zelda");

                    switch (Console.ReadKey().Key)
                    {
                        case ConsoleKey.NumPad1:
                        case ConsoleKey.D1:

                            creator = Game.Create("A Strange World",
                                "You wake up at the entrance to a small clearing...",
                                Everglades.GenerateOverworld,
                                Everglades.GeneratePC,
                                g => false);

                            break;

                        case ConsoleKey.NumPad2:
                        case ConsoleKey.D2:

                            creator = Game.Create("Escape From Your Flat!",
                                "You wake up in the bedroom of your flat. Your a little disorientated, but then again you are most mornings! You're itching for some punk rock!",
                                Flat.GenerateOverworld,
                                Flat.GeneratePC,
                                g => false);

                            break;

                        case ConsoleKey.NumPad3:
                        case ConsoleKey.D3:

                            creator = Game.Create("The Legend Of Zelda: Links Texting!",
                                "It's a sunny day in Hyrule and Link is in his tree hut...",
                                Zelda.GenerateOverworld,
                                Zelda.GeneratePC,
                                Zelda.DetermineIfGameHasCompleted);

                            break;
                    }
                }

                using (var game = creator.Invoke())
                {
                    HostSetup.SetupWindowsConsole(game);
                    game.Execute();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception caught running demo: {e.Message}");
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
    }
}