var builder = WebApplication.CreateBuilder(args);

var Services = builder.Services;

Services.AddDbContext<ApiDbContext>();

Services.AddControllers();

Services.AddEndpointsApiExplorer();
Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();

app.UseHttpsRedirection();

app.Run();
