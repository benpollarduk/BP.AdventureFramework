using System;
using System.Linq;
using System.Text;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Interpretation;
using BP.AdventureFramework.Rendering.LayoutBuilders;
using BP.AdventureFramework.Rendering.MapBuilders;

namespace BP.AdventureFramework.Rendering.FrameBuilders.Legacy
{
    /// <summary>
    /// Provides a builder of legacy scene frames.
    /// </summary>
    public sealed class LegacySceneFrameBuilder : ISceneFrameBuilder
    {
        #region Properties

        /// <summary>
        /// Get the string layout builder.
        /// </summary>
        private IStringLayoutBuilder StringLayoutBuilder { get; }

        /// <summary>
        /// Get the room map builder.
        /// </summary>
        private IRoomMapBuilder RoomMapBuilder { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the LegacySceneFrameBuilder class.
        /// </summary>
        /// <param name="stringLayoutBuilder">A builder to use for the string layout.</param>
        /// <param name="roomMapBuilder">A builder to use for room maps.</param>
        public LegacySceneFrameBuilder(IStringLayoutBuilder stringLayoutBuilder, IRoomMapBuilder roomMapBuilder)
        {
            StringLayoutBuilder = stringLayoutBuilder;
            RoomMapBuilder = roomMapBuilder;
        }

        #endregion

        #region Implementation of ISceneFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="room">Specify the Room.</param>
        /// <param name="player">Specify the player.</param>
        /// <param name="message">Any additional message.</param>
        /// <param name="displayCommands">Specify if commands should be displayed as part of the scene.</param>
        /// <param name="keyType">The type of key to use.</param>
        /// <param name="width">The width of the frame.</param>
        /// <param name="height">The height of the frame.</param>
        public Frame Build(Room room, PlayableCharacter player, string message, bool displayCommands, KeyType keyType, int width, int height)
        {
            var scene = new StringBuilder();
            scene.Append(StringLayoutBuilder.BuildHorizontalDivider(width));
            scene.Append(StringLayoutBuilder.BuildWrappedPadded($"LOCATION: {room.Identifier}", width, false));
            scene.Append(StringLayoutBuilder.BuildHorizontalDivider(width));
            scene.Append(StringLayoutBuilder.BuildWrappedPadded(string.Empty, width, false));
            scene.Append(StringLayoutBuilder.BuildWrappedPadded(room.Description.GetDescription().EnsureFinishedSentence(), width, false));
            scene.Append(StringLayoutBuilder.BuildWrappedPadded(string.Empty, width, false));

            if (room.Items.Any())
                scene.Append(StringLayoutBuilder.BuildWrappedPadded(room.Examime().Desciption.EnsureFinishedSentence(), width, false));
            else
                scene.Append(StringLayoutBuilder.BuildWrappedPadded("There are no items in this area.", width, false));

            var visibleCharacters = room.Characters.Where(c => c.IsPlayerVisible && c.IsAlive).ToArray<Character>();

            if (visibleCharacters.Length > 0)
            {
                if (visibleCharacters.Length == 1)
                {
                    scene.Append(StringLayoutBuilder.BuildWrappedPadded(visibleCharacters[0].Identifier + " is in this area.", width, false));
                }
                else
                {
                    var characters = string.Empty;

                    foreach (var character in visibleCharacters)
                        characters += character.Identifier + ", ";

                    characters = characters.Remove(characters.Length - 2);
                    scene.Append(StringLayoutBuilder.BuildWrappedPadded(characters.Substring(0, characters.LastIndexOf(",", StringComparison.Ordinal)) + " and " + characters.Substring(characters.LastIndexOf(",", StringComparison.Ordinal) + 2) + " are in the " + room.Identifier + ".", width, false));
                }
            }

            scene.Append(StringLayoutBuilder.BuildWrappedPadded(string.Empty, width, false));
            scene.Append(StringLayoutBuilder.BuildHorizontalDivider(width));

            if (RoomMapBuilder != null)
            {
                scene.Append(StringLayoutBuilder.BuildWrappedPadded("AREA:", width, false));
                scene.Append(StringLayoutBuilder.BuildWrappedPadded(string.Empty, width, false));
                scene.Append(RoomMapBuilder.BuildRoomMap(room, keyType, width));
                scene.Append(StringLayoutBuilder.BuildHorizontalDivider(width));
            }

            if (displayCommands)
            {
                scene.Append(StringLayoutBuilder.BuildWrappedPadded("COMMANDS:", width, false));
                scene.Append(StringLayoutBuilder.BuildWrappedPadded(string.Empty, width, false));
                scene.Append(StringLayoutBuilder.BuildWrappedPadded("MOVEMENT:", width, false));
                scene.Append(StringLayoutBuilder.BuildWrappedPadded($"{GameCommandInterpreter.NorthShort}: {GameCommandInterpreter.North}, {GameCommandInterpreter.SouthShort}: {GameCommandInterpreter.South}, {GameCommandInterpreter.EastShort}: {GameCommandInterpreter.East}, {GameCommandInterpreter.WestShort}: {GameCommandInterpreter.West}", width, false));
                scene.Append(StringLayoutBuilder.BuildWrappedPadded(string.Empty, width, false));

                var usedLinesSoFar = scene.ToString().LineCount() + 14 + StringLayoutBuilder.BuildWrappedPadded(message, width, false).LineCount();

                if (height - usedLinesSoFar >= 0)
                {
                    scene.Append(StringLayoutBuilder.BuildWrappedPadded("INTERACTION:", width, false));

                    // TODO: this legacy scene builder depends on the GameCommandInterpreter and is not backwards compatible with command lists from other interpreters...
                    CommandHelp help;

                    if (player.Items.Any())
                    {
                        help = GameCommandInterpreter.DefaultSupportedCommands.FirstOrDefault(x => x.Command.Contains(GameCommandInterpreter.Drop));
                        
                        if (help != null)
                            scene.Append(StringLayoutBuilder.BuildWrappedPadded($"{help.Command}: {help.Description}", width, false));
                    }

                    help = GameCommandInterpreter.DefaultSupportedCommands.FirstOrDefault(x => x.Command.Contains(GameCommandInterpreter.Drop));

                    if (help != null)
                        scene.Append(StringLayoutBuilder.BuildWrappedPadded($"{help.Command}: {help.Description}", width, false));

                    if (room.Items.Any())
                    {
                        help = GameCommandInterpreter.DefaultSupportedCommands.FirstOrDefault(x => x.Command.Contains(GameCommandInterpreter.Take));

                        if (help != null)
                            scene.Append(StringLayoutBuilder.BuildWrappedPadded($"{help.Command}: {help.Description}", width, false));
                    }

                    if (room.Characters.Any())
                    {
                        help = GameCommandInterpreter.DefaultSupportedCommands.FirstOrDefault(x => x.Command.Contains(GameCommandInterpreter.Talk));

                        if (help != null)
                            scene.Append(StringLayoutBuilder.BuildWrappedPadded($"{help.Command}: {help.Description}", width, false));
                    }

                    if (room.Items.Any() || player.Items.Any())
                    {
                        help = GameCommandInterpreter.DefaultSupportedCommands.FirstOrDefault(x => x.Command.Contains(GameCommandInterpreter.Use));

                        if (help != null)
                            scene.Append(StringLayoutBuilder.BuildWrappedPadded($"{help.Command}: {help.Description}", width, false));

                        help = GameCommandInterpreter.DefaultSupportedCommands.FirstOrDefault(x => x.Command.Contains(GameCommandInterpreter.Use) && x.Command.Contains(GameCommandInterpreter.On.ToLower()));

                        if (help != null)
                            scene.Append(StringLayoutBuilder.BuildWrappedPadded($"{help.Command}: {help.Description}", width, false));
                    }

                    scene.Append(StringLayoutBuilder.BuildWrappedPadded(string.Empty, width, false));
                }
            }

            var wrappedMessage = StringLayoutBuilder.BuildWrappedPadded(message.EnsureFinishedSentence(), width, false);
            var linesAfterWhitespace = 6 + wrappedMessage.LineCount();
            var linesInString = scene.ToString().LineCount();

            scene.Append(StringLayoutBuilder.BuildPaddedArea(width, height - linesInString - linesAfterWhitespace));
            scene.Append(StringLayoutBuilder.BuildHorizontalDivider(width));
            scene.Append(StringLayoutBuilder.BuildWrappedPadded("INVENTORY: " + player.GetItemsAsList(), width, false));
            scene.Append(StringLayoutBuilder.BuildHorizontalDivider(width));
            var yPositionOfCursor = scene.ToString().LineCount();
            scene.Append(StringLayoutBuilder.BuildWrappedPadded("WHAT DO YOU DO? ", width, false));
            scene.Append(StringLayoutBuilder.BuildHorizontalDivider(width));
            scene.Append(wrappedMessage);
            var bottomdivider = StringLayoutBuilder.BuildHorizontalDivider(width);
            scene.Append(bottomdivider.Replace(StringLayoutBuilder.LineTerminator, string.Empty));

            return new Frame(scene.ToString(), 18, yPositionOfCursor);
        }

        #endregion
    }
}
