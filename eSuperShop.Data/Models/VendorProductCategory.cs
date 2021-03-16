using System;
using System.Collections.Generic;

namespace eSuperShop.Data
{
    public class VendorProductCategory
    {
        public VendorProductCategory()
        {
            VendorProductCategoryList = new HashSet<VendorProductCategoryList>();
        }
        public int VendorProductCategoryId { get; set; }
        public int VendorId { get; set; }
        public string Name { get; set; }
        public string ImageFileName { get; set; }

        public string ChangedName { get; set; }
        public string ChangedImageFileName { get; set; }
        public int? DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOnUtc { get; set; }

        public virtual Vendor Vendor { get; set; }

        public virtual ICollection<VendorProductCategoryList> VendorProductCategoryList { get; set; }
        public bool IsApprovedByAdmin { get; set; }
    }
}