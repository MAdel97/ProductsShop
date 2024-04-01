using Newtonsoft.Json;
using System;

using System.Net.Http;
using System.Text;

namespace ProductsShop.Helper
{
    class Response
    {
        public Data[] results { get; set; }
    }
    class Data
    {
        public decimal v { get; set; }
        public decimal vw { get; set; }
        public decimal o { get; set; }
        public decimal c { get; set; }
        public decimal h { get; set; }
        public decimal l { get; set; }
        public decimal n { get; set; }
    }
     public static class WebAPIConsumer
    {
        private const string URL = "https://api.polygon.io/v2/aggs/ticker/AAPL/range/1/day/2023-01-09/2023-01-09" +
            "?apiKey=tmOzGkfWVNvnhgrOtC8XOF5qsGxfGkU2";
        private const string DATA = @"{
            ""name"": ""Component 2"",
            ""description"": ""This is a JIRA component"",
            ""leadUserName"": ""xx"",
            ""assigneeType"": ""PROJECT_LEAD"",
            ""isAssigneeTypeValid"": false,
            ""project"": ""TP""}";

        /*static void Main(string[] args)
        {
            AddComponent();
        }
*/
        public static void AddComponent()
        {
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            client.BaseAddress = new System.Uri(URL);
            byte[] cred = UTF8Encoding.UTF8.GetBytes("username:password");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            System.Net.Http.HttpContent content = new StringContent("", UTF8Encoding.UTF8, "application/json");
            HttpResponseMessage messge = client.GetAsync(URL).Result;
            string description = string.Empty;
            if (messge.IsSuccessStatusCode)
            {
                string result = messge.Content.ReadAsStringAsync().Result;

                object jsonp = JsonConvert.DeserializeObject<Response>(result); 
                description = jsonp.ToString().ToLower();
            }
        }
    }
}