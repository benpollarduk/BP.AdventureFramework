using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Locations;

namespace BP.AdventureFramework.Rendering.FrameBuilders
{
    /// <summary>
    /// Represents any objects that can build scene frames.
    /// </summary>
    public interface ISceneFrameBuilder
    {
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
        Frame Build(Room room, PlayableCharacter player, string message, bool displayCommands, KeyType keyType, int width, int height);
    }
}
