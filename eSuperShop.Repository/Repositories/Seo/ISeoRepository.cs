using eSuperShop.Data;

namespace eSuperShop.Repository
{
    public interface ISeoRepository
    {
        Seo Seo { get; set; }
        void add(SeoAddModel model);
        void Update(SeoAddModel model);
        SeoModel Get(int id);
    }
}