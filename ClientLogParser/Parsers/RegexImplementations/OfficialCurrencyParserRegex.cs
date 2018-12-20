using System.Text.RegularExpressions;

namespace ClientLogParser.Parsers.RegexImplementations
{
    /// <summary>
    /// Implementation of IItemParser for the official https://www.pathofexile.com/trade/ for a currency trade using Regex to parse the whisper message.
    /// </summary>
    public class OfficialCurrencyParserRegex : IItemParser
    {
        /// <summary>
        /// Initializes a new regex parser for currency on the official site.
        /// </summary>
        public OfficialCurrencyParserRegex()
        {
            _tradeCurrency = new Regex($@"(.*)Hi, I'd like to buy your (.\d*) {RegexConstants._CurrencyRegex} for my {RegexConstants._CurrencyRegex} in {RegexConstants._LeagueNameRegex}\.(.*)", RegexOptions.Compiled);
        }

        private readonly Regex _tradeCurrency;

        /// <summary>
        /// Converts the whisper string to an <see cref="Item" />. The return value indicates success.
        /// </summary>
        /// <param name="message">Message to parse</param>
        /// <param name="item">Contains the parsed item if the method returned true, otherwise null.</param>
        /// <param name="remainingMessage">If the <paramref name="message" /> contained more than just a item, the remainder will be inside <paramref name="remainingMessage" /></param>
        /// <returns>true if <paramref name="message" /> was converted successfully; false otherwise.</returns>
        public bool TryParse(string message, out Item item, out string remainingMessage)
        {
            Match match = _tradeCurrency.Match(message);
            if (match.Success)
            {
                GroupCollection grps = match.Groups;
                item = new Currency(
                    name: grps[3].Value,
                    amount: int.Parse(grps[2].Value),
                    price: grps[4].Value,
                    league: grps[5].Value,
                    stash: string.Empty,
                    pos: string.Empty);
                remainingMessage = grps[6].Value;
                return true;
            }

            item = null;
            remainingMessage = string.Empty;
            return false;
        }
    }
}
