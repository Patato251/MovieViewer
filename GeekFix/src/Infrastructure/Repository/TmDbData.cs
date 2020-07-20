using System;
using System.IO;
using System.Net;
using GeekFix.Application.Common.Interfaces;
using GeekFix.Domain.Entities.Detailed;
using GeekFix.Domain.Entities.Search;
using Newtonsoft.Json;

namespace GeekFix.Infrastructure.Repository
{
  public class TmDbData : ITmDbData
  {
    public SearchInfo CallApiDiscover(string searchText, int page)
    {
      int pageNo = Convert.ToInt32(page) == 0 ? 1 : Convert.ToInt32(page);
      string apiKey = "f68c64ab26f5bb2a81e09f4af4dff582"; //TEMPORARY

      HttpWebRequest apiRequest = WebRequest.Create("https://api.themoviedb.org/3/search/discover?api_key=" + apiKey + "&language=en-US&sort_by=" + searchText + "&page=" + pageNo + "&include_adult=false") as HttpWebRequest;

      var apiResponse = ConvertToString(apiRequest);

      SearchInfo result = JsonConvert.DeserializeObject<SearchInfo>(apiResponse);

      return result;
    }

    public SearchInfo CallApiSearch(string searchText, int page)
    {
      int pageNo = Convert.ToInt32(page) == 0 ? 1 : Convert.ToInt32(page);
      string apiKey = "f68c64ab26f5bb2a81e09f4af4dff582"; //TEMPORARY

      HttpWebRequest apiRequest = WebRequest.Create("https://api.themoviedb.org/3/search/movie?api_key=" + apiKey + "&language=en-US&query=" + searchText + "&page=" + pageNo + "&include_adult=false") as HttpWebRequest;

      var apiResponse = ConvertToString(apiRequest);

      SearchInfo result = JsonConvert.DeserializeObject<SearchInfo>(apiResponse);

      return result;
    }

    public MovieInfo CallApiMovie(int id)
    {
      string apiKey = "f68c64ab26f5bb2a81e09f4af4dff582"; //TEMPORARY

      HttpWebRequest apiRequest = WebRequest.Create("https://api.themoviedb.org/3/movie/" + id + "?api_key=" + apiKey + "&language=en-US") as HttpWebRequest;

      var apiResponse = ConvertToString(apiRequest);

      MovieInfo result = JsonConvert.DeserializeObject<MovieInfo>(apiResponse);

      return result;
    }

    private string ConvertToString(HttpWebRequest apiRequest)
    {
      string apiResponse;
      using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
      {
        StreamReader reader = new StreamReader(response.GetResponseStream());
        apiResponse = reader.ReadToEnd();
      }
      return apiResponse;
    }
  }
}