using System;
using System.Collections.Generic;

namespace eSuperShop.Data
{
    public class Registration
    {
        public Registration()
        {
            AllAttribute = new HashSet<AllAttribute>();
            AllBrand = new HashSet<AllBrand>();
            AllSpecification = new HashSet<AllSpecification>();
            Catalog = new HashSet<Catalog>();
            Customer = new HashSet<Customer>();
            CatalogAttribute = new HashSet<CatalogAttribute>();
            CatalogBrand = new HashSet<CatalogBrand>();
            CatalogShownPlace = new HashSet<CatalogShownPlace>();
            CatalogSpecification = new HashSet<CatalogSpecification>();
            Seo = new HashSet<Seo>();
            Slider = new HashSet<Slider>();
            Vendor = new HashSet<Vendor>();
            VendorCatalog = new HashSet<VendorCatalog>();
            VendorWarehouse = new HashSet<VendorWarehouse>();
            Warehouse = new HashSet<Warehouse>();
        }

        public int RegistrationId { get; set; }
        public string UserName { get; set; }
        public bool? Validation { get; set; }
        public UserType Type { get; set; }
        public string Name { get; set; }
        public string DateofBirth { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedOnUtc { get; set; }

        public virtual ICollection<AllAttribute> AllAttribute { get; set; }
        public virtual ICollection<AllBrand> AllBrand { get; set; }
        public virtual ICollection<AllSpecification> AllSpecification { get; set; }
        public virtual ICollection<Catalog> Catalog { get; set; }
        public virtual ICollection<Customer> Customer { get; set; }
        public virtual ICollection<CatalogAttribute> CatalogAttribute { get; set; }
        public virtual ICollection<CatalogBrand> CatalogBrand { get; set; }
        public virtual ICollection<CatalogShownPlace> CatalogShownPlace { get; set; }
        public virtual ICollection<CatalogSpecification> CatalogSpecification { get; set; }
        public virtual ICollection<Seo> Seo { get; set; }
        public virtual ICollection<Slider> Slider { get; set; }
        public virtual ICollection<Vendor> Vendor { get; set; }
        public virtual ICollection<VendorCatalog> VendorCatalog { get; set; }
        public virtual ICollection<VendorWarehouse> VendorWarehouse { get; set; }
        public virtual ICollection<Warehouse> Warehouse { get; set; }
    }
}
