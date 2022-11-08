using AutoMapper;
using DiagnosticsSummary.Common;
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
                .ForMember(chdb => chdb.FIO, opt => opt.MapFrom(ch => ch.FIO!))
                .ForMember(chdb => chdb.Gender, opt => opt.MapFrom(
                    ch => GenderDictionary.GetValueOrDefault(ch.Gender!.Value)))
                .ForMember(chdb => chdb.Age, opt => opt.MapFrom(ch => ch.Age!))
                .ForMember(chdb => chdb.AgeGroup, opt => opt.MapFrom(
                    ch => AgeGroupDictionary.GetValueOrDefault(ch.AgeGroup!.Value)))
                .ForMember(chdb => chdb.Group, opt => opt.MapFrom(ch => ch.Group!))
                .ForMember(chdb => chdb.Diagnosis, opt => opt.MapFrom(
                    ch => DiagnosisDictionary.GetValueOrDefault(ch.Diagnosis!.Value)));

            CreateMap<DiagnosticInfoDb, DiagnosticInfo>().ReverseMap();

            CreateMap<DiagnosticDb, Diagnostic>()
                .ForMember(d => d.YearPart, opt => opt.MapFrom(ddb => YearPartDictionary.FirstOrDefault(
                    x => x.Value == ddb.YearPart).Key))
                //.ForMember(d => d.InterpretedValue, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(ddb => ddb.ChildId, opt => opt.MapFrom(d => d.ChildId!))
                .ForMember(ddb => ddb.Name, opt => opt.MapFrom(d => d.Name!))
                .ForMember(ddb => ddb.Value, opt => opt.MapFrom(d => d.Value!))
                .ForMember(ddb => ddb.Year, opt => opt.MapFrom(d => d.Year!))
                .ForMember(ddb => ddb.YearPart, opt => opt.MapFrom(
                    d => YearPartDictionary.GetValueOrDefault(d.YearPart!.Value)));
        }
    }
}
