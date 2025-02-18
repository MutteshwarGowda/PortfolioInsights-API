namespace IwMetricsWorks.Api
{
    public class ApiRoutes
    {
        public const string BaseRoute = "api/v{version:apiVersion}/[controller]";

        public static class UserProfile
        {
            public const string IdRoute = "{userProfileId}";
        }

        public static class Identity
        {
            public const string Login = "login";
            public const string Registration = "registration";
        }

        public static class Portfolio
        {
            public const string IdRoute = "{portfolioId}";
            public const string CreatePortfolio = "portfolio";
        }

        public static class Assets
        {
            public const string IdRoute = "{assetId}";
            public const string CreateAsset = "asset";
        }
    }
}
