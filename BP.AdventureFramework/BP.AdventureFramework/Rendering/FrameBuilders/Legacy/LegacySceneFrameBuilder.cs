using System;
using System.Linq;
using System.Text;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Interpretation;
using BP.AdventureFramework.Rendering.Drawers;
using BP.AdventureFramework.Rendering.MapBuilders;

namespace BP.AdventureFramework.Rendering.FrameBuilders.Legacy
{
    /// <summary>
    /// Provides a builder of legacy scene frames.
    /// </summary>
    public class LegacySceneFrameBuilder : ISceneFrameBuilder
    {
        #region Properties

        /// <summary>
        /// Get the drawer.
        /// </summary>
        public Drawer Drawer { get; }

        /// <summary>
        /// Get the room map builder.
        /// </summary>
        public IRoomMapBuilder RoomMapBuilder { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the LegacySceneFrameBuilder class.
        /// </summary>
        /// <param name="drawer">A drawer to use for the frame.</param>
        /// <param name="roomMapBuilder">A builder to use for room maps.</param>
        public LegacySceneFrameBuilder(Drawer drawer, IRoomMapBuilder roomMapBuilder)
        {
            Drawer = drawer;
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
            scene.Append(Drawer.ConstructDivider(width));
            scene.Append(Drawer.ConstructWrappedPaddedString($"LOCATION: {room.Identifier}", width));
            scene.Append(Drawer.ConstructDivider(width));
            scene.Append(Drawer.ConstructWrappedPaddedString(string.Empty, width));
            scene.Append(Drawer.ConstructWrappedPaddedString(room.Description.GetDescription().EnsureFinishedSentence(), width));
            scene.Append(Drawer.ConstructWrappedPaddedString(string.Empty, width));

            if (room.Items.Any())
                scene.Append(Drawer.ConstructWrappedPaddedString(room.Examime().Desciption.EnsureFinishedSentence(), width));
            else
                scene.Append(Drawer.ConstructWrappedPaddedString("There are no items in this area.", width));

            var visibleCharacters = room.Characters.Where(c => c.IsPlayerVisible && c.IsAlive).ToArray<Character>();

            if (visibleCharacters.Length > 0)
            {
                if (visibleCharacters.Length == 1)
                {
                    scene.Append(Drawer.ConstructWrappedPaddedString(visibleCharacters[0].Identifier + " is in this area.", width));
                }
                else
                {
                    var characters = string.Empty;

                    foreach (var character in visibleCharacters)
                        characters += character.Identifier + ", ";

                    characters = characters.Remove(characters.Length - 2);
                    scene.Append(Drawer.ConstructWrappedPaddedString(characters.Substring(0, characters.LastIndexOf(",", StringComparison.Ordinal)) + " and " + characters.Substring(characters.LastIndexOf(",", StringComparison.Ordinal) + 2) + " are in the " + room.Identifier + ".", width));
                }
            }

            scene.Append(Drawer.ConstructWrappedPaddedString(string.Empty, width));
            scene.Append(Drawer.ConstructDivider(width));

            if (RoomMapBuilder != null)
            {
                scene.Append(Drawer.ConstructWrappedPaddedString("AREA:", width));
                scene.Append(Drawer.ConstructWrappedPaddedString(string.Empty, width));
                scene.Append(RoomMapBuilder.BuildRoomMap(room, keyType, width));
                scene.Append(Drawer.ConstructDivider(width));
            }

            if (displayCommands)
            {
                scene.Append(Drawer.ConstructWrappedPaddedString("COMMANDS:", width));
                scene.Append(Drawer.ConstructWrappedPaddedString(string.Empty, width));
                scene.Append(Drawer.ConstructWrappedPaddedString("MOVEMENT:", width));
                scene.Append(Drawer.ConstructWrappedPaddedString($"{GameCommandInterpreter.NorthShort}: {GameCommandInterpreter.North}, {GameCommandInterpreter.SouthShort}: {GameCommandInterpreter.South}, {GameCommandInterpreter.EastShort}: {GameCommandInterpreter.East}, {GameCommandInterpreter.WestShort}: {GameCommandInterpreter.West}", width));
                scene.Append(Drawer.ConstructWrappedPaddedString(string.Empty, width));

                var usedLinesSoFar = scene.ToString().LineCount() + 14 + Drawer.ConstructWrappedPaddedString(message, width).LineCount();

                if (height - usedLinesSoFar >= 0)
                {
                    scene.Append(Drawer.ConstructWrappedPaddedString("INTERACTION:", width));

                    // TODO: this legacy scene builder depends on the GameCommandInterpreter and is not backwards compatible with command lists from other interpreters...
                    CommandHelp help;

                    if (player.Items.Any())
                    {
                        help = GameCommandInterpreter.DefaultSupportedCommands.FirstOrDefault(x => x.Command.Contains(GameCommandInterpreter.Drop));
                        
                        if (help != null)
                            scene.Append(Drawer.ConstructWrappedPaddedString($"{help.Command}: {help.Description}", width));
                    }

                    help = GameCommandInterpreter.DefaultSupportedCommands.FirstOrDefault(x => x.Command.Contains(GameCommandInterpreter.Drop));

                    if (help != null)
                        scene.Append(Drawer.ConstructWrappedPaddedString($"{help.Command}: {help.Description}", width));

                    if (room.Items.Any())
                    {
                        help = GameCommandInterpreter.DefaultSupportedCommands.FirstOrDefault(x => x.Command.Contains(GameCommandInterpreter.Take));

                        if (help != null)
                            scene.Append(Drawer.ConstructWrappedPaddedString($"{help.Command}: {help.Description}", width));
                    }

                    if (room.Characters.Any())
                    {
                        help = GameCommandInterpreter.DefaultSupportedCommands.FirstOrDefault(x => x.Command.Contains(GameCommandInterpreter.Talk));

                        if (help != null)
                            scene.Append(Drawer.ConstructWrappedPaddedString($"{help.Command}: {help.Description}", width));
                    }

                    if (room.Items.Any() || player.Items.Any())
                    {
                        help = GameCommandInterpreter.DefaultSupportedCommands.FirstOrDefault(x => x.Command.Contains(GameCommandInterpreter.Use));

                        if (help != null)
                            scene.Append(Drawer.ConstructWrappedPaddedString($"{help.Command}: {help.Description}", width));

                        help = GameCommandInterpreter.DefaultSupportedCommands.FirstOrDefault(x => x.Command.Contains(GameCommandInterpreter.Use) && x.Command.Contains(GameCommandInterpreter.On.ToLower()));

                        if (help != null)
                            scene.Append(Drawer.ConstructWrappedPaddedString($"{help.Command}: {help.Description}", width));
                    }

                    scene.Append(Drawer.ConstructWrappedPaddedString(string.Empty, width));
                }
            }

            var wrappedMessage = Drawer.ConstructWrappedPaddedString(message.EnsureFinishedSentence(), width);
            var linesAfterWhitespace = 7 + wrappedMessage.LineCount();
            var linesInString = scene.ToString().LineCount();

            scene.Append(Drawer.ConstructPaddedArea(Drawer.LeftBoundaryCharacter, Drawer.RightBoundaryCharacter, width, height - linesInString - linesAfterWhitespace));
            scene.Append(Drawer.ConstructDivider(width));
            scene.Append(Drawer.ConstructWrappedPaddedString("INVENTORY: " + player.GetItemsAsList(), width));
            scene.Append(Drawer.ConstructDivider(width));
            var yPositionOfCursor = scene.ToString().LineCount() - 1;
            scene.Append(Drawer.ConstructWrappedPaddedString("WHAT DO YOU DO? ", width));
            scene.Append(Drawer.ConstructDivider(width));
            scene.Append(wrappedMessage);
            var bottomdivider = Drawer.ConstructDivider(width);
            scene.Append(bottomdivider.Remove(bottomdivider.Length - 1));

            return new Frame(scene.ToString(), 18, yPositionOfCursor);
        }

        #endregion
    }
}
