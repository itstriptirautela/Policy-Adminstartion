using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuotesAPI.Models
{
    public class QuotesContext:DbContext
    {
        public QuotesContext()
        {

        }
        public QuotesContext(DbContextOptions<QuotesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<QuotesMaster> Quotes { get; set; }

    }
}
