using System.IO;
using System.Runtime.CompilerServices;

namespace ClientLogParser.Parsers
{
    /// <summary>
    /// Contains constant values used when parsing.
    /// </summary>
    internal static class ParserConstants
    {
        /// <summary>
        /// String used when describing oneself
        /// </summary>
        public const string Self = "self";

        /// <summary>
        /// String used for an unpriced item
        /// </summary>
        public const string Unpriced = "unpriced";

        /// <summary>
        /// Contains the relative path from the poe folder to the clientlog
        /// </summary>
        public static readonly string RelativePathToClient = $"logs{Path.DirectorySeparatorChar}Client.txt";

        /// <summary>
        /// Formats the stash left(x) and top(y) position to a uniform string.
        /// </summary>
        /// <param name="left">x position of item</param>
        /// <param name="top">y position of item</param>
        /// <returns>A uniform string representation of the position</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string FormatPositionString(string left, string top) => $"<{left}, {top}>";

        /// <summary>
        /// Formats the stash left(x) and top(y) position to a uniform string.
        /// </summary>
        /// <param name="left">x position of item</param>
        /// <param name="top">y position of item</param>
        /// <returns>A uniform string representation of the position</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string FormatPositionString(int left, int top) => FormatPositionString(left.ToString(), top.ToString());
    }
}
