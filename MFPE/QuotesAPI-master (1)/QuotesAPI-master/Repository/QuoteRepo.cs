using QuotesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuotesAPI.Repository
{
    public class QuoteRepo:IQuoteRepo
    {
        private readonly QuotesContext context;
        public QuoteRepo(QuotesContext _context)
        {
            context = _context;
        }
        public int GetQuotes(int businesssValue, int propertyValue)
        {
            QuotesMaster quote = context.Quotes.SingleOrDefault(x => x.BusinesssValueFrom <= businesssValue
               && x.BusinesssValueTo >= businesssValue
               && x.PropertyValueFrom <= propertyValue
               && x.PropertyValueTo >= propertyValue);
            return quote.QuoteValue;
        }
    }
}
