using AuthorizationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationAPI.Service
{
    public interface IAgentService
    {
        public List<Agent> getAllAgents();
        public Agent getAgent(User user);
        public string GenerateJSONWebToken(string userRole);
    }
}
