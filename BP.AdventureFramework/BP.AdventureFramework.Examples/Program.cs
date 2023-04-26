using System;
using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Examples.Assets;
using BP.AdventureFramework.Logic;
using BP.AdventureFramework.Utils;
using BP.AdventureFramework.Utils.Generation;
using BP.AdventureFramework.Utils.Generation.Simple.Themes;

namespace BP.AdventureFramework.Examples
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                GameCreationCallback creator = null;

                while (creator == null)
                {
                    Console.Clear();

                    Console.WriteLine("Select Demo Game:");
                    Console.WriteLine("1. Everglades");
                    Console.WriteLine("2. Flat");
                    Console.WriteLine("3. Zelda");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("4. Generate (Dungeon - Experimental)");
                    Console.WriteLine("5. Generate (Forest - Experimental)");
                    Console.ForegroundColor = ConsoleColor.White;

                    GameGenerationOptions options;
                    GameGenerator generator;
                    OverworldMaker overworld;

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
                                "You wake up in the bedroom of your flat. You're a little disorientated, but then again you are most mornings! You're itching for some punk rock!",
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

                        case ConsoleKey.NumPad4:
                        case ConsoleKey.D4:

                            options = new GameGenerationOptions();
                            generator = new GameGenerator(Identifier.Empty, Description.Empty);
                            overworld = generator.Generate(options, new Dungeon(), out var dungeonDeed);

                            creator = Game.Create($"Dungeon generated with {dungeonDeed}",
                                "",
                                p => overworld.Make(),
                                () => new PlayableCharacter("You", "Just you."),
                                g => false);

                            break;

                        case ConsoleKey.NumPad5:
                        case ConsoleKey.D5:

                            options = new GameGenerationOptions();
                            generator = new GameGenerator(Identifier.Empty, Description.Empty);
                            overworld = generator.Generate(options, new Forest(), out var forestSeed);

                            creator = Game.Create($"Forest generated with {forestSeed}",
                                "",
                                p => overworld.Make(),
                                () => new PlayableCharacter("You", "Just you."),
                                g => false);

                            break;
                    }
                }

                Game.Execute(creator);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception caught running demo: {e.Message}");
                Console.ReadKey();
            }
        }
    }
}