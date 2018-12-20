using System.Text.RegularExpressions;

namespace ClientLogParser.Parsers.RegexImplementations
{
    internal static class RegexConstants
    {
        internal const string _DateTimeRegex = @"([\d/]* [\d:]*)";
        internal const string _MaybeTicksRegex = @"([\d]+?)";
        internal const string _APrefixRegex = @"(.\w*)";
        internal const string _InfoClientRegex = @"\[INFO Client ([\d]+?)\]";
        internal const string _LogEntryRegex = _DateTimeRegex + " " + _MaybeTicksRegex + " " + _APrefixRegex + " " + _InfoClientRegex;
        internal const string _ItemNameRegex = @"([\p{L}\p{N}\p{P}\p{S}\p{Z}]+?)";
        internal const string _CurrencyRegex = @"([\w\s\p{P}]+?)";
        internal const string _LeagueNameRegex = @"([\p{L}\p{N}\p{P}\p{S}\p{Z}]+?)";
        internal const string _PositionRegex = @"left ([\d]+?), top ([\d]+?)";

        internal static readonly Regex MapPattern = new Regex(@"(.*) \(T(\d*)\)", RegexOptions.Compiled);
        internal static readonly Regex OffiGemPattern = new Regex(@"(.*)level (\d*) (\d*)% (.*)", RegexOptions.Compiled);
        internal static readonly Regex PoeAppGemPattern = new Regex(@"(.*) \((\d*)\/(\d*)%\)", RegexOptions.Compiled);
    }
}
