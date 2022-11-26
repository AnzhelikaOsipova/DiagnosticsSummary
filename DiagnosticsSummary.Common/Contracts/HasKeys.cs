
namespace DiagnosticsSummary.Common.Contracts
{
    public interface IHasKeys
    {
        IEnumerable<object> Keys { get; }
    }
}
