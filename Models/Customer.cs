using System;

namespace Models
{
    public class Customer : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public Gender Gender { get; set; }
        public string Email { get; set; }     
        public DateTime DateOfBirth { get; set; }
        public Address ShipAddress { get; set; }
        public Address InvoiceAddress { get; set; }
        public bool IsRemoved { get; set; }

        public Customer()
        {
            ShipAddress = new Address();
            InvoiceAddress = new Address();
        }
    }
}
