using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Tokens;

namespace Services
{
    public static class ServicesSetup
    {
        public static void AddServices(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddDbContext<ApiDbContext>();
            services
                .AddDataProtection()
                .PersistKeysToDbContext<ApiDbContext>()
                .SetApplicationName("IdentityServer");

            var identityServerSettings = configuration
                .GetSection("IdentityServer")
                .Get<IdentityServerSettings>();

            services
                .AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddJwtBearer(
                    "Bearer",
                    options =>
                    {
                        options.Authority = identityServerSettings.Authority;
                        options.Audience = identityServerSettings.Audience;

                        options.TokenValidationParameters = new TokenValidationParameters() { };
                    }
                );

            services.AddCors(options =>
            {
                options.AddPolicy(
                    "CorsPolicy",
                    corsBuilder =>
                    {
                        var allowedOrigins = configuration
                            .GetSection("AllowedOrigins")
                            .Get<List<string>>();

                        corsBuilder
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .SetIsOriginAllowed(origin => allowedOrigins.Contains(origin))
                            .AllowCredentials();
                    }
                );
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    "ApiScope",
                    policy =>
                    {
                        policy.RequireAuthenticatedUser();
                        policy.RequireClaim("scope", "api1");
                    }
                );
            });

            services.AddControllers();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
    }
}
