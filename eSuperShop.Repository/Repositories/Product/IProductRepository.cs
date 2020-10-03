namespace eSuperShop.Repository
{
    public interface IProductRepository
    {
        void Add(ProductAddModel model);
        bool IsExistSlugUrl(string slugUrl);
        bool IsExistSlugUrl(string slugUrl, int updateId);
        bool IsNull(int id);
        bool IsRelatedDataExist(int id);
    }

}