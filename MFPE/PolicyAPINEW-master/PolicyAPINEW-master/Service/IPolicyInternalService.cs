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
    public interface IPolicyInternalService
    {
        public Task<string> CreatePolicy(int propertyId, string authtoken);
        public Task<string> IssuePolicy(int policyId, string paymentDetails);
        public dynamic ViewPolicyById(int policyId, string authtoken);
    }
}
