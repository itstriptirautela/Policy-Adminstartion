using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PolicyAPI.Models
{
    public class ConsumerPolicy
    {
        [Key]
        public int PolicyId { get; set; }
        public int PropertyId { get; set; }
        public int QuoteValue { get; set; }
        public string PolicyStatus { get; set; }
        public int PolicyMasterId { get; set; }
    }
}
