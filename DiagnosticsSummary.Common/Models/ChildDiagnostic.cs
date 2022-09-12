using DiagnosticsSummary.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiagnosticsSummary.Common.Models
{
    public class ChildDiagnostic: IHasIdProperty<int>
    {
        public int Id { get; set; }
        public int ChildId { get; set; }
        public string DiagnosticName { get; set; }
        public int Value { get; set; }
        public int Year { get; set; }
        public YearTime YearPart { get; set; } 

        public enum YearTime
        {
            Start,
            End
        }
    }
}
