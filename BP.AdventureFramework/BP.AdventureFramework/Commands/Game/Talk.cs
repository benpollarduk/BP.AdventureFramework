using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Interaction;

namespace BP.AdventureFramework.Commands.Game
{
    /// <summary>
    /// Represents the Talk command.
    /// </summary>
    internal class Talk : ICommand
    {
        #region Properties

        /// <summary>
        /// Get the target.
        /// </summary>
        public ITalkative Target { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Talk command.
        /// </summary>
        /// <param name="target">The target.</param>
        public Talk(ITalkative target)
        {
            Target = target;
        }

        #endregion

        #region Implementation of ICommand

        /// <summary>
        /// Invoke the command.
        /// </summary>
        /// <returns>The reaction.</returns>
        public Reaction Invoke()
        {
            if (Target == null)
                return new Reaction(ReactionResult.None, "No-one is around to talk to.");

            if (Target is Character character && !character.IsAlive)
            {
                return new Reaction(ReactionResult.None, $"{character.Identifier.Name} is dead.");
            }

            return new Reaction(ReactionResult.Reacted, Target.Talk());
        }

        #endregion
    }
}
