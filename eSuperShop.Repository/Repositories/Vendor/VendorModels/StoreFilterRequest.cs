using Paging.Infrastructure;

namespace eSuperShop.Repository
{
    public class StoreFilterRequest : PageRequest
    {
    }

    public class StoreProductFilterRequest : PageRequest
    {
        public int VendorId { get; set; }
    }
}