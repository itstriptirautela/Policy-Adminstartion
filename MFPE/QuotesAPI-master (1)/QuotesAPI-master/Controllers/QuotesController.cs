using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuotesAPI.Models;
using QuotesAPI.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuotesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Authorize(Roles = "User")]
    public class QuotesController : ControllerBase
    {
        private readonly log4net.ILog _log4Net;
        private readonly IQuoteService service;
        public QuotesController(IQuoteService _service)
        {
            service = _service;
            _log4Net = log4net.LogManager.GetLogger(typeof(QuotesController));
        }

        /// <summary>
        /// Getting Quote Value based on Bussiness Value & Property Value
        /// </summary>
        /// <param name="businesssValue"></param>
        /// <param name="propertyValue"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public int GetQuote(int businesssValue,int propertyValue)
        {
            _log4Net.Info("Serching for the Range of BusinessValue and PropertyValue");
            return service.GetQuotes(businesssValue, propertyValue); 
        }
    }
}
