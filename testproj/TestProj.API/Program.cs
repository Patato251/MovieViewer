using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TestProj.API.Data;

namespace TestProj.API
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var host = CreateHostBuilder(args).Build();

      // Add the Seeding method calls and import only context as required by the scope of the section
      using (var scope = host.Services.CreateScope())
      {
        var services = scope.ServiceProvider;
        // Try catch block to prevent issues from occuring during seeding process
        try
        {
          // Assign context 
          var context = services.GetRequiredService<DataContext>();
          // Migrate the database and create if the database doesn't exist
          context.Database.Migrate();
          // Call the seed function 
          Seed.SeedValues(context);
          Seed.SeedUsers(context);
        }
        catch (Exception ex)
        {
          var logger = services.GetRequiredService<ILogger<Program>>();
          logger.LogError(ex, "An error occurred during the migration process");
        }
      }
      host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
              webBuilder.UseStartup<Startup>();
            });
  }
}
