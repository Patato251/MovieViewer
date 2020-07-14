using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using TestProj.API.Models;

namespace TestProj.API.Data
{
  // Seed Class 
  public class Seed
  {
    // Seeding Values
    public static void SeedValues(DataContext context)
    {
      // Check if existing data exists
      if (!context.Values.Any())
      {
        var valueData = System.IO.File.ReadAllText("Data/ValueSeedData.json");
        var values = JsonConvert.DeserializeObject<List<Value>>(valueData);
        foreach (var value in values)
        {
          context.Values.Add(value);
        }
        // Save changes as required 
        context.SaveChanges();
      }
    }

    // Seeding Users
    public static void SeedUsers(DataContext context)
    {
      // Check if existing data exists
      if (!context.Persons.Any())
      {
        var userData = System.IO.File.ReadAllText("Data/PersonSeedData.json");
        var users = JsonConvert.DeserializeObject<List<Person>>(userData);
        foreach (var user in users)
        {
          context.Persons.Add(user);
        }
        // Save changes as required 
        context.SaveChanges();
      }      
    }
  }
}