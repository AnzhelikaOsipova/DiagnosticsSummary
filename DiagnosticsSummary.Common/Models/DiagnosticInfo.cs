
namespace DiagnosticsSummary.Common.Models
{
    public class DiagnosticInfo
    {
        public string Name { get; set; }

        public string ValueInterpreter { get; set; } = "";
        private Dictionary<(int start, int end), string> ValueInterpreterDict { get; set; }

        public string Interpret(int value)
        {
            if (ValueInterpreterDict is null)
            {
                ValueInterpreterDict = DiagnosticInfoInterpreterParser.Parse(ValueInterpreter);
            }
            foreach (var rule in ValueInterpreterDict)
            {
                if (value >= rule.Key.start && value <= rule.Key.end)
                {
                    return rule.Value;
                }
            }
            return "Не интерпретировано";
        }
    }
}
