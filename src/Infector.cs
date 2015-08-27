using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DataAccess.Data
{
    public static class Inflector
    {
        private static readonly Dictionary<string, string> Irregular = new Dictionary<string, string>
            {
                {"person", "people"},
                {"man", "men"},
                {"child", "children"},
                {"sex", "sexes"},
                {"move", "moves"},
                {"cow", "kine"}
            };

        private static readonly Dictionary<string, string> Plural = new Dictionary<string, string>
            {
                {"$", "s"},
                {"s$", "s"},
                {"(ax|test)is$", "${1}es"},
                {"(octop|vir)us$", "${1}i"},
                {"(alias|status)$", "${1}es"},
                {"(bu)s$", "${1}ses"},
                {"(buffal|tomat)o$", "${1}oes"},
                {"([ti])um$", "${1}a"},
                {"sis$", "ses"},
                {"(?:([^f])fe|([lr])f)$", "${1}${2}ves"},
                {"(hive)$", "${1}s"},
                {"([^aeiouy]|qu)y$", "${1}ies"},
                {"(x|ch|ss|sh)$", "${1}es"},
                {"(matr|vert|ind)(?:ix|ex)$", "${1}ices"},
                {"([m|l])ouse$", "${1}ice"},
                {"^(ox)$", "${1}en"},
                {"(quiz)$", "${1}zes"}
            };

        private static readonly string[] Uncountable = new[]
            {
                "equipment", "information", "rice", "money", "species",
                "series",
                "fish", "sheep"
            };

        /// <summary>
        /// Returns the plural form of the word in the string.
        /// 
        /// Examples:
        /// "post".Pluralize()             # => "posts"
        /// "octopus".Pluralize()          # => "octopi"
        /// "sheep".Pluralize()            # => "sheep"
        /// "words".Pluralize()            # => "words"
        /// "CamelOctopus".Pluralize()     # => "CamelOctopi"
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Pluralize(this string input)
        {
            if (string.IsNullOrEmpty(input) || Uncountable.Contains(input.ToLower()))
            {
                return input;
            }

            foreach (var pair in Irregular)
            {
                if (input.Contains(pair.Key))
                {
                    return input.Replace(pair.Key, pair.Value);
                }
            }

            foreach (var regex in Plural.Reverse())
            {
                if (Regex.IsMatch(input.ToLower(), regex.Key))
                {
                    return Regex.Replace(input, regex.Key, regex.Value, RegexOptions.IgnoreCase);
                }
            }

            return input;
        }
    }
}
