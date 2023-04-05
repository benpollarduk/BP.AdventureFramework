using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Runtime.Serialization;

namespace AdventureFramework.IO
{
    /// <summary>
    /// Represents any object that can serialize to and from XML
    /// </summary>
    public abstract class XMLSerializableObject : IXmlSerializable 
    {
        #region Notes

        /*
         * Based on http://www.dotnetjohn.com/articles.aspx?articleid=173
         */

        #endregion

        #region StaticProperties

        /// <summary>
        /// Get or set the encoding object for UTF8
        /// </summary>
        private static UTF8Encoding encoding = new UTF8Encoding();

        /// <summary>
        /// Get or set the doc used for XML reading
        /// </summary>
        private static XmlDocument doc = new XmlDocument();

        #endregion

        #region Methods

        /// <summary>
        /// Get the XML schema
        /// </summary>
        /// <returns>The XMLSchema of this object</returns>
        protected virtual XmlSchema OnGetSchema()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Handle reading XML
        /// </summary>
        /// <param name="reader">The XML reader</param>
        protected virtual void OnReadXml(XmlReader reader)
        {
            // personally I would rather use XmlNodes than the reader, so...

            // read the doc
            XmlNode node = XMLSerializableObject.doc.ReadNode(reader);

            // read the xml as a node
            this.OnReadXmlNode(node);
        }

        /// <summary>
        /// Handle writing XML
        /// </summary>
        /// <param name="writer">The XML writer</param>
        protected virtual void OnWriteXml(XmlWriter writer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Handle reading XML
        /// </summary>
        /// <param name="node">The XML node to read</param>
        protected virtual void OnReadXmlNode(XmlNode node)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reconstruct an object from an xml node
        /// </summary>
        /// <param name="node">The node to reconstruct this object from</param>
        public void ReadXmlNode(XmlNode node)
        {
            // read the node
            this.OnReadXmlNode(node);
        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Serialize an object into UTF8
        /// </summary>
        /// <param name="obj">The object to serialize</param>
        /// <returns>A serialization of the object</returns>
        public static String SerializeToXML(IXmlSerializable obj)
        {
            // the xml string
            String xmlizedString = null;

            // hold stream
            MemoryStream memoryStream = new MemoryStream();

            // create serializer
            XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());

            // create writer
            XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);

            // serialise
            xmlSerializer.Serialize(xmlTextWriter, obj);

            // set stream from code
            memoryStream = (MemoryStream)xmlTextWriter.BaseStream;

            // get ast UTF8
            xmlizedString = XMLSerializableObject.UTF8ByteArrayToString(memoryStream.ToArray());

            // return string
            return xmlizedString;
        }

        /// <summary>
        /// Reconstruct an object from an XML string
        /// </summary>
        /// <param name="xmlString">The string to deserialize</param>
        /// <param name="typeOfObject">The type of the object to deserialize</param>
        /// <returns>The deserialized object</returns>
        public static object Deserialize(String xmlString, Type typeOfObject)
        {
            // create serialiser
            XmlSerializer xmlSerializer = new XmlSerializer(typeOfObject);

            // create stream
            MemoryStream memoryStream = new MemoryStream(XMLSerializableObject.StringToUTF8ByteArray(xmlString));

            // create writer
            XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);

            // return deserialised object
            return xmlSerializer.Deserialize(memoryStream);
        }

        /// <summary>
        /// To convert a Byte Array of Unicode values (UTF-8 encoded) to a complete String.
        /// </summary>
        /// <param name="characters">Unicode Byte Array to be converted to String</param>
        /// <returns>String converted from Unicode Byte Array</returns>
        private static String UTF8ByteArrayToString(Byte[] characters)
        {
            // encode
            return encoding.GetString(characters);
        }

        /// <summary>
        /// Converts a String to UTF8 Byte array
        /// </summary>
        /// <param name="xmlString">The xml string</param>
        /// <returns>A byte array based from the XML string</returns>
        private static Byte[] StringToUTF8ByteArray(String xmlString)
        {
            return encoding.GetBytes(xmlString);
        }

        /// <summary>
        /// Get if a node exists
        /// </summary>
        /// <param name="doc">The document to search</param>
        /// <param name="tagName">The tag to search for</param>
        /// <returns></returns>
        public static Boolean NodeExists(XmlDocument doc, String tagName)
        {
            // itterate nodes
            foreach (XmlNode node in doc)
            {
                // if node is correct
                if (node.Name == tagName)
                {
                    // return true
                    return true;
                }
            }

            // does not exists
            return false;
        }

        /// <summary>
        /// Get if a node exists
        /// </summary>
        /// <param name="node">The xml node to search</param>
        /// <param name="tagName">The tag to search for</param>
        /// <returns></returns>
        public static Boolean NodeExists(XmlNode node, String tagName)
        {
            // itterate nodes
            foreach (XmlNode nodeElement in node.ChildNodes)
            {
                // if node is correct
                if (nodeElement.Name == tagName)
                {
                    // return true
                    return true;
                }
            }

            // does not exists
            return false;
        }

        /// <summary>
        /// Get if a attribue exists
        /// </summary>
        /// <param name="node">The node to search for</param>
        /// <param name="attributeName">The attribute to search for</param>
        /// <returns></returns>
        public static Boolean AttributeExists(XmlNode node, String attributeName)
        {
            // itterate attribute
            foreach (XmlAttribute attribute in node.Attributes)
            {
                // check attribute name
                if (attribute.Name == attributeName)
                {
                    // return true
                    return true;
                }
            }

            // does not exists
            return false;
        }

        /// <summary>
        /// Get if a attribue exists
        /// </summary>
        /// <param name="doc">The document to search</param>
        /// <param name="tagName">The tag to search for</param>
        /// <param name="attributeName">The attribute to search for</param>
        /// <returns></returns>
        public static Boolean AttributeExists(XmlDocument doc, String tagName, String attributeName)
        {
            // itterate nodes
            foreach (XmlNode node in doc.GetElementsByTagName(tagName))
            {
                // itterate attribute
                foreach (XmlAttribute attribute in node.Attributes)
                {
                    // check attribute name
                    if (attribute.Name == attributeName)
                    {
                        // return true
                        return true;
                    }
                }
            }

            // does not exists
            return false;
        }

        /// <summary>
        /// Get an attribute
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="tagName"></param>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        public static XmlAttribute GetAttribute(XmlDocument doc, String tagName, String attributeName)
        {
            // itterate nodes
            foreach (XmlNode node in doc.GetElementsByTagName(tagName))
            {
                // itterate attribute
                foreach (XmlAttribute attribute in node.Attributes)
                {
                    // check attribute name
                    if (attribute.Name == attributeName)
                    {
                        // return attribute
                        return attribute;
                    }
                }
            }

            // return false
            return null;
        }

        /// <summary>
        /// Get a inner node from a node at a specified index
        /// </summary>
        /// <param name="node">The parent node to search</param>
        /// <param name="attributeName">The attribute that is being searched for</param>
        /// <returns></returns>
        public static XmlAttribute GetAttribute(XmlNode node, String attributeName)
        {
            // itterate attribute
            foreach (XmlAttribute attribute in node.Attributes)
            {
                // check attribute name
                if (attribute.Name == attributeName)
                {
                    // return attribute
                    return attribute;
                }
            }

            // return false
            return null;
        }

        /// <summary>
        /// Get a inner node from a node at a specified index
        /// </summary>
        /// <param name="node"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static XmlNode GetNode(XmlNode node, Int16 index)
        {
            // if valid index
            if (index < node.Attributes.Count)
            {
                // get attribute
                return node.ChildNodes[index];
            }
            else
            {
                // null
                return null;
            }
        }

        /// <summary>
        /// Get a node
        /// </summary>
        /// <param name="doc">The document to search</param>
        /// <param name="tagName">The tag name to search for</param>
        /// <returns></returns>
        public static XmlNode GetNode(XmlDocument doc, String tagName)
        {
            // itterate nodes
            foreach (XmlNode node in doc)
            {
                // if node is correct
                if (node.Name == tagName)
                {
                    // return node
                    return node;
                }
            }

            // return false
            return null;
        }

        /// <summary>
        /// Get an node
        /// </summary>
        /// <param name="parentNode">The parent node to search</param>
        /// <param name="tagName">The tag name to search for</param>
        /// <returns></returns>
        public static XmlNode GetNode(XmlNode parentNode, String tagName)
        {
            // itterate nodes
            foreach (XmlNode node in parentNode.ChildNodes)
            {
                // if node is correct
                if (node.Name == tagName)
                {
                    // return node
                    return node;
                }
            }

            // return false
            return null;
        }

        #endregion

        #region IXmlSerializable Members

        /// <summary>
        /// Get the XmlSchema of this object
        /// </summary>
        /// <returns>This objects XmlSchema</returns>
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return this.OnGetSchema();
        }

        /// <summary>
        /// Read Xml for this object
        /// </summary>
        /// <param name="reader">The XmlReader to read the Xml from</param>
        public void ReadXml(XmlReader reader)
        {
            this.OnReadXml(reader);
        }

        /// <summary>
        /// Write Xml for this object
        /// </summary>
        /// <param name="writer">The writer to use for all XmlWriting</param>
        public void WriteXml(XmlWriter writer)
        {
            this.OnWriteXml(writer);
        }

        #endregion
    }
}
