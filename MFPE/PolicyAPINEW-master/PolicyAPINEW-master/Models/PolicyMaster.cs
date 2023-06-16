using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PolicyAPI.Models
{
    public class PolicyMaster
    {
        [Key]
        public int ID { get; set; }
        public string PropertyType { get; set; }
        public string ConsumerType { get; set; }
        public long AssuredSum { get; set; }
        public int Tenure { get; set; }
        public int BusinessValue { get; set; }
        public int PropertyValue { get; set; }
        public string BaseLocation { get; set; }
        public string PlolicyType { get; set; }

        public virtual List<ConsumerPolicy> consumerpolicies { get; set; }
    }
}
