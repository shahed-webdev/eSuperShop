using System;
using System.Collections.Generic;

namespace eSuperShop.Data
{
    public partial class Registration
    {
        public Registration()
        {
            Catalog = new HashSet<Catalog>();
            CatalogShownPlace = new HashSet<CatalogShownPlace>();
            Seo = new HashSet<Seo>();
            Sider = new HashSet<Slider>();
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

        public virtual ICollection<Catalog> Catalog { get; set; }
        public virtual ICollection<CatalogShownPlace> CatalogShownPlace { get; set; }
        public virtual ICollection<Seo> Seo { get; set; }
        public virtual ICollection<Slider> Sider { get; set; }
    }
}
