using System.Collections.Generic;
using GeekFix.Domain.Entities.Cache;
using GeekFix.Domain.Entities.Detailed;

namespace GeekFix.Application.Common.Interfaces
{
  public interface IMovieCacheHandler
  {
    // What does the cache need to be able to do

    // Get a movie according to Id 
    CachedMovieDetails GetSingleMovie(int id);

    // Add Singular object/element into the listr
    CachedMovieDetails AddMovieToCache(int id);

    // Map Method
    CachedMovieDetails MapMovieDetails(MovieInfo copiedMovie);

    // Prune Caller to trigger pruning method
    void PruneMoviesFromCache(int MinPruneCount);

    // Remove singular objects/elements into the list
    void RemoveMovieFromCache(int id);
    
    // Alter the pruning timer of the cache

    
    // Setting the number of elements to keep during pruning


    // Check count within cache and the total number of elements within
    int CheckCacheCount();
    
    // Check reference count for a single movie
    int CheckReferenceCount(int id);

  }
}