namespace DiagnosticsSummary.Api.Models
{
    public static class ApiRoutesConstants
    {
        public static class ControllersRoutes
        {
            public const string
                ChildApi = "api/children",
                DiagnosticInfoApi = "api/diagnosticinfo",
                DiagnosticApi = "api/childdiagnostic";
        }
        public static class Child
        {
            public const string
                Find = "find",
                Create = "create",
                Update = "update",
                Delete = "delete/{id}";
        }

        public static class DiagnosticInfo
        {
            public const string
                Find = "find/{name}",
                GetAll = "getall",
                Create = "create",
                Update = "update",
                Delete = "delete/{name}";
        }

        public static class Diagnostic
        {
            public const string
                Find = "find",
                Create = "create",
                Update = "update",
                Delete = "delete/{id}";
        }
    }
}
