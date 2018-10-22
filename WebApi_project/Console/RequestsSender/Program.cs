using Autofac;
using Bogus;
using Domain.Model;
using Helper.Common.ConfigStrings;
using Helper.Common.Configuration;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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

        public static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(AppDomain.CurrentDomain.SetupInformation
                .ConfigurationFile));

            if (args.Length > 1 && args.Length < 1)
            {
                DisplayInstructions();
                Environment.Exit(-1);
            }

            try
            {
                Logger.Info("Requests sender started");
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

                // there's no point registering it sooner, as we only need it here
                using (IContainer container = Startup.ConfigureContainer())
                {
                    IConfiguration config = container.Resolve<IConfiguration>();

                    Client.BaseAddress = new Uri(config.BaseApiAddress);
                    Client.DefaultRequestHeaders.Accept.Clear();
                    Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaType.Json));

                    HttpResponseMessage response = await Client.PostAsync(config.PostRequestsUri, stringContent);
                    response.EnsureSuccessStatusCode();

                    Logger.Info(await response.Content.ReadAsStringAsync());
                }
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
