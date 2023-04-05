using System.Collections.Generic;
using System.Linq;
using System.Xml;
using AdventureFramework.IO;

namespace BP.AdventureFramework.Interaction
{
    /// <summary>
    /// Represents an in-game conversation with a character.
    /// </summary>
    public class Conversation : XMLSerializableObject
    {
        #region Properties

        /// <summary>
        /// Get the lines of this conversation.
        /// </summary>
        public List<ConversationElement> Lines { get; protected set; }

        /// <summary>
        /// Get the current line of this conversation.
        /// </summary>
        public int CurrentLine { get; protected set; }

        /// <summary>
        /// Get if this has some remaining lines.
        /// </summary>
        public bool HasSomeRemainingLines
        {
            get { return CurrentLine < Lines.Count; }
        }

        /// <summary>
        /// Get or set if the last element of the conversation should be repeated.
        /// </summary>
        public bool RepeatLastElement { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the Conversation class.
        /// </summary>
        public Conversation()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Conversation class.
        /// </summary>
        /// <param name="lines">The lines to add in this conversation.</param>
        public Conversation(params string[] lines)
        {
            foreach (var t in lines)
                Lines.Add(new ConversationElement(t));
        }

        /// <summary>
        /// Initializes a new instance of the Conversation class.
        /// </summary>
        /// <param name="lines">The lines to add in this conversation.</param>
        public Conversation(params ConversationElement[] lines)
        {
            Lines.AddRange(lines);
        }

        /// <summary>
        /// Reset this conversation.
        /// </summary>
        public virtual void Reset()
        {
            CurrentLine = 0;
        }

        /// <summary>
        /// Get the next line of the conversation.
        /// </summary>
        /// <returns>The next line of the conversation.</returns>
        public virtual string NextLine()
        {
            if (HasSomeRemainingLines)
            {
                var e = Lines[CurrentLine];
                e.Action?.Invoke();
                CurrentLine++;
                return e.Line;
            }

            if (!RepeatLastElement || !Lines.Any())
                return string.Empty;

            var element = Lines[Lines.Count - 1];
            element.Action?.Invoke();
            return element.Line;
        }

        /// <summary>
        /// Add a line to this conversation.
        /// </summary>
        /// <param name="line">The line to add to this conversation.</param>
        public void AddLine(string line)
        {
            Lines.Add(new ConversationElement(line));
        }

        /// <summary>
        /// Add a line to this conversation.
        /// </summary>
        /// <param name="line">The line to add to this conversation.</param>
        public void AddLine(ConversationElement line)
        {
            Lines.Add(line);
        }

        /// <summary>
        /// Truncate this conversation at the current line.
        /// </summary>
        public void Truncate()
        {
            for (var index = 0; index < CurrentLine; index++)
                Lines.RemoveAt(index);
        }

        #region XMLSerialization

        /// <summary>
        /// Handle writing of Xml for this Conversation.
        /// </summary>
        /// <param name="writer">The XmlWriter to write Xml with.</param>
        protected override void OnWriteXml(XmlWriter writer)
        {
            writer.WriteStartElement(GetType().Name);
            writer.WriteAttributeString("CurrentLine", CurrentLine.ToString());
            writer.WriteAttributeString("RepeatLastElement", RepeatLastElement.ToString());

            writer.WriteStartElement("Lines");

            foreach (var element in Lines)
                element.WriteXml(writer);

            writer.WriteEndElement();
            writer.WriteEndElement();
        }

        /// <summary>
        /// Handle reading of Xml for this Conversation.
        /// </summary>
        /// <param name="node">The node to read Xml from.</param>
        protected override void OnReadXmlNode(XmlNode node)
        {
            CurrentLine = int.Parse(GetAttribute(node, "CurrentLine").Value);
            RepeatLastElement = bool.Parse(GetAttribute(node, "RepeatLastElement").Value);

            var linesNode = GetNode(node, "Lines");

            for (var index = 0; index < linesNode.ChildNodes.Count; index++)
            {
                if (Lines.Count <= index)
                    Lines.Add(new ConversationElement());

                Lines[index].ReadXmlNode(linesNode.ChildNodes[index]);
            }
        }

        #endregion

        #endregion
    }
}