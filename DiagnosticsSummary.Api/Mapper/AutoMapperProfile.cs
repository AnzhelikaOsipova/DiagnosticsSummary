using AutoMapper;
using DiagnosticsSummary.Common.Models;
using DiagnosticsSummary.DAL.Models;
using static DiagnosticsSummary.Common.ChildFieldsMappingLibrary;
using static DiagnosticsSummary.Common.DiagnosticFieldsMappingLibrary;

namespace DiagnosticsSummary.Api.Mapper
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<ChildDb, Child>()
                .ForMember(ch => ch.Gender, opt => opt.MapFrom(chdb => GenderDictionary.FirstOrDefault(
                    x => x.Value == chdb.Gender).Key))
                .ForMember(ch => ch.AgeGroup, opt => opt.MapFrom(chdb => AgeGroupDictionary.FirstOrDefault(
                    x => x.Value == chdb.AgeGroup).Key))
                .ForMember(ch => ch.Diagnosis, opt => opt.MapFrom(chdb => DiagnosisDictionary.FirstOrDefault(
                    x => x.Value == chdb.Diagnosis).Key))
                .ReverseMap()
                .ForMember(chdb => chdb.Gender, opt => opt.MapFrom(
                    ch => GenderDictionary.GetValueOrDefault(ch.Gender)))
                .ForMember(chdb => chdb.AgeGroup, opt => opt.MapFrom(
                    ch => AgeGroupDictionary.GetValueOrDefault(ch.AgeGroup)))
                .ForMember(chdb => chdb.Diagnosis, opt => opt.MapFrom(
                    ch => DiagnosisDictionary.GetValueOrDefault(ch.Diagnosis)));

            CreateMap<DiagnosticInfoDb, DiagnosticInfo>().ReverseMap();

            CreateMap<DiagnosticDb, Diagnostic>()
                .ForMember(d => d.YearPart, opt => opt.MapFrom(ddb => YearPartDictionary.FirstOrDefault(
                    x => x.Value == ddb.YearPart).Key))
                .ReverseMap()
                .ForMember(ddb => ddb.YearPart, opt => opt.MapFrom(
                    d => YearPartDictionary.GetValueOrDefault(d.YearPart)));
        }
    }
}
