using Charitas.Processes.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Numerics;
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

        public MersennePrimes()
        {
        }

        public async Task FindMersennePrimes(int start)
        {
            var _tokenSource = new CancellationTokenSource();
            var _token = _tokenSource.Token;
            var progress = new Progress<long>(value =>
            {
                _logger.LogInformation("Completed: " + value.ToString());
            });

            await Task.Run(() => CancellableFindMersennePrimes(start, _token, progress));
        }

        public bool IsMersennePrime(int candidate)
        {
            if (candidate == 1)
            {
                return true;
            }
            else if (candidate % 2 == 0)
            {
                // Lehmertest only works for odd primes (except 2)
                return candidate == 2 ? true : false;
            }
            else
            {
                BigInteger mersenneCandidate = BigInteger.Pow(new BigInteger(2), candidate) - 1;
                BigInteger sequence = new BigInteger(4);
                // do for odd numbers up to t he square root of the candidate only
                for (int i = 3; i <= (int)Math.Sqrt(candidate); i += 2)
                {
                    // check divisibility
                    if (candidate % i == 0)
                    {
                        return false;
                    }
                    for (int j = 3; j <= candidate; j++)
                    {
                        sequence = (sequence * sequence - new BigInteger(2)) % mersenneCandidate;
                    }
                }
                return sequence == 0;
            }
        }

        private void CancellableFindMersennePrimes(int start, CancellationToken token, IProgress<long> progress)
        {
            int working = start;
            while (!token.IsCancellationRequested)
            {
                var local = working;
                if (IsMersennePrime(local))
                {
                    progress.Report(local);
                }
                working++;
            }
        }
    }
}