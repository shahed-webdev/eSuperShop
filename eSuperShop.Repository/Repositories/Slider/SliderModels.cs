using eSuperShop.Data;


namespace eSuperShop.Repository
{
    public class SliderAddModel
    {
        public int SliderId { get; set; }
        public string ImageUrl { get; set; }
        public string RedirectUrl { get; set; }
        public string FileName { get; set; }
        public int? DisplayOrder { get; set; }
        public string DisplayPlace { get; set; }
        public int CreatedByRegistrationId { get; set; }
    }
    public class SliderListModel
    {
        public int SliderId { get; set; }
        public string ImageUrl { get; set; }
        public string RedirectUrl { get; set; }
        public string FileName { get; set; }
        public int? DisplayOrder { get; set; }
        public SliderDisplayPlace DisplayPlace { get; set; }
        public string CreatedBy { get; set; }
    }
    public class SliderSlideModel
    {
        public int SliderId { get; set; }
        public string ImageUrl { get; set; }
        public string RedirectUrl { get; set; }
    }
}