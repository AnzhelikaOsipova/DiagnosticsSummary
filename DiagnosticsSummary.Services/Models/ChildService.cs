using DiagnosticsSummary.Common.Models;
using DiagnosticsSummary.DAL;
using LanguageExt;
using AutoMapper;
using DiagnosticsSummary.DAL.Models;
using DiagnosticsSummary.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DiagnosticsSummary.Services.Models
{
    public class ChildService: IChildService
    {
        private readonly IRepository<ChildDb> _childRepository;
        private readonly IMapper _mapper;

        public ChildService(IRepository<ChildDb> childRepository, IMapper mapper)
        {
            _childRepository = childRepository;
            _mapper = mapper;
        }

        private List<Child> UseFiltersAsync(List<Child> children, ChildFilter filter)
        {
            if (filter.FIO is not null) children = children.Where(ch => ch.FIO == filter.FIO).ToList();
            if (filter.Gender is not null) children = children.Where(ch => ch.Gender == filter.Gender).ToList();
            if (filter.Age is not null) children = children.Where(ch => ch.Age == filter.Age).ToList();
            if (filter.AgeGroup is not null) children = children.Where(ch => ch.AgeGroup == filter.AgeGroup).ToList();
            if (filter.Group is not null) children = children.Where(ch => ch.Group == filter.Group).ToList();
            if (filter.Diagnosis is not null) children = children.Where(ch => ch.Diagnosis == filter.Diagnosis).ToList();
            return children.ToList();
        }

        public async Task<Either<Exception, List<Child>>> FindChildrenAsync(ChildFilter filter)
        {
            return await _childRepository.ReadAllAsync()
                .MapAsync(children => UseFiltersAsync(_mapper.Map<List<Child>>(children), filter));
        }

        public async Task<Option<Exception>> CreateAsync(Child newChild)
        {
            return await _childRepository.CreateAsync(_mapper.Map<ChildDb>(newChild));
        }

        private ChildDb FillUpdated(Child dbChild, ChildFilter updates)
        {
            var updatedChild = new Child()
            {
                Id = dbChild.Id,
                FIO = updates.FIO ?? dbChild.FIO,
                Gender = updates.Gender ?? dbChild.Gender,
                Age = updates.Age ?? dbChild.Age,
                AgeGroup = updates.AgeGroup ?? dbChild.AgeGroup,
                Group = updates.Group ?? dbChild.Group,
                Diagnosis = updates.Diagnosis ?? dbChild.Diagnosis
            };
            return _mapper.Map<ChildDb>(updatedChild);
        }

        public async Task<Option<Exception>> UpdateAsync(int childId, ChildFilter updates)
        {
            var childResult = await _childRepository.ReadAsync(childId);
            return await childResult
                .MatchAsync(
                ch => _childRepository.UpdateAsync(FillUpdated(_mapper.Map<Child>(ch), updates)),
                e => e
                );
        }

        public async Task<Option<Exception>> DeleteAsync(int childid)
        {
            return await _childRepository.DeleteAsync(childid);
        }
    }
}
