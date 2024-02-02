using Microsoft.EntityFrameworkCore;
using Models;

public class ApiDbContext : DbContext
{
    public virtual DbSet<Note> Notes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("MyDatabase");
    }
}
