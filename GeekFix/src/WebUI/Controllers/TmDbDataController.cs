using GeekFix.Application.Common.Interfaces;
using GeekFix.Domain.Entities.Detailed;
using GeekFix.Domain.Entities.Search;
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
    public SearchInfo GetDiscover(string searchtext, int page)
    {
      var pageResult = _data.CallApiDiscover(searchtext, page);

      return pageResult;
    }

    // Get a movie search with page
    [HttpGet("search/{searchtext}/{page}")]
    public SearchInfo GetSearch(string searchtext, int page)
    {
      SearchInfo pageResult = _data.CallApiSearch(searchtext, page);

      return pageResult;
    }

    // Get a specific movie according to Id
    [HttpGet("detailed/movie/{id}")]
    public MovieInfo GetMovie(int id)
    {
      MovieInfo movieResult = _data.CallApiMovie(id);

      return movieResult; 
    }
  }
}