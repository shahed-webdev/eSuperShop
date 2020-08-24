using eSuperShop.Repository;

namespace eSuperShop.BusinessLogic
{
    public interface ISeoCore
    {
        DbResponse<SeoModel> Add(SeoAddModel model, string userName);
        DbResponse<SeoModel> Update(SeoAddModel model);
        DbResponse<SeoModel> Get(int id);
        DbResponse Delete(int id);
    }
}