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
    public class PolicyService : IPolicyService
    {
        private readonly IPolicyRepo _policyRepo;
        private readonly IQuotesExternalService _quotesExternalService;
        private readonly IConsumerExternalService _consumerExternalService;


        public PolicyService(IPolicyRepo policyrepo,
            IQuotesExternalService quotesExternalService,
            IConsumerExternalService consumerExternalService)
        {
            _policyRepo = policyrepo;
            _quotesExternalService = quotesExternalService;
            _consumerExternalService = consumerExternalService;

        }

        public int GetQuote(int businessValue, int propertyValue, string authtoken)
        {
            return _quotesExternalService.GetQuote(businessValue, propertyValue, authtoken);
        }

        public Property GetPropertiesById(int id, string authtoken)
        {
            return _consumerExternalService.GetPropertiesById(id, authtoken);
        }
        public Business GetBusinessById(int id, string authtoken)
        {
            return _consumerExternalService.GetBusinessById(id, authtoken);
        }

        public List<ConsumerPolicy> GetConsumerPolicies()
        {
            return _policyRepo.GetConsumerPolicies().ToList();
        }


        public virtual async Task<string> CreatePolicy(int PropertyId, string authtoken)
        {
            string PolicyStatus;
            try
            {
                var property = GetPropertiesById(PropertyId, authtoken);

                if (property == null)
                {
                    PolicyStatus = "No such Property exists. Hence, Policy was not created.";
                    return PolicyStatus;
                }

                var business = GetBusinessById(property.BusinessId, authtoken);

                if (business.BusinessMaster != null && property.PropertyMaster != null)
                {
                    var quote = GetQuote(business.BusinessMaster.BusinessValue, property.PropertyMaster.PropertyValue, authtoken);
                    if (quote == 0)
                    {
                        PolicyStatus = "No such Quote exists. Hence, Policy was not created.";
                        return PolicyStatus;
                    }
                    PolicyMaster pm = _policyRepo.GetPolicyMaster(business);
                    if (pm == null)
                    {
                        PolicyStatus = "No such PolicyMaster exists. Hence, Policy was not created.";
                        return PolicyStatus;
                    }

                    PolicyStatus = "Initiated";

                    ConsumerPolicy policy = new ConsumerPolicy
                    {
                        PropertyId = PropertyId,
                        PolicyStatus = PolicyStatus,
                        QuoteValue = quote,
                        PolicyMasterId = property.PropertyMasterId
                    };

                    //context.consumerPolicies.Add(policy);
                    _policyRepo.AddConsumerPolicy(policy);
                    _policyRepo.Save();
                    return "Policy has been created with Policy Status \'" + PolicyStatus + "\'.";
                }
                return "Policy was not created.";
            }
            catch
            {
                PolicyStatus = "Policy was not created.";
                return PolicyStatus;
            }
        }

        public virtual async Task<string> IssuePolicy(int policyId, string paymentDetails)
        {
            try
            {
                if (paymentDetails == "Paid")
                {
                    ConsumerPolicy policy = _policyRepo.GetPoliciesById(policyId);
                    if (policy == null)
                    {
                        return "No Policy exists with ID " + policyId + ".";
                    }
                    if (policy.PolicyStatus == "Issued")
                    {
                        return "Policy has already been Issued.";
                    }
                    policy.PolicyStatus = "Issued";
                    _policyRepo.Save();
                    return "Policy has been " + policy.PolicyStatus + " for Policy ID " + policy.PolicyId + ".";
                }
                return "No Payment was made. Hence, Policy was not Issued.";
            }
            catch
            {
                return "Policy was not Issued.";
            }
        }

        public virtual dynamic ViewPolicyById(int policyId, string authtoken)
        {
            var policyById = GetPoliciesById(policyId);
            var property = GetPropertiesById(policyById.PropertyId, authtoken);
            var business = GetBusinessById(property.BusinessId, authtoken);
            try
            {
                return _policyRepo.ViewPolicyById(policyId, policyById, property, business);
            }
            catch
            {
                return "Id does not exist";
            }
        }

        public virtual ConsumerPolicy GetPoliciesById(int id)
        {
            return _policyRepo.GetPoliciesById(id);
        }
    }
}