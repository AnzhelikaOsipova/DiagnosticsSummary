using DiagnosticsSummary.Common.Models;
using DiagnosticsSummary.DAL;
using LanguageExt;
using AutoMapper;
using DiagnosticsSummary.DAL.Models;

namespace DiagnosticsSummary.Api.Services
{
    public class ChildService
    {
        private readonly IRepository<ChildDb> childRepository;
        private IMapper mapper;

        public ChildService(IRepository<ChildDb> childRepository, IMapper mapper)
        {
            this.childRepository = childRepository;
            this.mapper = mapper;
        }

        private List<Child> UseFilters(List<Child> children, Child filter)
        {
            var query = children.AsQueryable();
            if (filter.FIO is not null) query = query.Where(ch => ch.FIO == filter.FIO);
            if (filter.Gender is not null) query = query.Where(ch => ch.Gender == filter.Gender);
            if (filter.Age is not null) query = query.Where(ch => ch.Age == filter.Age);
            if (filter.AgeGroup is not null) query = query.Where(ch => ch.AgeGroup == filter.AgeGroup);
            if (filter.Group is not null) query = query.Where(ch => ch.Group == filter.Group);
            if (filter.Diagnosis is not null) query = query.Where(ch => ch.Diagnosis == filter.Diagnosis);
            return query.ToList();
        }

        public async Task<Either<Exception, List<Child>>> FindChildrenAsync(Child filter) =>
            await childRepository.ReadAllAsync().MapAsync(children => UseFilters(mapper.Map<List<Child>>(children), filter));

        public async Task<Option<Exception>> CreateAsync(Child newChild) =>
            await childRepository.CreateAsync(mapper.Map<ChildDb>(newChild));

        private ChildDb FillUpdated(Child dbChild, Child updatedChild)
        {
            updatedChild.FIO ??= dbChild.FIO;
            updatedChild.Gender ??= dbChild.Gender;
            updatedChild.Age ??= dbChild.Age;
            updatedChild.AgeGroup ??= dbChild.AgeGroup;
            updatedChild.Group ??= dbChild.Group;
            updatedChild.Diagnosis ??= dbChild.Diagnosis;
            return mapper.Map<ChildDb>(updatedChild);
        }

        public async Task<Option<Exception>> UpdateAsync(Child updatedChild) =>
            await (await childRepository.ReadAsync(updatedChild.Id))
                .MatchAsync(
                ch => childRepository.UpdateAsync(FillUpdated(mapper.Map<Child>(ch), updatedChild)),
                e => e
                );

        public async Task<Option<Exception>> DeleteAsync(int childid) =>
            await childRepository.DeleteAsync(childid);
    }
}
