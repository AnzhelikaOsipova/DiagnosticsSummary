using DiagnosticsSummary.Common.Models;
using DiagnosticsSummary.DataLayer;

namespace DiagnosticsSummary.Services
{
    public class ChildService
    {
        private IDataAccess dataAccess;

        public ChildService(IDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public bool TryAdd(ChildInfo newChild)
        {
            return dataAccess.TryAdd<ChildInfo,int>(newChild);
        }

        public bool TryUpdate(int id, ChildInfo newChild)
        {
            return dataAccess.TryUpdate(id, newChild);
        }

        public bool TryDelete(int id)
        {
            return dataAccess.TryDelete<ChildInfo, int>(id);
        }

        public ChildInfo? Find(int id)
        {
            if(!dataAccess.TryGet<ChildInfo, int>(out List<ChildInfo> children))
            {
                return null;
            }
            return children.Where(ch => ch.Id == id).FirstOrDefault();
        }

        public List<ChildInfo>? Find(string? FIO = null,
                                    ChildInfo.GenderType? gender = null,
                                    int? age = null,
                                    ChildInfo.AgeGroupType? ageGroup = null,
                                    string? group = null,
                                    ChildInfo.DiagnosisType? diagnosis = null)
        {
            if (!dataAccess.TryGet<ChildInfo, int>(out List<ChildInfo> children))
            {
                return null;
            }
            var query = children.AsQueryable();
            if (FIO is not null) { query = query.Where(ch => ch.FIO == FIO); }
            if (gender is not null) { query = query.Where(ch => ch.Gender == gender); }
            if (age is not null) { query = query.Where(ch => ch.Age == age); }
            if (ageGroup is not null) { query = query.Where(ch => ch.AgeGroup == ageGroup); }
            if (group is not null) { query = query.Where(ch => ch.Group == group); }
            if (diagnosis is not null) { query = query.Where(ch => ch.Diagnosis == diagnosis); }

            return query.ToList();
        }
    }
}
