namespace IwMetricsWorks.Api.Contracts.Portfolio.Requests
{
    public record PortfolioCreateRequest
    {
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; init; } // Portfolio name


        [Required]
        public string RiskLevel { get; init; } 

    }
}
