using Bogus;
using Models;

namespace Fakers
{
    public class AddressFaker : Faker<Address>
    {
        public AddressFaker()
        {
            RuleFor(p => p.City, f => f.Address.City());
            RuleFor(p => p.Country, f => f.Address.Country());
            RuleFor(p => p.PostCode, f => f.Address.ZipCode("##-###"));
            RuleFor(p => p.Street, f => f.Address.StreetName());

        }
    }
}
