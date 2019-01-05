using ClientLogParser.Messages;

namespace ClientLogParser.Parsers
{
    /// <summary>
    /// Provides methods for parsing a Clientlog entry to a <see cref="Whisper"/>
    /// </summary>
    public interface IWhisperParser : IParser
    {
        /// <summary>
        /// Converts the log entry string to a <see cref="Whisper"/>. The return value indicates success.
        /// </summary>
        /// <param name="entry">Clientlog entry to parse</param>
        /// <param name="whisper">Contains the parsed <see cref="Whisper"/> if the method returned true, otherwise null.</param>
        /// <returns>true if <paramref name="entry"/> was converted successfully; false otherwise.</returns>
        bool TryParse(string entry, out Whisper whisper);
    }
}
