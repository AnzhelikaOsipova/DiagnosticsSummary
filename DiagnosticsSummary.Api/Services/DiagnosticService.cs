using AutoMapper;
using DiagnosticsSummary.Common.Models;
using DiagnosticsSummary.DAL;
using DiagnosticsSummary.DAL.Models;
using LanguageExt;
using System.Linq;

namespace DiagnosticsSummary.Api.Services
{
    public class DiagnosticService
    {
        private readonly IRepository<DiagnosticDb> diagnosticRepository;
        private IMapper mapper;

        public DiagnosticService(IRepository<DiagnosticDb> diagnosticRepository, IMapper mapper)
        {
            this.diagnosticRepository = diagnosticRepository;
            this.mapper = mapper;
        }

        private List<Diagnostic> UseFilters(List<Diagnostic> diagnostics, Diagnostic filter)
        {
            var query = diagnostics.AsQueryable();
            if (filter.ChildId is not null) query = query.Where(d => d.ChildId == filter.ChildId);
            if (filter.Name is not null) query = query.Where(d => d.Name == filter.Name);
            if (filter.Value is not null) query = query.Where(d => d.Value == filter.Value);
            if (filter.Year is not null) query = query.Where(d => d.Year == filter.Year);
            if (filter.YearPart is not null) query = query.Where(d => d.YearPart == filter.YearPart);
            return query.ToList();
        }

        public async Task<Either<Exception, List<Diagnostic>>> FindDiagnosticsAsync(Diagnostic filter) =>
            await diagnosticRepository.ReadAllAsync()
                .MapAsync(lddb => UseFilters(mapper.Map<List<Diagnostic>>(lddb), filter));

        public async Task<Option<Exception>> CreateAsync(Diagnostic newDiagnostic) =>
            await diagnosticRepository.CreateAsync(mapper.Map<DiagnosticDb>(newDiagnostic));

        private DiagnosticDb FillUpdated(Diagnostic dbDiagnostic, Diagnostic updatedDiagnostic)
        {
            updatedDiagnostic.ChildId ??= dbDiagnostic.ChildId;
            updatedDiagnostic.Name ??= dbDiagnostic.Name;
            updatedDiagnostic.Value ??= dbDiagnostic.Value;
            updatedDiagnostic.Year ??= dbDiagnostic.Year;
            updatedDiagnostic.YearPart ??= dbDiagnostic.YearPart;
            return mapper.Map<DiagnosticDb>(updatedDiagnostic);
        }

        public async Task<Option<Exception>> UpdateAsync(Diagnostic updatedDiagnostic) =>
            await (await diagnosticRepository.ReadAsync(updatedDiagnostic.Id))
                .MatchAsync(
                ddb => diagnosticRepository.UpdateAsync(FillUpdated(mapper.Map<Diagnostic>(ddb), updatedDiagnostic)),
                e => e
                );

        public async Task<Option<Exception>> DeleteManyAsync(IEnumerable<int> ids)
        {
            var dels = (await Task.WhenAll(ids.ToList().Select(id => diagnosticRepository.DeleteAsync(id))));
            if (dels.Any(del => del.IsSome)) return new Exception();
            return Option<Exception>.None;
        }

        public async Task<Option<Exception>> DeleteAsync(int diagnosticid) =>
            await diagnosticRepository.DeleteAsync(diagnosticid);
    }
}
