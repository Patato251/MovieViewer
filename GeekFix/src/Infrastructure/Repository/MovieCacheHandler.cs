using System;
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
    private int maxPruneCount;
    private int minPruneCount;
    private int pruneTimer;

    public MovieCacheHandler(ITmDbData data, int max=20, int min=10, int timer=60)
    {
      _data = data;
      Init(max, min, timer);
    }

    public void Init(int max, int min, int timer)
    {
      if (min < 0) throw new ArgumentOutOfRangeException("Appropriate message");
      if (max < 0 || max <= min) throw new ArgumentOutOfRangeException();
      maxPruneCount = max;
      minPruneCount = min;
      pruneTimer = timer;
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
        movieObject = MapMovieToCache(id);
        _cache.Add(movieObject.movieInfo.id, movieObject);
        return _cache[id];
      }
    }

    public CachedMovieDetails MapMovieToCache(int id)
    {
      CachedMovieDetails mappedMovie = new CachedMovieDetails();
      // Check if the max prune limit has been reached
      if (_cache.Count >= maxPruneCount)
      {
        // Prune the cache
        PruneMoviesFromCache(minPruneCount);
      }
      // Under max prune limit, add the object into the list
      // Call the Api Method for Movie identification obtaining and grab the object
      var copiedMovie = _data.CallApiMovie(id);
      // Map Details for adding into cache
      mappedMovie = MapMovieDetails(copiedMovie);

      return mappedMovie;
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

    public void ClearMovieCache()
    {
      var keysToRemove = _cache.OrderByDescending(k => k.Value.referenceCount);

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

    public void ChangeMaxCacheSize(int newSize)
    {
      maxPruneCount = newSize;
    }

    public int CheckMaxCacheSize()
    {
      return maxPruneCount;
    }

    public void ChangeMinCacheSize(int newSize)
    {
      minPruneCount = newSize;
    }

    public int CheckMinCacheSize()
    {
      return minPruneCount;
    }
  }
}