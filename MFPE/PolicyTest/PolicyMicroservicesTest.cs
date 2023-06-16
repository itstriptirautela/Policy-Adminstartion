
using Moq;
using NUnit.Framework;
using PolicyAPI.Models;
using PolicyAPI.Repository;
using PolicyAPI.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PolicyTest
{
    
    public class PolicyMicroservicesTest
  


        {
            private Mock<IPolicyRepo> repoMock;
            private PolicyService policyService;
            private SampleRepo samplePolicyRepo;

            [SetUp]
            public void Setup()
            {
                repoMock = new Mock<IPolicyRepo>();
                samplePolicyRepo = new SampleRepo();
            }

            [TestCase(1, 2, 2, "Policy has been created with Policy Status 'Initiated'")]
            [TestCase(2, 2, 2, "Policy has been created with Policy Status 'Initiated'")]
            [TestCase(3, 2, 2, "No such property exists. Hence, Policy was not created")]
            [TestCase(2, 7, 7, "No such PolicyMaster exists. Hence, Policy was not created")]
            public void CreatePolicy(int propertyId, int bvalue, int pvalue, string expectedRes)
            {
                List<Property> properties = samplePolicyRepo.GetSampleProperties();
                List<PolicyMaster> policyMasters = samplePolicyRepo.GetSamplePolicyMasters();
                policyService = new PolicyService(repoMock.Object);
                Property pro = properties.Find(p => p.PropertyId == propertyId);
                string policyStatus = "";
                if (pro == null)
                {
                    policyStatus = "No such property exists. Hence, Policy was not created";
                    repoMock.Setup(p => p.CreatePolicy(propertyId)).Returns(Task.FromResult(policyStatus));
                }
                else if (policyService.GetQuote(bvalue, pvalue) == null)
                {
                    policyStatus = "No such Quote exists. Hence, Policy was not created";
                    repoMock.Setup(p => p.CreatePolicy(propertyId)).Returns(Task.FromResult(policyStatus));
                }
                else if (!policyMasters.Exists(pm => pm.BusinessValue == bvalue))
                {
                    policyStatus = "No such PolicyMaster exists. Hence, Policy was not created";
                    repoMock.Setup(p => p.CreatePolicy(propertyId)).Returns(Task.FromResult(policyStatus));
                }
                else
                {
                    policyStatus = "Initiated";
                    repoMock.Setup(p => p.CreatePolicy(propertyId)).Returns(Task.FromResult("Policy has been created with Policy Status \'" + policyStatus + "\'"));
                }

                policyService = new PolicyService(repoMock.Object);
                string actualRes = policyService.CreatePolicy(propertyId).Result.ToString();
                Assert.AreEqual(actualRes.ToString(), expectedRes);
            }

            [TestCase(1, "Paid", "Policy has been Issued for Policy ID 1")]
            [TestCase(2, "Paids", "No Payment was made. Hence, Policy was not Issued")]
            [TestCase(2, "Paid", "Policy has already been Issued")]
            [TestCase(3, "Paid", "No Policy exists with ID 3")]
            public void IssuePolicyTest(int PolicyId, string PaymentDetails, string expectedRes)
            {
                List<ConsumerPolicy> policies = samplePolicyRepo.GetSamplePolicies();
                if (PaymentDetails == "Paid")
                {
                    if (policies.Exists(p => p.PolicyId == PolicyId))
                    {
                        ConsumerPolicy policy = policies.Find(p => p.PolicyId == PolicyId);
                        if (policy.PolicyStatus == "Issued")
                        {
                            repoMock.Setup(r => r.IssuePolicy(PolicyId, PaymentDetails))
                                .Returns(Task.FromResult("Policy has already been Issued"));
                        }
                        else
                        {
                            repoMock.Setup(r => r.IssuePolicy(PolicyId, PaymentDetails))
                                .Returns(Task.FromResult("Policy has been Issued for Policy ID " + PolicyId));
                        }
                    }
                    else
                    {
                        repoMock.Setup(r => r.IssuePolicy(PolicyId, PaymentDetails))
                            .Returns(Task.FromResult("No Policy exists with ID " + PolicyId));
                    }
                }
                else
                {
                    repoMock.Setup(r => r.IssuePolicy(PolicyId, PaymentDetails))
                            .Returns(Task.FromResult("No Payment was made. Hence, Policy was not Issued"));
                }

                policyService = new PolicyService(repoMock.Object);
                string actuaRes = policyService.IssuePolicy(PolicyId, PaymentDetails).Result.ToString();
                Assert.That(actuaRes, Is.EqualTo(expectedRes));
            }

            [TestCase(1)]
            [TestCase(2)]
            public void ViewPolicyByIdTest(int PolicyId)
            {
                List<ConsumerPolicy> policies = samplePolicyRepo.GetSamplePolicies();
                dynamic expectedRes;
                if (policies.Exists(p => p.PolicyId == PolicyId))
                {
                    repoMock.Setup(p => p.ViewPolicyById(PolicyId)).Returns(policies.Find(po => po.PolicyId == PolicyId));
                    policyService = new PolicyService(repoMock.Object);
                    expectedRes = policyService.ViewPolicyById(PolicyId);//samplePolicyRepo.GetSamplePolicies().Find(po => po.PolicyId == PolicyId);
                }
                else
                {
                    repoMock.Setup(p => p.ViewPolicyById(PolicyId)).Returns(null);
                    expectedRes = null;
                }

                policyService = new PolicyService(repoMock.Object);
                dynamic actualRes = policyService.ViewPolicyById(PolicyId);
                if (actualRes == null || expectedRes == null)
                {
                    Assert.AreEqual(actualRes, expectedRes);
                }
                else
                {
                    Assert.True(actualRes != null);
                    Assert.That(expectedRes.PolicyId, Is.EqualTo(actualRes.PolicyId));
                    Assert.That(expectedRes.PropertyId, Is.EqualTo(actualRes.PropertyId));
                    Assert.That(expectedRes.QuoteValue, Is.EqualTo(actualRes.QuoteValue));
                    Assert.That(expectedRes.PolicyStatus, Is.EqualTo(actualRes.PolicyStatus));
                    Assert.That(expectedRes.PolicyMasterId, Is.EqualTo(actualRes.PolicyMasterId));
                }
            }

            [TestCase(2, 2)]
            [TestCase(0, 9)]
            [TestCase(2, 4)]
            public void GetQuoteTest(int BusinessValue, int PropertyValue)
            {
                List<Quotes> quotes = samplePolicyRepo.GetSampleQuotes();
                Quotes qu = null;
                int expectedRes = 0;

                if (BusinessValue >= 0 && BusinessValue <= 10 && PropertyValue >= 0 && PropertyValue <= 10)
                {
                    foreach (Quotes q in quotes)
                    {
                        if (BusinessValue >= q.BusinesssValueFrom && BusinessValue <= q.BusinesssValueTo &&
                            PropertyValue >= q.PropertyValueFrom && PropertyValue <= q.PropertyValueTo)
                        {
                            repoMock.Setup(p => p.GetQuote(BusinessValue, PropertyValue)).Returns(q.QuoteValue);
                            policyService = new PolicyService(repoMock.Object);
                            expectedRes = policyService.GetQuote(BusinessValue, PropertyValue);
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
                    repoMock.Setup(p => p.GetQuote(BusinessValue, PropertyValue)).Returns(0);
                    policyService = new PolicyService(repoMock.Object);
                    expectedRes = policyService.GetQuote(BusinessValue, PropertyValue); ;
                }

                policyService = new PolicyService(repoMock.Object);
                dynamic actualRes = policyService.GetQuote(BusinessValue, PropertyValue);


                Assert.True(actualRes != null);
                Assert.True(expectedRes != null);
                Assert.That(actualRes, Is.EqualTo(expectedRes));



            }

            [TestCase(1)]
            [TestCase(2)]
            public void ViewPropertiesTest(int id)
            {
                repoMock.Setup(p => p.GetProperties(id)).Returns(samplePolicyRepo.GetSampleProperties().Find(po => po.PropertyId == id));//Returns(samplePolicyRepo.GetSampleProperties());
                policyService = new PolicyService(repoMock.Object);

                var expectedRes = samplePolicyRepo.GetSampleProperties().Find(po => po.PropertyId == id);
                var actualRes = policyService.GetProperties(id);

                Assert.True(actualRes != null);
                //Assert.AreEqual(actualRes.Count, expectedRes.Count);
                // for (int i = 0; i < expectedRes.Count; i++)
                //{
                Assert.That(expectedRes.PropertyId, Is.EqualTo(actualRes.PropertyId));
                Assert.That(expectedRes.BuildingType, Is.EqualTo(actualRes.BuildingType));
                Assert.That(expectedRes.BuildingStoreys, Is.EqualTo(actualRes.BuildingStoreys));
                Assert.That(expectedRes.BuildingAge, Is.EqualTo(actualRes.BuildingAge));
                Assert.That(expectedRes.BusinessId, Is.EqualTo(actualRes.BusinessId));
                Assert.That(expectedRes.PropertyMasterId, Is.EqualTo(actualRes.PropertyMasterId));
                //}
            }

            [TestCase(1)]
            [TestCase(2)]
            public void GetBusinessTest(int id)
            {
                repoMock.Setup(p => p.GetBusiness(id)).Returns(samplePolicyRepo.GetSampleBusiness().Find(po => po.BusinessId == id));//Returns(samplePolicyRepo.GetSampleProperties());
                policyService = new PolicyService(repoMock.Object);

                var expectedRes = samplePolicyRepo.GetSampleBusiness().Find(po => po.BusinessId == id);
                var actualRes = policyService.GetBusiness(id);

                Assert.True(actualRes != null);
                //Assert.AreEqual(actualRes.Count, expectedRes.Count);
                // for (int i = 0; i < expectedRes.Count; i++)
                //{
                Assert.That(expectedRes.BusinessId, Is.EqualTo(actualRes.BusinessId));
                Assert.That(expectedRes.BusinessName, Is.EqualTo(actualRes.BusinessName));
                Assert.That(expectedRes.BusinessName, Is.EqualTo(actualRes.BusinessName));
                Assert.That(expectedRes.TotalEmployees, Is.EqualTo(actualRes.TotalEmployees));
                Assert.That(expectedRes.BusinessMasterId, Is.EqualTo(actualRes.BusinessMasterId));
                Assert.That(expectedRes.ConsumerId, Is.EqualTo(actualRes.ConsumerId));
                //}
            }


            [Test]
            public void GetBusinessMaster()
            {
                List<BusinessMaster> businessesmasters = samplePolicyRepo.GetSampleBusinessMaster();
                repoMock.Setup(c => c.GetBusinessMaster()).Returns(businessesmasters);
                policyService = new PolicyService(repoMock.Object);

                foreach (BusinessMaster b in businessesmasters)
                {
                    BusinessMaster actualRes = policyService.GetBusinessMaster().Find(po => po.BusinessMasterId == b.BusinessMasterId);
                    Assert.That(actualRes.BusinessTurnOver, Is.EqualTo(b.BusinessTurnOver));
                    Assert.That(actualRes.BusinessValue, Is.EqualTo(b.BusinessValue));
                    Assert.That(actualRes.CapitalInvest, Is.EqualTo(b.CapitalInvest));
                }

            }

            [TestCase(1)]
            [TestCase(2)]
            public void ViewPoliciesTest(int id)
            {
                repoMock.Setup(p => p.GetPoliciesById(id)).Returns(samplePolicyRepo.GetSamplePolicies().Find(po => po.PolicyId == id));
                policyService = new PolicyService(repoMock.Object);

                var expectedRes = samplePolicyRepo.GetSamplePolicies().Find(po => po.PolicyId == id); ;
                var actualRes = policyService.GetPoliciesById(id);

                Assert.True(actualRes != null);
                //Assert.AreEqual(actualRes.Count, expectedRes.Count);
                // for (int i = 0; i < expectedRes.Count; i++)
                //{
                Assert.That(expectedRes.PolicyId, Is.EqualTo(actualRes.PolicyId));
                Assert.That(expectedRes.PropertyId, Is.EqualTo(actualRes.PropertyId));
                Assert.That(expectedRes.QuoteValue, Is.EqualTo(actualRes.QuoteValue));
                Assert.That(expectedRes.PolicyStatus, Is.EqualTo(actualRes.PolicyStatus));
                Assert.That(expectedRes.PolicyMasterId, Is.EqualTo(actualRes.PolicyMasterId));
                //}
            }
        }
    }