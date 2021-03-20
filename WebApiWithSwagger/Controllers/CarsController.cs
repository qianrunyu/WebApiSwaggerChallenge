using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApiWithSwagger.BusinessLogic;
using WebApiWithSwagger.EFCore;
using WebApiWithSwagger.Models;
using WebApiWithSwagger.Validation;

namespace WebApiWithSwagger.Controllers
{

    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ApiContext apiContext;
        private readonly ICarManagement carManagementService;
        private readonly IValidation validationService;
        private readonly ILogger logger;

        public CarsController(ApiContext apiContext, ICarManagement carManagement, IValidation validation, ILogger<CarsController> logger)
        {
            this.apiContext = apiContext;
            this.carManagementService = carManagement;
            this.validationService = validation;
            this.logger = logger;
        }
        // GET api/cars/A01
        [HttpGet]
        [Route("api/cars/{dealercode}")]
        public IActionResult GetAllStock(string dealercode)
        {
            if (!validationService.ValidateStringParameter(dealercode))
            {
                logger.LogError("Dealer code validation failed.");
                return BadRequest("Check dealer code.");
            }
            var cars = apiContext.GetCars();
            var response = carManagementService.GetAllCarsStock(cars, dealercode);
            return Ok(response);
        }

        // GET api/cars/search
        [HttpGet]
        [Route("api/cars/{dealercode}/search")]
        public IActionResult SearchStock(string dealercode, [FromQuery] string make, [FromQuery] string model)
        {
            if (!validationService.ValidateStringParameter(dealercode) || !validationService.ValidateStringParameter(make) || !validationService.ValidateStringParameter(model))
            {
                logger.LogError("Input values validation failed.");
                return BadRequest("Check search values.");
            }
            var cars = apiContext.GetCars();
            var response = carManagementService.SearchStock(cars, dealercode, make, model);

            return Ok(response);
        }

        // POST api/cars/A01
        [HttpPost]
        [Route("api/cars/addcar")]
        public IActionResult AddCar([FromBody] NewCarRequest request)
        {
            if (!validationService.ValidateCarRequest(request))
            {
                logger.LogError("New Car Request validation failed.");
                return BadRequest("Cannot Create Car. Check input values.");
            }

            apiContext.AddCar(request);
            return Ok();
        }
        // DELETE api/cars/A01/1
        [HttpDelete]
        [Route("api/cars/removecar")]
        public IActionResult RemoveCar([FromBody] RemoveCarRequest request)
        {
            if (!validationService.ValidateCarRequest(request))
            {
                logger.LogError("Remove Car Request validation failed.");
                return BadRequest("Cannot Remove Car. Check input values.");
            }
            if (apiContext.RemoveCar(request))
            {
                return Ok();
            }
            logger.LogError("Car not found or stock is already zero.");
            return BadRequest("Car not found or stock is already zero.");
        }

        //PUT api/cars/updatestock
        [HttpPut]
        [Route("api/cars/updatestock")]
        public IActionResult UpdateStock([FromBody] UpdateStockRequest request)
        {
            if (!validationService.ValidateIntParameter(request.StockId) || !validationService.ValidateIntParameter(request.StockLevel) || !validationService.ValidateStringParameter(request.DealerCode))
            {
                logger.LogError("UpdateStockRequest validation failed.");
                return BadRequest("Cannot update stock. Check update values.");
            }
            var response = apiContext.UpdateStock(request);
            if (response != null)
            {
                return Ok(response);
            }
            logger.LogError($"Cannot update stock level. Stock not Found.");
            return BadRequest("Cannot update stock level. Stock not Found.");
        }


    }
}