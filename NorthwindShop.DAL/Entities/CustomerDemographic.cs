using System.Collections.Generic;

namespace NorthwindShop.DAL.Entities
{
    public class CustomerDemographic
    {
        public string CustomerTypeId { get; set; }
        public string CustomerDesc { get; set; }

        public ICollection<CustomerCustomerDemo> CustomerCustomerDemo { get; set; }
    }
}
