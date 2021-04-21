using Bogus;
using Models;
using System;
using Bogus.Extensions.Poland;

namespace Fakers
{

    // PM> Install-Package Bogus
    // PM> Install-Package Sulmar.Bogus.Extensions.Poland
    public class CustomerFaker : Faker<Customer>
    {
        public CustomerFaker(Faker<Address> addressFaker)
        {
            // RuleFor(p => p.Id, f => f.IndexFaker);

            StrictMode(true);
            Ignore(p => p.Id);
            RuleFor(p => p.FirstName, f => f.Person.FirstName);
            RuleFor(p => p.LastName, f => f.Person.LastName);
            RuleFor(p => p.Email, f => f.Person.Email);
            RuleFor(p => p.DateOfBirth, f => f.Person.DateOfBirth);
            RuleFor(p => p.Gender, f => (Gender) f.Person.Gender);
            RuleFor(p => p.InvoiceAddress, f => addressFaker.Generate());
            RuleFor(p => p.ShipAddress, f => addressFaker.Generate());
            RuleFor(p => p.IsRemoved, f => f.Random.Bool(0.3f));
            RuleFor(p => p.Pesel, f => f.Person.Pesel());
            Ignore(p => p.IsSelected);
            Ignore(p => p.Avatar);

        }
    }
}
