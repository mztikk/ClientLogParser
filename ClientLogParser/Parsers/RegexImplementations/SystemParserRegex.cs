using System;
using System.Text.RegularExpressions;

namespace ClientLogParser.Parsers.RegexImplementations
{
    /// <summary>
    /// Implementation of <see cref="ISystemParser"/> using Regex to parse the entry.
    /// </summary>
    public class SystemParserRegex : ISystemParser
    {
        /// <summary>
        /// Initializes a new regex system parser.
        /// </summary>
        public SystemParserRegex()
        {
            _maintenance = new Regex(RegexConstants._LogEntryRegex + " Abnormal disconnect: The realm is currently down for maintenance. Try again later.", RegexOptions.Compiled);
            _newpatch = new Regex(RegexConstants._LogEntryRegex + " There has been a patch that you need to update to. Please restart Path of Exile.", RegexOptions.Compiled);
            _reenterPw = new Regex(RegexConstants._LogEntryRegex + " You are logging in from a new location. Please re-enter your password.", RegexOptions.Compiled);
            _serverdown = new Regex(RegexConstants._LogEntryRegex + " Abnormal disconnect: An unexpected disconnection occurred.", RegexOptions.Compiled);
        }

        private readonly Regex _maintenance;
        private readonly Regex _newpatch;
        private readonly Regex _reenterPw;
        private readonly Regex _serverdown;

        /// <summary>
        /// Converts the log entry string to a <see cref="SystemMessage" />. The return value indicates success.
        /// </summary>
        /// <param name="entry">Clientlog entry to parse</param>
        /// <param name="systemMessage">Contains the parsed <see cref="SystemMessage" /> if the method returned true, otherwise null.</param>
        /// <returns>true if <paramref name="entry" /> was converted successfully; false otherwise.</returns>
        public bool TryParse(string entry, out SystemMessage systemMessage)
        {
            Match match;
            if ((match = _maintenance.Match(entry)).Success)
            {
                systemMessage = new SystemMessage(SystemMessageType.MaintenanceDisconnect, DateTime.Parse(match.Groups[1].Value));
                return true;
            }
            if ((match = _newpatch.Match(entry)).Success)
            {
                systemMessage = new SystemMessage(SystemMessageType.NewPatchDisconnect, DateTime.Parse(match.Groups[1].Value));
                return true;
            }
            if ((match = _reenterPw.Match(entry)).Success)
            {
                systemMessage = new SystemMessage(SystemMessageType.ReenterPassword, DateTime.Parse(match.Groups[1].Value));
                return true;
            }
            if ((match = _serverdown.Match(entry)).Success)
            {
                systemMessage = new SystemMessage(SystemMessageType.ServerDown, DateTime.Parse(match.Groups[1].Value));
                return true;
            }

            systemMessage = null;
            return false;
        }
    }
}
