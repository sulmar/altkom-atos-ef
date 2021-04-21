using Bogus;
using Models;
using System;

namespace Fakers
{
    public class ProductFaker : Faker<Product>
    {
        public ProductFaker()
        {
            StrictMode(true);
            Ignore(p => p.Id);
            RuleFor(p => p.Name, f => f.Commerce.ProductName());
            RuleFor(p => p.BarCode, f => f.Commerce.Ean13());
            RuleFor(p => p.Color, f => f.Commerce.Color());
            RuleFor(p => p.Weight, f => f.Random.Float(1, 1000));
            RuleFor(p => p.UnitPrice, f => Math.Round(f.Random.Decimal(1, 100), 2));
        }
    }
}
