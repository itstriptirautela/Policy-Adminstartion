using QuotesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotesTests
{
    public class SampleRepo
    {
        public List<QuotesMaster> GetSampleQuotesMaster()
        {
            List<QuotesMaster> quotesMasters = new List<QuotesMaster>
            {
                new QuotesMaster
                {
                    QuoteId = 1,
                    PropertyValueFrom = 0,
                    PropertyValueTo = 2,
                    BusinesssValueFrom = 0,
                    BusinesssValueTo = 2,
                    PropertyType = "Office",
                    QuoteValue = 1000
                },
                new QuotesMaster
                {
                    QuoteId = 2,
                    PropertyValueFrom = 3,
                    PropertyValueTo = 5,
                    BusinesssValueFrom = 3,
                    BusinesssValueTo = 5,
                    PropertyType = "Office",
                    QuoteValue = 102010
                }
            };
            return quotesMasters;
        }


        public int GetQuoteTest(int BusinessValue, int PropertyValue)
        {
            List<QuotesMaster> quotes = GetSampleQuotesMaster();
            if (BusinessValue >= 0 && BusinessValue <= 10 && PropertyValue >= 0 && PropertyValue <= 10)
            {
                foreach (QuotesMaster q in quotes)
                {
                    if (BusinessValue >= q.BusinesssValueFrom && BusinessValue <= q.BusinesssValueTo &&
                        PropertyValue >= q.PropertyValueFrom && PropertyValue <= q.PropertyValueTo)
                    {
                        return q.QuoteValue;
                    }
                }
            }
            return 0;
        }
    }
}

