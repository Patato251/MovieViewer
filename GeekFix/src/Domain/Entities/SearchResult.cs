using System.Collections.Generic;

namespace GeekFix.Domain.Entities
{
  public class SearchResult
  {
    public int page { get; set; }
    public int total_results { get; set; }
    public int total_pages { get; set; }
    public List<Result> results { get; set; }
  }
}