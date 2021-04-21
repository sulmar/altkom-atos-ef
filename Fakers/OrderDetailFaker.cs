using Bogus;
using Models;
using System;
using System.Collections.Generic;

namespace Fakers
{
    public class OrderDetailFaker : Faker<OrderDetail>
    {
        public OrderDetailFaker(IEnumerable<Item> items)
        {
            StrictMode(true);
            Ignore(p => p.Id);
            RuleFor(p => p.Item, f => f.PickRandom(items));
            RuleFor(p => p.Quantity, f => f.Random.Int(1, 100));
            RuleFor(p => p.UnitPrice, f => Math.Round(f.Random.Decimal(1, 100), 2));
        }
    }
}
