using AuthorizationAPI.Models;
using AuthorizationAPI.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationAPI.Service
{
    public class AgentService:IAgentService
    {
        private readonly IAgentRepo agentRepo;
        private readonly IConfiguration _config;

        public AgentService(IAgentRepo _agentRepo, IConfiguration config)
        {
            agentRepo = _agentRepo;
            _config = config;
        }

        public List<Agent> getAllAgents()
        {
            return agentRepo.getAllAgents();
        }
        public Agent getAgent(User user)
        {
            return agentRepo.getAgent(user);

        }

        public string GenerateJSONWebToken(string userRole)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
                {
                new Claim(ClaimTypes.Role, userRole),
                };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
            _config["Jwt:Issuer"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
 }

