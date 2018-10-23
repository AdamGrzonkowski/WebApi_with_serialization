using Application.Model;
using Bogus;
using Helper.Common.ConfigStrings;
using Helper.Common.Configuration;
using Helper.Common.Http;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RequestsSender.Core
{
    public class Logic : ILogic
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Logic));
        private readonly IConfiguration _config;
        private readonly IHttpHandler _httpHandler;

        public Logic(IConfiguration config, IHttpHandler httpHandler)
        {
            _config = config;
            _httpHandler = httpHandler;
        }

        public async Task RunAsync(string[] args)
        {
            if (args.Length > 1 || args.Length < 1)
            {
                DisplayInstructions();
            }

            if (int.TryParse(args[0], out var numberOfItemsToCreate))
            {
                string json = JsonConvert.SerializeObject(GetFakeRequests(numberOfItemsToCreate));
                StringContent stringContent = new StringContent(json, Encoding.UTF8, MediaType.Json);

                Logger.Info("Sending request...");
                string url = _config.BaseApiAddress + _config.PostRequestsUri;
                // This is a candidate for using message queue
                HttpResponseMessage response = await _httpHandler.PostAsync(url, stringContent);
                response.EnsureSuccessStatusCode();

                Logger.Info(await response.Content.ReadAsStringAsync());
            }
            else
            {
                DisplayInstructions();
            }
        }

        private void DisplayInstructions()
        {
            Console.WriteLine("Invalid invocation. Specify single integer parameter = number of models to create.");
            Console.WriteLine("Example usage:");
            Console.WriteLine("RequestsSender 3");
            Console.WriteLine("Above execution would create three new Request records in db.");

            throw new InvalidOperationException("Argument invalid.");
        }

        private static List<RequestModel> GetFakeRequests(int count)
        {
            var fakeRequests = new Faker<RequestModel>()
                .RuleFor(u => u.Name, f => f.Commerce.ProductName())
                .RuleFor(u => u.Date, f => f.Date.Recent())
                .RuleFor(u => u.Visits, f => f.Random.Int(0, 100));

            return fakeRequests.Generate(count);
        }
    }
}
