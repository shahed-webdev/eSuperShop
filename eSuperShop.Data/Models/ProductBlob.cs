using System;

namespace eSuperShop.Data
{
    public class ProductBlob
    {
        public int ProductBlobId { get; set; }
        public int ProductId { get; set; }
        public string BlobFileName { get; set; }
        public int? DisplayOrder { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public virtual Product Product { get; set; }
    }
}
