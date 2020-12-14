using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolitiTweet_UI.Models
{
    public class Tweet
    {
        public int TweetId { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }
        public float Score { get; set; }
    }
}
