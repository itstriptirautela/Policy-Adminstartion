using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationAPI.Models
{
    public class Agent
    {
        [Key]
        public int AgentId { get; set; }
        public string AgentName { get; set; }
        public string Password { get; set; }
    }
}
