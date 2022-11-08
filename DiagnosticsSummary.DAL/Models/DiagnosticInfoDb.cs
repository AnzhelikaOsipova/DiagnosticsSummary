using DiagnosticsSummary.Common.Contracts;

namespace DiagnosticsSummary.DAL.Models
{
    public class DiagnosticInfoDb: IHasKeys
    {
        public string Name { get; set; }
        public string ValueInterpreter { get; set; }
        public IEnumerable<object> Keys => new object[] { Name };
    }
}
