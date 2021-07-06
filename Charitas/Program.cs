using Charitas.Processes.Implemetations;
using Charitas.Processes.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace Charitas
{
    public class Program
    {
        private ILogger<Program> _logger;

        public Program(ILogger<Program> logger)
        {
            _logger = logger;
        }

        public void Run()
        {
            ShowOptions();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureServices(
                services =>
                {
                    services.AddTransient<Program>();
                    services.AddTransient<IMersennePrimes, MersennePrimes>();
                });
        }

        private static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            host.Services.GetRequiredService<Program>().Run();
        }

        private void ParseOptions(string input)
        {
            Console.Clear();
            var working = input.Trim().ToLowerInvariant();
            switch (working)
            {
                case "1":
                    {
                    }
                    break;

                case "2":
                    {
                    }
                    break;

                case "q":
                    {
                        Environment.Exit(0);
                    }
                    break;

                default:
                    {
                        _logger.LogError("That does not seem to be a valid option. Please try again.");
                    }
                    break;
            }
            // Restart the app loop
            ShowOptions();
        }

        private void ShowOptions()
        {
            Console.WriteLine("");
            Console.WriteLine("Welcome!");
            Console.WriteLine("--------");
            Console.WriteLine("");
            Console.WriteLine("Options:");
            Console.WriteLine("--------");
            Console.WriteLine("1. Run the Mersenne Prime finder from the beginning");
            Console.WriteLine("2. Run the Mersenne Prime finder from a specified offset");
            Console.WriteLine("");
            Console.WriteLine("Q. Quit");
            Console.WriteLine("");
            Console.WriteLine("(Enter the number matching your choice and press enter, to continue)");
            Console.WriteLine("");
            ParseOptions(Console.ReadLine());
        }
    }
}