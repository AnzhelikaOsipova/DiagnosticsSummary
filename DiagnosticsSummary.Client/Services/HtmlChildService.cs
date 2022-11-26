using DiagnosticsSummary.Client.Models;
using DiagnosticsSummary.Client.Models.Input;
using static DiagnosticsSummary.Common.ChildFieldsMappingLibrary;

namespace DiagnosticsSummary.Client.Services
{
    public static class HtmlChildService
    {
        public static Dictionary<LabelList,string> LabelDictionary = 
            new Dictionary<LabelList, string>()
        {
                { LabelList.FIO, "ФИО" },
                { LabelList.Gender, "Пол" },
                { LabelList.Age, "Возраст" },
                { LabelList.AgeGroup, "Возрастная группа" },
                { LabelList.Group, "Группа" },
                { LabelList.Diagnosis, "Диагноз" }
        };
        public enum LabelList
        {
            FIO,
            Gender,
            Age,
            AgeGroup,
            Group,
            Diagnosis
        }
        public static HtmlType[] ChildInfoInitialize(ChildView? child = null)
        {
            return new[]
            {
                new HtmlType()
                {
                    Label = LabelDictionary[LabelList.FIO],
                    Value = child?.FIO,
                    Method = HtmlMethod.Input,
                    InputType = "text",
                    IsValidValue = (val) => { return !String.IsNullOrEmpty(val); }
                },
                new HtmlType()
                {
                    Label = LabelDictionary[LabelList.Gender],
                    Value = child?.Gender,
                    Method = HtmlMethod.Select,
                    SelectObjects = SelectObject.Create(GenderDictionary.Values.ToArray()),
                    IsValidValue = (val) => { return !String.IsNullOrEmpty(val) && GenderDictionary.Values.Contains(val); }
                },
                new HtmlType()
                {
                    Label = LabelDictionary[LabelList.Age],
                    Value = child?.Age.ToString(),
                    Method = HtmlMethod.Input,
                    InputType = "number",
                    IsValidValue = (val) => { return int.TryParse(val, out int res) && res >= 0 && res < 18; }
                },
                new HtmlType()
                {
                    Label = LabelDictionary[LabelList.AgeGroup],
                    Value = child?.AgeGroup,
                    Method = HtmlMethod.Select,
                    SelectObjects = SelectObject.Create(AgeGroupDictionary.Values.ToArray()),
                    IsValidValue = (val) => { return !String.IsNullOrEmpty(val) && AgeGroupDictionary.Values.Contains(val); }
                },
                 new HtmlType()
                {
                    Label = LabelDictionary[LabelList.Group],
                    Value = child?.Group,
                    Method = HtmlMethod.Input,
                    InputType = "text",
                    IsValidValue = (val) => { return !String.IsNullOrEmpty(val); }
                },
                 new HtmlType()
                {
                    Label = LabelDictionary[LabelList.Diagnosis],
                    Value = child?.Diagnosis,
                    Method = HtmlMethod.Select,
                    SelectObjects = SelectObject.Create(DiagnosisDictionary.Values.ToArray()),
                    IsValidValue = (val) => { return !String.IsNullOrEmpty(val) && DiagnosisDictionary.Values.Contains(val); }
                }
            };
        }
    }
}
