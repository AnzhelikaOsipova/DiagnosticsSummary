using AutoMapper;
using DiagnosticsSummary.Common.Models;
using DiagnosticsSummary.DAL;
using DiagnosticsSummary.DAL.Models;
using LanguageExt;

namespace DiagnosticsSummary.Api.Services
{
    public class DiagnosticInfoService
    {
        private readonly IRepository<DiagnosticInfoDb> diagnosticRepository;
        private IMapper mapper;

        public DiagnosticInfoService(IRepository<DiagnosticInfoDb> diagnosticRepository, IMapper mapper)
        {
            this.diagnosticRepository = diagnosticRepository;
            this.mapper = mapper;
        }

        public async Task<Either<Exception, DiagnosticInfo>> FindAsync(string name) =>
            (await diagnosticRepository.ReadAsync(name))
            .Map(d => mapper.Map<DiagnosticInfo>(d));

        public async Task<Either<Exception, List<DiagnosticInfo>>> GetAllAsync() =>
            (await diagnosticRepository.ReadAllAsync())
            .Map(d => mapper.Map<List<DiagnosticInfo>>(d));

        public async Task<Option<Exception>> CreateAsync(DiagnosticInfo newDiagnostic) =>
            await diagnosticRepository.CreateAsync(mapper.Map<DiagnosticInfoDb>(newDiagnostic));

        public async Task<Option<Exception>> UpdateAsync(DiagnosticInfo updatedDiagnostic) =>
            await diagnosticRepository.UpdateAsync(mapper.Map<DiagnosticInfoDb>(updatedDiagnostic));

        public async Task<Option<Exception>> DeleteAsync(string name) =>
            await diagnosticRepository.DeleteAsync(name);

    }
}
