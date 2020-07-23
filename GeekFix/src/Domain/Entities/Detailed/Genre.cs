using GeekFix.Domain.Common;

namespace GeekFix.Domain.Entities.Detailed
{
  public class Genre : AuditableEntity
  {
    public int id { get; set; }
    public string name { get; set; }
  }
}