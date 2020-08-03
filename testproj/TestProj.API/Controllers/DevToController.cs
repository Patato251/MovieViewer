using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestProj.API.Models;
using TestProj.API.Services;

namespace TestProj.API.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class DevToController
  {
    /*
    private static readonly String baseAddress = "https://dev.to/api";

    private IHttpClientWrapper clientWrapper;
    public DevToController()
    {
      this.clientWrapper = (IHttpClientWrapper)CreateClient();
    }

    private HttpClientWrapper CreateClient()
    {
      HttpClientHandler handler = new HttpClientHandler();
      handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
      return new HttpClientWrapper(
          new HttpClient(handler)
          {
            BaseAddress = new Uri(baseAddress)
          }
      );
    }

    // Redirect Url 
    [HttpGet("{username}")]
    public async Task<string> RequestUserDetails(string username)
    {
      // Make everything a local variable for testing
      RequestInfo requestModel = new RequestInfo();
      requestModel.Username = username;

      string requestUrl = "users/by_username?url=" + requestModel.Username;

      var response = await this.clientWrapper.DoRequest(requestModel);

      return response;
    }
*/

    private static readonly HttpClient client = new HttpClient();

    [HttpGet("yeet")]
    public async Task<string> test()
    {
      var url = "https://dev.to/api/users/by_username?url=ben";
      var req = new HttpRequestMessage(HttpMethod.Get, url); // Organising where its sent to, and what method it's going to be

      req.Headers.Add("api-key", "fAXDHCyeD9Q4ivkjXaEk42r2");

      var getResponse = await client.SendAsync(req);

      return "Yes";
    }

    [HttpGet("{username}")]
    public async Task<UserDetails> GetUserDetails(string username)
    {
      var url = "https://dev.to/api/users/by_username?url=" + username;
      var req = new HttpRequestMessage(HttpMethod.Get, url); // Organising where its sent to, and what method it's going to be

      req.Headers.Add("api-key", "fAXDHCyeD9Q4ivkjXaEk42r2");

      var getResponse = await client.SendAsync(req);

      getResponse.EnsureSuccessStatusCode();
      string responseBody = await getResponse.Content.ReadAsStringAsync();

      UserDetails result = JsonConvert.DeserializeObject<UserDetails>(responseBody);

      return result;
    }

    [HttpGet("{username}/all")]
    public async Task<List<ArticleDetails>> GetUserArticles(string username)
    {
      var url = "https://dev.to/api/articles?username=" + username;
      var req = new HttpRequestMessage(HttpMethod.Get, url); // Organising where its sent to, and what method it's going to be

      req.Headers.Add("api-key", "fAXDHCyeD9Q4ivkjXaEk42r2");

      var getResponse = await client.SendAsync(req);

      getResponse.EnsureSuccessStatusCode();

      string responseBody = await getResponse.Content.ReadAsStringAsync();

      List<ArticleDetails> result = JsonConvert.DeserializeObject<List<ArticleDetails>>(responseBody);

      return result;
    }

  }
}