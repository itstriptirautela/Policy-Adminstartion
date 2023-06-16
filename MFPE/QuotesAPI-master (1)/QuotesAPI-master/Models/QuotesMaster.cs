using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuotesAPI.Models
{
    public class QuotesMaster
    {
        [Key]
        public int QuoteId { get; set; }
        public int BusinesssValueFrom { get; set; }
        public int BusinesssValueTo { get; set; }
        public int PropertyValueFrom { get; set; }
        public int PropertyValueTo { get; set; }
        public string PropertyType { get; set; }
        public int QuoteValue { get; set; }
    }
}
