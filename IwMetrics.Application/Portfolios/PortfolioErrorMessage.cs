
namespace IwMetrics.Application.Portfolios
{
    public class PortfolioErrorMessage
    {
        public const string NotFound = "No Portfolio Found with ID {0}";
        public const string PortfolioUpdateNotPossible = "Post update not possible b'cos it's not the owner that initiates the update";
        public const string ManagerUnmatched = "Portfolio manager is mismatch";
        public const string ManagerNotFound = "No Manager Found with ID {0}";
    }
}
