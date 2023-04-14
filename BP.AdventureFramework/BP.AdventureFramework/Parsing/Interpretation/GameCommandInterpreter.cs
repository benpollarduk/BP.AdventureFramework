﻿using System;
using System.Linq;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.GameStructure;
using BP.AdventureFramework.Interaction;
using BP.AdventureFramework.Locations;
using BP.AdventureFramework.Parsing.Commands;
using BP.AdventureFramework.Parsing.Commands.Game;

namespace BP.AdventureFramework.Parsing.Interpretation
{
    /// <summary>
    /// Represents an object that can be used for interpreting game commands.
    /// </summary>
    public class GameCommandInterpreter : IInterpreter
    {
        #region Constants

        private const string North = "North";
        private const string NorthShort = "N";
        private const string South = "South";
        private const string SouthShort = "S";
        private const string East = "East";
        private const string EastShort = "E";
        private const string West = "West";
        private const string WestShort = "W";
        private const string Drop = "Drop";
        private const string Use = "Use";
        private const string On = "On";
        private const string Talk = "Talk";
        private const string To = "To";
        private const string Take = "Take";
        private const string Examine = "Examine";
        private const string Me = "Me";
        private const string Room = "Room";
        private const string Region = "Region";
        private const string Overworld = "Overworld";

        #endregion

        #region StaticMethods

        private static void SplitTextToNounAndObject(string text, out string noun, out string obj)
        {
            // if there is a space
            if (text.IndexOf(" ", StringComparison.Ordinal) > -1)
            {
                // noun all text up to space
                noun = text.Substring(0, text.IndexOf(" ", StringComparison.Ordinal)).Trim();
                // object is all text after space
                obj = text.Substring(text.IndexOf(" ", StringComparison.Ordinal)).Trim();
            }
            else
            {
                noun = text;
                obj = string.Empty;
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
            SplitTextToNounAndObject(text, out var noun, out var obj);

            if (!Drop.Equals(noun, StringComparison.CurrentCultureIgnoreCase))
            {
                command = null;
                return false;
            }

            game.Player.FindItem(obj, out var item);
            command = new Drop(game.Player, item, game.Overworld.CurrentRegion.CurrentRoom);
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
            SplitTextToNounAndObject(text, out var noun, out var obj);

            if (!Take.Equals(noun, StringComparison.CurrentCultureIgnoreCase))
            {
                command = null;
                return false;
            }

            Item item;

            // it no item specified then find the first takeable one
            if (string.IsNullOrEmpty(obj))
            {
                item = game.Overworld.CurrentRegion.CurrentRoom.Items.FirstOrDefault(x => x.IsTakeable);

                if (item == null)
                {
                    command = new Unactionable("There are no takeable items in the room.");
                    return true;
                }
            }
            else
            {
                if (!game.Overworld.CurrentRegion.CurrentRoom.FindItem(obj, out item))
                {
                    command = new Unactionable("There is no such item in the room.");
                    return true;
                }
            }

            command = new Take(game.Player, item, game.Overworld.CurrentRegion.CurrentRoom);
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
            SplitTextToNounAndObject(text, out var noun, out var obj);

            if (!Talk.Equals(noun, StringComparison.CurrentCultureIgnoreCase))
            {
                command = null;
                return false;
            }

            // determine if a target has been specified
            if (obj.Length > 3 && string.Equals(obj.Substring(0, 2), $"{To} ", StringComparison.CurrentCultureIgnoreCase))
            {
                obj = obj.Remove(0, 3);

                if (game.Overworld.CurrentRegion.CurrentRoom.FindCharacter(obj, out var nPC))
                {
                    command = new Talk(nPC);
                    return true;
                }
            }

            if (game.Overworld.CurrentRegion.CurrentRoom.Characters.Count == 1)
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
            SplitTextToNounAndObject(text, out var noun, out var obj);

            if (!Examine.Equals(noun, StringComparison.CurrentCultureIgnoreCase))
            {
                command = null;
                return false;
            }

            if (string.IsNullOrEmpty(obj))
            {
                // default to current room
                command = new Examine(game.Overworld.CurrentRegion.CurrentRoom);
                return true;
            }

            // check player items
            if (game.Player.FindItem(obj, out var item))
            {
                command = new Examine(item);
                return true;
            }

            // check items in room
            if (game.Overworld.CurrentRegion.CurrentRoom.FindItem(obj, out item))
            {
                command = new Examine(item);
                return true;
            }

            // check characters in room
            if (game.Overworld.CurrentRegion.CurrentRoom.FindCharacter(obj, out var character))
            {
                command = new Examine(character);
                return true;
            }

            // check exits to room
            if (TryParseToCardinalDirection(obj, out var direction))
            {
                if (game.Overworld.CurrentRegion.CurrentRoom.FindExit(direction, out var exit))
                {
                    command = new Examine(exit);
                    return true;
                }

                command = new Unactionable($"There is no exit in this room to the {direction}");
                return true;
            }

            // check self examination
            if (Me.Equals(obj, StringComparison.CurrentCultureIgnoreCase) || obj.EqualsExaminable(game.Player))
            {
                command = new Examine(game.Player);
                return true;
            }

            // check room examination
            if (Room.Equals(obj, StringComparison.CurrentCultureIgnoreCase) || obj.EqualsExaminable(game.Overworld.CurrentRegion.CurrentRoom))
            {
                command = new Examine(game.Overworld.CurrentRegion.CurrentRoom);
                return true;
            }

            // check region examination
            if (Region.Equals(obj, StringComparison.CurrentCultureIgnoreCase) || obj.EqualsExaminable(game.Overworld.CurrentRegion))
            {
                command = new Examine(game.Overworld.CurrentRegion);
                return true;
            }

            // check overworld examination
            if (Overworld.Equals(obj, StringComparison.CurrentCultureIgnoreCase) || obj.EqualsExaminable(game.Overworld))
            {
                command = new Examine(game.Overworld);
                return true;
            }

            // unknown
            if (!string.IsNullOrEmpty(obj))
            {
                command = new Unactionable($"Can't examine {obj}.");
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
            SplitTextToNounAndObject(text, out var noun, out var obj);

            if (!Use.Equals(noun, StringComparison.CurrentCultureIgnoreCase))
            {
                command = null;
                return false;
            }

            IInteractWithItem target;
            obj = obj.ToUpper();
            var on = On.ToUpper();
            var itemName = obj;

            if (obj.Contains($" {On.ToUpper()} "))
            {
                itemName = obj.Substring(0, obj.IndexOf($" {on} ", StringComparison.CurrentCultureIgnoreCase));
                obj = obj.Replace(itemName, string.Empty);
                var targetName = obj.Replace($" {on} ", string.Empty);

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

            command = new UseOn(item, target, game.Player, game.Overworld.CurrentRegion.CurrentRoom);
            return true;
        }

        /// <summary>
        /// Try and parse a string to a CardinalDirection.
        /// </summary>
        /// <param name="text">The string to parse.</param>
        /// <param name="direction">The direction.</param>
        /// <returns>The result of the parse.</returns>
        private static bool TryParseToCardinalDirection(string text, out CardinalDirection direction)
        {
            switch (text.ToUpper())
            {
                case East:
                case EastShort:
                    direction = CardinalDirection.East;
                    return true;
                case North:
                case NorthShort:
                    direction = CardinalDirection.North;
                    return true;
                case South:
                case SouthShort:
                    direction = CardinalDirection.South;
                    return true;
                case West:
                case WestShort:
                    direction = CardinalDirection.West;
                    return true;
                default:
                    direction = CardinalDirection.East;
                    return false;
            }
        }


        #endregion

        #region Implementation of IInterpreter

        /// <summary>
        /// Interpret a string.
        /// </summary>
        /// <param name="input">The string to interpret.</param>
        /// <param name="game">The game.</param>
        /// <returns>The result of the interpretation.</returns>
        public InterpretationResult Interpret(string input, Game game)
        {
            // try and parse as movement
            if (TryParseToCardinalDirection(input, out var direction))
                return new InterpretationResult(true, new Move(game.Overworld.CurrentRegion, direction));

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

        #endregion
    }
}