using System.Threading.Tasks;
using GeekFix.Application.Common.Interfaces;
using GeekFix.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GeekFix.WebUI.Controllers
{
  public class TmDbDataController : ApiController
  {
    private readonly ITmDbData _data;

    public TmDbDataController(ITmDbData data)
    {
      _data = data;
    }

    // Get A specific page of Discover 
    [HttpGet("discover/{searchtext}/{page}")]
    public SearchResult GetDiscover(string searchtext, int page)
    {
      var pageResult = _data.CallApiDiscover(searchtext, page);

      return pageResult;
    }

    // Get a movie search with page
    [HttpGet("movie/{searchtext}/{page}")]
    public SearchResult GetSearch(string searchtext, int page)
    {
      SearchResult pageResult = _data.CallApiSearch(searchtext, page);

      return pageResult;
    }
  }
}