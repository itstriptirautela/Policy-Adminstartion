using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationAPI.Models
{
    public class AgentContext:DbContext
    {
        public AgentContext()
        {

        }
        public AgentContext(DbContextOptions<AgentContext> options):base(options)
        {

        }

        public virtual DbSet<Agent> agents { get; set; }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsbuilder)
        {
           if (!optionsbuilder.IsConfigured)
           {
                optionsbuilder.UseSqlServer("Data Source=LTIN228011\\SQLEXPRESS;Initial Catalog=PolicyAdminDB;Integrated Security=True");
           }
        }*/
    }
}
