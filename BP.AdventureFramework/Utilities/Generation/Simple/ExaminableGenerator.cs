using System;
using System.Collections.Generic;
using System.Linq;
using BP.AdventureFramework.Assets;

namespace BP.AdventureFramework.Utilities.Generation.Simple
{
    /// <summary>
    /// Provides a examinable generator.
    /// </summary>
    public sealed class ExaminableGenerator : IExaminableGenerator
    {
        #region Properties

        /// <summary>
        /// Get or set the nouns.
        /// </summary>
        private Dictionary<string, int> Nouns { get; }

        /// <summary>
        /// Get or set the adjectives.
        /// </summary>
        private Dictionary<string, int> Adjectives { get; }

        /// <summary>
        /// Get or set the description generator.
        /// </summary>
        private IDescriptionGenerator DescriptionGenerator { get; }

        /// <summary>
        /// Get or set if reuse of nouns or adjectives are used.
        /// </summary>
        private bool AllowReuse { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ExaminableGenerator class.
        /// </summary>
        /// <param name="nouns">The nouns.</param>
        /// <param name="adjectives">The adjectives.</param>
        /// <param name="descriptionGenerator">A generator to use for generating descriptions.</param>
        /// <param name="allowReuse">If reuse of nouns or adjectives are used.</param>
        public ExaminableGenerator(IEnumerable<string> nouns, IEnumerable<string> adjectives, IDescriptionGenerator descriptionGenerator, bool allowReuse)
        {
            Nouns = nouns.Distinct().ToDictionary(x => x, x => 0);
            Adjectives = adjectives.Distinct().ToDictionary(x => x, x => 0);
            DescriptionGenerator = descriptionGenerator;
            AllowReuse = allowReuse;
        }

        #endregion

        #region Implementation of IExaminableGenerator

        /// <summary>
        /// Generate an examinable.
        /// </summary>
        /// <param name="generator">The generator.</param>
        /// <returns>The generated examinable.</returns>
        public IExaminable Generate(Random generator)
        {
            var adjective = string.Empty;
            var noun = string.Empty;

            if (AllowReuse)
            {
                noun = Nouns.ElementAt(generator.Next(0, Nouns.Count)).Key;
                adjective = Adjectives.ElementAt(generator.Next(0, Adjectives.Count)).Key;

                Nouns[noun]++;
                Adjectives[adjective]++;
            }
            else
            {
                var unusedNouns = Nouns.Where(x => x.Value == 0).Select(x => x.Key).ToArray();

                if (unusedNouns.Any())
                    noun = unusedNouns.ElementAt(generator.Next(0, unusedNouns.Length));

                var unusedAdjectives = Adjectives.Where(x => x.Value == 0).Select(x => x.Key).ToArray();

                if (unusedAdjectives.Any())
                    adjective = unusedAdjectives.ElementAt(generator.Next(0, unusedAdjectives.Length));
                
                if (Nouns.ContainsKey(noun)) 
                    Nouns[noun]++;
                
                if (Adjectives.ContainsKey(adjective))
                    Adjectives[adjective]++;
            }

            var hasNoun = !string.IsNullOrEmpty(noun);
            var hasAdjective = !string.IsNullOrEmpty(adjective);
            Identifier identifier;

            if (hasNoun && hasAdjective)
                identifier = new Identifier($"{adjective} {noun}");
            else if (hasNoun)
                identifier = new Identifier(noun);
            else
                return null;

            return new GeneratedExaimnable(identifier, DescriptionGenerator.Generate(identifier));
        }

        #endregion
    }
}
