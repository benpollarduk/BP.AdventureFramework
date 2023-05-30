using System;
using System.Collections.Generic;
using System.Linq;
using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Commands;
using BP.AdventureFramework.Commands.Game;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Logic;

namespace BP.AdventureFramework.Interpretation
{
    /// <summary>
    /// Provides an object that can be used for interpreting game commands.
    /// </summary>
    internal class GameCommandInterpreter : IInterpreter
    {
        #region Constants

        /// <summary>
        /// Get the north command.
        /// </summary>
        public const string North = "North";

        /// <summary>
        /// Get the north (short) command.
        /// </summary>
        public const string NorthShort = "N";

        /// <summary>
        /// Get the south command.
        /// </summary>
        public const string South = "South";

        /// <summary>
        /// Get the south (short) command.
        /// </summary>
        public const string SouthShort = "S";

        /// <summary>
        /// Get the east command.
        /// </summary>
        public const string East = "East";

        /// <summary>
        /// Get the east (short) command.
        /// </summary>
        public const string EastShort = "E";

        /// <summary>
        /// Get the west command.
        /// </summary>
        public const string West = "West";

        /// <summary>
        /// Get the west (short) command.
        /// </summary>
        public const string WestShort = "W";

        /// <summary>
        /// Get the up command.
        /// </summary>
        public const string Up = "Up";

        /// <summary>
        /// Get the up (short) command.
        /// </summary>
        public const string UpShort = "U";

        /// <summary>
        /// Get the down command.
        /// </summary>
        public const string Down = "Down";

        /// <summary>
        /// Get the down (short) command.
        /// </summary>
        public const string DownShort = "D";

        /// <summary>
        /// Get the drop command.
        /// </summary>
        public const string Drop = "Drop";

        /// <summary>
        /// Get the drop (short) command.
        /// </summary>
        public const string DropShort = "R";

        /// <summary>
        /// Get the use command.
        /// </summary>
        public const string Use = "Use";

        /// <summary>
        /// Get the on command.
        /// </summary>
        public const string On = "On";

        /// <summary>
        /// Get the talk command.
        /// </summary>
        public const string Talk = "Talk";

        /// <summary>
        /// Get the talk (short) command.
        /// </summary>
        public const string TalkShort = "L";

        /// <summary>
        /// Get the to command.
        /// </summary>
        public const string To = "To";

        /// <summary>
        /// Get the take command.
        /// </summary>
        public const string Take = "Take";

        /// <summary>
        /// Get the take (short) command.
        /// </summary>
        public const string TakeShort = "T";

        /// <summary>
        /// Get the all command.
        /// </summary>
        public const string All = "All";

        /// <summary>
        /// Get the examine command.
        /// </summary>
        public const string Examine = "Examine";

        /// <summary>
        /// Get the examine (short) command.
        /// </summary>
        public const string ExamineShort = "X";

        /// <summary>
        /// Get the me command.
        /// </summary>
        public const string Me = "Me";

        /// <summary>
        /// Get the room command.
        /// </summary>
        public const string Room = "Room";

        /// <summary>
        /// Get the region command.
        /// </summary>
        public const string Region = "Region";

        /// <summary>
        /// Get the overworld command.
        /// </summary>
        public const string Overworld = "Overworld";

        /// <summary>
        /// Get a string representing a variable.
        /// </summary>
        private const string Variable = "__";

        #endregion

        #region StaticProperties

        /// <summary>
        /// Get an array of all supported commands.
        /// </summary>
        public static CommandHelp[] DefaultSupportedCommands { get; } =
        {
            new CommandHelp($"{North}/{NorthShort}", "Move north"),
            new CommandHelp($"{East}/{EastShort}", "Move east"),
            new CommandHelp($"{South}/{SouthShort}", "Move south"),
            new CommandHelp($"{West}/{WestShort}", "Move west"),
            new CommandHelp($"{Up}/{UpShort}", "Move up"),
            new CommandHelp($"{Down}/{DownShort}", "Move down"),
            new CommandHelp($"{Drop}/{DropShort} {Variable}", "Drop an item"),
            new CommandHelp($"{Examine}/{ExamineShort} {Variable}", "Examine anything in the game"),
            new CommandHelp($"{Take}/{TakeShort} {Variable}", "Take an item"),
            new CommandHelp($"{Take}/{TakeShort} {All}", "Take all items in a room"),
            new CommandHelp($"{Talk}/{TalkShort} {To.ToLower()} {Variable}", "Talk to a character"),
            new CommandHelp($"{Use} {Variable}", "Use an item on this room"),
            new CommandHelp($"{Use} {Variable} {On.ToLower()} {Variable}", "Use an item on another item or character")
        };

        #endregion

        #region StaticMethods

        /// <summary>
        /// Split text in to a verb and a noun.
        /// </summary>
        /// <param name="text">The text to split.</param>
        /// <param name="verb">The verb.</param>
        /// <param name="noun">The noun.</param>
        private static void SplitTextToVerbAndNoun(string text, out string verb, out string noun)
        {
            // if there is a space
            if (text.IndexOf(" ", StringComparison.Ordinal) > -1)
            {
                // verb all text up to space
                verb = text.Substring(0, text.IndexOf(" ", StringComparison.Ordinal)).Trim();

                // noun is all text after space
                noun = text.Substring(text.IndexOf(" ", StringComparison.Ordinal)).Trim();
            }
            else
            {
                verb = text;
                noun = string.Empty;
            }
        }

        /// <summary>
        /// Try and parse the Drop command.
        /// </summary>
        /// <param name="text">The text to parse.</param>
        /// <param name="game">The game.</param>
        /// <param name="command">The resolved command.</param>
        /// <returns>True if the input could be parsed, else false.</returns>
        private static bool TryParseDropCommand(string text, Game game, out ICommand command)
        {
            SplitTextToVerbAndNoun(text, out var verb, out var noun);

            if (!Drop.Equals(verb, StringComparison.CurrentCultureIgnoreCase) && !DropShort.Equals(verb, StringComparison.CurrentCultureIgnoreCase))
            {
                command = null;
                return false;
            }

            game.Player.FindItem(noun, out var item);
            command = new Drop(item);
            return true;
        }

        /// <summary>
        /// Try and parse the Take command.
        /// </summary>
        /// <param name="text">The text to parse.</param>
        /// <param name="game">The game.</param>
        /// <param name="command">The resolved command.</param>
        /// <returns>True if the input could be parsed, else false.</returns>
        private static bool TryParseTakeCommand(string text, Game game, out ICommand command)
        {
            SplitTextToVerbAndNoun(text, out var verb, out var noun);

            if (!Take.Equals(verb, StringComparison.CurrentCultureIgnoreCase) && !TakeShort.Equals(verb, StringComparison.CurrentCultureIgnoreCase))
            {
                command = null;
                return false;
            }

            Item item;

            // it no item specified then find the first takeable one
            if (string.IsNullOrEmpty(noun))
            {
                item = game.Overworld.CurrentRegion.CurrentRoom.Items.FirstOrDefault(x => x.IsTakeable);

                if (item == null)
                {
                    command = new Unactionable("There are no takeable items in the room.");
                    return true;
                }
            }
            else if (noun.Equals(All, StringComparison.CurrentCultureIgnoreCase))
            {
                command = new TakeAll();
                return true;
            }
            else
            {
                if (!game.Overworld.CurrentRegion.CurrentRoom.FindItem(noun, out item))
                {
                    command = new Unactionable("There is no such item in the room.");
                    return true;
                }
            }

            command = new Take(item);
            return true;
        }

        /// <summary>
        /// Try and parse the Talk command.
        /// </summary>
        /// <param name="text">The text to parse.</param>
        /// <param name="game">The game.</param>
        /// <param name="command">The resolved command.</param>
        /// <returns>True if the input could be parsed, else false.</returns>
        private static bool TryParseTalkCommand(string text, Game game, out ICommand command)
        {
            SplitTextToVerbAndNoun(text, out var verb, out var noun);

            if (!Talk.Equals(verb, StringComparison.CurrentCultureIgnoreCase) && !TalkShort.Equals(verb, StringComparison.CurrentCultureIgnoreCase))
            {
                command = null;
                return false;
            }

            // determine if a target has been specified
            if (noun.Length > 3 && string.Equals(noun.Substring(0, 2), $"{To} ", StringComparison.CurrentCultureIgnoreCase))
            {
                noun = noun.Remove(0, 3);

                if (game.Overworld.CurrentRegion.CurrentRoom.FindCharacter(noun, out var nPC))
                {
                    command = new Talk(nPC);
                    return true;
                }
            }

            if (game.Overworld.CurrentRegion.CurrentRoom.Characters.Length == 1)
            {
                command = new Talk(game.Overworld.CurrentRegion.CurrentRoom.Characters.First());
                return true;
            }

            command = new Unactionable("No-one is around to talk to");
            return true;
        }

        /// <summary>
        /// Try and parse the Examine command.
        /// </summary>
        /// <param name="text">The text to parse.</param>
        /// <param name="game">The game.</param>
        /// <param name="command">The resolved command.</param>
        /// <returns>True if the input could be parsed, else false.</returns>
        private static bool TryParseExamineCommand(string text, Game game, out ICommand command)
        {
            SplitTextToVerbAndNoun(text, out var verb, out var noun);

            if (!verb.Equals(Examine, StringComparison.CurrentCultureIgnoreCase) && !verb.Equals(ExamineShort, StringComparison.CurrentCultureIgnoreCase))
            {
                command = null;
                return false;
            }

            if (string.IsNullOrEmpty(noun))
            {
                // default to current room
                command = new Examine(game.Overworld.CurrentRegion.CurrentRoom);
                return true;
            }

            // check player items
            if (game.Player.FindItem(noun, out var item))
            {
                command = new Examine(item);
                return true;
            }

            // check items in room
            if (game.Overworld.CurrentRegion.CurrentRoom.FindItem(noun, out item))
            {
                command = new Examine(item);
                return true;
            }

            // check characters in room
            if (game.Overworld.CurrentRegion.CurrentRoom.FindCharacter(noun, out var character))
            {
                command = new Examine(character);
                return true;
            }

            // check exits to room
            if (TryParseToDirection(noun, out var direction))
            {
                if (game.Overworld.CurrentRegion.CurrentRoom.FindExit(direction, false, out var exit))
                {
                    command = new Examine(exit);
                    return true;
                }

                command = new Unactionable($"There is no exit in this room to the {direction}");
                return true;
            }

            // check self examination
            if (Me.Equals(noun, StringComparison.CurrentCultureIgnoreCase) || noun.EqualsExaminable(game.Player))
            {
                command = new Examine(game.Player);
                return true;
            }

            // check room examination
            if (Room.Equals(noun, StringComparison.CurrentCultureIgnoreCase) || noun.EqualsExaminable(game.Overworld.CurrentRegion.CurrentRoom))
            {
                command = new Examine(game.Overworld.CurrentRegion.CurrentRoom);
                return true;
            }

            // check region examination
            if (Region.Equals(noun, StringComparison.CurrentCultureIgnoreCase) || noun.EqualsExaminable(game.Overworld.CurrentRegion))
            {
                command = new Examine(game.Overworld.CurrentRegion);
                return true;
            }

            // check overworld examination
            if (Overworld.Equals(noun, StringComparison.CurrentCultureIgnoreCase) || noun.EqualsExaminable(game.Overworld))
            {
                command = new Examine(game.Overworld);
                return true;
            }

            // unknown
            if (!string.IsNullOrEmpty(noun))
            {
                command = new Unactionable($"Can't examine {noun}.");
                return true;
            }

            // default to current room
            command = new Examine(game.Overworld.CurrentRegion.CurrentRoom);
            return true;
        }

        /// <summary>
        /// Try and parse the UseOn command.
        /// </summary>
        /// <param name="text">The text to parse.</param>
        /// <param name="game">The game.</param>
        /// <param name="command">The resolved command.</param>
        /// <returns>True if the input could be parsed, else false.</returns>
        private static bool TryParseUseOnCommand(string text, Game game, out ICommand command)
        {
            SplitTextToVerbAndNoun(text, out var verb, out var noun);

            if (!Use.Equals(verb, StringComparison.CurrentCultureIgnoreCase))
            {
                command = null;
                return false;
            }

            IInteractWithItem target;
            noun = noun.ToUpper();
            var on = On.ToUpper();
            var itemName = noun;

            if (noun.Contains($" {On.ToUpper()} "))
            {
                itemName = noun.Substring(0, noun.IndexOf($" {on} ", StringComparison.CurrentCultureIgnoreCase));
                noun = noun.Replace(itemName, string.Empty);
                var targetName = noun.Replace($" {on} ", string.Empty);

                if (targetName.Equals(Me, StringComparison.CurrentCultureIgnoreCase))
                    target = game.Player;
                else if (targetName.Equals(Room, StringComparison.CurrentCultureIgnoreCase))
                    target = game.Overworld.CurrentRegion.CurrentRoom;
                else
                    target = game.FindInteractionTarget(targetName);
            }
            else
            {
                target = game.Overworld.CurrentRegion.CurrentRoom;
            }

            if (!game.Player.FindItem(itemName, out var item) && !game.Overworld.CurrentRegion.CurrentRoom.FindItem(itemName, out item))
            {
                command = new Unactionable("You don't have that item.");
                return true;
            }

            command = new UseOn(item, target);
            return true;
        }

        /// <summary>
        /// Try and parse a string to a Direction.
        /// </summary>
        /// <param name="text">The string to parse.</param>
        /// <param name="direction">The direction.</param>
        /// <returns>The result of the parse.</returns>
        private static bool TryParseToDirection(string text, out Direction direction)
        {
            if (text.Equals(North, StringComparison.CurrentCultureIgnoreCase) || text.Equals(NorthShort, StringComparison.CurrentCultureIgnoreCase))
            {
                direction = Direction.North;
                return true;
            }

            if (text.Equals(East, StringComparison.CurrentCultureIgnoreCase) || text.Equals(EastShort, StringComparison.CurrentCultureIgnoreCase))
            {
                direction = Direction.East;
                return true;
            }

            if (text.Equals(South, StringComparison.CurrentCultureIgnoreCase) || text.Equals(SouthShort, StringComparison.CurrentCultureIgnoreCase))
            {
                direction = Direction.South;
                return true;
            }

            if (text.Equals(West, StringComparison.CurrentCultureIgnoreCase) || text.Equals(WestShort, StringComparison.CurrentCultureIgnoreCase))
            {
                direction = Direction.West;
                return true;
            }

            if (text.Equals(Up, StringComparison.CurrentCultureIgnoreCase) || text.Equals(UpShort, StringComparison.CurrentCultureIgnoreCase))
            {
                direction = Direction.Up;
                return true;
            }

            if (text.Equals(Down, StringComparison.CurrentCultureIgnoreCase) || text.Equals(DownShort, StringComparison.CurrentCultureIgnoreCase))
            {
                direction = Direction.Down;
                return true;
            }

            direction = Direction.East;
            return false;
        }


        #endregion

        #region Implementation of IInterpreter

        /// <summary>
        /// Get an array of all supported commands.
        /// </summary>
        public CommandHelp[] SupportedCommands { get; } = DefaultSupportedCommands;

        /// <summary>
        /// Interpret a string.
        /// </summary>
        /// <param name="input">The string to interpret.</param>
        /// <param name="game">The game.</param>
        /// <returns>The result of the interpretation.</returns>
        public InterpretationResult Interpret(string input, Game game)
        {
            // try and parse as movement
            if (TryParseToDirection(input, out var direction))
                return new InterpretationResult(true, new Move(direction));

            // handle as drop command
            if (TryParseDropCommand(input, game, out var drop))
                return new InterpretationResult(true, drop);

            // handle as examine command
            if (TryParseExamineCommand(input, game, out var examine))
                return new InterpretationResult(true, examine);

            // handle as take command
            if (TryParseTakeCommand(input, game, out var take))
                return new InterpretationResult(true, take);

            // handle as talk command
            if (TryParseTalkCommand(input, game, out var talk))
                return new InterpretationResult(true, talk);

            // handle as use on command
            if (TryParseUseOnCommand(input, game, out var useOn))
                return new InterpretationResult(true, useOn);

            return new InterpretationResult(false, new Unactionable("Invalid input."));
        }

        /// <summary>
        /// Get contextual command help for a game, based on its current state.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns>The contextual help.</returns>
        public CommandHelp[] GetContextualCommandHelp(Game game)
        {
            if (game.ActiveConverser?.Conversation != null)
                return new CommandHelp[0];

            var commands = new List<CommandHelp>();

            if (game.Overworld.CurrentRegion.CurrentRoom.CanMove(Direction.North))
                commands.Add(new CommandHelp($"{North}/{NorthShort}", "Move north"));

            if (game.Overworld.CurrentRegion.CurrentRoom.CanMove(Direction.East))
                commands.Add(new CommandHelp($"{East}/{EastShort}", "Move east"));

            if (game.Overworld.CurrentRegion.CurrentRoom.CanMove(Direction.South))
                commands.Add(new CommandHelp($"{South}/{SouthShort}", "Move south"));

            if (game.Overworld.CurrentRegion.CurrentRoom.CanMove(Direction.West))
                commands.Add(new CommandHelp($"{West}/{WestShort}", "Move west"));

            if (game.Overworld.CurrentRegion.CurrentRoom.CanMove(Direction.Up))
                commands.Add(new CommandHelp($"{Up}/{UpShort}", "Move up"));

            if (game.Overworld.CurrentRegion.CurrentRoom.CanMove(Direction.Down))
                commands.Add(new CommandHelp($"{Down}/{DownShort}", "Move down"));

            commands.Add(new CommandHelp($"{Examine}/{ExamineShort} {Variable}", "Examine anything in the game"));

            if (game.Player.Items.Any())
                commands.Add(new CommandHelp($"{Drop}/{DropShort} {Variable}", "Drop an item"));

            if (game.Overworld.CurrentRegion.CurrentRoom.Items.Any())
            {
                commands.Add(new CommandHelp($"{Take}/{TakeShort} {Variable}", "Take an item"));
                commands.Add(new CommandHelp($"{Take}/{TakeShort} {All}", "Take all items in a room"));
            }

            if (game.Overworld.CurrentRegion.CurrentRoom.Characters.Any())
                commands.Add(new CommandHelp($"{Talk}/{TalkShort} {To.ToLower()} {Variable}", "Talk to a character"));

            if (game.Overworld.CurrentRegion.CurrentRoom.Items.Any() || game.Player.Items.Any())
            {
                commands.Add(new CommandHelp($"{Use} {Variable}", "Use an item on this room"));
                commands.Add(new CommandHelp($"{Use} {Variable} {On.ToLower()} {Variable}", "Use an item on another item or character"));
            }

            return commands.ToArray();
        }

        #endregion
    }
}
