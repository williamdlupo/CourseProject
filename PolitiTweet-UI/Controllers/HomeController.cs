using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PolitiTweet_UI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PolitiTweet_UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _client;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory client)
        {
            _logger = logger;
            _client = client;
        }
        public async Task<IActionResult> Index()
        {
            TweetResultViewModel viewModel = new TweetResultViewModel();

            //send warm up message to ensure azure function is on
           // var request = new HttpRequestMessage(HttpMethod.Get, $"api/BuildIndex");
           // var client = _client.CreateClient("searchApi");
           //client.SendAsync(request).Wait();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Search([FromBody] string query)
        {
            TweetResultViewModel viewModel = new TweetResultViewModel();

            if (String.IsNullOrEmpty(query))
            {
                viewModel.Message = "You need to enter a query...";
                return View("Index", viewModel);
            }
            viewModel.queryInput = query;
            var client = _client.CreateClient("searchApi");
            var responseMessage = await client.PostAsJsonAsync($"api/Search",
                new QueryModel { query = query });

            if (!responseMessage.IsSuccessStatusCode)
            {
                _logger.LogError(responseMessage.ReasonPhrase);
                viewModel.Message = "Yikes something went wrong. Try again";
            }
            else
            {
                var stringJson = await responseMessage.Content.ReadAsStringAsync();
                List<Tweet> resultTweets = JsonConvert.DeserializeObject<List<Tweet>>(stringJson);
                viewModel.tweetResults = resultTweets.OrderByDescending(x => x.Score).ToList();

                if (!viewModel.tweetResults.Any()) viewModel.Message = "No results!";
            }
            return View("Index", viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
