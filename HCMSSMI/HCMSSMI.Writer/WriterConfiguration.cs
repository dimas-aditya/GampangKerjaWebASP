using Newtonsoft.Json.Linq;
using RestSharp;

namespace HCMSSMI.Writer
{
    public class WriterConfiguration
    {
        public RestClient Client { get; set; }
        public RestRequest Request { get; set; }

        public JObject JsonObject { get; set; }

        public string ClientURL { get; set; }
        public string RequestURL { get; set; }
    }
}
