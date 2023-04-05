using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using AdventureFramework.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace AdventureFramework.Interaction
{
    /// <summary>
    /// Represents an in-game conversation with a character
    /// </summary>
    public class Conversation : XMLSerializableObject
    {
        #region Propeties

        /// <summary>
        /// Get the lines of this conversation
        /// </summary>
        public ConversationElement[] Lines
        {
            get { return this.lines.ToArray<ConversationElement>(); }
            protected set { this.lines = new List<ConversationElement>(value); }
        }

        /// <summary>
        /// Get or set the lines of the convesation
        /// </summary>
        protected List<ConversationElement> lines = new List<ConversationElement>();

        /// <summary>
        /// Get the current line of this conversation
        /// </summary>
        public Int32 CurrentLine
        {
            get { return this.currentLine; }
            protected set { this.currentLine = value; }
        }

        /// <summary>
        /// Get or set the current line
        /// </summary>
        protected Int32 currentLine = 0;

        /// <summary>
        /// Get if this has some remaining lines
        /// </summary>
        public Boolean HasSomeRemainingLines
        {
            get { return this.currentLine < (this.Lines.Length); }
        }

        /// <summary>
        /// Get or set if the last element of the conversation should be repeated
        /// </summary>
        public Boolean RepeatLastElement
        {
            get { return this.repeatLastElement; }
            set { this.repeatLastElement = value; }
        }

        /// <summary>
        /// Get or set if the last element of the conversation should be repeated
        /// </summary>
        private Boolean repeatLastElement = false;
        
        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the Conversation class
        /// </summary>
        public Conversation()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Conversation class
        /// </summary>
        /// <param name="lines">The lines to add in this conversation</param>
        public Conversation(params String[] lines)
        {
            // add all lines
            for (Int32 index = 0; index < lines.Length; index++)
            {
                // create new
                this.lines.Add(new ConversationElement(lines[index]));
            }
        }

        /// <summary>
        /// Initializes a new instance of the Conversation class
        /// </summary>
        /// <param name="lines">The lines to add in this conversation</param>
        public Conversation(params ConversationElement[] lines)
        {
            // add all lines
            this.lines.AddRange(lines);
        }

        /// <summary>
        /// Reset this conversation
        /// </summary>
        public virtual void Reset()
        {
            // reset line
            this.currentLine = 0;
        }

        /// <summary>
        /// Get the next line of the conversation
        /// </summary>
        /// <returns>The next line of the conversation</returns>
        public virtual String NextLine()
        {
            // if some remaining lines
            if (this.HasSomeRemainingLines)
            {
                // get line
                ConversationElement element = this.Lines[this.CurrentLine];

                // if an action
                if (element.Action != null)
                {
                    // do action
                    element.Action();
                }

                // set current line
                this.currentLine++;

                // return line
                return element.Line;
            }
            else
            {
                // if repeating last element and some elements
                if ((this.RepeatLastElement) &&
                    (this.Lines.Length > 0))
                {
                    // return last element
                    ConversationElement element = this.Lines[this.Lines.Length - 1];

                    // if an action
                    if (element.Action != null)
                    {
                        // do action
                        element.Action();
                    }

                    // return line
                    return element.Line;
                }
                else
                {
                    // nothing
                    return String.Empty;
                }
            }
        }

        /// <summary>
        /// Add a line to this conversation
        /// </summary>
        /// <param name="line">The line to add to this conversation</param>
        public void AddLine(String line)
        {
            // add a line
            this.lines.Add(new ConversationElement(line));
        }

        /// <summary>
        /// Add a line to this conversation
        /// </summary>
        /// <param name="line">The line to add to this conversation</param>
        public void AddLine(ConversationElement line)
        {
            // add a line
            this.lines.Add(line);
        }

        /// <summary>
        /// Truncate this conversation at the current line
        /// </summary>
        public void Truncate()
        {
            // itterate all lines
            for (Int32 index = 0; index < this.CurrentLine; index++)
            {
                // remove the line
                this.lines.RemoveAt(index);
            }
        }

        #region XMLSerialization

        /// <summary>
        /// Handle writing of Xml for this Conversation
        /// </summary>
        /// <param name="writer">The XmlWriter to write Xml with</param>
        protected override void OnWriteXml(System.Xml.XmlWriter writer)
        {
            // write start element
            writer.WriteStartElement(this.GetType().Name);
            
            // write the current line
            writer.WriteAttributeString("CurrentLine", this.CurrentLine.ToString());

            // write if repeating last element
            writer.WriteAttributeString("RepeatLastElement", this.RepeatLastElement.ToString());

            // write start
            writer.WriteStartElement("Lines");

            // itterate lines
            foreach (ConversationElement element in this.Lines)
            {
                // write
                element.WriteXml(writer);
            }

            // write end
            writer.WriteEndElement();

            // write end element
            writer.WriteEndElement();
        }

        /// <summary>
        /// Handle reading of Xml for this Conversation
        /// </summary>
        /// <param name="node">The node to read Xml from</param>
        protected override void OnReadXmlNode(System.Xml.XmlNode node)
        {
            // read current line
            this.currentLine = Int32.Parse(XMLSerializableObject.GetAttribute(node, "CurrentLine").Value);

            // read if repeating last element
            this.RepeatLastElement = Boolean.Parse(XMLSerializableObject.GetAttribute(node, "RepeatLastElement").Value);

            // get node
            XmlNode linesNode = XMLSerializableObject.GetNode(node, "Lines");

            // itterate all child nodes
            for (Int32 index = 0; index < linesNode.ChildNodes.Count; index++)
            {
                // if not big enough
                if (this.Lines.Length <= index)
                {
                    // add new element
                    this.lines.Add(new ConversationElement());
                }

                // read
                this.Lines[index].ReadXmlNode(linesNode.ChildNodes[index]);
            }
        }

        #endregion

        #endregion
    }
}
