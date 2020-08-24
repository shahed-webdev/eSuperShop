using eSuperShop.Repository;

namespace eSuperShop.BusinessLogic
{
    public interface ISeoCore
    {
        DbResponse Post(SeoAddModel model, string userName);
        DbResponse<SeoModel> Get(int id);
        DbResponse Delete(int id);
    }
}