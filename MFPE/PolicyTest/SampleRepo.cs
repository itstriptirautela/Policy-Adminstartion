using PolicyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicyTest
{
    public class SampleRepo
    {
        public List<Property> GetSampleProperties()
        {
            List<Property> properties = new List<Property>
            {
                new Property
                {
                    PropertyId = 1,
                    BuildingType = "Office",
                    BuildingStoreys = 10,
                    BuildingAge = 10,
                    BusinessId = 1,
                    PropertyMasterId = 1
                },
                new Property
                {
                    PropertyId = 2,
                    BuildingType = "Factory",
                    BuildingStoreys = 5,
                    BuildingAge = 11,
                    BusinessId = 2,
                    PropertyMasterId = 2
                }
            };
            return properties;
        }

        public List<ConsumerPolicy> GetSamplePolicies()
        {
            List<ConsumerPolicy> policies = new List<ConsumerPolicy>
            {
                new ConsumerPolicy
                {
                    PolicyId = 1,
                    PropertyId = 1,
                    QuoteValue = 1,
                    PolicyStatus = "Initiated",
                    PolicyMasterId = 1
                },
                new ConsumerPolicy
                {
                    PolicyId = 2,
                    PropertyId = 1,
                    QuoteValue = 1,
                    PolicyStatus = "Issued",
                    PolicyMasterId = 1
                }
            };
            return policies;
        }


        public List<Quotes> GetSampleQuotes()
        {
            List<Quotes> quotes = new List<Quotes>
            {
                new Quotes
                {
                    QuoteId = 1,
                    PropertyValueFrom = 0,
                    PropertyValueTo = 2,
                    BusinesssValueFrom = 0,
                    BusinesssValueTo = 2,
                    PropertyType = "Office",
                    QuoteValue = 1000
                },
                new Quotes
                {
                    QuoteId = 2,
                    PropertyValueFrom = 3,
                    PropertyValueTo = 5,
                    BusinesssValueFrom = 3,
                    BusinesssValueTo = 5,
                    PropertyType = "Office",
                    QuoteValue = 102010
                }
            };
            return quotes;
        }

        public List<BusinessMaster> GetSampleBusinessMaster()
        {
            List<BusinessMaster> businessmasters = new List<BusinessMaster>
            {
                new BusinessMaster
                {
                    BusinessMasterId =1,
                    BusinessValue =454545,
                    BusinessTurnOver =898989,
                    CapitalInvest =111111111
                },
                new BusinessMaster
                {
                    BusinessMasterId =1,
                    BusinessValue =454545,
                    BusinessTurnOver =898989,
                    CapitalInvest =111111111
                }
        };
            return businessmasters;
        }

        public List<Business> GetSampleBusiness()
        {
            List<Business> businesses = new List<Business>
            {
                new Business
                {
                     BusinessId =1,
                     BusinessName="XEnterprise",
                     BusinessType ="IT",
                     TotalEmployees =789,
                     BusinessMasterId =1,
                      ConsumerId =1
                },
                new Business
                {
                     BusinessId =2,
                     BusinessName="YEnterprise",
                     BusinessType ="Hardware",
                     TotalEmployees =600,
                     BusinessMasterId =2,
                      ConsumerId =2
                }
            };
            return businesses;
        }

        public List<PolicyMaster> GetSamplePolicyMasters()
        {
            List<PolicyMaster> policyMasters = new List<PolicyMaster>
            {
                new PolicyMaster
                {
                    ID = 1,
                    PropertyType = "Building",
                    PropertyValue = 2,
                    ConsumerType = "regular",
                    AssuredSum = 10000,
                    Tenure = 102910,
                    BusinessValue = 2,
                    BaseLocation = "Kolkata",
                    PlolicyType = "sample"
                },
                new PolicyMaster
                {
                    ID = 2,
                    PropertyType = "Building",
                    PropertyValue = 2,
                    ConsumerType = "regular",
                    AssuredSum = 10000,
                    Tenure = 102910,
                    BusinessValue = 2,
                    BaseLocation = "Kolkata",
                    PlolicyType = "sample"
                }
            };
            return policyMasters;
        }
        public int GetQuote(int BusinessValue, int PropertyValue)
        {
            List<Quotes> quotes = GetSampleQuotes();
            if (BusinessValue >= 0 && BusinessValue <= 10 && PropertyValue >= 0 && PropertyValue <= 10)
            {
                foreach (Quotes q in quotes)
                {
                    if (BusinessValue >= q.BusinesssValueFrom && BusinessValue <= q.BusinesssValueTo &&
                        PropertyValue >= q.PropertyValueFrom && PropertyValue <= q.PropertyValueTo)
                    {
                        return q.QuoteValue;
                    }
                }
            }
            return 0;
        }

    }
}


