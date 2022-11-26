using AutoMapper;
using DiagnosticsSummary.Client.Models;
using DiagnosticsSummary.Common.Models;
using static DiagnosticsSummary.Common.ChildFieldsMappingLibrary;
using static DiagnosticsSummary.Client.Services.HtmlChildService;
using DiagnosticsSummary.Client.Models.Input;

namespace DiagnosticsSummary.Client.Mapper
{
    public class ChildMapperProfile: Profile
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
        public ChildMapperProfile()
        {
            CreateMap<HtmlType[], ChildFilter>()
                .ForMember(info => info.FIO, opt => opt.MapFrom(fields =>
                fields.Where(f => f.Label == LabelDictionary[LabelList.FIO]).FirstOrDefault().Value))
                .ForMember(info => info.Gender, opt => opt.MapFrom(fields =>
                GenderDictionary.FirstOrDefault(x => x.Value ==
                    fields.Where(f => f.Label == LabelDictionary[LabelList.Gender]).FirstOrDefault().Value
                    ).Key
                ))
                .ForMember(info => info.Age, opt => opt.MapFrom(fields => MapAge(fields)))
                .ForMember(info => info.AgeGroup, opt => opt.MapFrom(fields =>
                AgeGroupDictionary.FirstOrDefault(x => x.Value ==
                    fields.Where(f => f.Label == LabelDictionary[LabelList.AgeGroup]).FirstOrDefault().Value
                    ).Key
                ))
                .ForMember(info => info.Group, opt => opt.MapFrom(fields =>
                fields.Where(f => f.Label == LabelDictionary[LabelList.Group]).FirstOrDefault().Value.ToLower()))
                .ForMember(info => info.Diagnosis, opt => opt.MapFrom(fields =>
                DiagnosisDictionary.FirstOrDefault(x => x.Value ==
                    fields.Where(f => f.Label == LabelDictionary[LabelList.Diagnosis]).FirstOrDefault().Value
                    ).Key
                ));
            
            CreateMap<ChildView, Child>()
                .ForMember(ch => ch.Gender, opt => opt.MapFrom(chv => GenderDictionary.FirstOrDefault(
                    x => x.Value == chv.Gender).Key))
                .ForMember(ch => ch.AgeGroup, opt => opt.MapFrom(chv => AgeGroupDictionary.FirstOrDefault(
                    x => x.Value == chv.AgeGroup).Key))
                .ForMember(ch => ch.Group, opt => opt.MapFrom(chv => chv.Group.ToLower()))
                .ForMember(ch => ch.Diagnosis, opt => opt.MapFrom(chv => DiagnosisDictionary.FirstOrDefault(
                    x => x.Value == chv.Diagnosis).Key))
                .ReverseMap()
                .ForMember(chv => chv.Gender, opt => opt.MapFrom(
                    ch => GenderDictionary.GetValueOrDefault(ch.Gender)))
                .ForMember(chv => chv.AgeGroup, opt => opt.MapFrom(
                    ch => AgeGroupDictionary.GetValueOrDefault(ch.AgeGroup)))
                .ForMember(chv => chv.Group, opt => opt.MapFrom(ch => ch.Group.Substring(0, 1).ToUpper() +
                ch.Group.Substring(1, ch.Group.Length - 1).ToLower()))
                .ForMember(chv => chv.Diagnosis, opt => opt.MapFrom(
                    ch => DiagnosisDictionary.GetValueOrDefault(ch.Diagnosis)));
        }
    }
}
