using Bogus;
using Domain.Model;
using Helper.Common.ConfigStrings;
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
        private static readonly HttpClient Client = new HttpClient();
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Program));

        private static string BaseApiUrl => ConfigurationManager.AppSettings["apiBaseAddress"];
        private static string PostRequestsMethod => ConfigurationManager.AppSettings["apiPostRequestUri"];

        public static void Main(string[] args)
        {
            if (args.Length > 1 && args.Length < 1)
            {
                DisplayInstructions();
                Environment.Exit(-1);
            }

            try
            {
                RunAsync(args[0]).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private static async Task RunAsync(string parameter)
        {
            if (int.TryParse(parameter, out var numberOfItemsToCreate))
            {
                string json = JsonConvert.SerializeObject(GetFakeRequests(numberOfItemsToCreate));
                StringContent stringContent = new StringContent(json, Encoding.UTF8, MediaType.Json);

                Client.BaseAddress = new Uri(BaseApiUrl);
                Client.DefaultRequestHeaders.Accept.Clear();
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaType.Json));

                HttpResponseMessage response = await Client.PostAsync(PostRequestsMethod, stringContent);
                response.EnsureSuccessStatusCode();

                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
            else
            {
                DisplayInstructions();
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

        private static void DisplayInstructions()
        {
            Console.WriteLine("Specify single integer parameter = number of models to create.");
            Console.WriteLine("Example usage:");
            Console.WriteLine("RequestsSender 3");
            Console.WriteLine("Above execution would create three new Request records in db.");
        }
    }
}
