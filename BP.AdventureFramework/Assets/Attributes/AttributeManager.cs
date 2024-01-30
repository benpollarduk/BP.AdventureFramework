using System.Collections.Generic;
using System.Linq;
using BP.AdventureFramework.Extensions;

namespace BP.AdventureFramework.Assets.Attributes
{
    internal class AttributeManager
    {
        private Dictionary<Attribute, double> Attributes = new Dictionary<Attribute, double>();

        /// <summary>
        /// Get all attributes.
        /// </summary>
        /// <returns>An array of attribtes.</returns>
        public Attribute[] GetAttributes()
        {
            return Attributes.Keys.ToArray();
        }

        /// <summary>
        /// Get the value of an attribute.
        /// </summary>
        /// <param name="attributeName">The name of the attribute.</param>
        /// <returns>The value.</returns>
        public double GetValue(string attributeName)
        {
            return GetValue(Attributes.Keys.FirstOrDefault(x => x.Name.InsensitiveEquals(attributeName)));
        }

        /// <summary>
        /// Get the value of an attribute.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <returns>The value.</returns>
        public double GetValue(Attribute attribute)
        {
            return Attributes.TryGetValue(attribute, out var value) ? value : 0d;
        }

        /// <summary>
        /// Add a value to an attribute.
        /// </summary>
        /// <param name="attributeName">The name of the attribute.</param>
        /// <param name="value">The value.</param>
        public void Add(string attributeName, double value)
        {
            var attribute = Attributes.Keys.FirstOrDefault(x => x.Name.InsensitiveEquals(attributeName)) ?? new Attribute(attributeName, string.Empty, double.MinValue, double.MaxValue);
            Add(attribute, value);
        }

        /// <summary>
        /// Add a value to an attribute.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <param name="value">The value.</param>
        public void Add(Attribute attribute, double value)
        {
            if (Attributes.ContainsKey(attribute))
                Attributes[attribute] += value;
            else
                Attributes.Add(attribute, value);
        }

        /// <summary>
        /// Subtract a value from an attribute.
        /// </summary>
        /// <param name="attributeName">The name of the attribute.</param>
        /// <param name="value">The value.</param>
        public void Subtract(string attributeName, double value)
        {
            var attribute = Attributes.Keys.FirstOrDefault(x => x.Name.InsensitiveEquals(attributeName)) ?? new Attribute(attributeName, string.Empty, double.MinValue, double.MaxValue);
            Subtract(attribute, value);
        }

        /// <summary>
        /// Subtract a value from an attribute.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <param name="value">The value.</param>
        public void Subtract(Attribute attribute, double value)
        {
            if (Attributes.ContainsKey(attribute))
                Attributes[attribute] -= value;
            else
                Attributes.Add(attribute, value);
        }

        /// <summary>
        /// Remove an attribute.
        /// </summary>
        /// <param name="attributeName">The name of the attribute.</param>
        public void Remove(string attributeName)
        {
            var attribute = Attributes.Keys.FirstOrDefault(x => x.Name.InsensitiveEquals(attributeName)) ?? new Attribute(attributeName, string.Empty, double.MinValue, double.MaxValue);
            Remove(attribute);
        }

        /// <summary>
        /// Remove an attribute.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        public void Remove(Attribute attribute)
        {
            if (Attributes.ContainsKey(attribute))
                Attributes.Remove(attribute);
        }
    }
}
