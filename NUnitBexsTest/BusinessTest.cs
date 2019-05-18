using BexsTestBS;
using BexsTestDomain;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using NUnitBexsTest;

namespace Tests
{
    public class BusinessTest
    {
        public ServiceProvider Setup()
        {
            IServiceCollection serviceProvider = new ServiceCollection();
            serviceProvider.AddScoped<IAirportBusiness, AirportBusiness>();
            serviceProvider.AddScoped<IValidationBusiness, ValidationBusiness>();
            serviceProvider.AddScoped<ITravelPathBusiness, TravelPathBusiness>();
            serviceProvider.AddScoped<IReadDestinationFile, MockFileReader>();



            return serviceProvider.BuildServiceProvider();
        }

        [Test]
        public void TestPaths()
        {




            ServiceProvider serviceProvider = Setup();


            ITravelPathBusiness travelPathBusiness = serviceProvider.GetService<ITravelPathBusiness>();
            IAirportBusiness airportBusiness = serviceProvider.GetService<IAirportBusiness>();
            IReadDestinationFile fileReader = serviceProvider.GetService<IReadDestinationFile>();

            airportBusiness.Initialize("", fileReader);

            TravelPath test1 = travelPathBusiness.FindBestPath("A-B", airportBusiness.GetAirportList());
            TravelPath test2 = travelPathBusiness.FindBestPath("A-C", airportBusiness.GetAirportList());
            TravelPath test3 = travelPathBusiness.FindBestPath("A-E", airportBusiness.GetAirportList());

            Assert.AreEqual("Caminho A -> 0 B -> 10 Custo Total - 10", test1.ToString());
            Assert.AreEqual("Caminho A -> 0 B -> 10 C -> 10 Custo Total - 20", test2.ToString());
            Assert.AreEqual("Caminho A -> 0 B -> 10 C -> 10 E -> 10 Custo Total - 30", test3.ToString());

        }

        [Test]
        public void TestOutput()
        {
            ServiceProvider serviceProvider = Setup();

            ITravelPathBusiness travelPathBusiness = serviceProvider.GetService<ITravelPathBusiness>();
            IAirportBusiness airportBusiness = serviceProvider.GetService<IAirportBusiness>();
            IReadDestinationFile fileReader = serviceProvider.GetService<IReadDestinationFile>();

            airportBusiness.Initialize("", fileReader);

            TravelPath test1 = travelPathBusiness.FindBestPath("A-B", airportBusiness.GetAirportList());
            Assert.AreEqual("Caminho A -> 0 B -> 10 Custo Total - 10", travelPathBusiness.GenerateOutput(test1));
        }
    }
}