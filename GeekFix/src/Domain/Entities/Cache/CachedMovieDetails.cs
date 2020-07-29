using GeekFix.Domain.Entities.Detailed;
using GeekFix.Domain.Entities.MetaData;

namespace GeekFix.Domain.Entities.Cache
{
  public class CachedMovieDetails
  {
    public int referenceCount { get; set; }
    public MovieInfo movieInfo { get; set; }
    public EmotionList emotions { get; set; }
  }
}