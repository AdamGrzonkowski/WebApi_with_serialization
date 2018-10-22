using Bogus;
using Domain.Model;
using System.Collections.Generic;

namespace Test.Builders
{
    public static class RequestsGenerator
    {
        public static List<Request> GetNRequests(int count)
        {
            var fakeRequests = new Faker<Request>()
                .RuleFor(u => u.Name, f => f.Commerce.ProductName())
                .RuleFor(u => u.Date, f => f.Date.Recent())
                .RuleFor(u => u.Visits, f => f.Random.Int(0, 100));

            return fakeRequests.Generate(count);
        }
    }
}
