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


    // Change Min Keep Size

     
    // 

  }
}