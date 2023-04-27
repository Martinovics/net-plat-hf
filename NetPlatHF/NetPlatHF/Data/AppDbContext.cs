using Microsoft.EntityFrameworkCore;
using NetPlatHF.Models.Entities;

namespace NetPlatHF.Data;




public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }


    public DbSet<Exercise> Exercise { get; set; }

}
