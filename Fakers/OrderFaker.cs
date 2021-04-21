using Bogus;
using Models;
using System;
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

    public class ProductFaker : Faker<Product>
    {
        public ProductFaker()
        {
            StrictMode(true);
            Ignore(p => p.Id);
            RuleFor(p => p.Name, f => f.Commerce.ProductName());
            RuleFor(p => p.Color, f => f.Commerce.Color());
            RuleFor(p => p.Weight, f => f.Random.Float(1, 1000));
            RuleFor(p => p.UnitPrice, f => Math.Round(f.Random.Decimal(1, 100), 2));
        }
    }

    public class ServiceFaker : Faker<Service>
    {
        public ServiceFaker()
        {
            StrictMode(true);
            Ignore(p => p.Id);
            RuleFor(p => p.Name, f => f.Hacker.Verb());
            RuleFor(p => p.Duration, f => f.Date.Timespan());
            RuleFor(p => p.UnitPrice, f => Math.Round(f.Random.Decimal(1, 100), 2));
        }
    }
}
