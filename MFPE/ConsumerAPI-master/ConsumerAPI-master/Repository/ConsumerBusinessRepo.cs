using ConsumerMicroservice.Models;
using ConsumerMicroservice.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumerMicroservice.Repository
{
    public class ConsumerBusinessRepo:IConsumerBusinessRepo
    {
        private readonly ConsumerContext _dbContext;
        public ConsumerBusinessRepo(ConsumerContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Business> GetBusiness()
        {
            return _dbContext.Business.Include(c => c.BusinessMaster).ToList();
        }

        public Business GetBusinesss(int businessId)
        {
            return _dbContext.Business.Include(c => c.BusinessMaster).FirstOrDefault(x => x.BusinessId == businessId);
        }

        
        public bool CreateBusiness(Business business)
        {
            _dbContext.Business.Add(business);
            return SavingDetails();
        }

        
        public bool UpdateBusiness(int consumerId, Business business)
        {
            _dbContext.Business.Update(business);
            return SavingDetails();
        }

        
        public bool BusinessExists(int businessId)
        {
            return _dbContext.Business.Any(a => a.BusinessId == businessId);
        }

        /// <summary>
        /// return true if there is a insertion into database else false
        /// </summary>
        /// <returns></returns>
        public bool SavingDetails()
        {
            return _dbContext.SaveChanges() >= 0 ? true : false;
        }
    }
}
