using GeekFix.Domain.Common;

namespace GeekFix.Domain.Entities.Detailed
{
  public class Language : AuditableEntity
  {
    public string iso_639_1 { get; set; }
    public string name { get; set; }
  }
}