using IdentityServer4.AccessTokenValidation;
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
                        options.RequireHttpsMetadata = false;

                        options.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidateAudience = false
                        };
                    }
                );

            services.AddCors(options =>
            {
                options.AddPolicy(
                    "AllowAllOrigins",
                    policy =>
                    {
                        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
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
