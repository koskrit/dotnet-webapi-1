namespace Services
{
    public static class ServicesSetup
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddDbContext<ApiDbContext>();

            services.AddControllers();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
    }
}
