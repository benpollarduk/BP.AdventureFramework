using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventureFramework.Locations;
using AdventureFramework.Interaction;
using AdventureFramework.Rendering;
using AdventureFramework.Structure;

namespace AdventureFramework.IO
{
    /// <summary>
    /// An parser used for parsing text into in-game interactions
    /// </summary>
    public class TextParser
    {
        #region Methods

        /// <summary>
        /// Initializes a new instance of the TextParser class
        /// </summary>
        public TextParser()
        {
        }

        /// <summary>
        /// Try and parse a string to an ECardinalDirection
        /// </summary>
        /// <param name="obj">The string to parse</param>
        /// <param name="direction">The direction</param>
        /// <returns>The result of the parse</returns>
        public virtual Boolean TryParseToECardinalDirection(String obj, out ECardinalDirection direction)
        {
            // select direction
            switch (obj.ToUpper())
            {
                case ("E"):
                    {
                        // set direction
                        direction = ECardinalDirection.East;

                        // pass
                        return true;
                    }
                case ("N"):
                    {
                        // set driection
                        direction = ECardinalDirection.North;

                        // pass
                        return true;
                    }
                case ("S"):
                    {
                        // set direction
                        direction = ECardinalDirection.South;

                        // pass
                        return true;
                    }
                case ("W"):
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
                        Boolean result = this.checkEnumerationForCaseInsensitiveMember(typeof(ECardinalDirection), obj, out valueInEnum);

                        // if parsed
                        if (result)
                        {
                            // set command
                            direction = (ECardinalDirection)valueInEnum;
                        }
                        else
                        {
                            // default
                            direction = (ECardinalDirection)0;
                        }

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
        public virtual Boolean IsTextCardinalDirection(String input)
        {
            // select input
            switch (input.ToUpper())
            {
                case ("E"):
                case ("N"):
                case ("S"):
                case ("W"):
                    {
                        // pass
                        return true;
                    }
                default:
                    {
                        return Enum.GetNames(typeof(ECardinalDirection)).Where<String>((String name) => name.ToUpper() == input.ToUpper()).Count<String>() > 0;
                    }
            }
        }

        /// <summary>
        /// Try and parse a string to a ECommand
        /// </summary>
        /// <param name="obj">The string to parse</param>
        /// <param name="command">The command</param>
        /// <returns>The result of the parse</returns>
        public virtual Boolean TryParseToECommand(String obj, out ECommand command)
        {
            // hold value in enum
            object valueInEnum;

            // check
            Boolean result = this.checkEnumerationForCaseInsensitiveMember(typeof(ECommand), obj, out valueInEnum);

            // if parsed
            if (result)
            {
                // set command
                command = (ECommand)valueInEnum;
            }
            else
            {
                // default
                command = (ECommand)0;
            }

            // retunr result
            return result;
        }

        /// <summary>
        /// Get if text is a ECommand
        /// </summary>
        /// <param name="input">The input to check</param>
        /// <returns>True is the input is a comman</returns>
        public virtual Boolean IsCommand(String input)
        {
            // see if in enum
            return Enum.GetNames(typeof(ECommand)).Where<String>((String name) => name.ToUpper() == input.ToUpper()).Count<String>() > 0;
        }

        /// <summary>
        /// Check an enumeration for a case insensitive value
        /// </summary>
        /// <param name="typeOfEnum">The type of the enum to check</param>
        /// <param name="name">The name of the enumeration member</param>
        /// <param name="obj">The enumeration member, if found</param>
        /// <returns>The result of the check</returns>
        private Boolean checkEnumerationForCaseInsensitiveMember(Type typeOfEnum, String name, out object obj)
        {
            // get names
            String[] names = Enum.GetNames(typeOfEnum);

            // itterate all names
            for (Int32 index = 0; index < names.Length; index++)
            {
                // if found
                if (names[index].ToUpper() == name.ToUpper())
                {
                    // parse
                    obj = Enum.Parse(typeOfEnum, names[index]);

                    // worked
                    return true;
                }
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
        public virtual Boolean TryParseToEGameCommand(String obj, out EGameCommand command)
        {
            // hold value in enum
            object valueInEnum;

            // check
            Boolean result = this.checkEnumerationForCaseInsensitiveMember(typeof(EGameCommand), obj, out valueInEnum);

            // if parsed
            if (result)
            {
                // set command
                command = (EGameCommand)valueInEnum;
            }
            else
            {
                // default
                command = (EGameCommand)0;
            }

            // return result
            return result;
        }

        /// <summary>
        /// Get if text is a EGameCommand
        /// </summary>
        /// <param name="input">The input to check</param>
        /// <returns>True is the input is a comman</returns>
        public virtual Boolean IsEGameCommand(String input)
        {
            return Enum.GetNames(typeof(EGameCommand)).Where<String>((String name) => name.ToUpper() == input.ToUpper()).Count<String>() > 0;
        }

        /// <summary>
        /// Try and parse a string to a EFrameDrawerOption
        /// </summary>
        /// <param name="obj">The string to parse</param>
        /// <param name="command">The command</param>
        /// <returns>The result of the parse</returns>
        public virtual Boolean TryParseToEFrameDrawerOption(String obj, out EFrameDrawerOption command)
        {
            // hold value in enum
            object valueInEnum;

            // check
            Boolean result = this.checkEnumerationForCaseInsensitiveMember(typeof(EFrameDrawerOption), obj, out valueInEnum);

            // if parsed
            if (result)
            {
                // set command
                command = (EFrameDrawerOption)valueInEnum;
            }
            else
            {
                // default
                command = (EFrameDrawerOption)0;
            }

            // return result
            return result;
        }

        /// <summary>
        /// Get if text is a EFrameDrawerOption
        /// </summary>
        /// <param name="input">The input to check</param>
        /// <returns>True is the input is a comman</returns>
        public virtual Boolean IsEFrameDrawerOption(String input)
        {
            return Enum.GetNames(typeof(EFrameDrawerOption)).Where<String>((String name) => name.ToUpper() == input.ToUpper()).Count<String>() > 0;
        }

        /// <summary>
        /// React to an input string. This will take all necessary action to the input on the Game parameter
        /// </summary>
        /// <param name="input">The input to action</param>
        /// <param name="game">The game to action the input on</param>
        /// <param name="result">Any result of the reaction</param>
        /// <returns>The reaction to the input</returns>
        public virtual EReactionToInput ReactToInput(String input, Game game, out String result)
        {
            // always use as upper case
            input = input.ToUpper();

            // hold noun
            String noun = String.Empty;

            // hold the object
            String obj = String.Empty; 

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
            if (this.IsTextCardinalDirection(noun))
            {
                // hold direction
                ECardinalDirection direction;

                // parse
                this.TryParseToECardinalDirection(noun, out direction);

                // move
                if (game.Overworld.CurrentRegion.Move(direction))
                {
                    // no error
                    result = "Moved " + direction;

                    // pass
                    return EReactionToInput.CouldReact;
                }
                else
                {
                    // set error
                    result = "Could not move " + direction;

                    // move failed
                    return EReactionToInput.CouldntReact;
                }
            }
            else if (this.IsCommand(noun))
            {
                // hold command
                ECommand command;

                // parse
                this.TryParseToECommand(noun, out command);

                // select command
                switch (command)
                {
                    case (ECommand.Drop):
                        {
                            // if an item
                            if (!String.IsNullOrEmpty(obj))
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
                                else
                                {
                                    // nothing to drop
                                    result = "You don't have that item";

                                    // pass
                                    return EReactionToInput.CouldReact;
                                }
                            }
                            else
                            {
                                // examine the room
                                result = "You must specify what to drop";

                                // pass
                                return EReactionToInput.CouldntReact;
                            }
                        }
                    case (ECommand.Examine):
                        {
                            // hold examination result
                            ExaminationResult examinationResult = null;

                            // if an item
                            if (!String.IsNullOrEmpty(obj))
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
                                else if (this.IsTextCardinalDirection(obj))
                                {
                                    // hold direction
                                    ECardinalDirection direction;

                                    // parse
                                    this.TryParseToECardinalDirection(obj, out direction);

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
                                        result = String.Format("There is no exit in this room to the {0}", direction);

                                        // fail
                                        return EReactionToInput.CouldntReact;
                                    }
                                }
                                else if ((obj == "ME") ||
                                         (obj == game.Player.Name.ToUpper()))
                                {
                                    // examine the player
                                    examinationResult = game.Player.Examime();
                                }
                                else if ((obj == "ROOM") ||
                                         (obj == game.Overworld.CurrentRegion.CurrentRoom.Name.ToUpper()))
                                {
                                    // examine the region
                                    examinationResult = game.Overworld.CurrentRegion.CurrentRoom.Examime();
                                }
                                else if ((obj == "REGION") ||
                                         (obj == game.Overworld.CurrentRegion.Name.ToUpper()))
                                {
                                    // examine the region
                                    examinationResult = new ExaminationResult(game.Overworld.CurrentRegion.Description.GetDescription(), EExaminationResults.DescriptionReturned);
                                }
                                else if ((obj == "OVERWORLD") ||
                                         (obj == game.Overworld.Name.ToUpper()))
                                {
                                    // examine the region
                                    examinationResult = new ExaminationResult(game.Overworld.Description.GetDescription(), EExaminationResults.DescriptionReturned);
                                }
                                else if (!String.IsNullOrEmpty(obj))
                                {
                                    // can't examine
                                    result = "Can't examine " + obj.ToString();

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
                                case (EExaminationResults.DescriptionReturned):
                                    {
                                        // set reaction
                                        return EReactionToInput.CouldReact;
                                    }
                                case (EExaminationResults.SelfContained):
                                    {
                                        // set reaction
                                        return EReactionToInput.SelfContainedReaction;
                                    }
                                default: { throw new NotImplementedException(); }
                            }
                        }
                    case (ECommand.Take):
                        {
                            // hold removed item
                            Item removedItem;

                            // if only one takeable item in the room, and name not entered
                            if ((game.Overworld.CurrentRegion.CurrentRoom.Items.Where<Item>((Item i) => i.IsTakeable).Count<Item>() == 1) &&
                                (String.IsNullOrEmpty(obj)))
                            {
                                // game item name
                                obj = game.Overworld.CurrentRegion.CurrentRoom.Items.Where<Item>((Item i) => i.IsTakeable).ToArray<Item>()[0].Name;
                            }

                            // try remove item
                            Decision reaction = game.Overworld.CurrentRegion.CurrentRoom.RemoveItemFromRoom(obj, out removedItem);

                            // if worked
                            if (reaction.Result == EReactionToInput.CouldReact)
                            {
                                // the removed item
                                game.Player.AquireItem(removedItem);
                            }

                            // set message
                            result = reaction.Reason;

                            // set result
                            return reaction.Result;
                        }
                    case (ECommand.Talk):
                        {
                            // get all alive characters
                            Character[] aliveCharactersInRoom = game.Overworld.CurrentRegion.CurrentRoom.Characters.Where<Character>((Character c) => ((c.IsAlive) && (c.IsPlayerVisible))).ToArray<Character>();

                            // if only one item in the room, and name not entered
                            if ((aliveCharactersInRoom.Length == 1) &&
                                (String.IsNullOrEmpty(obj)))
                            {
                                // game item name
                                obj = aliveCharactersInRoom[0].Name;
                            }

                            // hold no playable character
                            NonPlayableCharacter nPC;

                            // see if a 'to '
                            if ((obj.Length > 3) &&
                                (obj.Substring(0, 3) == "TO "))
                            {
                                // remove 'to '
                                obj = obj.Remove(0, 3);
                            }

                            // if character found
                            if ((game.Overworld.CurrentRegion.CurrentRoom.FindCharacter(obj, out nPC)) &&
                                (nPC.IsAlive))
                            {
                                // set message
                                result = nPC.Talk();

                                // set result
                                return EReactionToInput.CouldReact;
                            }
                            else
                            {
                                // get if something
                                Boolean isSomething = ((game.Overworld.CurrentRegion.CurrentRoom.ContainsItem(obj)) || 
                                                       (game.Overworld.CurrentRegion.CurrentRoom.ContainsCharacter(obj)) ||
                                                       (game.Player.Items.Where<Item>((Item i) => i.Name.ToUpper() == obj).Count<Item>() > 0));

                                // if something
                                if (isSomething)
                                {
                                    // set result
                                    result = obj.Substring(0,1) + obj.Substring(1).ToLower() + " cannot be talked to";
                                }
                                else
                                {
                                    // no-one to talk to (need to format obj to sentance case)
                                    result = String.IsNullOrEmpty(obj) ? "No-one is around to talk to" : "Can't talk to that as it doesn't exist";
                                }
                                 
                                // set result
                                return EReactionToInput.CouldntReact;
                            }
                        }
                    case (ECommand.Use):
                        {
                            // hold item
                            Item item;

                            // hold target
                            IInteractWithItem target = null;

                            // hold item name
                            String itemName = null;

                            // hold target name
                            String targetName = null;

                            // if ON verb found
                            if (obj.Contains(" ON "))
                            {
                                // everything before verb is source
                                itemName = obj.Substring(0, obj.IndexOf(" ON "));

                                // remove item name
                                obj = obj.Replace(itemName, String.Empty);

                                // remove verb
                                targetName = obj.Replace(" ON ", String.Empty);

                                // if no item name
                                if (String.IsNullOrEmpty(itemName))
                                {
                                    // no target
                                    result = "That is not an item";

                                    // couldnt react
                                    return EReactionToInput.CouldntReact;
                                }

                                // if no target name
                                if (String.IsNullOrEmpty(targetName))
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
                            if (String.IsNullOrEmpty(itemName))
                            {
                                // set message
                                result = "You must specify an item";

                                // fail
                                return EReactionToInput.CouldntReact;
                            }
                            else if (!game.Player.FindItem(itemName, out item))
                            {
                                // try find item in room
                                if (!((game.Overworld.CurrentRegion.CurrentRoom.FindItem(itemName, out item))))
                                {
                                    // set message
                                    result = "You don't have that item";

                                    // fail
                                    return EReactionToInput.CouldntReact;
                                }
                            }

                            // hold result of interaction
                            InteractionResult interaction;

                            // if a target name
                            if (!String.IsNullOrEmpty(targetName))
                            {
                                // select name
                                switch (targetName)
                                {
                                    case ("ME"):
                                        {
                                            // player
                                            target = game.Player;

                                            break;
                                        }
                                    case ("ROOM"):
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
                                IExaminable examinableTarget = target as IExaminable;

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
                                case (EInteractionEffect.FatalEffect):
                                    {
                                        // kill player
                                        game.Player.Kill(interaction.Desciption);

                                        // display result
                                        result = interaction.Desciption;

                                        // handled
                                        return EReactionToInput.SelfContainedReaction;
                                    }
                                case (EInteractionEffect.ItemUsedUp):
                                    {
                                        // if in room
                                        if (game.Overworld.CurrentRegion.CurrentRoom.ContainsItem(item))
                                        {
                                            // loose the item
                                            game.Overworld.CurrentRegion.CurrentRoom.RemoveItemFromRoom(interaction.Item);
                                        }
                                        else if (game.Player.FindItem(item.Name, out item))
                                        {
                                            // dequire item
                                            game.Player.DequireItem(item);
                                        }

                                        break;
                                    }
                                case (EInteractionEffect.TargetUsedUp):
                                    {
                                        // get target
                                        IExaminable eO = target as IExaminable;

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
                                                {
                                                    // set target item
                                                    target = targetItem;
                                                }
                                            }
                                        }

                                        break;
                                    }
                                case (EInteractionEffect.NoEffect):
                                case (EInteractionEffect.ItemMorphed):
                                case (EInteractionEffect.SelfContained):
                                    {
                                        // nothing as these should all be self contained

                                        break;
                                    }
                                default: { throw new NotImplementedException(); }
                            }

                            // display result
                            result = interaction.Desciption;

                            // handled
                            return EReactionToInput.CouldReact;
                        }
                    default: { throw new NotImplementedException(); }
                }
            }
            else if (game.IsValidActionableCommand(noun))
            {
                // try and find command
                ActionableCommand command = game.FindActionableCommand(noun);

                // if found
                if (command != null)
                {
                    // interact
                    InteractionResult customResult = command.Action();

                    // set description
                    result = customResult.Desciption;

                    // return that reacted
                    return EReactionToInput.CouldReact;
                }
                else
                {
                    // throw exception
                    throw new ArgumentException("String {0} is not a custom command", noun);
                }
            }
            else
            {
                // if was no input
                if (String.IsNullOrEmpty(noun))
                {
                    // empty
                    result = String.Empty;

                    // react
                    return EReactionToInput.CouldReact;
                }
                else
                {
                    // treat as input error

                    // set error
                    result = "Invalid input";

                    // couldn't react
                    return EReactionToInput.CouldntReact;
                }
            }
        }

        #endregion
    }
}
