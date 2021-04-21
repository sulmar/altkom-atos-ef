using Bogus;
using Models;
using System;

namespace Fakers
{
    public class ServiceFaker : Faker<Service>
    {
        public ServiceFaker()
        {
            StrictMode(true);
            Ignore(p => p.Id);
            RuleFor(p => p.Name, f => f.Hacker.Verb());
            RuleFor(p => p.Duration, f => f.Date.Timespan(TimeSpan.FromHours(4)));
            RuleFor(p => p.UnitPrice, f => Math.Round(f.Random.Decimal(1, 100), 2));
        }
    }
}
