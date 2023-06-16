using ConsumerMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumerMicroservice.Repository.IRepository
{
    public interface IConsumerRepo
    {
        IEnumerable<Consumer> GetConsumers();
        Consumer GetConsumer(int consumerId);
        bool CreateConsumer(Consumer consumer);
        bool UpdateConsumer(int consumerId, Consumer consumer);
        bool ConsumerExists(int consumerId);
        IEnumerable<BusinessMaster> GetBusinessMaster();
        IEnumerable<PropertyMaster> GetPropertyMaster();
        bool SavingDetails();
    }
}
