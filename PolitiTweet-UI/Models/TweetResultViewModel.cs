using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolitiTweet_UI.Models
{
    public class TweetResultViewModel
    {
        public List<Tweet> tweetResults { get; set; }
        public string Message { get; set; }
        public string queryInput { get; set; }
        public TweetResultViewModel()
        {
            tweetResults = new List<Tweet>();
        }
    }
}
