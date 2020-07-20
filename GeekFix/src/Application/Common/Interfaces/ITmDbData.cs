using System.Threading.Tasks;
using GeekFix.Domain.Entities;

namespace GeekFix.Application.Common.Interfaces
{
  public interface ITmDbData
  {
    // Call API for Movies
    SearchResult CallApiSearch(string searchText, int page);

    // Call API for Discover
    SearchResult CallApiDiscover(string searchText, int page);
  }
}