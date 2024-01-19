using System;
using System.Collections.Generic;
using System.Linq;
using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Commands;
using BP.AdventureFramework.Examples.Assets.Player;
using BP.AdventureFramework.Examples.Assets.Regions.Everglades;
using BP.AdventureFramework.Examples.Assets.Regions.Flat;
using BP.AdventureFramework.Examples.Assets.Regions.Hub;
using BP.AdventureFramework.Examples.Assets.Regions.Zelda;
using BP.AdventureFramework.Examples.Assets.Regions.Zelda.Rooms;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Interpretation;
using BP.AdventureFramework.Logic;
using BP.AdventureFramework.Utilities.Generation;
using BP.AdventureFramework.Utilities.Generation.Themes;

namespace BP.AdventureFramework.Examples
{
    internal class Program
    {
        private static EndCheckResult DetermineIfGameHasCompleted(Game game)
        {
            var atDestination = TailCave.Name.EqualsExaminable(game.Overworld.CurrentRegion.CurrentRoom);

            if (!atDestination)
                return EndCheckResult.NotEnded;

            return new EndCheckResult(true, "Game Over", "You have reached the end of the game, thanks for playing!");
        }

        private static EndCheckResult DetermineIfGameOver(Game game)
        {
            if (game.Player.IsAlive)
                return EndCheckResult.NotEnded;

            return new EndCheckResult(true, "Game Over", "You are dead!");
        }

        private static void PopulateHub(Region hub, Overworld overworld, Region[] otherRegions)
        {
            var room = hub.CurrentRoom;

            foreach (var otherRegion in otherRegions)
            {
                room.AddItem(new Item($"{otherRegion.Identifier.Name} Sphere", "A glass sphere, about the size of a snooker ball. Inside you can see a swirling mist.", true)
                {
                    Commands = new[]
                    {
                        new CustomCommand(new CommandHelp($"Warp {otherRegion.Identifier.Name}", $"Use the {otherRegion.Identifier.Name} Sphere to warp to the {otherRegion.Identifier.Name}."), true, (g, _) =>
                        {
                            var move = overworld?.Move(otherRegion) ?? false;

                            if (!move)
                                return new Reaction(ReactionResult.Error, $"Could not move to {otherRegion.Identifier.Name}.");

                            g.DisplayTransition(string.Empty, $"You peer inside the sphere and feel faint. When the sensation passes you open you eyes and have been transported to the {otherRegion.Identifier.Name}.");

                            return new Reaction(ReactionResult.Internal, string.Empty);
                        })
                    }
                });
            }
        }

        private static void Main(string[] args)
        {
            try
            {
                OverworldCreationCallback overworldCreator = p =>
                {
                    var options = new GameGenerationOptions { MaximumRegions = 1, MinimumRegions = 1 };
                    var generator = new GameGenerator(string.Empty, string.Empty);
                    var castle = generator.Generate(options, new Castle(), out _).Make();

                    var regions = new List<Region>
                    {
                        new Everglades().Instantiate(),
                        new Flat().Instantiate(),
                        new Zelda(p).Instantiate(),
                        castle.Regions.First()
                    };
                    
                    var overworld = new Overworld("Demo", "A demo of the BP.AdventureFramework.");

                    var hub = new Hub().Instantiate();
                    PopulateHub(hub, overworld, regions.ToArray());
                    overworld.AddRegion(hub);

                    foreach (var region in regions)
                        overworld.AddRegion(region);

                    overworld.Commands = new[]
                    {
                        // add a hidden custom command to the overworld that allows jumping around a region for debugging purposes
                        new CustomCommand(new CommandHelp("Jump", "Jump to a location in a region."), false, (g, a) =>
                        {
                            var x = 0;
                            var y = 0;
                            var z = 0;

                            if (a?.Length >= 3)
                            {
                                int.TryParse(a[0], out x);
                                int.TryParse(a[1], out y);
                                int.TryParse(a[2], out z);
                            }

                            var result = g.Overworld.CurrentRegion.JumpToRoom(x, y, z);

                            if (!result)
                                return new Reaction(ReactionResult.Error, $"Failed to jump to {x} {y} {z}.");

                            return new Reaction(ReactionResult.OK, $"Jumped to {x} {y} {z}.");
                        })
                    };

                    return overworld;
                };

                var about = "This is a short demo of the BP.AdventureFramework made up from test chunks of games that were build to test different features during development.";

                var creator = Game.Create("BP.AdventureFramework Demo",
                    about,
                    about,
                    x => overworldCreator(x), 
                    () => new Player().Instantiate(),
                    DetermineIfGameHasCompleted,
                    DetermineIfGameOver);

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