using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolicyAPI.Models
{
    public class PolicyContext:DbContext
    {
        public PolicyContext()
        {

        }
        public PolicyContext(DbContextOptions<PolicyContext> options) : base(options)
        {

        }
        public virtual DbSet<ConsumerPolicy> consumerPolicies { get; set; }
        public virtual DbSet<PolicyMaster> policies { get; set; }
    }
}
