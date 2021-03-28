namespace eSuperShop.Repository
{
    public class ProductDetailsApprovedModel
    {
        public int ProductId { get; set; }
        public int CatalogId { get; set; }
        public string Name { get; set; }
        public string SlugUrl { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public decimal Price { get; set; }
        public decimal OldPrice { get; set; }
        public int StockQuantity { get; set; }
        public bool Published { get; set; }
        public string CatalogName { get; set; }
        public string BrandName { get; set; }
        public string[] BlobFileNames { get; set; }
    }
}