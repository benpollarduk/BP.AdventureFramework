using BP.AdventureFramework.Interaction;

namespace BP.AdventureFramework.Characters
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
        /// <param name="name">The name of this NonPlayableCharacter.</param>
        /// <param name="description">The description of this NonPlayableCharacter.</param>
        public NonPlayableCharacter(string name, string description)
        {
            Name = name;
            Description = new Description(description);
        }

        /// <summary>
        /// Initializes a new instance of the NonPlayableCharacter class.
        /// </summary>
        /// <param name="name">The name of this NonPlayableCharacter.</param>
        /// <param name="description">The description of this NonPlayableCharacter.</param>
        /// <param name="conversation">The conversation.</param>
        public NonPlayableCharacter(string name, string description, Conversation conversation) : this(name, description)
        {
            Conversation = conversation;
        }

        /// <summary>
        /// Initializes a new instance of the NonPlayableCharacter class.
        /// </summary>
        /// <param name="name">The name of this NonPlayableCharacter.</param>
        /// <param name="description">The description of this NonPlayableCharacter.</param>
        public NonPlayableCharacter(string name, Description description)
        {
            Name = name;
            Description = description;
        }

        /// <summary>
        /// Initializes a new instance of the NonPlayableCharacter class.
        /// </summary>
        /// <param name="name">The name of this NonPlayableCharacter.</param>
        /// <param name="description">The description of this NonPlayableCharacter.</param>
        /// <param name="conversation">The conversation.</param>
        public NonPlayableCharacter(string name, Description description, Conversation conversation) : this(name, description)   
        {
            Conversation = conversation;
        }

        /// <summary>
        /// Initializes a new instance of the NonPlayableCharacter class.
        /// </summary>
        /// <param name="name">The name of this NonPlayableCharacter.</param>
        /// <param name="description">The description of this NonPlayableCharacter.</param>
        /// <param name="conversation">The conversation.</param>
        /// <param name="isAlive">Set if this NonPlayableCharacter is alive.</param>
        /// <param name="interaction">Set this NonPlayableCharacter's interaction.</param>
        public NonPlayableCharacter(string name, Description description, Conversation conversation, bool isAlive, InteractionCallback interaction) : this(name, description, conversation)
        {
            IsAlive = isAlive;
            Interaction = interaction;
        }

        /// <summary>
        /// Initializes a new instance of the NonPlayableCharacter class.
        /// </summary>
        /// <param name="name">The name of this NonPlayableCharacter.</param>
        /// <param name="description">The description of this NonPlayableCharacter.</param>
        /// <param name="conversation">The conversation.</param>
        /// <param name="isAlive">Set if this NonPlayableCharacter is alive.</param>
        /// <param name="interaction">Set this NonPlayableCharacter's interaction.</param>
        /// <param name="examination">Set this NonPlayableCharacter's examination.</param>
        public NonPlayableCharacter(string name, Description description, Conversation conversation, bool isAlive, InteractionCallback interaction, ExaminationCallback examination) : this(name, description, conversation, isAlive, interaction)
        {
            Examination = examination;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handle talking.
        /// </summary>
        /// <returns>A string representing the dialogue.</returns>
        protected virtual string OnTalk()
        {
            if (Conversation == null) 
                return Name + " has nothing to say";
            
            if (Conversation.HasSomeRemainingLines || Conversation.RepeatLastElement)
                return Name + ": \"" + Conversation.NextLine() + "\"";

            return Name + " has nothing else to say";

        }

        #endregion

        #region ITalkative Members

        /// <summary>
        /// Talk to this object.
        /// </summary>
        /// <returns>A string representing the conversation.</returns>
        public string Talk()
        {
            return OnTalk();
        }

        #endregion
    }
}