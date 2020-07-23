using GeekFix.Domain.Common;

namespace GeekFix.Domain.Entities.Detailed
{
  public class ProductionCompany : AuditableEntity
  {
    public int id { get; set; }
    public string logo_path { get; set; }
    public string name { get; set; }
    public string origin_country { get; set; }
  }

  public class ProductionInfo : AuditableEntity
  {
    public string iso_3166_1 { get; set; }
    public string name { get; set; }
  }
}