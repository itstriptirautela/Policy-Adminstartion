using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolicyAPI.Models
{
    public class Quotes
    {
        public int QuoteId { get; set; }
        public int BusinesssValueFrom { get; set; }
        public int BusinesssValueTo { get; set; }
        public int PropertyValueFrom { get; set; }
        public int PropertyValueTo { get; set; }
        public string PropertyType { get; set; }
        public int QuoteValue { get; set; }
    }
}
