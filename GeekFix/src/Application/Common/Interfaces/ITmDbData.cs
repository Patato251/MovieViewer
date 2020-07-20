using GeekFix.Domain.Entities.Detailed;
using GeekFix.Domain.Entities.Search;

namespace GeekFix.Application.Common.Interfaces
{
  public interface ITmDbData
  {
    // Call API for general search
    SearchInfo CallApiSearch(string searchText, int page);

    // Call API for Discover
    SearchInfo CallApiDiscover(string searchText, int page);

    // Call API for Detailed Movie Info 
    MovieInfo CallApiMovie(int id);
  }
}