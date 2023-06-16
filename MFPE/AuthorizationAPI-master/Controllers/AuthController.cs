using AuthorizationAPI.Models;
using AuthorizationAPI.Repository;
using AuthorizationAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAgentService agentService;
        private readonly log4net.ILog _log4net;
        public AuthController(IAgentService _agentService)
        {
            agentService = _agentService;
            _log4net = log4net.LogManager.GetLogger(typeof(AuthController));

        }
       

        [HttpPost]
        public string Get([FromBody]User user)
        {
            var validuser = agentService.getAgent(user);
            if (validuser == null)
            {
                _log4net.Info("No Agent is Found");
                return "Agent Not Found";
            }
            else
            {
                string tokenString = agentService.GenerateJSONWebToken("User");
                _log4net.Info("Agent is Found");
                _log4net.Info("Token is Generated");
                return tokenString;
            }

        }
        [HttpGet("GetAllAgents")]
        public JsonResult GetAllAgents()
        {
            _log4net.Info("Getting All Agents");
            return new JsonResult(agentService.getAllAgents().ToList());
        }

    }
}
