using AuthorizationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationAPI.Repository
{
    public class AgentRepo:IAgentRepo
    {
        private readonly AgentContext authcontext;
        public AgentRepo(AgentContext _authcontext)
        {
            authcontext = _authcontext;
        }
        public List<Agent> getAllAgents()
        {
            List<Agent> users=authcontext.agents.ToList();
            return users;
        }

        public Agent getAgent(User user)
        {
            return getAllAgents().SingleOrDefault(a => a.AgentName == user.UserName && a.Password == user.Password);
        }


    }
}
