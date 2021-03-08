using System.Collections.Generic;

namespace eSuperShop.Repository
{
    public class StoreProductViewModel
    {
        public StoreProductViewModel()
        {
            Products = new HashSet<ProductListViewModel>();
        }
        public int VendorProductCategoryId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        public ICollection<ProductListViewModel> Products { get; set; }
    }
}