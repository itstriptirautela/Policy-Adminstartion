using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolicyAPI.Service
{
    public interface IQuotesExternalService
    {
        public int GetQuote(int businessValue, int propertyValue, string authToken);
    }
}
