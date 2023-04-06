using BP.AdventureFramework.Characters;

namespace BP.AdventureFramework.GameStructure
{
    /// <summary>
    /// Represents a callback for Player generation.
    /// </summary>
    /// <returns>A generated Player.</returns>
    public delegate PlayableCharacter PlayerGeneration();
}