using System;
using System.Text.RegularExpressions;

namespace ClientLogParser.Parsers.RegexImplementations
{
    /// <summary>
    /// Implementation of <see cref="IAreaChangeParser"/> using Regex to parse the entry.
    /// </summary>
    public class AreaChangeParserRegex : IAreaChangeParser
    {
        /// <summary>
        /// Initializes a new regex areachange parser.
        /// </summary>
        public AreaChangeParserRegex()
        {
            _areaChange = new Regex($"{RegexConstants.LogEntryRegex} : You have entered {RegexConstants.AreaRegex}\\.");
        }

        private readonly Regex _areaChange;

        /// <summary>
        /// Parses the log entry string. The return value indicates success.
        /// </summary>
        /// <param name="entry">Clientlog entry to parse</param>
        /// <param name="time">Contains the parsed <see cref="DateTime" /> if the method returned <see langword="true" />, otherwise null.</param>
        /// <param name="newArea">Contains the parsed new area the played entered if the method returned <see langword="true" />, otherwise null.</param>
        /// <returns>true if <paramref name="entry" /> was converted successfully; false otherwise.</returns>
        public bool TryParse(string entry, out DateTime time, out string newArea)
        {
            Match match;
            if ((match = _areaChange.Match(entry)).Success)
            {
                time = DateTime.Parse(match.Groups[1].Value);
                newArea = match.Groups[6].Value;
                return true;
            }

            time = DateTime.MinValue;
            newArea = string.Empty;
            return false;
        }
    }
}
