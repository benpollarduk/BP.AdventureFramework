using BP.AdventureFramework.GameAssets.Characters;

namespace BP.AdventureFramework.GameStructure
{
    /// <summary>
    /// Represents a callback for Player creation.
    /// </summary>
    /// <returns>A generated Player.</returns>
    public delegate PlayableCharacter PlayerCreationCallback();
}