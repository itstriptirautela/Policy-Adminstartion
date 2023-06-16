using ConsumerMicroservice.Models;
using ConsumerMicroservice.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumerMicroservice.Repository
{
    public class ConsumerRepo : IConsumerRepo
    {
        private readonly ConsumerContext _dbContext;
        public ConsumerRepo(ConsumerContext dbcontext)
        {
            _dbContext = dbcontext;
        }
        
        public bool CreateConsumer(Consumer consumer)
        {
            _dbContext.Consumer.Add(consumer);
            return SavingDetails();
        }

        
        public Consumer GetConsumer(int consumerId)
        {
            return _dbContext.Consumer.FirstOrDefault(x => x.ConsumerId == consumerId);
        }

        
        public IEnumerable<Consumer> GetConsumers()
        {
            return _dbContext.Consumer.ToList();
        }

        
        public bool UpdateConsumer(int consumerId, Consumer consumer)
        {
            _dbContext.Consumer.Update(consumer);
            return SavingDetails();
        }

        
        public bool ConsumerExists(int consumerId)
        {
            return _dbContext.Consumer.Any(a => a.ConsumerId == consumerId);
        }

        /// <summary>
        /// return True if there is a insertion into the database else false
        /// </summary>
        /// <returns></returns>        
        public bool SavingDetails()
        {
            return _dbContext.SaveChanges() >= 0 ? true : false;
        }

        public IEnumerable<BusinessMaster> GetBusinessMaster()
        {
            return _dbContext.BusinessMaster.ToList();
        }

        public IEnumerable<PropertyMaster> GetPropertyMaster()
        {
            return _dbContext.PropertyMaster.ToList();
        }
    }
}
