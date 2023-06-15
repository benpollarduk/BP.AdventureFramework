using System;
using System.Linq;
using System.Text;
using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Interpretation;
using BP.AdventureFramework.Rendering.Frames;
using BP.AdventureFramework.Utilities;

namespace BP.AdventureFramework.Rendering.FrameBuilders.Appenders.Legacy
{
    /// <summary>
    /// Provides a builder of legacy scene frames.
    /// </summary>
    public sealed class LegacySceneFrameBuilder : ISceneFrameBuilder
    {
        #region Fields

        private LineStringBuilder lineStringBuilder { get; }
        private IRoomMapBuilder roomMapBuilder { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the LegacySceneFrameBuilder class.
        /// </summary>
        /// <param name="lineStringBuilder">A builder to use for the string layout.</param>
        /// <param name="roomMapBuilder">A builder to use for room maps.</param>
        public LegacySceneFrameBuilder(LineStringBuilder lineStringBuilder, IRoomMapBuilder roomMapBuilder)
        {
            this.lineStringBuilder = lineStringBuilder;
            this.roomMapBuilder = roomMapBuilder;
        }

        #endregion

        #region Implementation of ISceneFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="room">Specify the Room.</param>
        /// <param name="viewPoint">Specify the viewpoint from the room.</param>
        /// <param name="player">Specify the player.</param>
        /// <param name="message">Any additional message.</param>
        /// <param name="contextualCommands">The contextual commands to display.</param>
        /// <param name="keyType">The type of key to use.</param>
        /// <param name="width">The width of the frame.</param>
        /// <param name="height">The height of the frame.</param>
        public IFrame Build(Room room, ViewPoint viewPoint, PlayableCharacter player, string message, CommandHelp[] contextualCommands, KeyType keyType, int width, int height)
        {
            var scene = new StringBuilder();
            var whitespace = lineStringBuilder.BuildWrappedPadded(string.Empty, width, false);
            var divider = lineStringBuilder.BuildHorizontalDivider(width);
            scene.Append(divider);
            scene.Append(lineStringBuilder.BuildWrappedPadded($"LOCATION: {room.Identifier}", width, false));
            scene.Append(divider);
            scene.Append(whitespace);
            scene.Append(lineStringBuilder.BuildWrappedPadded(room.Description.GetDescription().EnsureFinishedSentence(), width, false));
            scene.Append(whitespace);

            if (room.Items.Any())
                scene.Append(lineStringBuilder.BuildWrappedPadded(room.Examine().Description.EnsureFinishedSentence(), width, false));
            else
                scene.Append(lineStringBuilder.BuildWrappedPadded("There are no items in this area.", width, false));

            var visibleCharacters = room.Characters.Where(c => c.IsPlayerVisible && c.IsAlive).ToArray<Character>();

            if (visibleCharacters.Length > 0)
            {
                if (visibleCharacters.Length == 1)
                {
                    scene.Append(lineStringBuilder.BuildWrappedPadded(visibleCharacters[0].Identifier + " is in this area.", width, false));
                }
                else
                {
                    var characters = string.Empty;

                    foreach (var character in visibleCharacters)
                        characters += character.Identifier + ", ";

                    characters = characters.Remove(characters.Length - 2);
                    scene.Append(lineStringBuilder.BuildWrappedPadded(characters.Substring(0, characters.LastIndexOf(",", StringComparison.Ordinal)) + " and " + characters.Substring(characters.LastIndexOf(",", StringComparison.Ordinal) + 2) + " are in the " + room.Identifier + ".", width, false));
                }
            }

            scene.Append(whitespace);
            scene.Append(divider);

            if (roomMapBuilder != null)
            {
                scene.Append(lineStringBuilder.BuildWrappedPadded("AREA:", width, false));
                scene.Append(whitespace);
                scene.Append(roomMapBuilder.BuildRoomMap(lineStringBuilder, room, viewPoint, keyType, width));
                scene.Append(divider);
            }

            if (contextualCommands?.Any() ?? false)
            {
                scene.Append(lineStringBuilder.BuildWrappedPadded("COMMANDS:", width, false));
                scene.Append(whitespace);

                foreach (var contextualCommand in contextualCommands)
                    scene.Append(lineStringBuilder.BuildWrappedPadded($"{contextualCommand.Command}: {contextualCommand.Description}", width, false));

                scene.Append(whitespace);
            }

            var wrappedMessage = lineStringBuilder.BuildWrappedPadded(message.EnsureFinishedSentence(), width, false);
            var linesAfterWhitespace = 6 + wrappedMessage.LineCount();
            var linesInString = scene.ToString().LineCount();

            scene.Append(lineStringBuilder.BuildPaddedArea(width, height - linesInString - linesAfterWhitespace));
            scene.Append(divider);
            scene.Append(lineStringBuilder.BuildWrappedPadded("INVENTORY: " + StringUtilities.ConstructExaminablesAsSentence(player.Items.Cast<IExaminable>().ToArray()), width, false));
            scene.Append(divider);
            var yPositionOfCursor = scene.ToString().LineCount();
            scene.Append(lineStringBuilder.BuildWrappedPadded("WHAT DO YOU DO? ", width, false));
            scene.Append(divider);
            scene.Append(wrappedMessage);
            scene.Append(divider.Replace(lineStringBuilder.LineTerminator, string.Empty));

            return new TextFrame(scene.ToString(), 18, yPositionOfCursor);
        }

        #endregion
    }
}
