using System.Collections.Generic;
using ClientLogParser.Parsers.RegexImplementations;

namespace ClientLogParser.Parsers
{
    /// <summary>
    /// Represents a collection of different parsers.
    /// </summary>
    public class ParserCollection
    {
        /// <summary>
        /// An empty collection, containing no parsers.
        /// </summary>
        public static readonly ParserCollection Empty = new ParserCollection(
            new List<ISystemParser>(),
            new List<IWhisperParser>(),
            new List<IItemParser>(),
            new List<IAreaChangeParser>());

        /// <summary>
        /// Contains default parser implementations.
        /// </summary>
        public static readonly ParserCollection Default = new ParserCollection(
            new List<ISystemParser>
            {
                new SystemParserRegex()
            },
            new List<IWhisperParser>
            {
                new WhisperParserRegex()
            },
            new List<IItemParser>
            {
                new OfficialSiteParserRegex(),
                new PoeAppParserRegex()
            },
            new List<IAreaChangeParser>()
            {
                new AreaChangeParserRegex()
            });

        internal IEnumerable<IWhisperParser> _whisperParsers { get; }

        internal IEnumerable<IItemParser> _itemParsers { get; }

        internal IEnumerable<ISystemParser> _systemParsers { get; }

        internal IEnumerable<IAreaChangeParser> _areaChangeParsers { get; }

        /// <summary>
        /// Initializes a new collection of parsers.
        /// </summary>
        /// <param name="systemParsers">The <see cref="ISystemParser"/>s to use.</param>
        /// <param name="whisperParsers">The <see cref="IWhisperParser"/>s to use.</param>
        /// <param name="itemParsers">The <see cref="IItemParser"/>s to use.</param>
        /// <param name="areaChangeParsers">The <see cref="IAreaChangeParser"/>s to use.</param>
        public ParserCollection(IEnumerable<ISystemParser> systemParsers, IEnumerable<IWhisperParser> whisperParsers, IEnumerable<IItemParser> itemParsers, IEnumerable<IAreaChangeParser> areaChangeParsers)
        {
            _systemParsers = systemParsers;
            _whisperParsers = whisperParsers;
            _itemParsers = itemParsers;
            _areaChangeParsers = areaChangeParsers;
        }
    }
}
