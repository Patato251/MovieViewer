using System.Collections.Generic;

namespace TestProj.API.Models
{
  public class SearchResult
  {
    public int Page { get; set; }
    public int TotalResults { get; set; }
    public int TotalPages { get; set; }
    public ICollection<MovieDetails> MovieDetails { get; set; }
  }
}