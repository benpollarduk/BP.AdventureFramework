using BP.AdventureFramework.Assets.Attributes;
using BP.AdventureFramework.Commands;

namespace BP.AdventureFramework.Assets
{
    /// <summary>
    /// Represents any object that is examinable.
    /// </summary>
    public interface IExaminable : IPlayerVisible
    {
        /// <summary>
        /// Get this objects identifier.
        /// </summary>
        Identifier Identifier { get; }
        /// <summary>
        /// Get or set a description of this object.
        /// </summary>
        Description Description { get; set; }
        /// <summary>
        /// Get or set this objects commands.
        /// </summary>
        CustomCommand[] Commands { get; set; }
        /// <summary>
        /// Get the attribute manager for this object.
        /// </summary>
        AttributeManager Attributes { get; }
        /// <summary>
        /// Examine this object.
        /// </summary>
        /// <returns>A ExaminationResult detailing the examination of this object.</returns>
        ExaminationResult Examine();
    }
}