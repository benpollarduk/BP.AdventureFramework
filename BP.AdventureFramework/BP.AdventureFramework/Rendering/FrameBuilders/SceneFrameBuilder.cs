using System;
using System.Linq;
using System.Text;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Interpretation;
using BP.AdventureFramework.Rendering.Drawers;

namespace BP.AdventureFramework.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of scene frames.
    /// </summary>
    public class SceneFrameBuilder : ISceneFrameBuilder
    {
        #region StaticProperties

        /// <summary>
        /// Get or set if commands are displayed.
        /// </summary>
        public static bool DisplayCommands { get; set; } = true;

        #endregion

        #region Properties

        /// <summary>
        /// Get the frame drawer.
        /// </summary>
        public FrameDrawer FrameDrawer { get; }

        /// <summary>
        /// Get the map drawer.
        /// </summary>
        public MapDrawer MapDrawer { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the SceneFrameBuilder class.
        /// </summary>
        /// <param name="frameDrawer">A drawer to use for the frame.</param>
        /// <param name="mapDrawer">A drawer to use for the map.</param>
        public SceneFrameBuilder(FrameDrawer frameDrawer, MapDrawer mapDrawer)
        {
            FrameDrawer = frameDrawer;
            MapDrawer = mapDrawer;
        }

        #endregion

        #region Implementation of ISceneFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="room">Specify the Room.</param>
        /// <param name="player">Specify the player.</param>
        /// <param name="message">Any additional message.</param>
        /// <param name="width">The width of the frame.</param>
        /// <param name="height">The height of the frame.</param>
        public Frame Build(Room room, PlayableCharacter player, string message, int width, int height)
        {
            var scene = new StringBuilder();
            scene.Append(FrameDrawer.ConstructDivider(width));
            scene.Append(FrameDrawer.ConstructWrappedPaddedString($"LOCATION: {room.Identifier}", width));
            scene.Append(FrameDrawer.ConstructDivider(width));
            scene.Append(FrameDrawer.ConstructWrappedPaddedString(string.Empty, width));
            scene.Append(FrameDrawer.ConstructWrappedPaddedString(room.Description.GetDescription().EnsureFinishedSentence(), width));
            scene.Append(FrameDrawer.ConstructWrappedPaddedString(string.Empty, width));

            if (room.Items.Any())
                scene.Append(FrameDrawer.ConstructWrappedPaddedString(room.Examime().Desciption.EnsureFinishedSentence(), width));
            else
                scene.Append(FrameDrawer.ConstructWrappedPaddedString("There are no items in this area.", width));

            var visibleCharacters = room.Characters.Where(c => c.IsPlayerVisible && c.IsAlive).ToArray<Character>();

            if (visibleCharacters.Length > 0)
            {
                if (visibleCharacters.Length == 1)
                {
                    scene.Append(FrameDrawer.ConstructWrappedPaddedString(visibleCharacters[0].Identifier + " is in this area.", width));
                }
                else
                {
                    var characters = string.Empty;

                    foreach (var character in visibleCharacters)
                        characters += character.Identifier + ", ";

                    characters = characters.Remove(characters.Length - 2);
                    scene.Append(FrameDrawer.ConstructWrappedPaddedString(characters.Substring(0, characters.LastIndexOf(",", StringComparison.Ordinal)) + " and " + characters.Substring(characters.LastIndexOf(",", StringComparison.Ordinal) + 2) + " are in the " + room.Identifier + ".", width));
                }
            }

            scene.Append(FrameDrawer.ConstructWrappedPaddedString(string.Empty, width));
            scene.Append(FrameDrawer.ConstructDivider(width));

            if (MapDrawer != null)
            {
                scene.Append(FrameDrawer.ConstructWrappedPaddedString("AREA:", width));
                scene.Append(FrameDrawer.ConstructWrappedPaddedString(string.Empty, width));
                scene.Append(MapDrawer.ConstructRoomMap(room, width));
                scene.Append(FrameDrawer.ConstructDivider(width));
            }

            if (DisplayCommands)
            {
                scene.Append(FrameDrawer.ConstructWrappedPaddedString("COMMANDS:", width));
                scene.Append(FrameDrawer.ConstructWrappedPaddedString(string.Empty, width));
                scene.Append(FrameDrawer.ConstructWrappedPaddedString("MOVEMENT:", width));
                scene.Append(FrameDrawer.ConstructWrappedPaddedString($"{GameCommandInterpreter.NorthShort}: {GameCommandInterpreter.North}, {GameCommandInterpreter.SouthShort}: {GameCommandInterpreter.South}, {GameCommandInterpreter.EastShort}: {GameCommandInterpreter.East}, {GameCommandInterpreter.WestShort}: {GameCommandInterpreter.West}", width));
                scene.Append(FrameDrawer.ConstructWrappedPaddedString(string.Empty, width));

                var usedLinesSoFar = FrameDrawer.DetermineLinesInString(scene.ToString()) + 14 + FrameDrawer.DetermineLinesInString(FrameDrawer.ConstructWrappedPaddedString(message, width));

                if (height - usedLinesSoFar >= 0)
                {
                    scene.Append(FrameDrawer.ConstructWrappedPaddedString("INTERACTION:", width));

                    if (player.Items.Any())
                        scene.Append(FrameDrawer.ConstructWrappedPaddedString($"{GameCommandInterpreter.Drop} __: Drop an item", width));

                    scene.Append(FrameDrawer.ConstructWrappedPaddedString($"{GameCommandInterpreter.Examine} __: Examine a character, item, room, region, overworld or me", width));

                    if (room.Items.Any())
                        scene.Append(FrameDrawer.ConstructWrappedPaddedString($"{GameCommandInterpreter.Take} __: Take an item", width));

                    if (room.Characters.Any())
                        scene.Append(FrameDrawer.ConstructWrappedPaddedString($"{GameCommandInterpreter.Talk} {GameCommandInterpreter.To.ToLower()} __: Talk to a character", width));

                    if (room.Items.Any() || player.Items.Any())
                    {
                        scene.Append(FrameDrawer.ConstructWrappedPaddedString($"{GameCommandInterpreter.Use} __: Use an item on the this Room", width));
                        scene.Append(FrameDrawer.ConstructWrappedPaddedString($"{GameCommandInterpreter.Use} __ {GameCommandInterpreter.On.ToLower()} __: Use an item on another item or character", width));
                    }

                    scene.Append(FrameDrawer.ConstructWrappedPaddedString(string.Empty, width));
                }
            }

            var wrappedMessage = FrameDrawer.ConstructWrappedPaddedString(message.EnsureFinishedSentence(), width);
            var linesAfterWhitespace = 7 + FrameDrawer.DetermineLinesInString(wrappedMessage);
            var linesInString = FrameDrawer.DetermineLinesInString(scene.ToString());

            scene.Append(FrameDrawer.ConstructPaddedArea(FrameDrawer.LeftBoundaryCharacter, FrameDrawer.RightBoundaryCharacter, width, height - linesInString - linesAfterWhitespace));
            scene.Append(FrameDrawer.ConstructDivider(width));
            scene.Append(FrameDrawer.ConstructWrappedPaddedString("INVENTORY: " + player.GetItemsAsList(), width));
            scene.Append(FrameDrawer.ConstructDivider(width));
            var yPositionOfCursor = FrameDrawer.DetermineLinesInString(scene.ToString());
            scene.Append(FrameDrawer.ConstructWrappedPaddedString("WHAT DO YOU DO? ", width));
            scene.Append(FrameDrawer.ConstructDivider(width));
            scene.Append(wrappedMessage);
            var bottomdivider = FrameDrawer.ConstructDivider(width);
            scene.Append(bottomdivider.Remove(bottomdivider.Length - 1));

            return new Frame(scene.ToString(), 12, yPositionOfCursor);
        }

        #endregion
    }
}
