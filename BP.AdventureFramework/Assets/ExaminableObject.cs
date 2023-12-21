﻿using System;
using System.Linq;
using BP.AdventureFramework.Commands;
using BP.AdventureFramework.Extensions;

namespace BP.AdventureFramework.Assets
{
    /// <summary>
    /// Represents an object that can be examined.
    /// </summary>
    public class ExaminableObject : IExaminable
    {
        #region Properties

        /// <summary>
        /// Get or set the callback handling all examination of this object.
        /// </summary>
        public ExaminationCallback Examination { get; set; } = obj =>
        {
            var description = string.Empty;

            if (obj.Description != null)
                description = obj.Description.GetDescription();

            if (obj.Commands?.Any() ?? false)
            {
                if (!string.IsNullOrEmpty(description))
                    description += " ";

                description += $"{Environment.NewLine}{Environment.NewLine}{obj.Identifier.Name} provides the following commands: ";

                foreach (var customCommand in obj.Commands)
                    description += $"{Environment.NewLine}\"{customCommand.Help.Command}\" - {customCommand.Help.Description.RemoveSentenceEnd()}, ";

                if (description.EndsWith(", "))
                {
                    description = description.Remove(description.Length - 2);
                    description.EnsureFinishedSentence();
                }
            }

            if (string.IsNullOrEmpty(description))
                description = obj.GetType().Name;

            return new ExaminationResult(description);
        };

        #endregion

        #region Overrides of Object

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return Identifier.Name;
        }

        #endregion

        #region IExaminable Members

        /// <summary>
        /// Get this objects identifier.
        /// </summary>
        public Identifier Identifier { get; protected set; }

        /// <summary>
        /// Get or set a description of this object.
        /// </summary>
        public Description Description { get; set; }

        /// <summary>
        /// Get or set this objects commands.
        /// </summary>
        public CustomCommand[] Commands { get; set; }

        /// <summary>
        /// Examine this object.
        /// </summary>
        /// <returns>A ExaminationResult detailing the examination of this object.</returns>
        public virtual ExaminationResult Examine()
        {
            return Examination(this);
        }

        #endregion

        #region Implementation of IPlayerVisible

        /// <summary>
        /// Get or set if this is visible to the player.
        /// </summary>
        public bool IsPlayerVisible { get; set; } = true;

        #endregion
    }
}