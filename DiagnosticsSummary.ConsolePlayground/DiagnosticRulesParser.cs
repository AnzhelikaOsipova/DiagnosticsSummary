using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DiagnosticsSummary.ConsolePlayground
{
    public class DiagnosticRulesParser
    {
        //Dictionary<(int start, int end), string>
        public bool TryParse(string rules, out Dictionary<(int start, int end), string> dict)
        {
            dict = new Dictionary<(int start, int end), string>();
            var sepRules = rules.Split(Environment.NewLine);
            string pattern = @"^\d+ - \d+ \w+$";
            foreach(var rule in sepRules)
            {
                if(!Regex.IsMatch(rule, pattern))
                {
                    return false;
                }
                var comps = rule.Split(" ");
                try
                {
                    dict.Add((int.Parse(comps[0]), int.Parse(comps[2])), comps[3]);
                }
                catch(Exception)
                {
                    return false;
                }
            }
            int lastEnd = int.MinValue;
            foreach(var rule in dict)
            {
                if(rule.Key.start > rule.Key.end || rule.Key.start <= lastEnd)
                {
                    return false;
                }
                lastEnd = rule.Key.end;
            }
            return true;
        }

        public Dictionary<(int start, int end), string> Parse(string rules)
        {
            var dict = new Dictionary<(int start, int end), string>();
            var sepRules = rules.Split(Environment.NewLine);
            foreach (var rule in sepRules)
            {
                var comps = rule.Split(" ");
                dict.Add((int.Parse(comps[0]), int.Parse(comps[2])), comps[3]);
            }
            return dict;
        }
    }
}
