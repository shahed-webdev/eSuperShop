using System.Collections.Generic;

namespace eSuperShop.Data
{
    public class Region
    {
        public Region()
        {
            Areas = new HashSet<Area>();
        }

        public int RegionId { get; set; }
        public string RegionName { get; set; }
        public virtual ICollection<Area> Areas { get; set; }
    }
}