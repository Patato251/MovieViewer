using GeekFix.Domain.Entities.Cache;
using GeekFix.Domain.Entities.Detailed;

namespace GeekFix.Application.Common.Interfaces
{
  public interface IMovieCacheHandler
  {
    CachedMovieDetails GetSingleMovie(int id);
    CachedMovieDetails MapMovieToCache(int id);
    CachedMovieDetails MapMovieDetails(MovieInfo copiedMovie);
    void PruneMoviesFromCache(int MinPruneCount);
    void ClearMovieCache();
    void RemoveMovieFromCache(int id);
    int CheckCacheCount();
    int CheckReferenceCount(int id);
    void ChangeMaxCacheSize(int newSize);
    int CheckMaxCacheSize();
    void ChangeMinCacheSize(int newSize);
    int CheckMinCacheSize();
  }
}