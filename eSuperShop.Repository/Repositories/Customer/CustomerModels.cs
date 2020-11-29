using System;

namespace eSuperShop.Repository
{
    public class CustomerAddModel
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class CustomerinfoModel
    {

    }

    public class CustomerDashboardModel
    {

    }

    public class CustomerAddressBookModel
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
    }
}