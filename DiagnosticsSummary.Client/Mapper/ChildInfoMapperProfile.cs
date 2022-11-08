using AutoMapper;
using DiagnosticsSummary.Client.Models;
using DiagnosticsSummary.Client.Services;
using DiagnosticsSummary.Common.Models;
using static DiagnosticsSummary.Client.Services.HtmlChildService;

namespace DiagnosticsSummary.Client.Mapper
{
    public class ChildInfoMapperProfile: Profile
    {

        private int? MapAge(HtmlType[] fields)
        {
            var age = fields.Where(f => f.Label == LabelDictionary[LabelList.Age])
                    .FirstOrDefault().Value;
            if (int.TryParse(age, out var res))
            {
                return res;
            }
            return null;
        }
        public ChildInfoMapperProfile()
        {
            CreateMap<HtmlType[], Child>()
                .ForMember(info => info.FIO, opt => opt.MapFrom(fields =>
                fields.Where(f => f.Label == LabelDictionary[LabelList.FIO]).FirstOrDefault().Value))
                .ForMember(info => info.Gender, opt => opt.MapFrom(fields =>
                GenderConvert(
                fields.Where(f => f.Label == LabelDictionary[LabelList.Gender]).FirstOrDefault().Value)))
                .ForMember(info => info.Age, opt => opt.MapFrom(fields => MapAge(fields)))
                .ForMember(info => info.AgeGroup, opt => opt.MapFrom(fields =>
                AgeGroupConvert(
                fields.Where(f => f.Label == LabelDictionary[LabelList.AgeGroup]).FirstOrDefault().Value)))
                .ForMember(info => info.Group, opt => opt.MapFrom(fields =>
                fields.Where(f => f.Label == LabelDictionary[LabelList.Group]).FirstOrDefault().Value.ToLower()))
                .ForMember(info => info.Diagnosis, opt => opt.MapFrom(fields =>
                DiagnosisConvert(
                fields.Where(f => f.Label == LabelDictionary[LabelList.Diagnosis]).FirstOrDefault().Value)));
        }
    }
}
