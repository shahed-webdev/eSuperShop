using System;

namespace eSuperShop.Data
{
    public class CustomerAddressBook
    {
        public int CustomerAddressBookId { get; set; }
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string AlternativePhone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public bool IsDefault { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public virtual Customer Customer { get; set; }
    }
}