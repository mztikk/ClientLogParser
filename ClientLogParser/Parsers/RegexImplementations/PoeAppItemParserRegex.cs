using System.Text.RegularExpressions;

namespace ClientLogParser.Parsers.RegexImplementations
{
    /// <summary>
    /// Implementation of IItemParser for https://poeapp.com/ for a normal item trade using Regex to parse the whisper message.
    /// </summary>
    public class PoeAppItemParserRegex : IItemParser
    {
        /// <summary>
        /// Initializes a new regex parser for a regular item on poeapp
        /// </summary>
        public PoeAppItemParserRegex()
        {
            _tradeItem = new Regex($"(.*)wtb {RegexConstants._ItemNameRegex} listed for {RegexConstants._CurrencyRegex} in {RegexConstants._LeagueNameRegex} \\(stash \"(.*)\"; {RegexConstants._PositionRegex}\\)(.*)", RegexOptions.Compiled);
            _tradeGem = RegexConstants.PoeAppGemPattern;
            _tradeMap = RegexConstants.MapPattern;
        }

        private readonly Regex _tradeItem;
        private readonly Regex _tradeGem;
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
            Match match;
            if ((match = _tradeItem.Match(message)).Success)
            {
                GroupCollection grps = match.Groups;
                var itemName = grps[2].Value;
                Match typeMatch;
                if ((typeMatch = _tradeGem.Match(itemName)).Success)
                {
                    GroupCollection itemGrps = typeMatch.Groups;
                    item = new Gem(
                        itemGrps[1].Value,
                        itemName,
                        int.Parse(itemGrps[2].Value),
                        int.Parse(itemGrps[3].Value),
                        grps[3].Value,
                        grps[4].Value,
                        grps[5].Value,
                        ParserConstants.FormatPositionString(grps[6].Value, grps[7].Value));
                }
                else if ((typeMatch = _tradeMap.Match(itemName)).Success)
                {
                    GroupCollection itemGrps = typeMatch.Groups;
                    item = new Map(
                        itemGrps[1].Value,
                        itemName,
                        int.Parse(itemGrps[2].Value),
                        grps[3].Value,
                        grps[4].Value,
                        grps[5].Value,
                        ParserConstants.FormatPositionString(grps[6].Value, grps[7].Value));
                }
                else
                {
                    item = new Item(
                        itemName,
                        grps[3].Value,
                        grps[4].Value,
                        grps[5].Value,
                        ParserConstants.FormatPositionString(grps[6].Value, grps[7].Value));
                }

                remainingMessage = grps[8].Value;
                return true;
            }

            item = null;
            remainingMessage = string.Empty;
            return false;
        }
    }
}
