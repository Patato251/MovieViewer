using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestProj.API.Models;

namespace TestProj.API.Data
{
  public class ValuesRespository : IValuesRespository
  {
    private readonly DataContext _context;

    public ValuesRespository(DataContext context)
    {
      _context = context;
    }

    public Task<IEnumerable<Value>> GetDivValue(int divisor)
    {
      return null;
    }

    public async Task<Value> GetValue(int id)
    {
      var value = await _context.Values.FirstOrDefaultAsync(x => x.Id == id); // Assign the first value where id matches the Id inside the dB

      return value;
    }

    public async Task<IEnumerable<Value>> GetValues()
    {
      var values = await _context.Values.ToListAsync();

      return values;
    }

    public async Task<Value> GetSpecValue(int assignedValue)
    {
      var specValue = await _context.Values.FirstOrDefaultAsync(z => z.AssignedValue == assignedValue);

      return specValue;
    }

    
  }
}