using Newtonsoft.Json;
using static DiagnosticsSummary.Common.DiagnosticInfoInterpreterParser;

namespace DiagnosticsSummary.Common
{
    public class DictionaryJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Dictionary<(int start, int end), string>);
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            var str = reader.Value.ToString();
            return Parse(str);

        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (value is null) return;
            var dict = value as Dictionary<(int start, int end), string>;
            var str = Unparse(dict);
            writer.WriteValue(str);
        }
    }
}
