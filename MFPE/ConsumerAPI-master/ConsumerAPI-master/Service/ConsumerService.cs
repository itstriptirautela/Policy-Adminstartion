using ConsumerMicroservice.Models;
using ConsumerMicroservice.Repository.IRepository;
using ConsumerMicroservice.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumerMicroservice.Service
{
    public class ConsumerService : IConsumerService
    {
        private readonly IConsumerRepo _consumerRepository;
        private readonly IConsumerBusinessRepo _consumerBusinessRepository;
        private readonly IPropertyRepo _businessPropertyRepository;
        public ConsumerService(IConsumerRepo consumerRepository, IConsumerBusinessRepo consumerBusinessRepository,
            IPropertyRepo businessPropertyRepository)
        {
            _consumerRepository = consumerRepository;
            _consumerBusinessRepository = consumerBusinessRepository;
            _businessPropertyRepository = businessPropertyRepository;
        }

        public bool BusinessExists(int businessId)
        {
            try
            {
                if (businessId != null)
                {
                    bool result = _consumerBusinessRepository.BusinessExists(businessId);
                    if (result != false)
                    {
                        return result;
                    }
                    else
                    {
                        return false;
                    }
                }

            }
            catch
            {
                return false;
            }
            return false;
        }

        public bool ConsumerExists(int consumerId)
        {
            try
            {
                if(consumerId!=null)
                {
                    bool result = _consumerRepository.ConsumerExists(consumerId);
                    if (result != false)
                    {
                        return result;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
            return false;
            
        }

        public bool CreateBusiness(Business business)
        {
            try
            {
                if(business!=null)
                {
                    bool result = _consumerBusinessRepository.CreateBusiness(business);
                    if (result != false)
                    {
                        return result;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
            return false;
            
        }

        public bool CreateConsumer(Consumer consumer)
        {
            try
            {
                if(consumer!=null)
                {
                    bool result = _consumerRepository.CreateConsumer(consumer);
                    if (result != false)
                    {
                        return result;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
            return false;
             
        }

        public bool CreateProperty(Property property)
        {
            try
            {
                if(property!=null)
                {
                    bool result = _businessPropertyRepository.CreateProperty(property);
                    if (result != false)
                    {
                        return result;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
            return false;
        }

        public IEnumerable<BusinessMaster> GetBusinessMaster()
        {
            return _consumerRepository.GetBusinessMaster();
        }

        public IEnumerable<Business> GetBusiness()
        {
            return _consumerBusinessRepository.GetBusiness();
        }

        public Business GetBusinesss(int businessId)
        {
            try
            {
                if (businessId != null)
                {
                    Business business = _consumerBusinessRepository.GetBusinesss(businessId);
                    if (business != null)
                    {
                        return business;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch
            {
                return null;
            }
            return null;

        }

        public Consumer GetConsumer(int consumerId)
        {
            try
            {
                if (consumerId != null)
                {
                    Consumer consumer = _consumerRepository.GetConsumer(consumerId);
                    if (consumer != null)
                    {
                        return consumer;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch
            {
                return null;
            }
            return null;
        }

        public IEnumerable<Consumer> GetConsumers()
        {
            return _consumerRepository.GetConsumers();
        }

        public Property GetProperties(int propertyId)
        {
            try
            {
                if (propertyId != null)
                {
                    Property property = _businessPropertyRepository.GetProperties(propertyId);
                    if (property != null)
                    {
                        return property;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch
            {
                return null;
            }
            return null;

        }

        public IEnumerable<Property> GetProperty()
        {
            return _businessPropertyRepository.GetProperty();
        }

        public IEnumerable<PropertyMaster> GetPropertyMaster()
        {
            return _consumerRepository.GetPropertyMaster();
        }

        public bool PropertyExists(int propertyId)
        {
            try
            {
                if(propertyId!=null)
                {
                    bool result = _businessPropertyRepository.PropertyExists(propertyId);
                    if(result!=false)
                    {
                        return result;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
            return false;
        }

        public bool SavingDetails()
        {
            return _consumerRepository.SavingDetails();
        }

        public bool UpdateBusiness(int businessId, Business business)
        {
            try
            {
                if(businessId!=null&&business!=null)
                {
                    bool result = _consumerBusinessRepository.UpdateBusiness(businessId, business);
                    if(result!=false)
                    {
                        return result;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
            return false;
            
        }

        public bool UpdateConsumer(int consumerId, Consumer consumer)
        {
            try
            {
                if (consumerId != null && consumer != null)
                {
                    bool result = _consumerRepository.UpdateConsumer(consumerId, consumer);
                    if (result != false)
                    {
                        return result;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
            return false;
        }

        public bool UpdateProperty(int propertyId, Property property)
        {
            try
            {
                if (propertyId != null && property != null)
                {
                    bool result = _businessPropertyRepository.UpdateProperty(propertyId, property);
                    if (result != false)
                    {
                        return result;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
            return false;
        }

        public BusinessMaster BusinessMasterByValues(int businessTurnOver, int capitalInvest)
        {
            BusinessMaster businessmaster=GetBusinessMaster().FirstOrDefault(x => x.BusinessTurnOver >= businessTurnOver 
        && x.CapitalInvest >= capitalInvest);
            return businessmaster;
        }

        public PropertyMaster PropertyMasterByValues(int costOfAsset, int salvageValue, int usefulLifeOfAsset)
        {
            PropertyMaster propertyMaster = GetPropertyMaster().FirstOrDefault(x => x.CostOfAssest >= costOfAsset
              && x.SalvageValue >= salvageValue && x.UsefulLifeOfAssest >= usefulLifeOfAsset);
            return propertyMaster;
        }
    }
}
