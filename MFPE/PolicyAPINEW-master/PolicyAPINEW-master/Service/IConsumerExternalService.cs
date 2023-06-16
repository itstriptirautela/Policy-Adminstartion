using PolicyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PolicyAPI.Service
{
    public interface IConsumerExternalService
    {
        public Property GetPropertiesById(int id, string authtoken);
        public Business GetBusinessById(int id, string authtoken);

    }
}
