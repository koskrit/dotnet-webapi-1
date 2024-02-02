namespace Services
{
    public static class ServicesSetup
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddDbContext<ApiDbContext>();

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

            services.AddControllers();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
    }
}
