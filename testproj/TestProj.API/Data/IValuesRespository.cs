using System.Collections.Generic;
using System.Threading.Tasks;
using TestProj.API.Models;

namespace TestProj.API.Data
{
  public interface IValuesRespository
  {
    /* Method Declaration */
    // Get All Values
    Task<IEnumerable<Value>> GetValues(); 
    
    // Get single Value
    Task<Value> GetValue(int id);
    
    // Get values divisible by a singular value
    Task<IEnumerable<Value>> GetDivValue(int divisor);
    
    // Get single value according to it's assigned Number
    Task<Value> GetSpecValue(int assignedValue);
  }
}