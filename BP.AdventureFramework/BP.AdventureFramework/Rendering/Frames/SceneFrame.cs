using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.AdventureFramework.Characters;
using BP.AdventureFramework.Interaction;
using BP.AdventureFramework.Locations;

namespace BP.AdventureFramework.Rendering.Frames
{
    /// <summary>
    /// Represents a frame that describes a scene.
    /// </summary>
    public class SceneFrame : Frame
    {
        #region Properties

        /// <summary>
        /// Get or set the Room.
        /// </summary>
        public Room Room { get; set; }

        /// <summary>
        /// Get or set the player.
        /// </summary>
        public PlayableCharacter Player { get; set; }

        /// <summary>
        /// Get or set the message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Get or set the drawer used for constructing room maps.
        /// </summary>
        public MapDrawer MapDrawer { get; set; } = new MapDrawer();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the SceneFrame class.
        /// </summary>
        /// <param name="room">Specify the Room.</param>
        /// <param name="player">Specify the player.</param>
        /// <param name="message">Any additional message.</param>
        public SceneFrame(Room room, PlayableCharacter player, string message)
        {
            Room = room;
            Player = player;
            Message = message;
        }

        /// <summary>
        /// Initializes a new instance of the SceneFrame class.
        /// </summary>
        /// <param name="room">Specify the Room.</param>
        /// <param name="player">Specify the player.</param>
        /// <param name="message">Any additional message.</param>
        /// <param name="mapDrawer">Specify a drawer for constructing room maps.</param>
        public SceneFrame(Room room, PlayableCharacter player, string message, MapDrawer mapDrawer)
        {
            Room = room;
            Player = player;
            Message = message;
            MapDrawer = mapDrawer;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Build this SceneFrame into a text based display.
        /// </summary>
        /// <param name="width">Specify the width of the Frame.</param>
        /// <param name="height">Specify the height of the Frame.</param>
        /// <param name="drawer">The FrameDrawer to draw the Frame with.</param>
        /// <returns>A string representing the Frame.</returns>
        public override string BuildFrame(int width, int height, FrameDrawer drawer)
        {
            var scene = new StringBuilder();
            scene.Append(drawer.ConstructDivider(width));
            scene.Append(drawer.ConstructWrappedPaddedString($"LOCATION: {Room.Identifier}", width));
            scene.Append(drawer.ConstructDivider(width));
            scene.Append(drawer.ConstructWrappedPaddedString(string.Empty, width));
            scene.Append(drawer.ConstructWrappedPaddedString(Room.Description.GetDescription(), width));
            scene.Append(drawer.ConstructWrappedPaddedString(string.Empty, width));

            if (Room.Items.Any())
                scene.Append(drawer.ConstructWrappedPaddedString(Room.Examime().Desciption, width));
            else
                scene.Append(drawer.ConstructWrappedPaddedString("There are no items in this area", width));

            var visibleCharacters = Room.Characters.Where(c => c.IsPlayerVisible && c.IsAlive).ToArray<Character>();

            if (visibleCharacters.Length > 0)
            {
                if (visibleCharacters.Length == 1)
                {
                    scene.Append(drawer.ConstructWrappedPaddedString(visibleCharacters[0].Identifier + " is in this area", width));
                }
                else
                {
                    var characters = string.Empty;

                    foreach (var character in visibleCharacters)
                        characters += character.Identifier + ", ";

                    characters = characters.Remove(characters.Length - 2);
                    scene.Append(drawer.ConstructWrappedPaddedString(characters.Substring(0, characters.LastIndexOf(",", StringComparison.Ordinal)) + " and " + characters.Substring(characters.LastIndexOf(",", StringComparison.Ordinal) + 2) + " are in the " + Room.Identifier, width));
                }
            }

            scene.Append(drawer.ConstructWrappedPaddedString(string.Empty, width));
            scene.Append(drawer.ConstructDivider(width));

            if (MapDrawer != null)
            {
                scene.Append(drawer.ConstructWrappedPaddedString("AREA:", width));
                scene.Append(drawer.ConstructWrappedPaddedString(string.Empty, width));
                scene.Append(MapDrawer.ConstructRoomMap(Room, width));
                scene.Append(drawer.ConstructDivider(width));
            }

            if (drawer.DisplayCommands)
            {
                scene.Append(drawer.ConstructWrappedPaddedString("COMMANDS:", width));
                scene.Append(drawer.ConstructWrappedPaddedString(string.Empty, width));
                scene.Append(drawer.ConstructWrappedPaddedString("MOVEMENT:", width));
                scene.Append(drawer.ConstructWrappedPaddedString("N: North, S: South, E: East, W: West", width));
                scene.Append(drawer.ConstructWrappedPaddedString(string.Empty, width));

                var usedLinesSoFar = drawer.DetermineLinesInString(scene.ToString()) + 14 + drawer.DetermineLinesInString(drawer.ConstructWrappedPaddedString(Message, width));

                if (height - usedLinesSoFar >= 0)
                {
                    scene.Append(drawer.ConstructWrappedPaddedString("INTERACTION:", width));

                    if (Player.Items.Any())
                        scene.Append(drawer.ConstructWrappedPaddedString("Drop __: Drop an item", width));

                    scene.Append(drawer.ConstructWrappedPaddedString("Examine __: Examine a character, item, room, region, overworld or me", width));

                    if (Room.Items.Any())
                        scene.Append(drawer.ConstructWrappedPaddedString("Take __: Take an item", width));

                    if (Room.Characters.Any())
                        scene.Append(drawer.ConstructWrappedPaddedString("Talk to __: Talk to a character", width));

                    if (Room.Items.Any() || Player.Items.Any())
                    {
                        scene.Append(drawer.ConstructWrappedPaddedString("Use __: Use an item on the this Room", width));
                        scene.Append(drawer.ConstructWrappedPaddedString("Use __ on __: Use an item on another item or character", width));
                    }

                    scene.Append(drawer.ConstructWrappedPaddedString(string.Empty, width));
                }

                var customCommands = new List<IImplementOwnActions>();
                customCommands.AddRange(Player.GetAllObjectsWithAdditionalCommands());
                customCommands.AddRange(Room.GetAllObjectsWithAdditionalCommands());

                if (customCommands.Any(c => c.AdditionalCommands.Any()))
                {
                    var hasAddedTitle = false;

                    foreach (var commandable in customCommands)
                    {
                        foreach (var command in commandable.AdditionalCommands.Where(aC => aC.IsPlayerVisible))
                        {
                            if (!hasAddedTitle)
                            {
                                scene.Append(drawer.ConstructWrappedPaddedString("ADDITIONAL COMMANDS:", width));
                                hasAddedTitle = true;
                            }

                            scene.Append(drawer.ConstructWrappedPaddedString($"{command.Command.ToUpper()}: {command.Description}", width));
                        }
                    }

                    scene.Append(drawer.ConstructWrappedPaddedString(string.Empty, width));
                }
            }

            var wrappedMessage = drawer.ConstructWrappedPaddedString(Message, width);
            var linesAfterWhitespace = 7 + drawer.DetermineLinesInString(wrappedMessage);
            var linesInString = drawer.DetermineLinesInString(scene.ToString());

            scene.Append(drawer.ConstructPaddedArea(drawer.LeftBoundaryCharacter, drawer.RightBoundaryCharacter, width, height - linesInString - linesAfterWhitespace));
            scene.Append(drawer.ConstructDivider(width));
            scene.Append(drawer.ConstructWrappedPaddedString("INVENTORY: " + Player.GetItemsAsList(), width));
            scene.Append(drawer.ConstructDivider(width));
            var yPositionOfCursor = drawer.DetermineLinesInString(scene.ToString());
            scene.Append(drawer.ConstructWrappedPaddedString("WHAT DO YOU DO? ", width));
            scene.Append(drawer.ConstructDivider(width));
            scene.Append(wrappedMessage);
            var bottomdivider = drawer.ConstructDivider(width);
            scene.Append(bottomdivider.Remove(bottomdivider.Length - 1));

            CursorLeft = 18;
            CursorTop = yPositionOfCursor;

            return scene.ToString();
        }

        #endregion
    }
}