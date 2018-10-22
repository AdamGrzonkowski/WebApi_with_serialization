using Bogus;
using Domain.Model;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RequestsSender
{
    public class Program
    {
        static HttpClient client = new HttpClient();
        private static readonly ILog _logger = LogManager.GetLogger(typeof(Program));

        private static string BaseApiUrl => ConfigurationManager.AppSettings["apiBaseAddress"];
        private static string PostRequestsMethod => ConfigurationManager.AppSettings["apiPostRequestUri"];

        static void Main(string[] args)
        {
            if (args.Length > 1 && args.Length < 1)
            {
                Console.WriteLine("Specify single integer parameter = number of models to create");
                Console.WriteLine("Example usage: RequestsSender 3");
                Console.WriteLine("Above execution will create three new models in db.");
                Environment.Exit(-1);
            }

            RunAsync(args[0]).GetAwaiter().GetResult();
        }

        private static async Task RunAsync(string parameter)
        {
            if (int.TryParse(parameter, out var numberOfItemsToCreate))
            {
                string json = JsonConvert.SerializeObject(GetFakeRequests(numberOfItemsToCreate));
                StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                client.BaseAddress = new Uri(BaseApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsync(PostRequestsMethod, stringContent);
                response.EnsureSuccessStatusCode();

                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
            else
            {
                Console.WriteLine("Argument must be an integer.");
                _logger.Error("Invalid format!");
            }
        }

        private static List<Request> GetFakeRequests(int count)
        {
            var fakeRequests = new Faker<Request>()
                .RuleFor(u => u.Name, f => f.Commerce.ProductName())
                .RuleFor(u => u.Date, f => f.Date.Recent())
                .RuleFor(u => u.Visits, f => f.Random.Int(0, 100));

            return fakeRequests.Generate(count);
        }
    }
}
