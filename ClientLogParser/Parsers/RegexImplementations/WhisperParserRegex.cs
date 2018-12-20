using System;
using System.Text.RegularExpressions;

namespace ClientLogParser.Parsers.RegexImplementations
{
    /// <summary>
    /// Implementation of IWhisperParser using Regex to parse the entry.
    /// </summary>
    public class WhisperParserRegex : IWhisperParser
    {
        /// <summary>
        /// Initializes a new regex whisper parser.
        /// </summary>
        public WhisperParserRegex()
        {
            _fromWhisper = new Regex(RegexConstants._LogEntryRegex + " @From (.*?): (.*)", RegexOptions.Compiled);
            _toWhisper = new Regex(RegexConstants._LogEntryRegex + " @To (.*?): (.*)", RegexOptions.Compiled);
            _guild = new Regex("<(.*)> (.*)", RegexOptions.Compiled);
        }

        private readonly Regex _fromWhisper;
        private readonly Regex _toWhisper;
        private readonly Regex _guild;

        /// <summary>
        /// Converts the log entry string to a <see cref="Whisper" />. The return value indicates success.
        /// </summary>
        /// <param name="entry">Clientlog entry to parse</param>
        /// <param name="whisper">Contains the parsed <see cref="Whisper" /> if the method returned true, otherwise null.</param>
        /// <returns>true if <paramref name="entry" /> was converted successfully; false otherwise.</returns>
        public bool TryParse(string entry, out Whisper whisper)
        {
            Match match;
            if ((match = _fromWhisper.Match(entry)).Success)
            {
                whisper = new Whisper(RemoveGuildPrefix(match.Groups[5].Value), ParserConstants.Self, match.Groups[6].Value, DateTime.Parse(match.Groups[1].Value));
                return true;
            }
            else if ((match = _toWhisper.Match(entry)).Success)
            {
                whisper = new Whisper(ParserConstants.Self, RemoveGuildPrefix(match.Groups[5].Value), match.Groups[6].Value, DateTime.Parse(match.Groups[1].Value));
                return true;
            }

            whisper = null;
            return false;
        }

        private string RemoveGuildPrefix(string fullName)
        {
            Match prefixMatch = _guild.Match(fullName);
            if (prefixMatch.Success)
            {
                return prefixMatch.Groups[2].Value;
            }

            return fullName;
        }
    }
}
