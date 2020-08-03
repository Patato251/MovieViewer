namespace TestProj.API.Models
{
  public class UserDetails
  {
    public string type_of { get; set; }
    public int id { get; set; }
    public string username { get; set; }
    public string name { get; set; }
    public object summary { get; set; }
    public object twitter_username { get; set; }
    public string github_username { get; set; }
    public object website_url { get; set; }
    public object location { get; set; }
    public string joined_at { get; set; }
    public string profile_image { get; set; }
  }
}