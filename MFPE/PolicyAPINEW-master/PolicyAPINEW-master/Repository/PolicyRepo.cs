using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PolicyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PolicyAPI.Repository
{
    public class PolicyRepo : IPolicyRepo
    {

        private readonly PolicyContext context;

        public PolicyRepo(PolicyContext policyDBContext)
        {
            context = policyDBContext;
        }

        public virtual ConsumerPolicy GetPoliciesById(int id)
        {
            return context.consumerPolicies.Find(id);
        }

        public void AddConsumerPolicy(ConsumerPolicy policy)
        {
            context.consumerPolicies.Add(policy);
            return;
        }
        public PolicyMaster GetPolicyMaster(Business business)
        {
            return context.policies
                    .Where(pm => pm.BusinessValue >= business.BusinessMaster.BusinessValue)
                    .SingleOrDefault();
        }

        public List<ConsumerPolicy> GetConsumerPolicies()
        {
            return context.consumerPolicies.ToList();
        }
        public virtual dynamic ViewPolicyById(int PolicyId, ConsumerPolicy policyById, Property property, Business business)
        {
            var policy = context.consumerPolicies
                    .Where(cp => cp.PolicyId == PolicyId)
                    .Select(cp => new
                    {
                        PolicyId = cp.PolicyId,
                        BuildingType = property.BuildingType,
                        PolicyStatus = cp.PolicyStatus,
                        PropertyId = cp.PropertyId,
                        PropertyValue = property.PropertyMaster.PropertyValue,
                        BusinessValue = business.BusinessMaster.BusinessValue,
                        QuoteValue = cp.QuoteValue,
                        ConsumerId = business.ConsumerId,
                    }).FirstOrDefault();
            return policy;
        }

        public void Save()
        {
            context.SaveChanges();
            return;
        }
    }
}
