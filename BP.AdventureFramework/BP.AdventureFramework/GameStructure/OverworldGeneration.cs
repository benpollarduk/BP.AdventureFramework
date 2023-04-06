using BP.AdventureFramework.Characters;
using BP.AdventureFramework.Locations;

namespace BP.AdventureFramework.GameStructure
{
    /// <summary>
    /// Represents a callback for Overworld generation.
    /// </summary>
    /// <param name="pC">The playable character that will appear in the Overworld.</param>
    /// <returns>A generated Overworld.</returns>
    public delegate Overworld OverworldGeneration(PlayableCharacter pC);
}