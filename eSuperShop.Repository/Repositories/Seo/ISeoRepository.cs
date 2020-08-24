using eSuperShop.Data;

namespace eSuperShop.Repository
{
    public interface ISeoRepository
    {
        Seo Seo { get; set; }

        void add();
    }
}