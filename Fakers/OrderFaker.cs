using Bogus;
using Models;
using System.Collections.Generic;
using System.Text;

namespace Fakers
{
    public class OrderFaker : Faker<Order>
    {
        public OrderFaker(IEnumerable<Customer> customers, OrderDetailFaker orderDetailFaker)
        {
            StrictMode(true);
            Ignore(p => p.Id);
            RuleFor(p => p.OrderDate, f => f.Date.Past());
            RuleFor(p => p.OrderNumber, f => $"ZAM {f.IndexFaker}/2021");
            RuleFor(p => p.Status, f => f.PickRandom<OrderStatus>());
            RuleFor(p => p.Customer, f => f.PickRandom(customers));
            RuleFor(p => p.Details, f => orderDetailFaker.Generate(f.Random.Int(1, 10)));
        }
    }
}
