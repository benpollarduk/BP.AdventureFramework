using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventureFramework.Locations;
using AdventureFramework.Interaction;

namespace AdventureFramework.Rendering.Frames
{
    /// <summary>
    /// Represents a frame that describes a scene
    /// </summary>
    public class SceneFrame : Frame
    {
        #region Properties

        /// <summary>
        /// Get or set the Room
        /// </summary>
        public Room Room
        {
            get { return this.room; }
            set { this.room = value; }
        }

        /// <summary>
        /// Get or set the Room
        /// </summary>
        private Room room;

        /// <summary>
        /// Get or set the player
        /// </summary>
        public PlayableCharacter Player
        {
            get { return this.player; }
            set { this.player = value; }
        }

        /// <summary>
        /// Get or set the player
        /// </summary>
        private PlayableCharacter player;

        /// <summary>
        /// Get or set the message
        /// </summary>
        public String Message
        {
            get { return this.message; }
            set { this.message = value; }
        }

        /// <summary>
        /// Get or set the message
        /// </summary>
        private String message;

        /// <summary>
        /// Get or set the drawer used for constructing room maps
        /// </summary>
        public MapDrawer MapDrawer
        {
            get { return this.mapDrawer; }
            set { this.mapDrawer = value; }
        }

        /// <summary>
        /// Get or set the drawer used for constructing room maps
        /// </summary>
        private MapDrawer mapDrawer = new MapDrawer();

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the SceneFrame class
        /// </summary>
        /// <param name="room">Specify the Room</param>
        /// <param name="player">Specify the player</param>
        /// <param name="message">Any additional message</param>
        public SceneFrame(Room room, PlayableCharacter player, String message)
        {
            // set room
            this.Room = room;

            // set player
            this.Player = player;

            // set message
            this.Message = message;
        }

        /// <summary>
        /// Initializes a new instance of the SceneFrame class
        /// </summary>
        /// <param name="room">Specify the Room</param>
        /// <param name="player">Specify the player</param>
        /// <param name="message">Any additional message</param>
        /// <param name="mapDrawer">Sepcify a drawer for constructing room maps</param>
        public SceneFrame(Room room, PlayableCharacter player, String message, MapDrawer mapDrawer)
        {
            // set room
            this.Room = room;

            // set player
            this.Player = player;

            // set message
            this.Message = message;

            // set drawer
            this.MapDrawer = mapDrawer;
        }

        /// <summary>
        /// Build this SceneFrame into a text based display
        /// </summary>
        /// <param name="width">Specify the width of the Frame</param>
        /// <param name="height">Specify the height of the Frame</param>
        /// <param name="drawer">The FrameDrawer to draw the Frame with</param>
        /// <returns>A string representing the Frame</returns>
        public override string BuildFrame(int width, int height, FrameDrawer drawer)
        {
            // hold scene
            StringBuilder scene = new StringBuilder();

            // add devider
            scene.Append(drawer.ConstructDevider(width));

            // add title
            scene.Append(drawer.ConstructWrappedPaddedString(String.Format("LOCATION: {0}", this.Room.Name), width));

            // add devider
            scene.Append(drawer.ConstructDevider(width));

            // add top spacer
            scene.Append(drawer.ConstructWrappedPaddedString(String.Empty, width));

            // add description
            scene.Append(drawer.ConstructWrappedPaddedString(this.Room.Description.GetDescription(), width));

            // add middle spacer
            scene.Append(drawer.ConstructWrappedPaddedString(String.Empty, width));

            // if some items
            if (this.Room.Items.Length > 0)
            {
                // add items
                scene.Append(drawer.ConstructWrappedPaddedString(this.Room.Examime().Desciption, width));
            }
            else
            {
                // add items
                scene.Append(drawer.ConstructWrappedPaddedString("There are no items in this area", width));
            }

            // get visible characters
            Character[] visibleCharacters = this.Room.Characters.Where<Character>((Character c) => ((c.IsPlayerVisible) && (c.IsAlive))).ToArray<Character>();

            // if some characters
            if (visibleCharacters.Length > 0)
            {
                // if just one
                if (visibleCharacters.Length == 1)
                {
                    // append character information
                    scene.Append(drawer.ConstructWrappedPaddedString(visibleCharacters[0].Name + " is in this area", width));
                }
                else
                {
                    // append data
                    String characters = String.Empty;

                    // itterate each character
                    foreach (Character character in visibleCharacters)
                    {
                        // add each chanracter
                        characters += character.Name + ", ";
                    }

                    // remove last ", "
                    characters = characters.Remove(characters.Length - 2);

                    // append character information
                    scene.Append(drawer.ConstructWrappedPaddedString(characters.Substring(0, characters.LastIndexOf(",")) + " and " + characters.Substring(characters.LastIndexOf(",") + 2) + " are in the " + this.Room.Name, width));
                }
            }

            // add bottom spacer
            scene.Append(drawer.ConstructWrappedPaddedString(String.Empty, width));

            // add devider
            scene.Append(drawer.ConstructDevider(width));

            // if a map drawer
            if (this.MapDrawer != null)
            {
                // add map title
                scene.Append(drawer.ConstructWrappedPaddedString("AREA:", width));

                // add map spacer
                scene.Append(drawer.ConstructWrappedPaddedString(String.Empty, width));

                // add map
                scene.Append(this.MapDrawer.ConstructRoomMap(this.Room, width));

                // add devider
                scene.Append(drawer.ConstructDevider(width));
            }

            // if displaying commands
            if (drawer.DisplayCommands)
            {
                // add commands title
                scene.Append(drawer.ConstructWrappedPaddedString("COMMANDS:", width));

                // add spacer
                scene.Append(drawer.ConstructWrappedPaddedString(String.Empty, width));

                // add directions title
                scene.Append(drawer.ConstructWrappedPaddedString("MOVEMENT:", width));

                scene.Append(drawer.ConstructWrappedPaddedString("N: North, S: South, E: East, W: West", width));

                // add spacer
                scene.Append(drawer.ConstructWrappedPaddedString(String.Empty, width));

                // work used lines so far
                Int32 usedLinesSoFar = drawer.DetermineLinesInString(scene.ToString()) + 14 + drawer.DetermineLinesInString(drawer.ConstructWrappedPaddedString(message, width));

                // if enough this.Room for commands
                if (height - usedLinesSoFar >= 0)
                {
                    // add interaction title
                    scene.Append(drawer.ConstructWrappedPaddedString("INTERACTION:", width));

                    // add commands
                    
                    // if some items
                    if (this.Player.Items.Length > 0)
                    {
                        // add drop
                        scene.Append(drawer.ConstructWrappedPaddedString("Drop __: Drop an item", width));
                    }

                    // always add examine
                    scene.Append(drawer.ConstructWrappedPaddedString("Examine __: Examine a character, item, room, region, overworld or me", width));
                    
                    // if something to take
                    if (this.Room.Items.Length > 0)
                    {
                        // add take
                        scene.Append(drawer.ConstructWrappedPaddedString("Take __: Take an item", width));
                    }

                    // if someone to talk to
                    if (this.Room.Characters.Length > 0)
                    {
                        // add talk
                        scene.Append(drawer.ConstructWrappedPaddedString("Talk to __: Talk to a character", width));
                    } 
                    
                    // if some item
                    if ((this.Room.Items.Length > 0) ||
                        (this.Player.Items.Length > 0))
                    {
                        // add use
                        scene.Append(drawer.ConstructWrappedPaddedString("Use __: Use an item on the this Room", width));
                        scene.Append(drawer.ConstructWrappedPaddedString("Use __ on __: Use an item on another item or character", width));
                    }

                    // add spacer
                    scene.Append(drawer.ConstructWrappedPaddedString(String.Empty, width));
                }

                // hold all custom commands
                List<IImplementOwnActions> customCommands = new List<IImplementOwnActions>();

                // add this.Player
                customCommands.AddRange(this.Player.GetAllObjectsWithAdditionalCommands());

                // add this.Room items
                customCommands.AddRange(this.Room.GetAllObjectsWithAdditionalCommands());

                // if some commands
                if (customCommands.Where<IImplementOwnActions>((IImplementOwnActions c) => c.AdditionalCommands.Count > 0).Count<IImplementOwnActions>() > 0)
                {
                    // hold if added title
                    Boolean hasAddedTitle = false;

                    // itterate all
                    foreach (IImplementOwnActions commandable in customCommands)
                    {
                        // itterate all commands
                        foreach (ActionableCommand command in commandable.AdditionalCommands.Where<ActionableCommand>((ActionableCommand aC) => aC.IsPlayerVisible))
                        {
                            // if not added title already
                            if (!hasAddedTitle)
                            {
                                // add interaction title
                                scene.Append(drawer.ConstructWrappedPaddedString("ADDITIONAL COMMANDS:", width));

                                // now added title
                                hasAddedTitle = true;
                            }

                            // append custom command
                            scene.Append(drawer.ConstructWrappedPaddedString(String.Format("{0}: {1}", command.Command.ToUpper(), command.Description), width));
                        }
                    }

                    // add spacer
                    scene.Append(drawer.ConstructWrappedPaddedString(String.Empty, width));
                }
            }

            // hold wrapped message
            String wrappedMessage = drawer.ConstructWrappedPaddedString(message, width);

            // hold lines after whitespace
            Int32 linesAfterWhitespace = 7 + drawer.DetermineLinesInString(wrappedMessage);

            // finish up
            Int32 linesInString = drawer.DetermineLinesInString(scene.ToString());

            // pad white space
            scene.Append(drawer.ConstructPaddedArea(drawer.LeftBoundaryCharacter, drawer.RightBoundaryCharacter, width, height - linesInString - linesAfterWhitespace));

            // add devider
            scene.Append(drawer.ConstructDevider(width));

            // append items
            scene.Append(drawer.ConstructWrappedPaddedString("INVENTORY: " + this.Player.GetItemsAsList(), width));

            // add devider
            scene.Append(drawer.ConstructDevider(width));

            // hold y position
            Int32 yPositionOfCursor = drawer.DetermineLinesInString(scene.ToString());

            // add command input
            scene.Append(drawer.ConstructWrappedPaddedString("WHAT DO YOU DO? ", width));

            // add devider
            scene.Append(drawer.ConstructDevider(width));

            // add message space
            scene.Append(wrappedMessage);

            // hold bottom devider
            String bottomDevider = drawer.ConstructDevider(width);

            // add devider removing the last \n
            scene.Append(bottomDevider.Remove(bottomDevider.Length - 1));

            // set cursor left
            this.CursorLeft = 18;

            // set y
            this.CursorTop = yPositionOfCursor;

            // return the scene
            return scene.ToString();
        }

        #endregion
    }
}
