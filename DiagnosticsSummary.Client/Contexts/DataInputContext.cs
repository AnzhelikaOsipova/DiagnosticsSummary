using DiagnosticsSummary.Client.Models.Input;

namespace DiagnosticsSummary.Client.Contexts
{
    public static class DataInputContext
    {
        public static string OkNavigateTo { get; set; }
        public static string BackNavigateTo { get; set; }
        public static HtmlType[] Fields { get; set; } = new HtmlType[0];
        public static bool AreNullsAvailable { get; set; }

        public static void FillContext(string okNavigateTo, string backNavigateTo, 
            bool areNullsAvailable, HtmlType[] fillFields)
        {
            OkNavigateTo = okNavigateTo;
            BackNavigateTo = backNavigateTo;
            AreNullsAvailable = areNullsAvailable;
            Fields = fillFields;
        }
    }
}
