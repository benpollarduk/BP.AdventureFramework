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
        /// <param name="mapDrawer">Specify a drawer for constructing room maps.</param>
        /// <param name="frameDrawer">Specify the frame drawer.</param>
        /// <param name="width">The width of the frame.</param>
        /// <param name="height">The height of the frame.</param>
        Frame Build(Room room, PlayableCharacter player, string message, MapDrawer mapDrawer, FrameDrawer frameDrawer, int width, int height);
    }
}
