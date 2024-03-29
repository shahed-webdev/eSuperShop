﻿using System.Collections.Generic;

namespace eSuperShop.Data
{
    public class Customer
    {
        public Customer()
        {
            ProductReview = new HashSet<ProductReview>();
            ProductFaq = new HashSet<ProductFaq>();
            VendorFollower = new HashSet<VendorFollower>();
            VendorReview = new HashSet<VendorReview>();
            Order = new HashSet<Order>();
            CustomerAddressBook = new HashSet<CustomerAddressBook>();
            OrderCart = new HashSet<OrderCart>();
        }

        public int CustomerId { get; set; }
        public int RegistrationId { get; set; }
        public decimal Point { get; set; }
        public string VerifiedPhone { get; set; }
        public virtual Registration Registration { get; set; }
        public virtual ICollection<ProductReview> ProductReview { get; set; }
        public virtual ICollection<ProductFaq> ProductFaq { get; set; }
        public virtual ICollection<VendorFollower> VendorFollower { get; set; }
        public virtual ICollection<VendorReview> VendorReview { get; set; }
        public virtual ICollection<Order> Order { get; set; }
        public virtual ICollection<CustomerAddressBook> CustomerAddressBook { get; set; }
        public virtual ICollection<OrderCart> OrderCart { get; set; }
    }
}
