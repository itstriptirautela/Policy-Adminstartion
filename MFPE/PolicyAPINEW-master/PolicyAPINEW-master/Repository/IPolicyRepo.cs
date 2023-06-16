using PolicyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolicyAPI.Repository
{
    public interface IPolicyRepo
    {
        public dynamic ViewPolicyById(int PolicyId, ConsumerPolicy policyById, Property property, Business business);
        public ConsumerPolicy GetPoliciesById(int id);
        public PolicyMaster GetPolicyMaster(Business business);
        public void AddConsumerPolicy(ConsumerPolicy policy);
        public void Save();
        public List<ConsumerPolicy> GetConsumerPolicies();
    }
}
