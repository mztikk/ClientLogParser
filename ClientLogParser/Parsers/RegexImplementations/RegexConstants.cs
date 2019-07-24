using System.Text.RegularExpressions;

namespace ClientLogParser.Parsers.RegexImplementations
{
    internal static class RegexConstants
    {
        internal const string DateTimeRegex = @"([\d/]* [\d:]*)";
        internal const string MaybeTicksRegex = @"([\d]+?)";
        internal const string APrefixRegex = @"(.\w*)";
        internal const string InfoClientRegex = @"\[INFO Client ([\d]+?)\]";
        internal const string LogEntryRegex = DateTimeRegex + " " + MaybeTicksRegex + " " + APrefixRegex + " " + InfoClientRegex;
        internal const string ItemNameRegex = @"([\p{L}\p{N}\p{P}\p{S}\p{Z}]+?)";
        internal const string CurrencyRegex = @"([\w\s\p{P}]+?)";
        internal const string LeagueNameRegex = @"([\p{L}\p{N}\p{P}\p{S}\p{Z}]+?)";
        internal const string PositionRegex = @"left ([\d]+?), top ([\d]+?)";
        internal const string AreaRegex = @"([\p{L}\p{N}\p{P}\p{S}\p{Z}][^\.]+)";
        internal const string TierRegex = @"T(\d*)";

        internal static readonly Regex MapPattern = new Regex(@"(.*) \(T(\d*)\)", RegexOptions.Compiled);
        internal static readonly Regex OffiGemPattern = new Regex(@"(.*)level (\d*) (\d*)% (.*)", RegexOptions.Compiled);
        internal static readonly Regex PoeAppGemPattern = new Regex(@"(.*) \((\d*)\/(\d*)%\)", RegexOptions.Compiled);
    }
}
