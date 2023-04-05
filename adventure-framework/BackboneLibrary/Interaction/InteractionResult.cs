using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventureFramework.IO;
using System.Xml;

namespace AdventureFramework.Interaction
{
    /// <summary>
    /// Represents a result of an interaction
    /// </summary>
    public class InteractionResult : Result
    {
        #region Properies

        /// <summary>
        /// Get the effect
        /// </summary>
        public EInteractionEffect Effect
        {
            get { return this.effect; }
            protected set { this.effect = value; }
        }

        /// <summary>
        /// Get or set the effect
        /// </summary>
        private EInteractionEffect effect;

        /// <summary>
        /// Get the item used in the interaction
        /// </summary>
        public Item Item
        {
            get { return this.item; }
            protected set { this.item = value; }
        }

        /// <summary>
        /// Get or set the item used in the interaction
        /// </summary>
        private Item item;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the InteractionResult class
        /// </summary>
        public InteractionResult()
        {
            // no effect
            this.Effect = EInteractionEffect.NoEffect;

            // set description
            this.Desciption = "There was no effect";
        }

        /// <summary>
        /// Initializes a new instance of the InteractionResult class
        /// </summary>
        /// <param name="effect">The effect of this interaction</param>
        /// <param name="item">The item used in this interaction</param>
        public InteractionResult(EInteractionEffect effect, Item item)
        {
            // set effect
            this.Effect = effect;

            // set item
            this.Item = item;

            // select effect
            switch (effect)
            {
                case (EInteractionEffect.FatalEffect):
                    {
                        // set description
                        this.Desciption = "There was a fatal effect";

                        break;
                    }
                case (EInteractionEffect.ItemMorphed):
                    {
                        // set description
                        this.Desciption = "The item morphed";

                        break;
                    }
                case (EInteractionEffect.ItemUsedUp):
                    {
                        // set description
                        this.Desciption = "The item was used up";

                        break;
                    }
                case (EInteractionEffect.NoEffect):
                    {
                        // set description
                        this.Desciption = "There was no effect";

                        break;
                    }
                case (EInteractionEffect.SelfContained):
                    {
                        // set description
                        this.Desciption = "The effect was self contained";

                        break;
                    }
                case (EInteractionEffect.TargetUsedUp):
                    {
                        // set description
                        this.Desciption = "The target was used up";

                        break;
                    }
                default: { throw new NotImplementedException(); }
            }
        }

        /// <summary>
        /// Initializes a new instance of the InteractionResult class
        /// </summary>
        /// <param name="effect">The effect of this interaction</param>
        /// <param name="item">The item used in this interaction</param>
        /// <param name="descriptionOfEffect">A description of the effect</param>
        public InteractionResult(EInteractionEffect effect, Item item, String descriptionOfEffect)
        {
            // set effect
            this.Effect = effect;

            // set item
            this.Item = item;

            // set description
            this.Desciption = descriptionOfEffect;
        }

        /// <summary>
        /// Initializes a new instance of the InteractionResult class
        /// </summary>
        /// <param name="effect">The effect of this interaction</param>
        /// <param name="descriptionOfEffect">A description of the effect</param>
        public InteractionResult(EInteractionEffect effect, String descriptionOfEffect)
        {
            // set effect
            this.Effect = effect;

            // set description
            this.Desciption = descriptionOfEffect;
        }

        #region XMLSerialization

        /// <summary>
        /// Handle writing of Xml for this InteractionResult
        /// </summary>
        /// <param name="writer">The XmlWriter to write Xml with</param>
        protected override void OnWriteXml(System.Xml.XmlWriter writer)
        {
            // write start element
            writer.WriteStartElement("InteractionResult");

            // write effect
            writer.WriteAttributeString("Effect", this.Effect.ToString());

            // write base
            base.WriteXml(writer);

            // write end element
            writer.WriteEndElement();
        }

        /// <summary>
        /// Handle reading of Xml for this InteractionResult
        /// </summary>
        /// <param name="node">The node to read Xml from</param>
        protected override void OnReadXmlNode(System.Xml.XmlNode node)
        {
            // parse effect
            this.Effect = (EInteractionEffect)Enum.Parse(typeof(EInteractionEffect), XMLSerializableObject.GetAttribute(node, "Effect").Value);

            // get base node
            XmlNode baseNode = InteractionResult.GetNode(node, "Result");

            // set description
            this.Desciption = XMLSerializableObject.GetAttribute(baseNode, "Description").Value;
        }

        #endregion

        #endregion
    }

    /// <summary>
    /// Enumeration of interaction effects
    /// </summary>
    public enum EInteractionEffect
    {
        /// <summary>
        /// No effect to the interaction on either the item or the target
        /// </summary>
        NoEffect = 0,
        /// <summary>
        /// Item was used up
        /// </summary>
        ItemUsedUp,
        /// <summary>
        /// Item morphed into another object
        /// </summary>
        ItemMorphed,
        /// <summary>
        /// A fatal effect to the interaction
        /// </summary>
        FatalEffect,
        /// <summary>
        /// The target was used up
        /// </summary>
        TargetUsedUp,
        /// <summary>
        /// Any other self contained effect
        /// </summary>
        SelfContained
    }

    /// <summary>
    /// Represents the callback for interacting with objects
    /// </summary>
    /// <param name="item">The item to interact with</param>
    /// <param name="target">The target interaction element</param>
    /// <returns>The result of the interaction</returns>
    public delegate InteractionResult InteractionCallback(Item item, IInteractWithItem target);
}
