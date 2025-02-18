
namespace IwMetricsWorks.Api.Registrars
{
    public class IdentityRegistrar : IWebApplicationBuilderRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            var jwtSettings = new JwtSettings();
            builder.Configuration.Bind(nameof(JwtSettings), jwtSettings);

            var jwtSection = builder.Configuration.GetSection(nameof(JwtSettings));
            builder.Services.Configure<JwtSettings>(jwtSection);

            builder.Services
                .AddAuthentication(a =>
                {
                    a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    a.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(jwt =>
                {
                    jwt.SaveToken = true;
                    jwt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.SigningKey)),
                        ValidateIssuer = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidateAudience = true,
                        ValidAudiences = jwtSettings.Audiences,
                        RequireExpirationTime = false,
                        ValidateLifetime = true
                    };
                    jwt.Audience = jwtSettings.Audiences[0];
                    jwt.ClaimsIssuer = jwtSettings.Issuer;

                });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireExternalUser", policy =>
                    policy.RequireClaim("ExternalRole", "Client"));

                options.AddPolicy("RequireInternalUser", policy =>
                    policy.RequireClaim("InternalRole", "Admin", "PortfolioManager"));

                options.AddPolicy("RequireAdminUser", policy =>
                    policy.RequireClaim("InternalRole", "Admin"));

                options.AddPolicy("RequirePortfolioManagerUser", policy =>
                    policy.RequireClaim("InternalRole", "PortfolioManager"));

                options.AddPolicy("RequireAnyUser", policy =>
                    policy.RequireAssertion(context =>
                        context.User.HasClaim(c => c.Type == "ExternalRole" && c.Value == "Client") ||
                        context.User.HasClaim(c => c.Type == "InternalRole" &&
                            (c.Value == "Admin" || c.Value == "PortfolioManager"))
                    ));
            });
        }
    }
}
