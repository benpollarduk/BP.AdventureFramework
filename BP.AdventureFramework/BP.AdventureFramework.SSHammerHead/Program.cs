using System;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Logic;

namespace BP.AdventureFramework.SSHammerHead
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                OverworldCreationCallback overworldCreator = p =>
                {
                    var ship = Ship.GenerateRegion(p);
                    var overworld = new Overworld("CTY-1 Galaxy", "A solar system in deep space, part of the SR389 galaxy.");
                    overworld.AddRegion(ship);
                    return overworld;
                };

                var about = "This is a short demo of the BP.AdventureFramework made up from test chunks of games that were build to test different features during development.";
                var introduction = "After years of absence, the SS Hammerhead reappeared in the delta quadrant of the CTY-1 solar system.\n\nA ship was hurriedly prepared and scrambled and made contact 27 days later.\n\nYou enter the outer most airlock and it closes behind you. With a sense of foreboding you see your ship detach from the airlock and retreat to a safe distance.";

                var creator = Game.Create("Trouble aboard the SS HammerHead",
                    introduction,
                    about,
                    x => overworldCreator(x),
                    Ship.GeneratePC,
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
