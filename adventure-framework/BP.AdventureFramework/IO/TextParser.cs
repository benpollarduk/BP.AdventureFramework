using System;
using System.Linq;
using AdventureFramework.Locations;
using AdventureFramework.Rendering;
using AdventureFramework.Structure;
using BP.AdventureFramework.Interaction;

namespace AdventureFramework.IO
{
    /// <summary>
    /// An parser used for parsing text into in-game interactions
    /// </summary>
    public class TextParser
    {
        #region Methods

        /// <summary>
        /// Try and parse a string to an ECardinalDirection
        /// </summary>
        /// <param name="obj">The string to parse</param>
        /// <param name="direction">The direction</param>
        /// <returns>The result of the parse</returns>
        public virtual bool TryParseToECardinalDirection(string obj, out ECardinalDirection direction)
        {
            // select direction
            switch (obj.ToUpper())
            {
                case "E":
                    {
                        // set direction
                        direction = ECardinalDirection.East;

                        // pass
                        return true;
                    }
                case "N":
                    {
                        // set driection
                        direction = ECardinalDirection.North;

                        // pass
                        return true;
                    }
                case "S":
                    {
                        // set direction
                        direction = ECardinalDirection.South;

                        // pass
                        return true;
                    }
                case "W":
                    {
                        // set direction
                        direction = ECardinalDirection.West;

                        // pass
                        return true;
                    }
                default:
                    {
                        // hold value in enum
                        object valueInEnum;

                        // check
                        var result = checkEnumerationForCaseInsensitiveMember(typeof(ECardinalDirection), obj, out valueInEnum);

                        // if parsed
                        if (result)
                            // set command
                            direction = (ECardinalDirection)valueInEnum;
                        else
                            // default
                            direction = 0;

                        // retunr result
                        return result;
                    }
            }
        }

        /// <summary>
        /// Get if text is a ECardinalDirection
        /// </summary>
        /// <param name="input">The input to check</param>
        /// <returns>True is the input is a cardinal direction</returns>
        public virtual bool IsTextCardinalDirection(string input)
        {
            // select input
            switch (input.ToUpper())
            {
                case "E":
                case "N":
                case "S":
                case "W":
                    {
                        // pass
                        return true;
                    }
                default:
                    {
                        return Enum.GetNames(typeof(ECardinalDirection)).Where(name => name.ToUpper() == input.ToUpper()).Count() > 0;
                    }
            }
        }

        /// <summary>
        /// Try and parse a string to a ECommand
        /// </summary>
        /// <param name="obj">The string to parse</param>
        /// <param name="command">The command</param>
        /// <returns>The result of the parse</returns>
        public virtual bool TryParseToECommand(string obj, out ECommand command)
        {
            // hold value in enum
            object valueInEnum;

            // check
            var result = checkEnumerationForCaseInsensitiveMember(typeof(ECommand), obj, out valueInEnum);

            // if parsed
            if (result)
                // set command
                command = (ECommand)valueInEnum;
            else
                // default
                command = 0;

            // retunr result
            return result;
        }

        /// <summary>
        /// Get if text is a ECommand
        /// </summary>
        /// <param name="input">The input to check</param>
        /// <returns>True is the input is a comman</returns>
        public virtual bool IsCommand(string input)
        {
            // see if in enum
            return Enum.GetNames(typeof(ECommand)).Where(name => name.ToUpper() == input.ToUpper()).Count() > 0;
        }

        /// <summary>
        /// Check an enumeration for a case insensitive value
        /// </summary>
        /// <param name="typeOfEnum">The type of the enum to check</param>
        /// <param name="name">The name of the enumeration member</param>
        /// <param name="obj">The enumeration member, if found</param>
        /// <returns>The result of the check</returns>
        private bool checkEnumerationForCaseInsensitiveMember(Type typeOfEnum, string name, out object obj)
        {
            // get names
            var names = Enum.GetNames(typeOfEnum);

            // itterate all names
            for (var index = 0; index < names.Length; index++)
                // if found
                if (names[index].ToUpper() == name.ToUpper())
                {
                    // parse
                    obj = Enum.Parse(typeOfEnum, names[index]);

                    // worked
                    return true;
                }

            // not found
            obj = null;

            // fail
            return false;
        }

        /// <summary>
        /// Try and parse a string to a EGameCommand
        /// </summary>
        /// <param name="obj">The string to parse</param>
        /// <param name="command">The command</param>
        /// <returns>The result of the parse</returns>
        public virtual bool TryParseToEGameCommand(string obj, out EGameCommand command)
        {
            // hold value in enum
            object valueInEnum;

            // check
            var result = checkEnumerationForCaseInsensitiveMember(typeof(EGameCommand), obj, out valueInEnum);

            // if parsed
            if (result)
                // set command
                command = (EGameCommand)valueInEnum;
            else
                // default
                command = 0;

            // return result
            return result;
        }

        /// <summary>
        /// Get if text is a EGameCommand
        /// </summary>
        /// <param name="input">The input to check</param>
        /// <returns>True is the input is a comman</returns>
        public virtual bool IsEGameCommand(string input)
        {
            return Enum.GetNames(typeof(EGameCommand)).Where(name => name.ToUpper() == input.ToUpper()).Count() > 0;
        }

        /// <summary>
        /// Try and parse a string to a EFrameDrawerOption
        /// </summary>
        /// <param name="obj">The string to parse</param>
        /// <param name="command">The command</param>
        /// <returns>The result of the parse</returns>
        public virtual bool TryParseToEFrameDrawerOption(string obj, out EFrameDrawerOption command)
        {
            // hold value in enum
            object valueInEnum;

            // check
            var result = checkEnumerationForCaseInsensitiveMember(typeof(EFrameDrawerOption), obj, out valueInEnum);

            // if parsed
            if (result)
                // set command
                command = (EFrameDrawerOption)valueInEnum;
            else
                // default
                command = 0;

            // return result
            return result;
        }

        /// <summary>
        /// Get if text is a EFrameDrawerOption
        /// </summary>
        /// <param name="input">The input to check</param>
        /// <returns>True is the input is a comman</returns>
        public virtual bool IsEFrameDrawerOption(string input)
        {
            return Enum.GetNames(typeof(EFrameDrawerOption)).Where(name => name.ToUpper() == input.ToUpper()).Count() > 0;
        }

        /// <summary>
        /// React to an input string. This will take all necessary action to the input on the Game parameter
        /// </summary>
        /// <param name="input">The input to action</param>
        /// <param name="game">The game to action the input on</param>
        /// <param name="result">Any result of the reaction</param>
        /// <returns>The reaction to the input</returns>
        public virtual EReactionToInput ReactToInput(string input, Game game, out string result)
        {
            // always use as upper case
            input = input.ToUpper();

            // hold noun
            var noun = string.Empty;

            // hold the object
            var obj = string.Empty;

            // is an object
            if (input.IndexOf(" ") > -1)
            {
                // get noun
                noun = input.Substring(0, input.IndexOf(" ")).Trim();

                // get object
                obj = input.Substring(input.IndexOf(" ")).Trim();
            }
            else
            {
                // just a noun
                noun = input;
            }

            // firstly check for movement
            if (IsTextCardinalDirection(noun))
            {
                // hold direction
                ECardinalDirection direction;

                // parse
                TryParseToECardinalDirection(noun, out direction);

                // move
                if (game.Overworld.CurrentRegion.Move(direction))
                {
                    // no error
                    result = "Moved " + direction;

                    // pass
                    return EReactionToInput.CouldReact;
                }

                // set error
                result = "Could not move " + direction;

                // move failed
                return EReactionToInput.CouldntReact;
            }

            if (IsCommand(noun))
            {
                // hold command
                ECommand command;

                // parse
                TryParseToECommand(noun, out command);

                // select command
                switch (command)
                {
                    case ECommand.Drop:
                        {
                            // if an item
                            if (!string.IsNullOrEmpty(obj))
                            {
                                // examine the item
                                Item item;

                                // if player holds item
                                if (game.Player.FindItem(obj, out item))
                                {
                                    // add item
                                    game.Overworld.CurrentRegion.CurrentRoom.AddItem(item);

                                    // dequire ite,,
                                    game.Player.DequireItem(item);

                                    // examine the item
                                    result = "Dropped " + item.Name;

                                    // pass
                                    return EReactionToInput.CouldReact;
                                }

                                // nothing to drop
                                result = "You don't have that item";

                                // pass
                                return EReactionToInput.CouldReact;
                            }

                            // examine the room
                            result = "You must specify what to drop";

                            // pass
                            return EReactionToInput.CouldntReact;
                        }
                    case ECommand.Examine:
                        {
                            // hold examination result
                            ExaminationResult examinationResult = null;

                            // if an item
                            if (!string.IsNullOrEmpty(obj))
                            {
                                // examine the item
                                Item item;

                                // if player holds item
                                if (game.Player.FindItem(obj, out item))
                                {
                                    // examine the item
                                    examinationResult = item.Examime();
                                }
                                else if (game.Overworld.CurrentRegion.CurrentRoom.FindItem(obj, out item))
                                {
                                    // examine the item
                                    examinationResult = item.Examime();
                                }
                                else if (game.Overworld.CurrentRegion.CurrentRoom.ContainsCharacter(obj))
                                {
                                    // hold character
                                    NonPlayableCharacter c;

                                    // get character
                                    game.Overworld.CurrentRegion.CurrentRoom.FindCharacter(obj, out c);

                                    // examine the npc
                                    examinationResult = c.Examime();
                                }
                                else if (IsTextCardinalDirection(obj))
                                {
                                    // hold direction
                                    ECardinalDirection direction;

                                    // parse
                                    TryParseToECardinalDirection(obj, out direction);

                                    // if contians direction
                                    if (game.Overworld.CurrentRegion.CurrentRoom.ContainsExit(direction))
                                    {
                                        // hold exit
                                        Exit exit;

                                        // get exit
                                        game.Overworld.CurrentRegion.CurrentRoom.FindExit(direction, out exit);

                                        // examine the exit
                                        examinationResult = exit.Examime();
                                    }
                                    else
                                    {
                                        // set result
                                        result = string.Format("There is no exit in this room to the {0}", direction);

                                        // fail
                                        return EReactionToInput.CouldntReact;
                                    }
                                }
                                else if (obj == "ME" ||
                                         obj == game.Player.Name.ToUpper())
                                {
                                    // examine the player
                                    examinationResult = game.Player.Examime();
                                }
                                else if (obj == "ROOM" ||
                                         obj == game.Overworld.CurrentRegion.CurrentRoom.Name.ToUpper())
                                {
                                    // examine the region
                                    examinationResult = game.Overworld.CurrentRegion.CurrentRoom.Examime();
                                }
                                else if (obj == "REGION" ||
                                         obj == game.Overworld.CurrentRegion.Name.ToUpper())
                                {
                                    // examine the region
                                    examinationResult = new ExaminationResult(game.Overworld.CurrentRegion.Description.GetDescription(), EExaminationResults.DescriptionReturned);
                                }
                                else if (obj == "OVERWORLD" ||
                                         obj == game.Overworld.Name.ToUpper())
                                {
                                    // examine the region
                                    examinationResult = new ExaminationResult(game.Overworld.Description.GetDescription(), EExaminationResults.DescriptionReturned);
                                }
                                else if (!string.IsNullOrEmpty(obj))
                                {
                                    // can't examine
                                    result = "Can't examine " + obj;

                                    // fail
                                    return EReactionToInput.CouldntReact;
                                }
                                else
                                {
                                    // examine the room
                                    examinationResult = game.Overworld.CurrentRegion.CurrentRoom.Examime();
                                }
                            }
                            else
                            {
                                // examine the room
                                examinationResult = game.Overworld.CurrentRegion.CurrentRoom.Examime();
                            }

                            // set result
                            result = examinationResult.Desciption;

                            // select type
                            switch (examinationResult.Type)
                            {
                                case EExaminationResults.DescriptionReturned:
                                    {
                                        // set reaction
                                        return EReactionToInput.CouldReact;
                                    }
                                case EExaminationResults.SelfContained:
                                    {
                                        // set reaction
                                        return EReactionToInput.SelfContainedReaction;
                                    }
                                default:
                                    {
                                        throw new NotImplementedException();
                                    }
                            }
                        }
                    case ECommand.Take:
                        {
                            // hold removed item
                            Item removedItem;

                            // if only one takeable item in the room, and name not entered
                            if (game.Overworld.CurrentRegion.CurrentRoom.Items.Where(i => i.IsTakeable).Count() == 1 &&
                                string.IsNullOrEmpty(obj))
                                // game item name
                                obj = game.Overworld.CurrentRegion.CurrentRoom.Items.Where(i => i.IsTakeable).ToArray()[0].Name;

                            // try remove item
                            var reaction = game.Overworld.CurrentRegion.CurrentRoom.RemoveItemFromRoom(obj, out removedItem);

                            // if worked
                            if (reaction.Result == EReactionToInput.CouldReact)
                                // the removed item
                                game.Player.AquireItem(removedItem);

                            // set message
                            result = reaction.Reason;

                            // set result
                            return reaction.Result;
                        }
                    case ECommand.Talk:
                        {
                            // get all alive characters
                            var aliveCharactersInRoom = game.Overworld.CurrentRegion.CurrentRoom.Characters.Where<Character>(c => c.IsAlive && c.IsPlayerVisible).ToArray();

                            // if only one item in the room, and name not entered
                            if (aliveCharactersInRoom.Length == 1 &&
                                string.IsNullOrEmpty(obj))
                                // game item name
                                obj = aliveCharactersInRoom[0].Name;

                            // hold no playable character
                            NonPlayableCharacter nPC;

                            // see if a 'to '
                            if (obj.Length > 3 &&
                                obj.Substring(0, 3) == "TO ")
                                // remove 'to '
                                obj = obj.Remove(0, 3);

                            // if character found
                            if (game.Overworld.CurrentRegion.CurrentRoom.FindCharacter(obj, out nPC) &&
                                nPC.IsAlive)
                            {
                                // set message
                                result = nPC.Talk();

                                // set result
                                return EReactionToInput.CouldReact;
                            }

                            // get if something
                            var isSomething = game.Overworld.CurrentRegion.CurrentRoom.ContainsItem(obj) ||
                                              game.Overworld.CurrentRegion.CurrentRoom.ContainsCharacter(obj) ||
                                              game.Player.Items.Where(i => i.Name.ToUpper() == obj).Count() > 0;

                            // if something
                            if (isSomething)
                                // set result
                                result = obj.Substring(0, 1) + obj.Substring(1).ToLower() + " cannot be talked to";
                            else
                                // no-one to talk to (need to format obj to sentance case)
                                result = string.IsNullOrEmpty(obj) ? "No-one is around to talk to" : "Can't talk to that as it doesn't exist";

                            // set result
                            return EReactionToInput.CouldntReact;
                        }
                    case ECommand.Use:
                        {
                            // hold item
                            Item item;

                            // hold target
                            IInteractWithItem target = null;

                            // hold item name
                            string itemName = null;

                            // hold target name
                            string targetName = null;

                            // if ON verb found
                            if (obj.Contains(" ON "))
                            {
                                // everything before verb is source
                                itemName = obj.Substring(0, obj.IndexOf(" ON "));

                                // remove item name
                                obj = obj.Replace(itemName, string.Empty);

                                // remove verb
                                targetName = obj.Replace(" ON ", string.Empty);

                                // if no item name
                                if (string.IsNullOrEmpty(itemName))
                                {
                                    // no target
                                    result = "That is not an item";

                                    // couldnt react
                                    return EReactionToInput.CouldntReact;
                                }

                                // if no target name
                                if (string.IsNullOrEmpty(targetName))
                                {
                                    // no target
                                    result = "That is not a target";

                                    // couldnt react
                                    return EReactionToInput.CouldntReact;
                                }
                            }
                            else
                            {
                                // set item name to entire object
                                itemName = obj;
                            }

                            // try find item on player
                            if (string.IsNullOrEmpty(itemName))
                            {
                                // set message
                                result = "You must specify an item";

                                // fail
                                return EReactionToInput.CouldntReact;
                            }

                            if (!game.Player.FindItem(itemName, out item))
                                // try find item in room
                                if (!game.Overworld.CurrentRegion.CurrentRoom.FindItem(itemName, out item))
                                {
                                    // set message
                                    result = "You don't have that item";

                                    // fail
                                    return EReactionToInput.CouldntReact;
                                }

                            // hold result of interaction
                            InteractionResult interaction;

                            // if a target name
                            if (!string.IsNullOrEmpty(targetName))
                            {
                                // select name
                                switch (targetName)
                                {
                                    case "ME":
                                        {
                                            // player
                                            target = game.Player;

                                            break;
                                        }
                                    case "ROOM":
                                        {
                                            // player
                                            target = game.Overworld.CurrentRegion.CurrentRoom;

                                            break;
                                        }
                                    default:
                                        {
                                            // find target in game
                                            target = game.FindInteractionTarget(targetName);

                                            // if target not found
                                            if (target == null)
                                            {
                                                // set message
                                                result = "That is not a target";

                                                // fail
                                                return EReactionToInput.CouldntReact;
                                            }

                                            break;
                                        }
                                }

                                // get target
                                var examinableTarget = target as IExaminable;

                                // get result of interaction
                                interaction = target.Interact(item);
                            }
                            else
                            {
                                // interact with room
                                interaction = game.Overworld.CurrentRegion.CurrentRoom.Interact(item);
                            }

                            // handle effect
                            switch (interaction.Effect)
                            {
                                case EInteractionEffect.FatalEffect:
                                    {
                                        // kill player
                                        game.Player.Kill(interaction.Desciption);

                                        // display result
                                        result = interaction.Desciption;

                                        // handled
                                        return EReactionToInput.SelfContainedReaction;
                                    }
                                case EInteractionEffect.ItemUsedUp:
                                    {
                                        // if in room
                                        if (game.Overworld.CurrentRegion.CurrentRoom.ContainsItem(item))
                                            // loose the item
                                            game.Overworld.CurrentRegion.CurrentRoom.RemoveItemFromRoom(interaction.Item);
                                        else if (game.Player.FindItem(item.Name, out item))
                                            // dequire item
                                            game.Player.DequireItem(item);

                                        break;
                                    }
                                case EInteractionEffect.TargetUsedUp:
                                    {
                                        // get target
                                        var eO = target as IExaminable;

                                        // if an examinable object
                                        if (eO != null)
                                        {
                                            // if target is about
                                            if (game.Overworld.CurrentRegion.CurrentRoom.ContainsInteractionTarget(eO.Name))
                                            {
                                                // remove target
                                                game.Overworld.CurrentRegion.CurrentRoom.RemoveInteractionTargetFromRoom(target);
                                            }
                                            else
                                            {
                                                // hold item
                                                Item targetItem;

                                                // player item
                                                if (game.Player.FindItem(targetName, out targetItem))
                                                    // set target item
                                                    target = targetItem;
                                            }
                                        }

                                        break;
                                    }
                                case EInteractionEffect.NoEffect:
                                case EInteractionEffect.ItemMorphed:
                                case EInteractionEffect.SelfContained:
                                    {
                                        // nothing as these should all be self contained

                                        break;
                                    }
                                default:
                                    {
                                        throw new NotImplementedException();
                                    }
                            }

                            // display result
                            result = interaction.Desciption;

                            // handled
                            return EReactionToInput.CouldReact;
                        }
                    default:
                        {
                            throw new NotImplementedException();
                        }
                }
            }

            if (game.IsValidActionableCommand(noun))
            {
                // try and find command
                var command = game.FindActionableCommand(noun);

                // if found
                if (command != null)
                {
                    // interact
                    var customResult = command.Action();

                    // set description
                    result = customResult.Desciption;

                    // return that reacted
                    return EReactionToInput.CouldReact;
                }

                // throw exception
                throw new ArgumentException("String {0} is not a custom command", noun);
            }

            // if was no input
            if (string.IsNullOrEmpty(noun))
            {
                // empty
                result = string.Empty;

                // react
                return EReactionToInput.CouldReact;
            }
            // treat as input error

            // set error
            result = "Invalid input";

            // couldn't react
            return EReactionToInput.CouldntReact;
        }

        #endregion
    }
}