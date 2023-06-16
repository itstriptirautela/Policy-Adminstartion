using AuthorizationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationAPI.Repository
{
    public interface IAgentRepo
    {
        public List<Agent> getAllAgents();
        public Agent getAgent(User user);
    }
}
