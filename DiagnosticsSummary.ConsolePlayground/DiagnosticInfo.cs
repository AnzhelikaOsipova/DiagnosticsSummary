using Newtonsoft.Json;

namespace DiagnosticsSummary.ConsolePlayground
{
    public class DiagnosticInfo
    {
        public string Name { get; set; }
        [JsonConverter(typeof(DictionaryJsonConverter))]
        public Dictionary<(int start, int end), string> ValueInterpreter { get; set; }

        public string Interpret(int value)
        {
            foreach (var rule in ValueInterpreter)
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
