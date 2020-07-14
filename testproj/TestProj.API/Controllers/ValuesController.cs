using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TestProj.API.Data;

namespace TestProj.API.Controllers
{
  [Route("api/[controller]")] //localhost:5000/api/Values
  [ApiController]
  public class ValuesController : ControllerBase
  {
    private readonly IValuesRespository _repo;
    private readonly IConfiguration _configuration;

    public ValuesController(IValuesRespository repo, IConfiguration configuration)
    {
      _repo = repo;
      _configuration = configuration;

    }

    // GET api/values
    // List values method to display all values within values table (Async)
    [HttpGet]
    public async Task<IActionResult> ListValues()
    {
      var list = await _repo.GetValues();

      return Ok(list);
    }

    // GET api/values/"id"
    // Show value method to display specific value within values table according to Id (Async)
    [HttpGet("{id}")]
    public async Task<IActionResult> ShowValue(int id)
    {
      var target = await _repo.GetValue(id);

      return Ok(target);
    }

    // GET api/values/spec/"value"
    // Show specific value method to display specific value within values table according to Value (Async)
    [HttpGet("spec/{value}")]
    public async Task<IActionResult> ShowSpecValue(int value)
    {
      var specific = await _repo.GetSpecValue(value);

      return Ok(specific);

    } 

    // POST api/values
    [HttpPost]
    public void Post([FromBody] string value)
    {
      
    }
  }
}
