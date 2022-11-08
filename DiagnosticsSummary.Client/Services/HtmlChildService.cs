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

        public static Child.GenderType? GenderConvert(string gender)
        {
            switch(gender)
            {
                case "Male":
                    return Child.GenderType.Male;
                case "Female":
                    return Child.GenderType.Female;
                default:
                    return null;
            }
        }

        public static string? GenderConvert(Child.GenderType? gender)
        {
            switch (gender)
            {
                case Child.GenderType.Male:
                    return "Мужской";
                case Child.GenderType.Female:
                    return "Женский";
                default:
                    return null;
            }
        }

        public static Child.AgeGroupType? AgeGroupConvert(string group)
        {
            switch(group)
            {
                case "Junior":
                    return Child.AgeGroupType.Junior;
                case "Middle":
                    return Child.AgeGroupType.Middle;
                case "Senior":
                    return Child.AgeGroupType.Senior;
                case "Preschool":
                    return Child.AgeGroupType.Preschool;
                default:
                    return null;
            }
        }

        public static string? AgeGroupConvert(Child.AgeGroupType? group)
        {
            switch (group)
            {
                case Child.AgeGroupType.Junior:
                    return "Младшая";
                case Child.AgeGroupType.Middle:
                    return "Средняя";
                case Child.AgeGroupType.Senior:
                    return "Старшая";
                case Child.AgeGroupType.Preschool:
                    return "Подготовительная";
                default:
                    return null;
            }
        }

        public static Child.DiagnosisType? DiagnosisConvert(string diagnosis)
        {
            switch(diagnosis)
            {
                case "TNR":
                    return Child.DiagnosisType.TNR;
                case "ZPR":
                    return Child.DiagnosisType.ZPR;
                default:
                    return null;
            }
        }

        public static string? DiagnosisConvert(Child.DiagnosisType? diagnosis)
        {
            switch (diagnosis)
            {
                case Child.DiagnosisType.TNR:
                    return "ТНР";
                case Child.DiagnosisType.ZPR:
                    return "ЗПР";
                default:
                    return null;
            }
        }
    }
}
