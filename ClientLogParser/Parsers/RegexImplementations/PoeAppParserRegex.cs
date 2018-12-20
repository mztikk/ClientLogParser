namespace ClientLogParser.Parsers.RegexImplementations
{
    /// <summary>
    /// Implementation of IItemParser for https://poeapp.com/ for all types of trades combining multiple parsers using Regex to parse the whisper message.
    /// </summary>
    public class PoeAppParserRegex : IItemParser
    {
        /// <summary>
        /// Initializes a new parser for https://poeapp.com/ specifying the parsers.
        /// </summary>
        /// <param name="pricedItemParser">Parser to use for a regular priced item.</param>
        /// <param name="unpricedItemParser">Parser to use for an unpriced item.</param>
        /// <param name="currencyParser">Parser to use for currency.</param>
        public PoeAppParserRegex(IItemParser pricedItemParser, IItemParser unpricedItemParser, IItemParser currencyParser)
        {
            _pricedItemParser = pricedItemParser;
            _unpricedItemParser = unpricedItemParser;
            _currencyParser = currencyParser;
        }

        /// <summary>
        /// Initializes a new parser for https://poeapp.com/ using the default parser implementations.
        /// </summary>
        public PoeAppParserRegex() : this(new PoeAppItemParserRegex(), new PoeAppUnpricedItemParserRegex(), new PoeAppCurrencyParserRegex()) { }

        private readonly IItemParser _pricedItemParser;
        private readonly IItemParser _unpricedItemParser;
        private readonly IItemParser _currencyParser;

        /// <summary>
        /// Converts the whisper string to an <see cref="Item" />. The return value indicates success.
        /// </summary>
        /// <param name="message">Message to parse</param>
        /// <param name="item">Contains the parsed item if the method returned true, otherwise null.</param>
        /// <param name="remainingMessage">If the <paramref name="message" /> contained more than just a item, the remainder will be inside <paramref name="remainingMessage" /></param>
        /// <returns>true if <paramref name="message" /> was converted successfully; false otherwise.</returns>
        public bool TryParse(string message, out Item item, out string remainingMessage)
        {
            if (_pricedItemParser.TryParse(message, out item, out remainingMessage))
            {
                return true;
            }

            if (_unpricedItemParser.TryParse(message, out item, out remainingMessage))
            {
                return true;
            }

            if (_currencyParser.TryParse(message, out item, out remainingMessage))
            {
                return true;
            }

            return false;
        }
    }
}
