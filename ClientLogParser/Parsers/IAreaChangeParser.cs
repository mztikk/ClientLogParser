using System;

namespace ClientLogParser.Parsers
{
    /// <summary>
    /// Provides methods for parsing an Area Change Clientlog entry.
    /// </summary>
    public interface IAreaChangeParser
    {
        /// <summary>
        /// Parses the log entry string. The return value indicates success.
        /// </summary>
        /// <param name="entry">Clientlog entry to parse</param>
        /// <param name="time">Contains the parsed <see cref="DateTime"/> if the method returned <see langword="true"/>, otherwise null.</param>
        /// <param name="newArea">Contains the parsed new area the played entered if the method returned <see langword="true"/>, otherwise null.</param>
        /// <returns>true if <paramref name="entry"/> was converted successfully; false otherwise.</returns>
        bool TryParse(string entry, out DateTime time, out string newArea);
    }
}
