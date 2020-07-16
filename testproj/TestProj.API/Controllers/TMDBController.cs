using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestProj.API.Models;


namespace TestProj.API.Controllers
{
  [Route("movies/[controller]")] //localhost:5000/api/Values
  [ApiController]
  public class TMDBController : ControllerBase
  {
    public TMDBController() { }

    [HttpGet] // movies/TMDB
    public ContentResult Index(string movieName, int? page)
    {
      var result = CallAPI("22%20Jump%20Street", 0);

      return result;
    }

    public ContentResult CallAPI(string searchText, int page)
    {
      int pageNo = Convert.ToInt32(page) == 0 ? 1 : Convert.ToInt32(page);

      string apiKey = "f68c64ab26f5bb2a81e09f4af4dff582";
      HttpWebRequest apiRequest = WebRequest.Create("https://api.themoviedb.org/3/search/movie?api_key=" + apiKey + "&language=en-US&query=" + searchText + "&page=" + pageNo + "&include_adult=false") as HttpWebRequest;

      var apiResponse = "";
      using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
      {
        StreamReader reader = new StreamReader(response.GetResponseStream());
        apiResponse = reader.ReadToEnd();
      }
      var result = apiResponse;
      // SearchResult result = JsonConvert.DeserializeObject<SearchResult>(apiResponse);

      return this.Content(result, "application/json");
    }
  }
}