using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BexsTestBS;
using BexsTestDomain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace APIBexsTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class BexsTestController : ControllerBase
    {
        IConfiguration configuration;
        IAirportBusiness airportBusiness;
        IReadDestinationFile fileReader;
        ITravelPathBusiness travelPathBusiness;
        IWriteDestinationFile fileWriter;


        public BexsTestController(IWriteDestinationFile writer , ITravelPathBusiness travelPathBusiness, IAirportBusiness airportBusiness, IReadDestinationFile fileReader, IConfiguration configuration)
        {
            this.airportBusiness = airportBusiness;
            this.fileReader = fileReader;
            this.configuration = configuration;
            this.travelPathBusiness = travelPathBusiness;
            this.fileWriter = writer;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return "up!";
        }

        [HttpGet("FindBestPath")]
        public JsonResult FindBestPath(string origin, string destination)
        {
            
            string filePath = configuration.GetSection("BexsTestConfig:InputFileLocation").Value.ToString();
            airportBusiness.Initialize(filePath, fileReader);
            TravelPath path = travelPathBusiness.FindBestPath(origin, destination, airportBusiness.GetAirportList());

            
            return new JsonResult(Newtonsoft.Json.JsonConvert.SerializeObject(path));

        }
        [HttpPost("WriteNewRoute")]
        public void WriteNewRoute(string origin, string destination, string cost)
        {            
            string filePath = configuration.GetSection("BexsTestConfig:InputFileLocation").Value.ToString();
            airportBusiness.Initialize(filePath, fileReader);
            airportBusiness.WriteNewRoute(origin, destination, cost, filePath, fileWriter);

        }
    }
}