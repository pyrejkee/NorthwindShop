using System.Collections.Generic;

namespace NorthwindShop.DAL.Entities
{
    public class Region
    {
        public int RegionId { get; set; }
        public string RegionDescription { get; set; }

        public ICollection<Territory> Territories { get; set; }
    }
}
