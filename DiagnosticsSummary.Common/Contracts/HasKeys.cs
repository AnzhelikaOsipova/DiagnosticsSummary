using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiagnosticsSummary.Common.Contracts
{
    public interface IHasKeys
    {
        IEnumerable<object> Keys { get; }
    }
}
