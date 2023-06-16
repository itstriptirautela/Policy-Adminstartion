using ConsumerMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumerMicroservice.Service
{
    public interface IConsumerService
    {
        IEnumerable<Consumer> GetConsumers();
        Consumer GetConsumer(int consumerId);
        bool CreateConsumer(Consumer consumer);
        bool UpdateConsumer(int consumerId, Consumer consumer);
        bool ConsumerExists(int consumerId);

        // For Business
        IEnumerable<Business> GetBusiness();
        Business GetBusinesss(int businessId);
        bool CreateBusiness(Business business);
        bool UpdateBusiness(int businessId, Business business);
        bool BusinessExists(int businessId);
        BusinessMaster BusinessMasterByValues(int businessTurnOver, int capitalInvest);

        // For PRoperty
        IEnumerable<Property> GetProperty();
        Property GetProperties(int propertyId);
        bool CreateProperty(Property property);
        bool UpdateProperty(int propertyId, Property property);
        bool PropertyExists(int propertyId);
        PropertyMaster PropertyMasterByValues(int costOfAsset, int salvageValue,int usefulLifeOfAsset);

        // Business Master and Property Master
        IEnumerable<BusinessMaster> GetBusinessMaster();
        IEnumerable<PropertyMaster> GetPropertyMaster();
        bool SavingDetails();
    }
}
