using Newtonsoft.Json;
using System;

namespace Logic.Requests
{
    [Serializable]
    public class RequestWeb
    {
        [JsonProperty("tag")]
        public string Tag { get; set; }
        public RequestWeb(string tag)
        {
            Tag = tag;
        }
    }
}
