using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumerMicroservice.Models
{
    public class ConsumerContext:DbContext
    {
        public ConsumerContext()
        {

        }

        public ConsumerContext(DbContextOptions<ConsumerContext> options): base(options)
        {
        }

        public virtual DbSet<Business> Business { get; set; }
        public virtual DbSet<BusinessMaster> BusinessMaster { get; set; }
        public virtual DbSet<Consumer> Consumer { get; set; }
        public virtual DbSet<Property> Property { get; set; }
        public virtual DbSet<PropertyMaster> PropertyMaster { get; set; }
    }
}
