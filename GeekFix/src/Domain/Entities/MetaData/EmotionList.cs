using GeekFix.Domain.Common;

namespace GeekFix.Domain.Entities.MetaData
{
  public class EmotionList : AuditableEntity
  {
    public int Id { get; set; }
    public int movieId { get; set; }
    public EmotionInfo emotion { get; set; }
  }
}