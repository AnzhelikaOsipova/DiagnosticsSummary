using DiagnosticsSummary.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiagnosticsSummary.Services
{
    public class DiagnosticsSummaryManager
    {
        private ChildService childService;
        private DiagnosticsService diagnosticsService;

        public DiagnosticsSummaryManager(ChildService childService, DiagnosticsService diagnosticsService) 
        {
            this.childService = childService;
            this.diagnosticsService = diagnosticsService;
        }
        public enum ActionType
        {
            Create,
            Read,
            Update,
            Delete,
        }
        public (List<ChildInfo>? Result, string Message) UseAction(ActionType action, ChildInfo childInfo)
        {
            switch(action)
            {
                case ActionType.Read:
                    var children = childService.Find(childInfo.FIO, childInfo.Gender,
                        childInfo.Age, childInfo.AgeGroup, childInfo.Group, childInfo.Diagnosis);
                    children?.Sort((i, j) => String.Compare(i.FIO, j.FIO));
                    return (children, "Список детей.");
                case ActionType.Create:
                    childInfo.Id = 0;
                    if (childService.TryAdd(childInfo))
                    {
                        return (childService.Find(childInfo.FIO, childInfo.Gender,
                        childInfo.Age, childInfo.AgeGroup, childInfo.Group, childInfo.Diagnosis), "Успех");
                    }
                    else
                    {
                        return (null, "Неудача. Обратитесь в поддержку.");
                    }
                case ActionType.Delete:
                    if(childService.TryDelete(childInfo.Id))
                    {
                        return (null, "Успех");
                    }
                    else
                    {
                        return (null, "Неудача. Обратитесь в поддержку.");
                    }
                case ActionType.Update:
                    var child = childService.Find(childInfo.Id);
                    childInfo.GetType().GetProperties().ToList().ForEach(prop => {
                        if (prop.GetValue(childInfo) is not null)
                            prop.SetValue(child, prop.GetValue(childInfo));
                    });
                    if (childService.TryUpdate(childInfo.Id, child!))
                    {
                        return (new List<ChildInfo>() { childService.Find(childInfo.Id)! }, "Успех");
                    }
                    else
                    {
                        return (null, "Неудача. Обратитесь в поддержку.");
                    }
                default:
                    return (null, "Для данного запроса отсутствует реализация. Обратитесь в поддержку.");
            }
        }
    }
}
