using System.Text;
using System.Text.RegularExpressions;

namespace DiagnosticsSummary.Common
{
    public static class DiagnosticInfoInterpreterParser
    {
        public static bool TryParse(string rules, out Dictionary<(int start, int end), string> dict)
        {
            dict = new Dictionary<(int start, int end), string>();
            var sepRules = rules.Split(Environment.NewLine);
            string pattern = @"^\d+ - \d+ \w+$";
            foreach (var rule in sepRules)
            {
                if (!Regex.IsMatch(rule, pattern))
                {
                    return false;
                }
                var comps = rule.Split(" ");
                try
                {
                    dict.Add((int.Parse(comps[0]), int.Parse(comps[2])), comps[3]);
                }
                catch (Exception)
                {
                    return false;
                }
            }
            int lastEnd = int.MinValue;
            foreach (var rule in dict)
            {
                if (rule.Key.start > rule.Key.end || rule.Key.start <= lastEnd)
                {
                    return false;
                }
                lastEnd = rule.Key.end;
            }
            return true;
        }

        public static Dictionary<(int start, int end), string> Parse(string rules)
        {
            var dict = new Dictionary<(int start, int end), string>();
            var sepRules = rules.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            foreach (var rule in sepRules)
            {
                var comps = rule.Split(" ");
                dict.Add((int.Parse(comps[0]), int.Parse(comps[1])), comps[2]);
            }
            return dict;
        }

        public static string Unparse(Dictionary<(int start, int end), string> dict)
        {
            StringBuilder interpreter = new StringBuilder();
            foreach(var rule in dict)
            {
                interpreter.AppendLine(String.Join(" ", rule.Key.start, rule.Key.end, rule.Value));
            }
            return interpreter.ToString();
        }
    }
}
