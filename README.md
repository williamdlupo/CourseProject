# PolitiTweet Application Suite

PolitiTweet is an application suite that mines US politician tweets, builds a Lucene Index and provides a web based application for querying and displaying results.

TweetMiner - A C# Azure function that runs at 12pm EST to extract and store the previous 24 hours of tweets of US politicians. Leverages the Twitter API and stores tweets in an Azure database.
Repo Url: https://github.com/williamdlupo/TweetMiner

SearchApi - A Java Azure function application that builds and queries a Lucene Index built upon tweets stored in an Azure database.
Repo Url: https://github.com/williamdlupo/SearchApi

PolitiTweet-UI - A .Net Core 3 MVC application that accepts user queries, sends queries to deployed SearchApi and displays matching tweets. 

