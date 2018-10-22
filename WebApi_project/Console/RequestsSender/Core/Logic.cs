using Bogus;
using Domain.Model;
using Helper.Common.ConfigStrings;
using Helper.Common.Configuration;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RequestsSender.Core
{
    public class Logic : ILogic
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Logic));
        private readonly IConfiguration _config;

        public Logic(IConfiguration config)
        {
            _config = config;
        }

        public async Task RunAsync(string parameter)
        {
            if (int.TryParse(parameter, out var numberOfItemsToCreate))
            {
                string json = JsonConvert.SerializeObject(GetFakeRequests(numberOfItemsToCreate));
                StringContent stringContent = new StringContent(json, Encoding.UTF8, MediaType.Json);

                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(_config.BaseApiAddress)
                };
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaType.Json));

                Logger.Info("Sending request...");
                // This is a candidate for using message queue
                HttpResponseMessage response = await client.PostAsync(_config.PostRequestsUri, stringContent);
                response.EnsureSuccessStatusCode();

                Logger.Info(await response.Content.ReadAsStringAsync());
            }
            else
            {
                DisplayInstructions();
            }
        }

        public void DisplayInstructions()
        {
            Console.WriteLine("Invalid invocation. Specify single integer parameter = number of models to create.");
            Console.WriteLine("Example usage:");
            Console.WriteLine("RequestsSender 3");
            Console.WriteLine("Above execution would create three new Request records in db.");
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
