﻿using System.Text.RegularExpressions;
using ClientLogParser.Items;

namespace ClientLogParser.Parsers.RegexImplementations
{
    /// <summary>
    /// Implementation of IItemParser for https://poeapp.com/ for a currency trade using Regex to parse the whisper message.
    /// </summary>
    public class PoeAppCurrencyParserRegex : IItemParser
    {
        /// <summary>
        /// Initializes a new regex parser for currency on poeapp
        /// </summary>
        public PoeAppCurrencyParserRegex()
        {
            _tradeCurrency = new Regex($@"(.*)I'd like to buy your (.\d*) {RegexConstants.CurrencyRegex} for my {RegexConstants.CurrencyRegex} in {RegexConstants.LeagueNameRegex}\.(.*)", RegexOptions.Compiled);
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
                    grps[3].Value,
                    int.Parse(grps[2].Value),
                    grps[4].Value,
                    grps[5].Value,
                    string.Empty,
                    string.Empty);
                remainingMessage = grps[6].Value;
                return true;
            }

            item = null;
            remainingMessage = string.Empty;
            return false;
        }
    }
}
