using BP.AdventureFramework.GameAssets.Characters;
using BP.AdventureFramework.GameAssets.Locations;

namespace BP.AdventureFramework.GameStructure
{
    /// <summary>
    /// Represents a callback for Overworld creation.
    /// </summary>
    /// <param name="pC">The playable character that will appear in the Overworld.</param>
    /// <returns>A generated Overworld.</returns>
    public delegate Overworld OverworldCreationCallback(PlayableCharacter pC);
}