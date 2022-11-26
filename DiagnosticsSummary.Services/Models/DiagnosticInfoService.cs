using AutoMapper;
using DiagnosticsSummary.Common.Models;
using DiagnosticsSummary.DAL;
using DiagnosticsSummary.DAL.Models;
using DiagnosticsSummary.Services.Contracts;
using LanguageExt;

namespace DiagnosticsSummary.Services.Models
{
    public class DiagnosticInfoService: IDiagnosticInfoService
    {
        private readonly IRepository<DiagnosticInfoDb> _diagnosticRepository;
        private readonly IMapper _mapper;

        public DiagnosticInfoService(IRepository<DiagnosticInfoDb> diagnosticRepository, IMapper mapper)
        {
            _diagnosticRepository = diagnosticRepository;
            _mapper = mapper;
        }

        public async Task<Either<Exception, DiagnosticInfo>> FindAsync(string name)
        {
            return await _diagnosticRepository.ReadAsync(name).MapAsync(d => _mapper.Map<DiagnosticInfo>(d));
        }

        public async Task<Either<Exception, List<DiagnosticInfo>>> GetAllAsync()
        {
            return await _diagnosticRepository.ReadAllAsync().MapAsync(d => _mapper.Map<List<DiagnosticInfo>>(d));
        }

        public async Task<Option<Exception>> CreateAsync(DiagnosticInfo newDiagnostic)
        {
            return await _diagnosticRepository.CreateAsync(_mapper.Map<DiagnosticInfoDb>(newDiagnostic));
        }

        public async Task<Option<Exception>> UpdateAsync(DiagnosticInfo updatedDiagnostic)
        {
            return await _diagnosticRepository.UpdateAsync(_mapper.Map<DiagnosticInfoDb>(updatedDiagnostic));
        }

        public async Task<Option<Exception>> DeleteAsync(string name)
        {
            return await _diagnosticRepository.DeleteAsync(name);
        }

    }
}
