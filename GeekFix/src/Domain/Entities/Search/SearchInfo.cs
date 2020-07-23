using System.Collections.Generic;
using GeekFix.Domain.Common;

namespace GeekFix.Domain.Entities.Search
{
  public class SearchInfo : AuditableEntity
  {
    public int page { get; set; }
    public int total_results { get; set; }
    public int total_pages { get; set; }
    public List<SearchResult> results { get; set; }
  }
}