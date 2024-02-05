using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

public class ApiDbContext : DbContext, IDataProtectionKeyContext
{
    private readonly IConfiguration _configuration;

    public ApiDbContext(IConfiguration configuration)
        : base()
    {
        _configuration = configuration;
    }

    public virtual DbSet<Note> Notes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        optionsBuilder.UseNpgsql(connectionString);
    }

    public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
}
