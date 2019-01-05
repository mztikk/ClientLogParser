using System;
using System.Collections.Generic;
using System.Linq;
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
        public static readonly ParserCollection Empty = new ParserCollection(new List<IParser>());

        /// <summary>
        /// Contains default parser implementations.
        /// </summary>
        public static readonly ParserCollection Default = new ParserCollection(
            new List<IParser>()
            {
                new SystemParserRegex(),
                new WhisperParserRegex(),
                new OfficialSiteParserRegex(),
                new PoeAppParserRegex(),
                new AreaChangeParserRegex()
            });

        [Obsolete("Use the general _parsers instead")]
        internal IEnumerable<IWhisperParser> _whisperParsers { get; }

        [Obsolete("Use the general _parsers instead")]
        internal IEnumerable<IItemParser> _itemParsers { get; }

        [Obsolete("Use the general _parsers instead")]
        internal IEnumerable<ISystemParser> _systemParsers { get; }

        [Obsolete("Use the general _parsers instead")]
        internal IEnumerable<IAreaChangeParser> _areaChangeParsers { get; }

        internal IEnumerable<IParser> _parsers { get; }

        /// <summary>
        /// Initializes a new collection of parsers.
        /// </summary>
        /// <param name="parsers">The <see cref="IParser"/>s to use.</param>
        public ParserCollection(IEnumerable<IParser> parsers)
        {
            _parsers = parsers;
            _systemParsers = parsers.OfType<ISystemParser>();
            _whisperParsers = parsers.OfType<IWhisperParser>();
            _itemParsers = parsers.OfType<IItemParser>();
            _areaChangeParsers = parsers.OfType<IAreaChangeParser>();

            //_systemParsers = systemParsers;
            //_whisperParsers = whisperParsers;
            //_itemParsers = itemParsers;
            //_areaChangeParsers = areaChangeParsers;
        }
    }
}
