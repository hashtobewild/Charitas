using Charitas.Processes.Implemetations;
using Charitas.Processes.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Numerics;

namespace Charitas
{
    public class Program
    {
        private ILogger<Program> _logger;
        private IMersennePrimes _primes;

        public Program(ILogger<Program> logger, IMersennePrimes primes)
        {
            _logger = logger;
            _primes = primes;
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

        private void ListKnownPrimes()
        {
            Console.WriteLine("Known Mersenne Primes:");
            foreach (var item in _primes.KnownExponentValues)
            {
                Console.WriteLine(item.ToString());
            }
        }

        private void ParseOptions(string input)
        {
            Console.Clear();
            var working = input.Trim().ToLowerInvariant();
            switch (working)
            {
                case "1":
                    {
                        RunAllPrimes();
                    }
                    break;

                case "2":
                    {
                        RunKnownPrimes();
                    }
                    break;

                case "3":
                    {
                        ListKnownPrimes();
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

        private void RunAllPrimes()
        {
            _primes.FindMersennePrimes(1);
            Console.WriteLine("Complete!");
        }

        private void RunKnownPrimes()
        {
            foreach (var item in _primes.KnownExponentValues)
            {
                TimeRun(item);
            }
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
            Console.WriteLine("2. Check known Mersenne Primes");
            Console.WriteLine("3. List known Mersenne Prime Exponents");
            Console.WriteLine("");
            Console.WriteLine("Q. Quit");
            Console.WriteLine("");
            Console.WriteLine("(Enter the number matching your choice and press enter, to continue)");
            Console.WriteLine("");
            ParseOptions(Console.ReadLine());
        }

        private void TimeRun(int candidate)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var output = _primes.IsMersennePrime(candidate);
            watch.Stop();
            Console.WriteLine("Runtime: " + watch.Elapsed.ToString());
            Console.WriteLine("Exponent: " + candidate.ToString());
            Console.WriteLine("Candidate:\n" + (BigInteger.Pow(new BigInteger(2), candidate) - 1).ToString());
            Console.WriteLine("");
        }
    }
}