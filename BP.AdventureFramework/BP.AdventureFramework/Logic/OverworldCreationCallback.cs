using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Locations;

namespace BP.AdventureFramework.Logic
{
    /// <summary>
    /// Represents a callback for Overworld creation.
    /// </summary>
    /// <param name="pC">The playable character that will appear in the Overworld.</param>
    /// <returns>A generated Overworld.</returns>
    public delegate Overworld OverworldCreationCallback(PlayableCharacter pC);
}