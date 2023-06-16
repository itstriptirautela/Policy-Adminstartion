using ConsumerMicroservice.Models;
using ConsumerMicroservice.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumerMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Authorize(Roles = "User")]
    public class ConsumerController : ControllerBase
    {
        private readonly IConsumerService _consumerService;
        private readonly log4net.ILog _log4Net;

        public ConsumerController(IConsumerService consumerService)
        {

            _consumerService = consumerService;
            _log4Net = log4net.LogManager.GetLogger(typeof(ConsumerController));
        }

        /// <summary>
        /// Display of all Consumer Details
        /// </summary>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("GetConsumer")]
        public IEnumerable<Consumer> GetConsumer()
        {
            _log4Net.Info("Getting Consumer details");
            return _consumerService.GetConsumers();
        }

        /// <summary>
        /// Display of all BUsinessMaster Details
        /// </summary>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("GetBusinessMaster")]
        public IEnumerable<BusinessMaster> GetBusienssMaster()
        {
            _log4Net.Info("Getting BusinessMaster details");
            return _consumerService.GetBusinessMaster();
        }

        /// <summary>
        /// Display of all PropertyMaster Details
        /// </summary>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("GetPropertyMaster")]
        public IEnumerable<PropertyMaster> GetPropertyMaster()
        {
            _log4Net.Info("Getting PropertyMaster details");
            return _consumerService.GetPropertyMaster();
        }

        /// <summary>
        /// Display of all Business Details
        /// </summary>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("GetBusiness")]
        public IEnumerable<Business> GetBusiness()
        {
            _log4Net.Info("Getting Business details");
            return _consumerService.GetBusiness();
        }

        /// <summary>
        /// Display of all Property Details
        /// </summary>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("GetProperty")]
        public IEnumerable<Property> GetProperty()
        {
            _log4Net.Info("Getting Property details");
            return _consumerService.GetProperty();
        }


        /// <summary>
        /// Display of consumer by ID
        /// </summary>
        /// <param name="consumerId"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("GetConsumerById")]
        public ActionResult GetConsumerById(int consumerId)
        {
            _log4Net.Info("Getting ConsumerById details");
            var obj = _consumerService.GetConsumer(consumerId);
            if (obj == null)
            {
                return NotFound("Consumer Details Not Found");
            }
            else
            {
                return Ok(obj);
            }
        }

        /// <summary>
        /// Display of business by ID
        /// </summary>
        /// <param name="BusinessId"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("GetBusinessById")]
        public ActionResult GetBusinessById(int businessId)
        {
            _log4Net.Info("Getting BusinessById details");
            var obj = _consumerService.GetBusinesss(businessId);
            if (obj == null)
            {
                return NotFound("Business Details Not Found");
            }
            else
            {
                return Ok(obj);
            }
        }


        /// <summary>
        /// Display of property by ID
        /// </summary>
        /// <param name="propertyId"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("GetPropertyById")]
        public ActionResult GetPropertyById(int propertyId)
        {
            _log4Net.Info("Getting PropertyById details");
            var obj = _consumerService.GetProperties(propertyId);
            if (obj == null)
            {
                return NotFound("Property Details Not Found");
            }
            else
            {
                return Ok(obj);
            }
        }

        /// <summary>
        /// Create Consumer using HTTPPOST
        /// </summary>
        /// <param name="consumer"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("CreateConsumer")]
        [ProducesResponseType(201, Type = typeof(Consumer))]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult CreateConsumer([FromBody] Consumer consumer)
        {
            _log4Net.Info("Creating Consumer");
            try
            {
                if (!ModelState.IsValid)
                {
                    return new BadRequestObjectResult(ModelState);
                }

                if (!_consumerService.CreateConsumer(consumer) && !_consumerService.ConsumerExists(consumer.ConsumerId))
                {
                    return new ObjectResult("Consumer Details Not Added") { StatusCode = 500 };
                }
                return new CreatedResult("GetConsumer", new { id = consumer.ConsumerId });
            }
            catch (Exception e)
            {

                return new ObjectResult("Consumer Id is incorrect, Check for Id" + e.Message) { StatusCode = 500 };
            }
        }

        /// <summary>
        /// Create Business using HTTPPOST
        /// </summary>
        /// <param name="business"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("CreateBusiness")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult CreateBusiness([FromBody] BusinessInput businessInput)
        {
            _log4Net.Info("Creating Business");
            try
            {
                BusinessMaster businessMaster = _consumerService.BusinessMasterByValues(businessInput.BusinessTurnOver, businessInput.CapitalInvest);
                if (!ModelState.IsValid)
                {
                    return new BadRequestObjectResult(ModelState);
                }

                Business business = new Business
                {
                    BusinessName = businessInput.BusinessName,
                    BusinessType = businessInput.BusinessType,
                    TotalEmployees = businessInput.TotalEmployees,
                    ConsumerId = businessInput.ConsumerId,
                    BusinessMasterId = businessMaster.BusinessMasterId,

                };
                if (_consumerService.CreateBusiness(business))
                {
                    return new CreatedResult("GetBusiness", new { id = business.BusinessId });
                }
                return new ObjectResult("Business Details Not Added") { StatusCode = 500 };
            }
            catch (Exception e)
            {

                return new ObjectResult("Business Id is incorrect, Check for Id" + e.Message) { StatusCode = 500 };
            }
        }

        /// <summary>
        /// Create Property using HTTPPOST
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("CreateProperty")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult CreateProperty([FromBody] PropertyInput propertyInput)
        {
            _log4Net.Info("Creating Property");
            try
            {
                PropertyMaster propertyMaster = _consumerService.PropertyMasterByValues(propertyInput.CostOfAssest, propertyInput.SalvageValue, propertyInput.UsefulLifeOfAssest);
                if (!ModelState.IsValid)
                {
                    return new BadRequestObjectResult(ModelState);
                }

                Property property = new Property
                {
                    BuildingType = propertyInput.BuildingType,
                    BuildingStoreys = propertyInput.BuildingStoreys,
                    BuildingAge=propertyInput.BuildingAge,
                    BusinessId=propertyInput.BusinessId,
                    PropertyMasterId=propertyMaster.PropertyMasterId,
                };

                if (_consumerService.CreateProperty(property))
                {
                    return new CreatedResult("GetProperty", new { id = property.PropertyId });
                }
                return new ObjectResult("Property Details Not Added") { StatusCode = 500 };
            }
            catch (Exception e)
            {

                return new ObjectResult("Property Id is incorrect, Check for Id" + e.Message) { StatusCode = 500 };
            }
        }

        /// <summary>
        /// Update Consumer
        /// </summary>
        /// <param name="consumerId"></param>
        /// <param name="consumer"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("UpdateConsumer")]
        public ActionResult UpdateConsumer(int consumerId, [FromBody] Consumer consumer)
        {
            _log4Net.Info("Updating Consumer");
            try
            {

                if (!ModelState.IsValid)
                {
                    return new BadRequestObjectResult(ModelState);
                }

                var updateResult = _consumerService.UpdateConsumer(consumerId, consumer);

                if (updateResult)
                {
                    return new CreatedResult("GetConsumerById", new { id = consumer.ConsumerId });
                }
                return new ObjectResult("Consumer Details Not Found") { StatusCode = 500 };
            }
            catch (Exception e)
            {

                return new ObjectResult("Consumer Id is incorrect, Check for Id") { StatusCode = 500 };
            }
        }

        /// <summary>
        /// Update Business
        /// </summary>
        /// <param name="businessId"></param>
        /// <param name="business"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("UpdateBusiness")]
        public ActionResult UpdateBusiness(int businessId, [FromBody] Business business)
        {
            _log4Net.Info("Updating Business");
            try
            {

                if (!ModelState.IsValid)
                {
                    return new BadRequestObjectResult(ModelState);
                }

                business = new Business
                {
                    BusinessId=businessId,
                    BusinessType=business.BusinessType,
                    BusinessName=business.BusinessName,
                    TotalEmployees=business.TotalEmployees,
                    ConsumerId=business.ConsumerId,
                    BusinessMasterId=business.BusinessMasterId
                };
                var updateResult = _consumerService.UpdateBusiness(businessId, business);

                if (updateResult)
                {
                    return new CreatedResult("GetBusinessById", new { id = business.BusinessId });
                }
                return new ObjectResult("Business Details Not Found") { StatusCode = 500 };
            }
            catch (Exception e)
            {

                return new ObjectResult("Business Id is incorrect, Check for Id") { StatusCode = 500 };
            }
        }

        /// <summary>
        /// Update Property
        /// </summary>
        /// <param name="propertyId"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("UpdateProperty")]
        public ActionResult UpdateProperty(int propertyId, [FromBody] Property property)
        {
            _log4Net.Info("Updating Property");
            try
            {

                if (!ModelState.IsValid)
                {
                    return new BadRequestObjectResult(ModelState);
                }
                property = new Property
                {
                    PropertyId=propertyId,
                    BuildingType=property.BuildingType,
                    BuildingStoreys=property.BuildingStoreys,
                    BuildingAge=property.BuildingAge,
                    BusinessId=property.BusinessId,
                    PropertyMasterId=property.PropertyMasterId
                };

                var updateResult = _consumerService.UpdateProperty(propertyId, property);

                if (updateResult)
                {
                    return new CreatedResult("GetPropertyById", new { id = property.PropertyId });
                }
                return new ObjectResult("Business Details Not Found") { StatusCode = 500 };
            }
            catch (Exception e)
            {

                return new ObjectResult("Property Id is incorrect, Check for Id") { StatusCode = 500 };
            }
        }
        
    }
}
