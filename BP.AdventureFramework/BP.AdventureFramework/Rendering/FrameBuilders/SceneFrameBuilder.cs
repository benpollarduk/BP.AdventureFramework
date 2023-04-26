using System;
using System.Linq;
using System.Text;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Interpretation;

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

        #region Implementation of ISceneFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="room">Specify the Room.</param>
        /// <param name="player">Specify the player.</param>
        /// <param name="message">Any additional message.</param>
        /// <param name="mapDrawer">Specify a drawer for constructing room maps.</param>
        /// <param name="frameDrawer">Specify the frame drawer.</param>
        /// <param name="width">The width of the frame.</param>
        /// <param name="height">The height of the frame.</param>
        public Frame Build(Room room, PlayableCharacter player, string message, MapDrawer mapDrawer, FrameDrawer frameDrawer, int width, int height)
        {
            var scene = new StringBuilder();
            scene.Append(frameDrawer.ConstructDivider(width));
            scene.Append(frameDrawer.ConstructWrappedPaddedString($"LOCATION: {room.Identifier}", width));
            scene.Append(frameDrawer.ConstructDivider(width));
            scene.Append(frameDrawer.ConstructWrappedPaddedString(string.Empty, width));
            scene.Append(frameDrawer.ConstructWrappedPaddedString(room.Description.GetDescription().EnsureFinishedSentence(), width));
            scene.Append(frameDrawer.ConstructWrappedPaddedString(string.Empty, width));

            if (room.Items.Any())
                scene.Append(frameDrawer.ConstructWrappedPaddedString(room.Examime().Desciption.EnsureFinishedSentence(), width));
            else
                scene.Append(frameDrawer.ConstructWrappedPaddedString("There are no items in this area.", width));

            var visibleCharacters = room.Characters.Where(c => c.IsPlayerVisible && c.IsAlive).ToArray<Character>();

            if (visibleCharacters.Length > 0)
            {
                if (visibleCharacters.Length == 1)
                {
                    scene.Append(frameDrawer.ConstructWrappedPaddedString(visibleCharacters[0].Identifier + " is in this area.", width));
                }
                else
                {
                    var characters = string.Empty;

                    foreach (var character in visibleCharacters)
                        characters += character.Identifier + ", ";

                    characters = characters.Remove(characters.Length - 2);
                    scene.Append(frameDrawer.ConstructWrappedPaddedString(characters.Substring(0, characters.LastIndexOf(",", StringComparison.Ordinal)) + " and " + characters.Substring(characters.LastIndexOf(",", StringComparison.Ordinal) + 2) + " are in the " + room.Identifier + ".", width));
                }
            }

            scene.Append(frameDrawer.ConstructWrappedPaddedString(string.Empty, width));
            scene.Append(frameDrawer.ConstructDivider(width));

            if (mapDrawer != null)
            {
                scene.Append(frameDrawer.ConstructWrappedPaddedString("AREA:", width));
                scene.Append(frameDrawer.ConstructWrappedPaddedString(string.Empty, width));
                scene.Append(mapDrawer.ConstructRoomMap(room, width));
                scene.Append(frameDrawer.ConstructDivider(width));
            }

            if (DisplayCommands)
            {
                scene.Append(frameDrawer.ConstructWrappedPaddedString("COMMANDS:", width));
                scene.Append(frameDrawer.ConstructWrappedPaddedString(string.Empty, width));
                scene.Append(frameDrawer.ConstructWrappedPaddedString("MOVEMENT:", width));
                scene.Append(frameDrawer.ConstructWrappedPaddedString($"{GameCommandInterpreter.NorthShort}: {GameCommandInterpreter.North}, {GameCommandInterpreter.SouthShort}: {GameCommandInterpreter.South}, {GameCommandInterpreter.EastShort}: {GameCommandInterpreter.East}, {GameCommandInterpreter.WestShort}: {GameCommandInterpreter.West}", width));
                scene.Append(frameDrawer.ConstructWrappedPaddedString(string.Empty, width));

                var usedLinesSoFar = frameDrawer.DetermineLinesInString(scene.ToString()) + 14 + frameDrawer.DetermineLinesInString(frameDrawer.ConstructWrappedPaddedString(message, width));

                if (height - usedLinesSoFar >= 0)
                {
                    scene.Append(frameDrawer.ConstructWrappedPaddedString("INTERACTION:", width));

                    if (player.Items.Any())
                        scene.Append(frameDrawer.ConstructWrappedPaddedString($"{GameCommandInterpreter.Drop} __: Drop an item", width));

                    scene.Append(frameDrawer.ConstructWrappedPaddedString($"{GameCommandInterpreter.Examine} __: Examine a character, item, room, region, overworld or me", width));

                    if (room.Items.Any())
                        scene.Append(frameDrawer.ConstructWrappedPaddedString($"{GameCommandInterpreter.Take} __: Take an item", width));

                    if (room.Characters.Any())
                        scene.Append(frameDrawer.ConstructWrappedPaddedString($"{GameCommandInterpreter.Talk} {GameCommandInterpreter.To.ToLower()} __: Talk to a character", width));

                    if (room.Items.Any() || player.Items.Any())
                    {
                        scene.Append(frameDrawer.ConstructWrappedPaddedString($"{GameCommandInterpreter.Use} __: Use an item on the this Room", width));
                        scene.Append(frameDrawer.ConstructWrappedPaddedString($"{GameCommandInterpreter.Use} __ {GameCommandInterpreter.On.ToLower()} __: Use an item on another item or character", width));
                    }

                    scene.Append(frameDrawer.ConstructWrappedPaddedString(string.Empty, width));
                }
            }

            var wrappedMessage = frameDrawer.ConstructWrappedPaddedString(message.EnsureFinishedSentence(), width);
            var linesAfterWhitespace = 7 + frameDrawer.DetermineLinesInString(wrappedMessage);
            var linesInString = frameDrawer.DetermineLinesInString(scene.ToString());

            scene.Append(frameDrawer.ConstructPaddedArea(frameDrawer.LeftBoundaryCharacter, frameDrawer.RightBoundaryCharacter, width, height - linesInString - linesAfterWhitespace));
            scene.Append(frameDrawer.ConstructDivider(width));
            scene.Append(frameDrawer.ConstructWrappedPaddedString("INVENTORY: " + player.GetItemsAsList(), width));
            scene.Append(frameDrawer.ConstructDivider(width));
            var yPositionOfCursor = frameDrawer.DetermineLinesInString(scene.ToString());
            scene.Append(frameDrawer.ConstructWrappedPaddedString("WHAT DO YOU DO? ", width));
            scene.Append(frameDrawer.ConstructDivider(width));
            scene.Append(wrappedMessage);
            var bottomdivider = frameDrawer.ConstructDivider(width);
            scene.Append(bottomdivider.Remove(bottomdivider.Length - 1));

            return new Frame(scene.ToString(), 12, yPositionOfCursor);
        }

        #endregion
    }
}
