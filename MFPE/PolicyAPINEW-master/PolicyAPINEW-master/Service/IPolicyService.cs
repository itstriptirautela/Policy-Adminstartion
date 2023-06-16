using PolicyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolicyAPI.Service
{
    public interface IPolicyService
    {

        public int GetQuote(int businessValue, int propertyValue, string authtoken);
        public Property GetPropertiesById(int id, string authtoken);
        public Business GetBusinessById(int id, string authtoken);
        public Task<string> CreatePolicy(int propertyId, string authtoken);
        public Task<string> IssuePolicy(int policyId, string paymentDetails);
        public dynamic ViewPolicyById(int policyId, string authtoken);
        public ConsumerPolicy GetPoliciesById(int id);
        public List<ConsumerPolicy> GetConsumerPolicies();
    }
}
