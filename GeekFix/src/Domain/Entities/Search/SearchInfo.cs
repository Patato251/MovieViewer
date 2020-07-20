using System.Collections.Generic;

namespace GeekFix.Domain.Entities.Search
{
  public class SearchInfo
  {
    public int page { get; set; }
    public int total_results { get; set; }
    public int total_pages { get; set; }
    public List<SearchResult> results { get; set; }
  }
}