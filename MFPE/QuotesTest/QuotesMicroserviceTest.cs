using NUnit.Framework;
using Moq;

using QuotesAPI.Models;
using QuotesAPI.Repository;
using QuotesAPI.Service;
using System.Collections.Generic;

namespace QuotesTests
{
    public class Tests
    {
        private QuoteService quoteService;
        private Mock<IQuoteRepo> quotesMock;
        private SampleRepo sampleRepo;
        [SetUp]
        public void Setup()
        {
            quotesMock = new Mock<IQuoteRepo>();
            sampleRepo = new SampleRepo();
        }


        [TestCase(2, 2, 1000)]
        [TestCase(0, 9, 0)]
        [TestCase(2, 4, 0)]
        public void GetQuoteTest(int BusinessValue, int PropertyValue, int actualRes)
        {
            List<QuotesMaster> quotesmasters = sampleRepo.GetSampleQuotesMaster();
            QuotesMaster qu = null;
            int expectedRes = 0;

            if (BusinessValue >= 0 && BusinessValue <= 10 && PropertyValue >= 0 && PropertyValue <= 10)
            {
                foreach (QuotesMaster q in quotesmasters)
                {
                    if (BusinessValue >= q.BusinesssValueFrom && BusinessValue <= q.BusinesssValueTo &&
                        PropertyValue >= q.PropertyValueFrom && PropertyValue <= q.PropertyValueTo)
                    {
                        quotesMock.Setup(p => p.GetQuotes(BusinessValue, PropertyValue)).Returns(q.QuoteValue);
                        quoteService = new QuoteService(quotesMock.Object);
                        expectedRes = quoteService.GetQuotes(BusinessValue, PropertyValue);
                    }
                }
                /**if (expectedRes!=0)
                {
                    repoMock.Setup(p => p.GetQuote(BusinessValue, PropertyValue)).Returns(quotes.Find(po => po.BusinesssValueFrom <= BusinessValue && po.BusinesssValueTo >= BusinessValue &&
                        po.PropertyValueFrom <= PropertyValue && po.PropertyValueTo >= PropertyValue).QuoteValue);//Task.FromResult(qu)
                    expectedRes = policyService.GetQuote(BusinessValue, PropertyValue); ;
                }**/
            }
            else
            {
                quotesMock.Setup(p => p.GetQuotes(BusinessValue, PropertyValue)).Returns(0);
                quoteService = new QuoteService(quotesMock.Object);
                expectedRes = quoteService.GetQuotes(BusinessValue, PropertyValue); ;
            }

            //quoteService = new QuotesService(quotesMock.Object);
            //int actualRes = sampleRepo.GetQuoteTest(BusinessValue, PropertyValue);


            Assert.True(actualRes != null);
            Assert.True(expectedRes != null);
            Assert.That(actualRes, Is.EqualTo(expectedRes));



        }

    }
}