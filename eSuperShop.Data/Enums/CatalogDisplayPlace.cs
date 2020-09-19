using System.ComponentModel;

namespace eSuperShop.Data
{
    public enum CatalogDisplayPlace
    {
        [Description("Main Catalog")] HomePageMain,
        [Description("Top Catalog")] HomePageTopCatalog,
        [Description("Quick Delivery")] HomePageQuickDelivery
    }
}