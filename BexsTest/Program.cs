using BexsTestBS;
using BexsTestDomain;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BexsTest
{
    class Program
    {
        static void Main(string[] args)
        {

            ServiceProvider serviceProvider = Setup();
            
            if (args.Length != 1)
            {
                throw new Exception("A solução deve receber somente um parametro, com o caminho para o arquivo de input");
            }

            String pathFile = args[0];

            IValidationBusiness validationBusiness = serviceProvider.GetService<IValidationBusiness>();
            ITravelPathBusiness travelPathBusiness = serviceProvider.GetService<ITravelPathBusiness>();
            IAirportBusiness airportBusiness = serviceProvider.GetService<IAirportBusiness>();
            IReadDestinationFile fileReader = serviceProvider.GetService<IReadDestinationFile>();

            airportBusiness.Initialize(pathFile, fileReader);

            while (true)
            {
                Console.WriteLine("Por favor, entre a rota no formato AAA-BBB:");
                String strLine = Console.ReadLine();
                validationBusiness.ValidateInput(strLine, airportBusiness.GetAirportList());
                TravelPath path = travelPathBusiness.FindBestPath(strLine, airportBusiness.GetAirportList());
                Console.WriteLine(travelPathBusiness.GenerateOutput(path));
            }
        }

        private static ServiceProvider Setup()
        {
            IServiceCollection serviceProvider = new ServiceCollection();
            serviceProvider.AddSingleton<IAirportBusiness, AirportBusiness>();
            serviceProvider.AddScoped<IValidationBusiness, ValidationBusiness>();
            serviceProvider.AddScoped<ITravelPathBusiness, TravelPathBusiness>();
            serviceProvider.AddScoped<IReadDestinationFile, ReadDestinationFileCSV>();

            return serviceProvider.BuildServiceProvider();
        }
    }
}
