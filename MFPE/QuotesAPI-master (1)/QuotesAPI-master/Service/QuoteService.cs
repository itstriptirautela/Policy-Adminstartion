using QuotesAPI.Models;
using QuotesAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuotesAPI.Service
{
    public class QuoteService:IQuoteService
    {
        private readonly IQuoteRepo repo;
        public QuoteService(IQuoteRepo _repo)
        {
            repo = _repo;
        }
        public int GetQuotes(int businesssValue, int propertyValue)
        {
            int quote=repo.GetQuotes(businesssValue, propertyValue);
            if (quote != null)
            {
                //_log4net.Info("Quote Value is returned");
                return quote;
            }
            else
            {
                //_log4net.Info("No Quote found so returing 0");
                return 0;
            }
        }
    }
}
