using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Customers.Profiles
{
    public class CustomerProfile : AutoMapper.Profile
    {
        public CustomerProfile()
        {
            CreateMap<Db.Customer, Models.Customer>();
        }
    }
}
