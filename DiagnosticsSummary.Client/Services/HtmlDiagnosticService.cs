
using DiagnosticsSummary.Client.Models;
using static DiagnosticsSummary.Client.Models.HtmlType;

namespace DiagnosticsSummary.Client.Services
{
    public static class HtmlDiagnosticService
    {
        public static Dictionary<LabelList, string> LabelDictionary =
            new Dictionary<LabelList, string>()
            {
                { LabelList.Year, "Год прохождения" },
                { LabelList.YearPart, "Половина года" }
            };

        public enum LabelList
        {
            Year,
            YearPart
        }

        public static HtmlType[] DiagnosticInfoInitialize()
        {
            return new[]
            {
                new HtmlType()
                {
                    Label = LabelDictionary[LabelList.Year],
                    HtmlMethod = Method.Input,
                    InputType = "number",
                    IsValidValue = (val) => { return int.TryParse(val,out var res) && res > 0; }
                },
                new HtmlType()
                {
                    Label = LabelDictionary[LabelList.YearPart],
                    HtmlMethod = Method.Select,
                    SelectObjects = new[]
                    {
                        new SelectObject()
                        {
                            Name = "Начало",
                            Value = "Start"
                        },
                        new SelectObject()
                        {
                            Name = "Конец",
                            Value = "End"
                        }
                    },
                    IsValidValue = (val) => { return int.TryParse(val,out var res) && res > 0; }
                }
            };
        }
    }
}
