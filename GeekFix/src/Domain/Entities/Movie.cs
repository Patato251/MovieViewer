using GeekFix.Domain.Common;

namespace GeekFix.Domain.Entities
{
  public class Movie : AuditableEntity
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public string MovieUrl { get; set; }
    public int ReleaseYear { get; set; }
    public int UpVote { get; set; }
    public int Rating { get; set; }
    public string PhotoUrl { get; set; }
    public string Emotion { get; set; }
    public string Description { get; set; }
  }
}