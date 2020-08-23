using eSuperShop.Repository;

namespace eSuperShop.BusinessLogic
{
    public interface ISeoCore
    {
        DbResponse<SeoModel> Add(SeoAddModel model, string userName);
        DbResponse<SeoModel> Update(SeoAddModel model);
        DbResponse Delete(int id);
    }

    public class SeoAddModel
    {
    }

    public class SeoModel
    {
    }
}