using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Timers;
using ClientLogParser.Events;
using ClientLogParser.Items;
using ClientLogParser.Messages;
using ClientLogParser.Parsers;

namespace ClientLogParser
{
    /// <summary>
    /// Provides methods and events for parsing/handling new clientlog entries.
    /// </summary>
    public class Overseer : IDisposable
    {
        private readonly string _clientLogPath;

        private readonly Timer _tick;

        private FileStream _stream;
        private StreamReader _reader;

        private readonly ParserCollection _parserCollection;

        /// <summary>
        /// Time in milliseconds after which to check for new entries in the clientlog. (Default: 1500)
        /// </summary>
        public double Interval
        {
            get => _tick.Interval;
            set => _tick.Interval = value;
        }

        /// <summary>
        /// Initializes a parser.
        /// </summary>
        /// <param name="clientLogPath">Path to the Client.txt of Path of Exile</param>
        /// <param name="parserCollection">Parsers to use.</param>
        public Overseer(string clientLogPath, ParserCollection parserCollection)
        {
            _clientLogPath = clientLogPath;
            _parserCollection = parserCollection;

            if (File.Exists(clientLogPath))
            {
                InitStream();
            }

            WhisperPreParseEvent += OnWhisper;

            _tick = new Timer { AutoReset = true, Interval = 1500 };
            _tick.Elapsed += (obj, args) => WatchFile();
            _tick.Start();
        }

        /// <summary>
        /// Initializes a parser only specifying the clientlog path, using default parser implementations
        /// </summary>
        /// <param name="clientLogPath">Path to the Client.txt of Path of Exile</param>
        public Overseer(string clientLogPath) : this(clientLogPath, ParserCollection.Default)
        { }

        /// <summary>
        /// This constructor does not initialize a filestream or timer so it can be used with testing by just calling the Parse function.
        /// </summary>
        /// <param name="parserCollection">Parsers to use.</param>
        public Overseer(ParserCollection parserCollection)
        {
            _parserCollection = parserCollection;

            WhisperPreParseEvent += OnWhisper;
        }

        // Lets cache the stream, as opening and closing the handle is expensive
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void InitStream()
        {
            _stream = new FileStream(_clientLogPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            _reader = new StreamReader(_stream);
        }

        private long _lastPos = -1;
        private bool _running;

        internal void WatchFile()
        {
            if (_running)
            {
                return;
            }

            _running = true;
            try
            {
                if (!File.Exists(_clientLogPath))
                {
                    // if it got deleted we need to dispose the streams if they exist so they get reinitialized afterwards
                    _reader?.Dispose();
                    _stream?.Dispose();
                    return;
                }

                if (_stream == null)
                {
                    InitStream();
                }

                if (_stream.Length == _lastPos)
                {
                    // file is the same as before, return early
                    return;
                }

                if (_lastPos == -1)
                {
                    // we just started so lets skip to the latest
                    _stream.Position = _stream.Length;
                }
                else if (_stream.Length > _lastPos)
                {
                    // file is bigger than before, so we set position to our last
                    _stream.Position = _lastPos;
                }
                else if (_stream.Length < _lastPos)
                {
                    // file is smaller than before, leave position at initial value to read complete file						
                }
                else
                {
                    // is there anything left to handle?
                }

                string line;
                while ((line = _reader.ReadLine()) != null)
                {
                    ParseNewEntry(line);
                }

                _lastPos = _stream.Length;
            }
            finally
            {
                _running = false;
            }
        }

        /// <summary>
        /// Call on a new whisper, before parsing the trade.
        /// </summary>
        /// <param name="e">Parsed <see cref="Whisper"/></param>
        protected virtual void OnWhisperPreParseEvent(Whisper e) => WhisperPreParseEvent?.Invoke(this, e);

        /// <summary>
        /// Event is fired when a new <see cref="Whisper"/> is detected, before parsing the trade.
        /// </summary>
        public event EventHandler<Whisper> WhisperPreParseEvent;

        /// <summary>
        /// Call on a new trade message, containing a <see cref="Whisper"/> and an <see cref="Item"/>
        /// </summary>
        /// <param name="e">Parsed <see cref="TradeMessageEventArgs"/></param>
        protected virtual void OnTradeMessageEvent(TradeMessageEventArgs e) => TradeMessageEvent?.Invoke(this, e);

        /// <summary>
        /// Event is fired when we receive a new whisper that contains a trade.
        /// </summary>
        public event EventHandler<TradeMessageEventArgs> TradeMessageEvent;

        /// <summary>
        /// Call after a failed trade message parse.
        /// </summary>
        /// <param name="e">Parsed <see cref="Whisper"/> without a trade message.</param>
        protected virtual void OnWhisperPostParseEvent(Whisper e) => WhisperPostParseEvent?.Invoke(this, e);

        /// <summary>
        /// Event is fired if there was a whisper but no trade message.
        /// </summary>
        public event EventHandler<Whisper> WhisperPostParseEvent;

        /// <summary>
        /// Call on a new <see cref="SystemMessage"/>
        /// </summary>
        /// <param name="e">Parsed <see cref="SystemMessage"/></param>
        protected virtual void OnSystemMessage(SystemMessage e) => SystemMessageEvent?.Invoke(this, e);

        /// <summary>
        /// Event is fired on a new system message.
        /// </summary>
        public event EventHandler<SystemMessage> SystemMessageEvent;

        /// <summary>
        /// Parses the entry and fires events accordingly.
        /// </summary>
        /// <param name="entry">Clientlog entry to parse</param>
        /// <remarks>Use this method to test the parsers.</remarks>
        public void ParseNewEntry(string entry)
        {
            foreach (IWhisperParser whisperParser in _parserCollection._whisperParsers)
            {
                if (whisperParser.TryParse(entry, out Whisper whisper))
                {
                    //var whisperEvent = new WhisperEventArgs(whisper.Sender, whisper.Recipient, whisper.Message, whisper.TimeOfMessage);
                    OnWhisperPreParseEvent(whisper);
                    // we found a match so skip everything else
                    return;
                }
            }

            foreach (ISystemParser parser in _parserCollection._systemParsers)
            {
                if (parser.TryParse(entry, out SystemMessage msg))
                {
                    //var systemEvent = new SystemMessageEvent(msg);
                    OnSystemMessage(msg);
                    return;
                }
            }
        }

        private void OnWhisper(object sender, Whisper e)
        {
            if (e.Sender == ParserConstants.Self)
            {
                return;
            }

            foreach (IItemParser itemParser in _parserCollection._itemParsers)
            {
                if (itemParser.TryParse(e.Message, out Item item, out var other))
                {
                    var tradeEvent = new TradeMessageEventArgs(e, item, other);
                    OnTradeMessageEvent(tradeEvent);
                    // we found a match so lets return early so we can trigger a postparse event if we didn't find anything
                    return;
                }
            }

            OnWhisperPostParseEvent(e);
        }

        #region IDisposable Support
        private bool _disposedValue; // To detect redundant calls

        /// <summary>
        /// Protected implementation of dispose.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _reader?.Dispose();
                    _stream?.Dispose();
                    _tick?.Dispose();
                }

                _disposedValue = true;
            }
        }

        /// <summary>
        /// Disposes objects used.
        /// </summary>
        public void Dispose() => Dispose(true);
        #endregion
    }
}
