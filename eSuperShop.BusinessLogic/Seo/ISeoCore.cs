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

    public class SeoAddModel
    {
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public int CreatedByRegistrationId { get; set; }
    }

    public class SeoModel
    {
        public int SeoId { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public string CreatedBy { get; set; }
    }
}