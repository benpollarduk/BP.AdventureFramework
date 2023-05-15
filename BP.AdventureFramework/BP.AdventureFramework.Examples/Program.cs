using System;
using System.Collections.Generic;
using System.Linq;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Examples.Assets;
using BP.AdventureFramework.Logic;
using BP.AdventureFramework.Utilities.Generation;
using BP.AdventureFramework.Utilities.Generation.Themes;

namespace BP.AdventureFramework.Examples
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                OverworldCreationCallback overworldCreator = x =>
                {
                    var options = new GameGenerationOptions { MaximumRegions = 1, MinimumRegions = 1 };
                    var generator = new GameGenerator(string.Empty, string.Empty);
                    var castle = generator.Generate(options, new Castle(), out _).Make();
                    var evergaldes = Everglades.GenerateRegion(x);
                    var flat = Flat.GenerateRegion(x);
                    var zelda = Zelda.GenerateRegion(x);

                    var regions = new List<Region>
                    {
                        evergaldes,
                        flat,
                        zelda,
                        castle.Regions.First()
                    };
                    
                    var overworld = new Overworld("Demo", "A demo of the BP.AdventureFramework.");
                    overworld.AddRegion(Hub.GenerateHub(regions.ToArray(), overworld));

                    foreach (var region in regions)
                        overworld.AddRegion(region);

                    return overworld;
                };

                var about = "This is a short demo of the BP.AdventureFramework made up from test chunks of games that were build to test different features during development.";

                var creator = Game.Create("BP.AdventureFramework Demo",
                    about,
                    about,
                    x => overworldCreator(x), 
                    Hub.GeneratePC,
                    g => CompletionCheckResult.NotComplete);

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