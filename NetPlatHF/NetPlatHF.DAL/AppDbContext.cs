using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace NetPlatHF.DAL;




public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }

}




public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    private readonly string _connectionString = "";  // ide kell egy connection string, amikor migrálunk meg ilyenek | ne maradjon benne
    // mivel paraméternélküli konstruktor kell, ezért át se lehet neki nagyon adni a stringet vagy optionst

    public AppDbContext CreateDbContext(string[] args)
    {
        if (_connectionString.Equals(""))
        {
            Console.WriteLine();
            throw new ArgumentException("Provide a valid connection string");
        }

        var builder = new DbContextOptionsBuilder<AppDbContext>();
        builder.UseSqlServer(_connectionString);  
        return new AppDbContext(builder.Options);
    }
}
