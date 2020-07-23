using GeekFix.Domain.Common;

namespace GeekFix.Domain.Entities.Detailed
{
  public class CollectionInfo : AuditableEntity
  {
    public int id { get; set; }
    public string name { get; set; }
    public string poster_path { get; set; }
    public string backdrop_path { get; set; }
  }
}