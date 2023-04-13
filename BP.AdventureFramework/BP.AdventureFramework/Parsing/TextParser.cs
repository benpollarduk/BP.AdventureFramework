using System;
using System.Linq;
using BP.AdventureFramework.Characters;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.GameStructure;
using BP.AdventureFramework.Interaction;
using BP.AdventureFramework.Locations;
using BP.AdventureFramework.Rendering;

namespace BP.AdventureFramework.Parsing
{
    /// <summary>
    /// A parser used for parsing text into in-game interactions.
    /// </summary>
    public class TextParser
    {
        #region Methods

        /// <summary>
        /// Try and parse a string to a CardinalDirection.
        /// </summary>
        /// <param name="obj">The string to parse.</param>
        /// <param name="direction">The direction.</param>
        /// <returns>The result of the parse.</returns>
        public bool TryParseToCardinalDirection(string obj, out CardinalDirection direction)
        {
            switch (obj.ToUpper())
            {
                case "E":
                    direction = CardinalDirection.East;
                    return true;
                case "N":
                    direction = CardinalDirection.North;
                    return true;
                case "S":
                    direction = CardinalDirection.South;
                    return true;
                case "W":
                    direction = CardinalDirection.West;
                    return true;
                default:
                    var result = CheckEnumerationForCaseInsensitiveMember(typeof(CardinalDirection), obj, out var valueInEnum);

                    if (result)
                        direction = (CardinalDirection)valueInEnum;
                    else
                        direction = 0;

                    return result;
            }
        }

        /// <summary>
        /// Get if text is a CardinalDirection.
        /// </summary>
        /// <param name="input">The input to check.</param>
        /// <returns>True is the input is a cardinal direction.</returns>
        public bool IsTextCardinalDirection(string input)
        {
            switch (input.ToUpper())
            {
                case "E":
                case "N":
                case "S":
                case "W":
                    return true;
                default:
                    return Enum.GetNames(typeof(CardinalDirection)).Any(name => string.Equals(name, input, StringComparison.CurrentCultureIgnoreCase));
            }
        }

        /// <summary>
        /// Try and parse a string to a Command.
        /// </summary>
        /// <param name="obj">The string to parse.</param>
        /// <param name="command">The command.</param>
        /// <returns>The result of the parse.</returns>
        public bool TryParseToCommand(string obj, out Command command)
        {
            var result = CheckEnumerationForCaseInsensitiveMember(typeof(Command), obj, out var valueInEnum);

            if (result)
                command = (Command)valueInEnum;
            else
                command = 0;

            return result;
        }

        /// <summary>
        /// Get if a string is a Command.
        /// </summary>
        /// <param name="input">The input to check.</param>
        /// <returns>True is the input is a command.</returns>
        public bool IsCommand(string input)
        {
            return Enum.GetNames(typeof(Command)).Any(name => string.Equals(name, input, StringComparison.CurrentCultureIgnoreCase));
        }

        /// <summary>
        /// Check an enumeration for a case insensitive value.
        /// </summary>
        /// <param name="typeOfEnum">The type of the enumeration to check.</param>
        /// <param name="name">The name of the enumeration member.</param>
        /// <param name="obj">The enumeration member, if found.</param>
        /// <returns>The result of the check.</returns>
        protected bool CheckEnumerationForCaseInsensitiveMember(Type typeOfEnum, string name, out object obj)
        {
            var names = Enum.GetNames(typeOfEnum);

            foreach (var t in names)
            {
                if (!string.Equals(t, name, StringComparison.CurrentCultureIgnoreCase)) 
                    continue;

                obj = Enum.Parse(typeOfEnum, t);
                return true;
            }

            obj = null;
            return false;
        }

        /// <summary>
        /// Try and parse a string to a GameCommand.
        /// </summary>
        /// <param name="obj">The string to parse.</param>
        /// <param name="command">The command.</param>
        /// <returns>The result of the parse.</returns>
        public bool TryParseToGameCommand(string obj, out GameCommand command)
        {
            var result = CheckEnumerationForCaseInsensitiveMember(typeof(GameCommand), obj, out var valueInEnum);

            if (result)
                command = (GameCommand)valueInEnum;
            else
                command = 0;

            return result;
        }

        /// <summary>
        /// Try and parse a string to a FrameDrawingOption.
        /// </summary>
        /// <param name="obj">The string to parse.</param>
        /// <param name="command">The command.</param>
        /// <returns>The result of the parse.</returns>
        public bool TryParseToFrameDrawingOption(string obj, out FrameDrawingOption command)
        {
            var result = CheckEnumerationForCaseInsensitiveMember(typeof(FrameDrawingOption), obj, out var valueInEnum);

            if (result)
                command = (FrameDrawingOption)valueInEnum;
            else
                command = 0;

            return result;
        }

        /// <summary>
        /// Get if a string is a FrameDrawingOption.
        /// </summary>
        /// <param name="input">The input to check.</param>
        /// <returns>True is the input is a command.</returns>
        public bool IsFrameDrawingOption(string input)
        {
            return Enum.GetNames(typeof(FrameDrawingOption)).Any(name => string.Equals(name, input, StringComparison.CurrentCultureIgnoreCase));
        }

        /// <summary>
        /// React to an input string. This will take all necessary action to the input on the Game parameter.
        /// </summary>
        /// <param name="input">The input to action.</param>
        /// <param name="game">The game to action the input on.</param>
        /// <param name="result">Any result of the reaction.</param>
        /// <returns>The reaction to the input.</returns>
        public ReactionToInput ReactToInput(string input, Game game, out string result)
        {
            input = input.ToUpper();
            string noun;
            string obj = string.Empty;

            if (input.IndexOf(" ", StringComparison.Ordinal) > -1)
            {
                noun = input.Substring(0, input.IndexOf(" ", StringComparison.Ordinal)).Trim();
                obj = input.Substring(input.IndexOf(" ", StringComparison.Ordinal)).Trim();
            }
            else
            {
                noun = input;
            }

            if (IsTextCardinalDirection(noun))
            {
                TryParseToCardinalDirection(noun, out var direction);

                if (game.Overworld.CurrentRegion.Move(direction))
                {
                    result = "Moved " + direction;
                    return ReactionToInput.CouldReact;
                }

                result = "Could not move " + direction;
                return ReactionToInput.CouldntReact;
            }

            if (IsCommand(noun))
            {
                TryParseToCommand(noun, out var command);

                switch (command)
                {
                    case Command.Drop:
                        
                        if (!string.IsNullOrEmpty(obj))
                        {
                            if (game.Player.FindItem(obj, out var droppedItem))
                            {
                                game.Overworld.CurrentRegion.CurrentRoom.AddItem(droppedItem);
                                game.Player.DequireItem(droppedItem);
                                result = "Dropped " + droppedItem.Identifier;
                                return ReactionToInput.CouldReact;
                            }

                            result = "You don't have that item";
                            return ReactionToInput.CouldReact;
                        }

                        result = "You must specify what to drop";
                        return ReactionToInput.CouldntReact;

                    case Command.Examine:

                        ExaminationResult examinationResult;

                        if (!string.IsNullOrEmpty(obj))
                        {
                            if (game.Player.FindItem(obj, out var examinedItem))
                            {
                                examinationResult = examinedItem.Examime();
                            }
                            else if (game.Overworld.CurrentRegion.CurrentRoom.FindItem(obj, out examinedItem))
                            {
                                examinationResult = examinedItem.Examime();
                            }
                            else if (game.Overworld.CurrentRegion.CurrentRoom.ContainsCharacter(obj))
                            {
                                game.Overworld.CurrentRegion.CurrentRoom.FindCharacter(obj, out var c);
                                examinationResult = c.Examime();
                            }
                            else if (IsTextCardinalDirection(obj))
                            {
                                TryParseToCardinalDirection(obj, out var direction);

                                if (game.Overworld.CurrentRegion.CurrentRoom.ContainsExit(direction))
                                {
                                    game.Overworld.CurrentRegion.CurrentRoom.FindExit(direction, out var exit);
                                    examinationResult = exit.Examime();
                                }
                                else
                                {
                                    result = $"There is no exit in this room to the {direction}";
                                    return ReactionToInput.CouldntReact;
                                }
                            }
                            else if (obj == "ME" || obj.EqualsExaminable(game.Player))
                            {
                                examinationResult = game.Player.Examime();
                            }
                            else if (obj == "ROOM" || obj.EqualsExaminable(game.Overworld.CurrentRegion.CurrentRoom))
                            {
                                examinationResult = game.Overworld.CurrentRegion.CurrentRoom.Examime();
                            }
                            else if (obj == "REGION" || obj.EqualsExaminable(game.Overworld.CurrentRegion))
                            {
                                examinationResult = new ExaminationResult(game.Overworld.CurrentRegion.Description.GetDescription(), ExaminationResults.DescriptionReturned);
                            }
                            else if (obj == "OVERWORLD" || obj.EqualsExaminable(game.Overworld))
                            {
                                examinationResult = new ExaminationResult(game.Overworld.Description.GetDescription(), ExaminationResults.DescriptionReturned);
                            }
                            else if (!string.IsNullOrEmpty(obj))
                            {
                                result = "Can't examine " + obj;
                                return ReactionToInput.CouldntReact;
                            }
                            else
                            {
                                examinationResult = game.Overworld.CurrentRegion.CurrentRoom.Examime();
                            }
                        }
                        else
                        {
                            examinationResult = game.Overworld.CurrentRegion.CurrentRoom.Examime();
                        }

                        result = examinationResult.Desciption;

                        switch (examinationResult.Type)
                        {
                            case ExaminationResults.DescriptionReturned:
                                return ReactionToInput.CouldReact;
                            case ExaminationResults.SelfContained:
                                return ReactionToInput.SelfContainedReaction;
                            default:
                                throw new NotImplementedException();
                        }

                    case Command.Take:
                        
                        if (game.Overworld.CurrentRegion.CurrentRoom.Items.Count(i => i.IsTakeable) == 1 && string.IsNullOrEmpty(obj))
                            obj = game.Overworld.CurrentRegion.CurrentRoom.Items.Where(i => i.IsTakeable).ToArray()[0].Identifier.Name;

                        var reaction = game.Overworld.CurrentRegion.CurrentRoom.RemoveItemFromRoom(obj, out var removedItem);

                        if (reaction.Result == ReactionToInput.CouldReact)
                            game.Player.AquireItem(removedItem);

                        result = reaction.Reason;
                        return reaction.Result;

                    case Command.Talk:
                        
                        var aliveCharactersInRoom = game.Overworld.CurrentRegion.CurrentRoom.Characters.Where<Character>(c => c.IsAlive && c.IsPlayerVisible).ToArray();

                        if (aliveCharactersInRoom.Length == 1 && string.IsNullOrEmpty(obj))
                            obj = aliveCharactersInRoom[0].Identifier.Name;

                        if (obj.Length > 3 && obj.Substring(0, 3) == "TO ")
                            obj = obj.Remove(0, 3);

                        if (game.Overworld.CurrentRegion.CurrentRoom.FindCharacter(obj, out var nPC) && nPC.IsAlive)
                        {
                            result = nPC.Talk();
                            return ReactionToInput.CouldReact;
                        }

                        var isSomething = game.Overworld.CurrentRegion.CurrentRoom.ContainsItem(obj) ||
                                          game.Overworld.CurrentRegion.CurrentRoom.ContainsCharacter(obj) ||
                                          game.Player.Items.Any(i => obj.EqualsExaminable(i));

                        if (isSomething)
                            result = obj.Substring(0, 1) + obj.Substring(1).ToLower() + " cannot be talked to";
                        else
                            result = string.IsNullOrEmpty(obj) ? "No-one is around to talk to" : "Can't talk to that as it doesn't exist";

                        return ReactionToInput.CouldntReact;

                    case Command.Use:

                        IInteractWithItem target = null;
                        string itemName;
                        string targetName = null;

                        if (obj.Contains(" ON "))
                        {
                            itemName = obj.Substring(0, obj.IndexOf(" ON ", StringComparison.Ordinal));
                            obj = obj.Replace(itemName, string.Empty);
                            targetName = obj.Replace(" ON ", string.Empty);

                            if (string.IsNullOrEmpty(itemName))
                            {
                                result = "That is not an item";
                                return ReactionToInput.CouldntReact;
                            }

                            if (string.IsNullOrEmpty(targetName))
                            {
                                result = "That is not a target";
                                return ReactionToInput.CouldntReact;
                            }
                        }
                        else
                        {
                            itemName = obj;
                        }

                        if (string.IsNullOrEmpty(itemName))
                        {
                            result = "You must specify an item";
                            return ReactionToInput.CouldntReact;
                        }

                        if (!game.Player.FindItem(itemName, out var item))
                        {
                            if (!game.Overworld.CurrentRegion.CurrentRoom.FindItem(itemName, out item))
                            {
                                result = "You don't have that item";
                                return ReactionToInput.CouldntReact;
                            }
                        }

                        InteractionResult interaction;

                        if (!string.IsNullOrEmpty(targetName))
                        {
                            switch (targetName)
                            {
                                case "ME":

                                    target = game.Player;
                                    break;

                                case "ROOM":
                                    
                                    target = game.Overworld.CurrentRegion.CurrentRoom;
                                    break;

                                default:
                                    
                                    target = game.FindInteractionTarget(targetName);

                                    if (target == null)
                                    {
                                        result = "That is not a target";
                                        return ReactionToInput.CouldntReact;
                                    }

                                    break;
                            }

                            interaction = target.Interact(item);
                        }
                        else
                        {
                            interaction = game.Overworld.CurrentRegion.CurrentRoom.Interact(item);
                        }

                        switch (interaction.Effect)
                        {
                            case InteractionEffect.FatalEffect:

                                game.Player.Kill(interaction.Desciption);
                                result = interaction.Desciption;
                                return ReactionToInput.SelfContainedReaction;

                            case InteractionEffect.ItemUsedUp:
                                
                                if (game.Overworld.CurrentRegion.CurrentRoom.ContainsItem(item))
                                    game.Overworld.CurrentRegion.CurrentRoom.RemoveItemFromRoom(interaction.Item);
                                else if (game.Player.FindItem(item.Identifier.Name, out item))
                                    game.Player.DequireItem(item);

                                break;

                            case InteractionEffect.TargetUsedUp:
                                
                                var examinable = target as IExaminable;

                                if (examinable != null)
                                {
                                    if (game.Overworld.CurrentRegion.CurrentRoom.ContainsInteractionTarget(examinable.Identifier.Name))
                                        game.Overworld.CurrentRegion.CurrentRoom.RemoveInteractionTargetFromRoom(target);
                                }

                                break;

                            case InteractionEffect.NoEffect:
                            case InteractionEffect.ItemMorphed:
                            case InteractionEffect.SelfContained:
                                break;
                            default:
                                throw new NotImplementedException();
                        }

                        result = interaction.Desciption;
                        return ReactionToInput.CouldReact;

                    default:
                        throw new NotImplementedException();
                }
            }

            if (game.IsValidActionableCommand(noun))
            {
                var command = game.FindActionableCommand(noun);

                if (command == null) 
                    throw new ArgumentException($"String {noun} is not a custom command");

                var customResult = command.Action();
                result = customResult.Desciption;
                return ReactionToInput.CouldReact;
            }

            if (string.IsNullOrEmpty(noun))
            {
                result = string.Empty;
                return ReactionToInput.CouldReact;
            }

            result = "Invalid input";
            return ReactionToInput.CouldntReact;
        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Get an objectifier for a word.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns>The objectifier.</returns>
        public static string GetObjectifier(string word)
        {
            if (string.IsNullOrEmpty(word))
                throw new ArgumentException("Parameter 'word' must have a value");

            if (IsPlural(word))
                return "some";
            if (IsVowel(word[0]) && word[0].ToString().ToUpper() != "U")
                return "an";

            return "a";
        }

        /// <summary>
        /// Get if a character is a vowel.
        /// </summary>
        /// <param name="c">The character to check.</param>
        /// <returns>True if the character is a vowel.</returns>
        public static bool IsVowel(char c)
        {
            var vowel = c.ToString().ToUpper();

            return vowel == "A" ||
                   vowel == "E" ||
                   vowel == "I" ||
                   vowel == "O" ||
                   vowel == "U";
        }

        /// <summary>
        /// Get if a word is plural.
        /// </summary>
        /// <param name="word">The word to check.</param>
        /// <returns>True if the word is plural.</returns>
        public static bool IsPlural(string word)
        {
            if (string.IsNullOrEmpty(word))
                throw new ArgumentException("Parameter 'word' must have a value");

            word = word.Trim(Convert.ToChar(" "));

            if (word.Contains(" "))
                word = word.Substring(0, word.IndexOf(" ", StringComparison.Ordinal));

            return word.Substring(word.Length - 1).ToUpper() == "S";
        }

        #endregion
    }
}