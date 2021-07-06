using Charitas.Processes.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Charitas.Processes.Implemetations
{
    public class MersennePrimes : IMersennePrimes
    {
        private ILogger<MersennePrimes> _logger;

        public MersennePrimes(ILogger<MersennePrimes> logger)
        {
            _logger = logger;
        }

        public async Task FindMersennePrimes(long start)
        {
            var _tokenSource = new CancellationTokenSource();
            var _token = _tokenSource.Token;
            var progress = new Progress<long>(value =>
            {
                _logger.LogInformation("Completed: " + value.ToString());
            });

            await Task.Run(() => CancellableFindMersennePrimes(start, _token, progress));
        }

        public bool IsMersennePrime(long candidate)
        {
            if (candidate > 2)
            {
                long convertedSquare = (long)Math.Sqrt(candidate);
                // Only odd numbers
                for (long i = 3; i < convertedSquare; i += 2)
                {
                    if (candidate % i == 0)
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return true;
            }
        }

        private void CancellableFindMersennePrimes(long start, CancellationToken token, IProgress<long> progress)
        {
            long working = start;
            while (!token.IsCancellationRequested && working > 0)
            {
                var local = working - 1;
                if (IsMersennePrime(local))
                {
                    progress.Report(local);
                }
                working *= 2;
            }
        }
    }
}