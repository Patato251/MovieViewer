using GeekFix.Application.Common.Interfaces;
using GeekFix.Domain.Entities.Cache;
using Microsoft.AspNetCore.Mvc;

namespace GeekFix.WebUI.Controllers
{
  public class MovieCacheController : ApiController
  {
    private readonly IMovieCacheHandler _handler;

    public MovieCacheController(IMovieCacheHandler handler)
    {
      _handler = handler;
    }

    // Get a single movie based on movie Id
    [HttpGet("movie/{id}")]
    public CachedMovieDetails GetMovie(int id)
    {
      var selectedMovie = _handler.GetSingleMovie(id);

      return selectedMovie;
    }

    // Change Max Cache Size
    [HttpPost("cachemax/{newSize}")]
    public string ChangeMaxCache(int newSize)
    {
      if (newSize > 0 && newSize > _handler.CheckMinCacheSize())
      {
        _handler.ChangeMaxCacheSize(newSize);
        return ("Cache Size has been successfully changed to " + newSize);
      }
      else
      {
        return ("The new cache Size is invalid, please try again with a new size");
      }
    }

    // Change Min Keep Size
    [HttpPost("cachemin/{newSize}")]
    public string ChangeMinCache(int newSize)
    {
      if (newSize > 0 && newSize < _handler.CheckMaxCacheSize())
      {
        _handler.ChangeMinCacheSize(newSize);
        return ("Tracked Cache Size has been successfully changed to " + newSize);
      }
      else
      {
        return ("The new tracked Size is invalid, please try again with a new size");
      }
    }

    // Check Max Cache Size
    [HttpGet("checkMax")]
    public string CheckMinCache()
    {
      int minSize = _handler.CheckMinCacheSize();
      return ("The number of kept items after pruning is " + minSize);
    }

    [HttpGet("checkMin")]
    public string CheckMaxCache()
    {
      int maxSize = _handler.CheckMaxCacheSize();
      return ("TThe maximum cache size is " + maxSize);
    }
  }
}