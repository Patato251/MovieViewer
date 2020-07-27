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
    public class TestClass
    {
      public int id { get; set; }
      public int reference { get; set; }
    }

    //////////////////////////////////////////
    private TestClass one = new TestClass();
    private TestClass two = new TestClass();
    private TestClass five = new TestClass();
    private TestClass eight = new TestClass();
    private TestClass ten = new TestClass();
    private TestClass six = new TestClass();
    //////////////////////////////////////////
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
    [HttpGet("list")]
    public IActionResult TestShowDict()
    {
      Dictionary<int, TestClass> dict = new Dictionary<int, TestClass>();

      one.id = 1;
      two.id = 2;
      ten.id = 10;
      five.id = 5;
      eight.id = 8;
      six.id = 6;

      one.reference = 1;
      two.reference = 2;
      ten.reference = 10;
      five.reference = 5;
      eight.reference = 8;
      six.reference = 6;

      dict.Add(1, one);
      dict.Add(2, two);
      dict.Add(10, ten);
      dict.Add(5, five);
      dict.Add(8, eight);
      dict.Add(6, six);

      dict.OrderByDescending(key => key.Value.reference);

      dict = dict.Take(4).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
      return Ok(dict);
    }
  }
}
