using System.Collections.Generic;
using System.Text;

namespace BP.AdventureFramework.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of help frames.
    /// </summary>
    public class HelpFrameBuilder : IHelpFrameBuilder
    {
        #region Implementation of IHelpFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
        /// <param name="commands">The command dictionary.</param>
        /// <param name="frameDrawer">Specify the frame drawer.</param>
        /// <param name="width">The width of the frame.</param>
        /// <param name="height">The height of the frame.</param>
        public Frame Build(string title, string description, Dictionary<string, string> commands, FrameDrawer frameDrawer, int width, int height)
        {
            var builder = new StringBuilder();
            builder.Append(frameDrawer.ConstructDivider(width));
            builder.Append(frameDrawer.ConstructCentralisedString(title, width));
            builder.Append(frameDrawer.ConstructDivider(width));
            builder.Append(frameDrawer.ConstructCentralisedString(description, width));
            builder.Append(frameDrawer.ConstructDivider(width));
            builder.Append(frameDrawer.ConstructWrappedPaddedString("GENERAL COMMANDS", width, false));
            builder.Append(frameDrawer.ConstructWrappedPaddedString(string.Empty, width, false));

            foreach (var key in commands.Keys)
            {
                if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(commands[key]))
                    builder.Append(frameDrawer.ConstructWrappedPaddedString($"{key}{frameDrawer.ConstructWhitespaceString(30 - key.Length)}- {commands[key]}", width, false));
                else if (!string.IsNullOrEmpty(key) && string.IsNullOrEmpty(commands[key]))
                    builder.Append(frameDrawer.ConstructWrappedPaddedString(string.Empty, width));
                else
                    builder.Append(frameDrawer.ConstructWrappedPaddedString(string.Empty, width));
            }

            builder.Append(frameDrawer.ConstructPaddedArea(width, height - (frameDrawer.DetermineLinesInString(builder.ToString()) + 7)));
            builder.Append(frameDrawer.ConstructWrappedPaddedString("Press Enter to return to the game", width, true));
            builder.Append(frameDrawer.ConstructPaddedArea(width, 4));
            var divider = frameDrawer.ConstructDivider(width);
            builder.Append(divider.Remove(divider.Length - 1));

            return new Frame(builder.ToString(), 0, 0) { AcceptsInput = false, ShowCursor = false };
        }

        #endregion
    }
}
