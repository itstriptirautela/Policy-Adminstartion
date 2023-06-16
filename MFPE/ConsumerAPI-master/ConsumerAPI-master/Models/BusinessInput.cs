using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumerMicroservice.Models
{
    public class BusinessInput
    { 
        public string BusinessName { get; set; }
        public string BusinessType { get; set; }
        public int TotalEmployees { get; set; }
        public int ConsumerId { get; set; }
        public int BusinessTurnOver { get; set; }
        public int CapitalInvest { get; set; }
    }
}
