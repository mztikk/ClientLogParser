using System.Text.RegularExpressions;
using ClientLogParser.Items;

namespace ClientLogParser.Parsers.RegexImplementations
{
    /// <summary>
    /// Implementation of IItemParser for https://poemap.live/ for a map exchange using Regex to parse the whisper message.
    /// </summary>
    public class PoeMapParserRegex : IItemParser
    {
        /// <summary>
        /// Initializes a new regex parser for a regular item on poeapp
        /// </summary>
        public PoeMapParserRegex()
        {
            _tradeMap = new Regex($@"(.*)I'd like to exchange my {RegexConstants.TierRegex}: \({RegexConstants.ItemNameRegex}\) for your {RegexConstants.TierRegex}: \({RegexConstants.ItemNameRegex}\) in {RegexConstants.LeagueNameRegex}\.(.*)", RegexOptions.Compiled);
        }

        private readonly Regex _tradeMap;

        /// <summary>
        /// Converts the whisper string to an <see cref="Item" />. The return value indicates success.
        /// </summary>
        /// <param name="message">Message to parse</param>
        /// <param name="item">Contains the parsed item if the method returned true, otherwise null.</param>
        /// <param name="remainingMessage">If the <paramref name="message" /> contained more than just a item, the remainder will be inside <paramref name="remainingMessage" /></param>
        /// <returns>true if <paramref name="message" /> was converted successfully; false otherwise.</returns>
        public bool TryParse(string message, out Item item, out string remainingMessage)
        {
            Match match = _tradeMap.Match(message);
            if (match.Success)
            {
                GroupCollection grps = match.Groups;
                item = new Map(
                    name: grps[5].Value,
                    fullItemIdentifier: "T" + grps[4].Value + " " + grps[5].Value,
                    tier: int.Parse(grps[4].Value),
                    price: "T" + grps[2].Value + " " + grps[3].Value,
                    league: grps[6].Value,
                    stash: string.Empty,
                    pos: string.Empty);
                remainingMessage = grps[7].Value;
                return true;
            }

            item = null;
            remainingMessage = string.Empty;
            return false;
        }
    }
}
