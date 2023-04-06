using BP.AdventureFramework.Interaction;
using BP.AdventureFramework.Locations;

namespace BP.AdventureFramework.Structure
{
    /// <summary>
    /// Represents a callback for Overworld generation.
    /// </summary>
    /// <param name="pC">The playable character that will appear in the Overworld.</param>
    /// <returns>A generated Overworld.</returns>
    public delegate Overworld OverworldGeneration(PlayableCharacter pC);
}