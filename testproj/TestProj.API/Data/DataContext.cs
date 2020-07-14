using Microsoft.EntityFrameworkCore;
using TestProj.API.Models;

namespace TestProj.API.Data
{
  public class DataContext : DbContext
  {
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    public DbSet<Value> Values { get; set; } // Generate Values table
    public DbSet<Person> Persons { get; set; } // Generate Seed Person Table
  }
}