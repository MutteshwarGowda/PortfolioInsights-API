
using IwMetrics.Application.UserProfiles.Queries;

namespace IwMetricsWorks.Api.Registrars
{
    public class BogardRegistrar : IWebApplicationBuilderRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(typeof(Program), typeof(GetAllUserProfile));
            builder.Services.AddMediatR(typeof(GetAllUserProfile));
        }
    }
}
