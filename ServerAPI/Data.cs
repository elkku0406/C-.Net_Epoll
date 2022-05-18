using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Linq;
using ServerAPI;

namespace ServerAPI
{
    [Serializable]
    public class PollList
    {
        [JsonProperty("id")]
        public int? id { get; set; }
        [JsonProperty("title")]
        public string? title { get; set; }
        [JsonProperty("options")]
        public Polls[]? options {get; set; }
       
    }
    [Serializable]
    public class PollsTest
    {
        [JsonProperty("id")]
        public int? id { get; set; }
        [JsonProperty("title")]
        public string? title { get; set; }
    }

    [Serializable]
    public class Polls
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("title")]
        public string title { get; set; }
        [JsonProperty("votes")]
        public int votes { get; set; }
    }
}

