using System;
using System.Collections.Generic;

namespace TestProj.API.Models
{
  public class ArticleDetails
  {
    public string type_of { get; set; }
    public int id { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public string cover_image { get; set; }
    public bool published { get; set; }
    public DateTime published_at { get; set; }
    public List<string> tag_list { get; set; }
    public string slug { get; set; }
    public string path { get; set; }
    public string url { get; set; }
    public string canonical_url { get; set; }
    public int comments_count { get; set; }
    public int public_reactions_count { get; set; }
    public int page_views_count { get; set; }
    public DateTime published_timestamp { get; set; }
    public string body_markdown { get; set; }
    public User user { get; set; }
    public Organization organization { get; set; }
    public FlareTag flare_tag { get; set; }
  }
  
  public class User
  {
    public string name { get; set; }
    public string username { get; set; }
    public string twitter_username { get; set; }
    public string github_username { get; set; }
    public string website_url { get; set; }
    public string profile_image { get; set; }
    public string profile_image_90 { get; set; }
  }

  public class Organization
  {
    public string name { get; set; }
    public string username { get; set; }
    public string slug { get; set; }
    public string profile_image { get; set; }
    public string profile_image_90 { get; set; }
  }

  public class FlareTag
  {
    public string name { get; set; }
    public string bg_color_hex { get; set; }
    public string text_color_hex { get; set; }
  }

}
