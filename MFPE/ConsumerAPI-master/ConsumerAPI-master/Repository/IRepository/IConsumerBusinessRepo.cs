using ConsumerMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumerMicroservice.Repository.IRepository
{
    public interface IConsumerBusinessRepo
    {
        IEnumerable<Business> GetBusiness();
        Business GetBusinesss(int businessId);
        bool CreateBusiness(Business business);
        bool UpdateBusiness(int consumerId, Business business);
        bool BusinessExists(int businessId);
        bool SavingDetails();
    }
}
