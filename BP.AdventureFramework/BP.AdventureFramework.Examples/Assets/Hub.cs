using System.Collections.Generic;
using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Commands;
using BP.AdventureFramework.Conversations;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Interpretation;
using BP.AdventureFramework.Rendering.FrameBuilders;
using BP.AdventureFramework.Utilities;

namespace BP.AdventureFramework.Examples.Assets
{
    internal class Hub
    {
        internal static PlayableCharacter GeneratePC()
        {
            var player = new PlayableCharacter("Ben", "You are a 25 year old man, dressed in shorts, a t-shirt and flip-flops.", new Item(Everglades.Knife, "A small pocket knife", true))
            {
                Interaction = (i, target) =>
                {
                    if (i == null)
                        return new InteractionResult(InteractionEffect.NoEffect, null);

                    if (Everglades.Knife.EqualsExaminable(i))
                        return new InteractionResult(InteractionEffect.FatalEffect, i, "You slash wildly at your own throat. You are dead.");

                    if (Flat.EmptyCoffeeMug.EqualsIdentifier(i.Identifier))
                        return new InteractionResult(InteractionEffect.NoEffect, i, "If there was some coffee in the mug you could drink it");

                    if (Flat.Guitar.EqualsIdentifier(i.Identifier))
                        return new InteractionResult(InteractionEffect.NoEffect, i, "You bust out some Bad Religion. Cracking, shame the guitar isn't plugged in to an amplified though...");

                    return new InteractionResult(InteractionEffect.NoEffect, i);
                }
            };

            return player;
        }

        internal static Region GenerateHub(Region[] otherRegions, Overworld overworld)
        {
            var regionMaker = new RegionMaker("Jungle", "A dense jungle, somewhere tropical.");
            var spheres = new List<Item>();

            foreach (var region in otherRegions)
            {
                spheres.Add(new Item($"{region.Identifier.Name} Sphere", "A glass sphere, about the size of a snooker ball. Inside you can see a swirling mist.", true)
                {
                    Commands = new[]
                    {
                        new CustomCommand(new CommandHelp($"Warp {region.Identifier.Name}", $"Use the {region.Identifier.Name} Sphere to warp to the {region.Identifier.Name}."), (g, a) =>
                        {
                            var move = overworld?.Move(region) ?? false;

                            if (!move)
                                return new Reaction(ReactionResult.Error, $"Could not move to {region.Identifier.Name}.");

                            g.DisplayTransition(string.Empty, $"You peer inside the sphere and feel faint. When the sensation passes you open you eyes and have been transported to the {region.Identifier.Name}.");

                            return new Reaction(ReactionResult.Internal, string.Empty);
                        })
                    }
                });
            }

            var conversation = new Conversation(
                new Paragraph("Squarrrkkk!"),
                new Paragraph("Would you like to change modes?")
                {
                    Responses = new[]
                    {
                        new Response("Yes please, change to default."),
                        new Response("Yes please, change to simple.", 2),
                        new Response("Yes please, change to legacy.", 3),
                        new Response("No thanks, keep things as they are.", 4)
                    }
                },
                new Paragraph("Arrk! Color it is.", g => g.FrameBuilders = FrameBuilderCollections.Default, -1),
                new Paragraph("Eeek, simple be fine too!", g => g.FrameBuilders = FrameBuilderCollections.Simple, -2),
                new Paragraph("Squarrk! Legacy, looks old. Arrk!", g => g.FrameBuilders = FrameBuilderCollections.Legacy, - 3),
                new Paragraph("Fine, suit yourself! Squarrk!", -4)
            );

            var parrot = new NonPlayableCharacter(new Identifier("Parrot"), new Description("A brightly colored parrot."))
            {
                Conversation = conversation
            };

            var clearing = new Room("Jungle Clearing",
                $"You are in a small clearing in a jungle, tightly enclosed by undergrowth. You have no idea how you got here. The chirps and buzzes coming from insects in the undergrowth are intense. There are {otherRegions.Length} stone pedestals in front of you. Each has a small globe on top of it.",
                new Exit[0],
                spheres.ToArray()
            );

            clearing.AddCharacter(parrot);

            regionMaker[0, 0, 0] = clearing;
            return regionMaker.Make(0, 0, 0);
        }
    }
}
