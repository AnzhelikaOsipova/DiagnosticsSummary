using DiagnosticsSummary.Common.Contracts;
using DiagnosticsSummary.DataLayer;
using System.Reflection;

namespace DiagnosticsSummary.Services
{
    public class DiagnosticsService
    {
        public List<IDiagnosticInfo> diagnostics;
        private IDataAccess dataAccess;

        public DiagnosticsService(IDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
            var inst = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.GetInterfaces().Contains(typeof(IDiagnosticInfo))
                && t.GetConstructor(Type.EmptyTypes) is not null)
                .Select(t => Activator.CreateInstance(t) as IDiagnosticInfo);
            diagnostics = inst.ToList();
        }
    }
}
