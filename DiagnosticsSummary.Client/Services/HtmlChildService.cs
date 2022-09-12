using DiagnosticsSummary.Client.Models;
using DiagnosticsSummary.Common.Models;
using static DiagnosticsSummary.Client.Models.HtmlType;

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
        public static HtmlType[] ChildInfoInitialize()
        {
            return new[]
            {
                new HtmlType()
                {
                    Label = LabelDictionary[LabelList.FIO],
                    HtmlMethod = Method.Input,
                    InputType = "text",
                    IsValidValue = (val) => { return !String.IsNullOrEmpty(val); }
                },
                new HtmlType()
                {
                    Label = LabelDictionary[LabelList.Gender],
                    HtmlMethod = Method.Select,
                    SelectObjects = new[]
                    {
                        new SelectObject()
                        {
                            Name = "Женский",
                            Value = "Female"
                        },
                        new SelectObject()
                        {
                            Name = "Мужской",
                            Value = "Male"
                        }
                    },
                    IsValidValue = (val) => { return !String.IsNullOrEmpty(val); }
                },
                new HtmlType()
                {
                    Label = LabelDictionary[LabelList.Age],
                    HtmlMethod = Method.Input,
                    InputType = "number",
                    IsValidValue = (val) => { return int.TryParse(val, out int res) && res >= 0; }
                },
                new HtmlType()
                {
                    Label = LabelDictionary[LabelList.AgeGroup],
                    HtmlMethod = Method.Select,
                    SelectObjects = new[]
                    {
                        new SelectObject()
                        {
                            Name = "Младшая",
                            Value = "Junior"
                        },
                        new SelectObject()
                        {
                            Name = "Средняя",
                            Value = "Middle"
                        },
                        new SelectObject()
                        {
                            Name = "Старшая",
                            Value = "Senior"
                        },
                        new SelectObject()
                        {
                            Name = "Подготовительная",
                            Value = "Preschool"
                        }
                    },
                    IsValidValue = (val) => { return !String.IsNullOrEmpty(val); }
                },
                 new HtmlType()
                {
                    Label = LabelDictionary[LabelList.Group],
                    HtmlMethod = Method.Input,
                    InputType = "text",
                    IsValidValue = (val) => { return !String.IsNullOrEmpty(val); }
                },
                 new HtmlType()
                {
                    Label = LabelDictionary[LabelList.Diagnosis],
                    HtmlMethod = Method.Select,
                    SelectObjects = new[]
                    {
                        new SelectObject()
                        {
                            Name = "ТНР",
                            Value = "TNR"
                        },
                        new SelectObject()
                        {
                            Name = "ЗПР",
                            Value = "ZPR"
                        }
                    },
                    IsValidValue = (val) => { return !String.IsNullOrEmpty(val); }
                }
            };
        }

        public static ChildInfo.GenderType? GenderConvert(string gender)
        {
            switch(gender)
            {
                case "Male":
                    return ChildInfo.GenderType.Male;
                case "Female":
                    return ChildInfo.GenderType.Female;
                default:
                    return null;
            }
        }

        public static string? GenderConvert(ChildInfo.GenderType? gender)
        {
            switch (gender)
            {
                case ChildInfo.GenderType.Male:
                    return "Мужской";
                case ChildInfo.GenderType.Female:
                    return "Женский";
                default:
                    return null;
            }
        }

        public static ChildInfo.AgeGroupType? AgeGroupConvert(string group)
        {
            switch(group)
            {
                case "Junior":
                    return ChildInfo.AgeGroupType.Junior;
                case "Middle":
                    return ChildInfo.AgeGroupType.Middle;
                case "Senior":
                    return ChildInfo.AgeGroupType.Senior;
                case "Preschool":
                    return ChildInfo.AgeGroupType.Preschool;
                default:
                    return null;
            }
        }

        public static string? AgeGroupConvert(ChildInfo.AgeGroupType? group)
        {
            switch (group)
            {
                case ChildInfo.AgeGroupType.Junior:
                    return "Младшая";
                case ChildInfo.AgeGroupType.Middle:
                    return "Средняя";
                case ChildInfo.AgeGroupType.Senior:
                    return "Старшая";
                case ChildInfo.AgeGroupType.Preschool:
                    return "Подготовительная";
                default:
                    return null;
            }
        }

        public static ChildInfo.DiagnosisType? DiagnosisConvert(string diagnosis)
        {
            switch(diagnosis)
            {
                case "TNR":
                    return ChildInfo.DiagnosisType.TNR;
                case "ZPR":
                    return ChildInfo.DiagnosisType.ZPR;
                default:
                    return null;
            }
        }

        public static string? DiagnosisConvert(ChildInfo.DiagnosisType? diagnosis)
        {
            switch (diagnosis)
            {
                case ChildInfo.DiagnosisType.TNR:
                    return "ТНР";
                case ChildInfo.DiagnosisType.ZPR:
                    return "ЗПР";
                default:
                    return null;
            }
        }
    }
}
