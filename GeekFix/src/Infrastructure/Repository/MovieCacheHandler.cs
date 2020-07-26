using System.Collections.Generic;
using System.Linq;
using GeekFix.Application.Common.Interfaces;
using GeekFix.Domain.Entities.Cache;
using GeekFix.Domain.Entities.Detailed;
using GeekFix.Domain.Entities.Search;

namespace GeekFix.Infrastructure.Repository
{
  public class MovieCacheHandler : IMovieCacheHandler
  {
    private readonly ITmDbData _data;

    static int MaxPruneCount;
    static int MinPruneCount;
    static int PruneTimer;

    public MovieCacheHandler(ITmDbData data, int max, int min, int timer)
    {
      _data = data;
      MaxPruneCount = max;
      MinPruneCount = min;
      PruneTimer = timer;
    }
    static Dictionary<int, CachedMovieDetails> _cache = new Dictionary<int, CachedMovieDetails>();

    public CachedMovieDetails GetSingleMovie(int id)
    {
      // Check if the movie exists within the cache database
      // if the movie does exist, grab the movie's details and increment the reference count 
      if (_cache.ContainsKey(id))
      {
        _cache[id].referenceCount++;
        return _cache[id];
      }
      // If the movie doesn't exist, grab the movie from the Api and create clone within our database
      else
      {
        // Call Api and grab Movieinfo object
        CachedMovieDetails movieObject = new CachedMovieDetails();
        movieObject = AddMovieToCache(id);
        _cache.Add(movieObject.movieInfo.id, movieObject);
        return _cache[id];
      }
    }

    public CachedMovieDetails AddMovieToCache(int id)
    {
      CachedMovieDetails addedMovie = new CachedMovieDetails();
      // Check if the max prune limit has been reached
      if (_cache.Count > MaxPruneCount)
      {
        // Prune the cache
        PruneMoviesFromCache(MinPruneCount);
      }
      // Under max prune limit, add the object into the list
      // Call the Api Method for Movie identification obtaining and grab the object
      var copiedMovie = _data.CallApiMovie(id);
      // Map Details for adding into cache
      addedMovie = MapMovieDetails(copiedMovie);

      return addedMovie;
    }

    public CachedMovieDetails MapMovieDetails(MovieInfo copiedMovie)
    {
      // Assign the respective variables to the assigned ones in dict
      CachedMovieDetails passThrough = new CachedMovieDetails();
      passThrough.referenceCount = 1;
      passThrough.movieInfo = copiedMovie;

      return passThrough;
    }

    public void PruneMoviesFromCache(int MinPruneCount)
    {
      // Determine the Top 10 movies according to reference count and keep in cache
      var keysToRemove = _cache.OrderByDescending(k => k.Value.referenceCount).Skip(MinPruneCount);

      // Repeatedly call remove from cache function
      foreach (var element in keysToRemove)
      {
        RemoveMovieFromCache(element.Key);
      }
    }

    public void RemoveMovieFromCache(int id)
    {
      // Should be removing values according to value/key
      _cache.Remove(id);
    }

    public int CheckCacheCount()
    {
      return _cache.Count();
    }

    public int CheckReferenceCount(int id)
    {
      if (_cache.ContainsKey(id))
      {
        return _cache[id].referenceCount;
      }
      else
      {
        return 0;
      }
    }
  }
}