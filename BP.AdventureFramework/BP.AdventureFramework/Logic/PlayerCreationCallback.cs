using BP.AdventureFramework.Assets.Characters;

namespace BP.AdventureFramework.Logic
{
    /// <summary>
    /// Represents a callback for Player creation.
    /// </summary>
    /// <returns>A generated Player.</returns>
    public delegate PlayableCharacter PlayerCreationCallback();
}