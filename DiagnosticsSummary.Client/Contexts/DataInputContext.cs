using DiagnosticsSummary.Client.Models;

namespace DiagnosticsSummary.Client.Contexts
{
    public static class DataInputContext
    {
        public static string OkNavigateTo { get; set; }
        public static HtmlType[] Fields { get; set; } = new HtmlType[0];
        public static bool AreNullsAvailable { get; set; }

        public static void FillContext(string navigateTo, bool areNullsAvailable, Func<HtmlType[]> fillFields)
        {
            OkNavigateTo = navigateTo;
            AreNullsAvailable = areNullsAvailable;
            Fields = fillFields();
        }
    }
}
