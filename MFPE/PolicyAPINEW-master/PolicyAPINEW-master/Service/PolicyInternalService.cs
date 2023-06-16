using Newtonsoft.Json;
using PolicyAPI.Models;
using PolicyAPI.Repository;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PolicyAPI.Service
{
    public class PolicyInternalService:IPolicyInternalService
    {
        private readonly IPolicyService policyservice;
        private readonly IPolicyRepo policyrepo;
        public PolicyInternalService(IPolicyService _policyservice, IPolicyRepo _policyrepo)
        {
            policyservice = _policyservice;
            policyrepo = _policyrepo;

        }
        public virtual Task<string> CreatePolicy(int PropertyId, string authtoken)
        {

            try
            {
                return policyservice.CreatePolicy(PropertyId, authtoken);
            }
            catch
            {

                return null;
            }
        }

        public virtual Task<string> IssuePolicy(int policyId, string paymentDetails)
        {
            try
            {
                return policyservice.IssuePolicy(policyId, paymentDetails);
            }
            catch
            {
                return null;
            }
        }

        public virtual dynamic ViewPolicyById(int policyId, string authtoken)
        {
            var policyById = policyservice.GetPoliciesById(policyId);
            var property = policyservice.GetPropertiesById(policyById.PropertyId, authtoken);
            var business = policyservice.GetBusinessById(property.BusinessId, authtoken);
            try
            {
                return policyrepo.ViewPolicyById(policyId, policyById, property, business);
            }
            catch
            {
                return "Id does not exist";
            }
        }
    }
}
