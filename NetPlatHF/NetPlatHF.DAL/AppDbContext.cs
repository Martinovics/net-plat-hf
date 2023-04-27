using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace NetPlatHF.DAL;




public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
    {

    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }

}
