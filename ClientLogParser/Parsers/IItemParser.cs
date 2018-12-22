using ClientLogParser.Items;
using ClientLogParser.Messages;

namespace ClientLogParser.Parsers
{
    /// <summary>
    /// Provides methods for parsing a <see cref="Whisper"/> message to an <see cref="Item"/>
    /// </summary>
    public interface IItemParser
    {
        /// <summary>
        /// Converts the whisper string to an <see cref="Item"/>. The return value indicates success.
        /// </summary>
        /// <param name="message">Message to parse</param>
        /// <param name="item">Contains the parsed item if the method returned true, otherwise null.</param>
        /// <param name="remainingMessage">If the <paramref name="message"/> contained more than just a item, the remainder will be inside <paramref name="remainingMessage"/></param>
        /// <returns>true if <paramref name="message"/> was converted successfully; false otherwise.</returns>
        bool TryParse(string message, out Item item, out string remainingMessage);
    }
}
