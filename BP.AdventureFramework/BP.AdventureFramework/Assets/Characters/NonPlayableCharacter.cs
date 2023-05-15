using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Conversations;

namespace BP.AdventureFramework.Assets.Characters
{
    /// <summary>
    /// Represents a non-playable character.
    /// </summary>
    public sealed class NonPlayableCharacter : Character, IConverser
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the NonPlayableCharacter class.
        /// </summary>
        /// <param name="identifier">This NonPlayableCharacter's identifier.</param>
        /// <param name="description">The description of this NonPlayableCharacter.</param>
        /// <param name="conversation">The conversation.</param>
        public NonPlayableCharacter(string identifier, string description, Conversation conversation = null) : this(new Identifier(identifier), new Description(description), conversation)
        {
        }

        /// <summary>
        /// Initializes a new instance of the NonPlayableCharacter class.
        /// </summary>
        /// <param name="identifier">This NonPlayableCharacter's identifier.</param>
        /// <param name="description">The description of this NonPlayableCharacter.</param>
        /// <param name="conversation">The conversation.</param>
        public NonPlayableCharacter(Identifier identifier, Description description, Conversation conversation = null) 
        {
            Identifier = identifier;
            Description = description;
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

        #region Implementation of IConverser

        /// <summary>
        /// Get or set the conversation.
        /// </summary>
        public Conversation Conversation { get; set; }

        #endregion
    }
}