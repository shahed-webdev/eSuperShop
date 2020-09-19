using System.ComponentModel;

namespace eSuperShop.Data
{
    public enum StoreTheme
    {
        [Description("Only Category (pink)")] Default,
        [Description("Full Slider (black)")] FullSlider,
        [Description("Half Slider (blue)")] HalfSlider,
        [Description("Full Banner image (gray)")] BannerImage,
    }
}