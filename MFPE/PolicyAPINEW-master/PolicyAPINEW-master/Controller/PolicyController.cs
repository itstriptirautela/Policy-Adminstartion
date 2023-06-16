using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PolicyAPI.Models;
using PolicyAPI.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolicyAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Authorize(Roles = "User")]
    public class PolicyController : ControllerBase
    {
        private readonly IPolicyService _policyService;
        private readonly log4net.ILog _log4Net;

        public PolicyController(IPolicyService policyService)
        {
            _policyService = policyService;
            _log4Net = log4net.LogManager.GetLogger(typeof(PolicyController));
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("GetQuote")]
        public int GetQuote(int businessValue, int propertyValue)
        {
            _log4Net.Info("Getting quote value");
            var authToken = this.Request.Headers["Authorization"][0];
            int quote = _policyService.GetQuote(businessValue, propertyValue, authToken);
            return quote;
        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("GetPropertiesById")]
        public IActionResult GetProperties(int id)
        {
            _log4Net.Info("Getting properties from Consumer API");
            var authToken = this.Request.Headers["Authorization"][0];
            var properties = _policyService.GetPropertiesById(id, authToken);
            return Ok(properties);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("GetBusinessById")]
        public IActionResult GetBusiness(int id)
        {
            _log4Net.Info("Getting business values from Consumer API");
            var authToken = this.Request.Headers["Authorization"][0];
            var business = _policyService.GetBusinessById(id, authToken);
            return Ok(business);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("CreatePolicy")]
        public async Task<string> CreatePolicy(int propertyId)
        {
            _log4Net.Info("Creating policy has been accessed");
            var authToken = this.Request.Headers["Authorization"][0];
            return await _policyService.CreatePolicy(propertyId, authToken);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("IssuePolicy")]
        public async Task<string> IssuePolicy(int policyId, string paymentDetails)
        {
            _log4Net.Info("IssuePolicy has been accessed");
            return await _policyService.IssuePolicy(policyId, paymentDetails);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("ViewConsumerPolicyById")]
        public virtual dynamic ViewConsumerPolicyById(int policyId)
        {
            _log4Net.Info("ViewConsumerPolicyById has been accessed");
            var authToken = this.Request.Headers["Authorization"][0];
            var policy = _policyService.ViewPolicyById(policyId, authToken);
            return policy;
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("ViewPoliciesById")]
        public dynamic GetPoliciesById(int id)
        {
            _log4Net.Info("Getting Policies By Id");
            var policy = _policyService.GetPoliciesById(id);
            return policy;
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("ConsumerPolicies")]
        public IActionResult GetConsumerPolicies()
        {
            _log4Net.Info("Getting consumer policies from Policy API");
            return Ok(_policyService.GetConsumerPolicies());
        }
    }
}
