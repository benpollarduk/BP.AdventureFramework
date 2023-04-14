using BP.AdventureFramework.Assets.Interaction;

namespace BP.AdventureFramework.Assets.Characters
{
    /// <summary>
    /// Represents a non-playable character.
    /// </summary>
    public class NonPlayableCharacter : Character, ITalkative
    {
        #region Properties

        /// <summary>
        /// Get or set the conversation.
        /// </summary>
        public Conversation Conversation { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the NonPlayableCharacter class.
        /// </summary>
        /// <param name="identifier">This NonPlayableCharacter's identifier.</param>
        /// <param name="description">The description of this NonPlayableCharacter.</param>
        public NonPlayableCharacter(Identifier identifier, Description description)
        {
            Identifier = identifier;
            Description = description;
        }

        /// <summary>
        /// Initializes a new instance of the NonPlayableCharacter class.
        /// </summary>
        /// <param name="identifier">This NonPlayableCharacter's identifier.</param>
        /// <param name="description">The description of this NonPlayableCharacter.</param>
        /// <param name="conversation">The conversation.</param>
        public NonPlayableCharacter(Identifier identifier, Description description, Conversation conversation) : this(identifier, description)   
        {
            Conversation = conversation;
        }

        /// <summary>
        /// Initializes a new instance of the NonPlayableCharacter class.
        /// </summary>
        /// <param name="identifier">This NonPlayableCharacter's identifier.</param>
        /// <param name="description">The description of this NonPlayableCharacter.</param>
        /// <param name="conversation">The conversation.</param>
        /// <param name="isAlive">Set if this NonPlayableCharacter is alive.</param>
        /// <param name="interaction">Set this NonPlayableCharacter's interaction.</param>
        public NonPlayableCharacter(Identifier identifier, Description description, Conversation conversation, bool isAlive, InteractionCallback interaction) : this(identifier, description, conversation)
        {
            IsAlive = isAlive;
            Interaction = interaction;
        }

        /// <summary>
        /// Initializes a new instance of the NonPlayableCharacter class.
        /// </summary>
        /// <param name="identifier">This NonPlayableCharacter's identifier.</param>
        /// <param name="description">The description of this NonPlayableCharacter.</param>
        /// <param name="conversation">The conversation.</param>
        /// <param name="isAlive">Set if this NonPlayableCharacter is alive.</param>
        /// <param name="interaction">Set this NonPlayableCharacter's interaction.</param>
        /// <param name="examination">Set this NonPlayableCharacter's examination.</param>
        public NonPlayableCharacter(Identifier identifier, Description description, Conversation conversation, bool isAlive, InteractionCallback interaction, ExaminationCallback examination) : this(identifier, description, conversation, isAlive, interaction)
        {
            Examination = examination;
        }

        #endregion

        #region ITalkative Members

        /// <summary>
        /// Talk to this object.
        /// </summary>
        /// <returns>A string representing the conversation.</returns>
        public string Talk()
        {
            if (Conversation == null)
                return Identifier + " has nothing to say";

            if (Conversation.HasSomeRemainingLines || Conversation.RepeatLastElement)
                return Identifier + ": \"" + Conversation.NextLine() + "\"";

            return Identifier + " has nothing else to say";
        }

        #endregion
    }
}