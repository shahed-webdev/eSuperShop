namespace eSuperShop.Repository
{
    public interface ISeoRepository
    {
        SeoModel GetSeo(int id);
        void SeoDelete(int id);
        bool IsSeoExist(int id);
        void PostSeo(SeoAddModel model);
    }
}