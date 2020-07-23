using GeekFix.Domain.Entities.Detailed;

namespace GeekFix.Domain.Entities.Cache
{
  public class CachedMovieDetails
  {
    public int referenceCount { get; set; }
    public MovieInfo movieInfo { get; set; }
  }
}