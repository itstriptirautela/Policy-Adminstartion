using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumerMicroservice.Models
{
    public class BusinessMaster
    {
        [Key]
        public int BusinessMasterId { get; set; }
        public int BusinessValue { get; set; }
        public int BusinessTurnOver { get; set; }
        public int CapitalInvest { get; set; }
    }
}
