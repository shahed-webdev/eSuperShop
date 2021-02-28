namespace eSuperShop.Data
{
    public class Area
    {
        public int AreaId { get; set; }
        public string AreaName { get; set; }
        public int RegionId { get; set; }
        public virtual Region Region { get; set; }
    }
}