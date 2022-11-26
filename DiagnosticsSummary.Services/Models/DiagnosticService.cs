using AutoMapper;
using DiagnosticsSummary.Common.Models;
using DiagnosticsSummary.DAL;
using DiagnosticsSummary.DAL.Models;
using DiagnosticsSummary.Services.Contracts;
using LanguageExt;

namespace DiagnosticsSummary.Services.Models
{
    public class DiagnosticService: IDiagnosticService
    {
        private readonly IRepository<DiagnosticDb> _diagnosticRepository;
        private readonly IMapper _mapper;

        public DiagnosticService(IRepository<DiagnosticDb> diagnosticRepository, IMapper mapper)
        {
            _diagnosticRepository = diagnosticRepository;
            _mapper = mapper;
        }

        private List<Diagnostic> UseFiltersAsync(List<Diagnostic> diagnostics, DiagnosticFilter filter)
        {
            var query = diagnostics.AsQueryable();
            if (filter.ChildId is not null) query = query.Where(d => d.ChildId == filter.ChildId);
            if (filter.Name is not null) query = query.Where(d => d.Name == filter.Name);
            if (filter.Value is not null) query = query.Where(d => d.Value == filter.Value);
            if (filter.Year is not null) query = query.Where(d => d.Year == filter.Year);
            if (filter.YearPart is not null) query = query.Where(d => d.YearPart == filter.YearPart);
            return query.ToList();
        }

        public async Task<Either<Exception, List<Diagnostic>>> FindDiagnosticsAsync(DiagnosticFilter filter)
        {
            return await _diagnosticRepository.ReadAllAsync()
                .MapAsync(lddb => UseFiltersAsync(_mapper.Map<List<Diagnostic>>(lddb), filter));
        }

        public async Task<Option<Exception>> CreateAsync(Diagnostic newDiagnostic)
        {
            return await _diagnosticRepository.CreateAsync(_mapper.Map<DiagnosticDb>(newDiagnostic));
        }

        private DiagnosticDb FillUpdated(Diagnostic dbDiagnostic, DiagnosticFilter updates)
        {
            var updatedDiagnostic = new Diagnostic()
            {
                Id = dbDiagnostic.Id,
                ChildId = updates.ChildId ?? dbDiagnostic.Id,
                Name = updates.Name ?? dbDiagnostic.Name,
                Value = updates.Value ?? dbDiagnostic.Value,
                Year = updates.Year ?? dbDiagnostic.Year,
                YearPart = updates.YearPart ?? dbDiagnostic.YearPart
            };
            return _mapper.Map<DiagnosticDb>(updatedDiagnostic);
        }

        public async Task<Option<Exception>> UpdateAsync(int diagnosticid, DiagnosticFilter updates)
        {
            var diagnosticResult = await _diagnosticRepository.ReadAsync(diagnosticid);
            return await diagnosticResult
                .MatchAsync(
                ddb => _diagnosticRepository.UpdateAsync(FillUpdated(_mapper.Map<Diagnostic>(ddb), updates)),
                e => e
                );
        }

        public async Task<Option<Exception>> DeleteManyAsync(IEnumerable<int> ids)
        {
            Exception error = null;
            var dels = await Task.WhenAll(ids.ToList().Select(id => _diagnosticRepository.DeleteAsync(id)));

            if (dels.Any(del =>
            {
                del.IfSome(e => error = e);
                return del.IsSome;
            }))
            {
                return error;
            }

            return Option<Exception>.None;
        }

        public async Task<Option<Exception>> DeleteAsync(int diagnosticid)
        {
            return await _diagnosticRepository.DeleteAsync(diagnosticid);
        }
    }
}
