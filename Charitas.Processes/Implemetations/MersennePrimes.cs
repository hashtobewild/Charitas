using Charitas.Processes.Interfaces;
using Microsoft.Extensions.Logging;
using Mpir.NET;
using System;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

namespace Charitas.Processes.Implemetations
{
    public class MersennePrimes : IMersennePrimes
    {
        private ILogger _logger;

        public MersennePrimes(ILogger logger)
        {
            _logger = logger;
        }

        public async Task FindMersennePrimes(BigInteger start)
        {
            var _tokenSource = new CancellationTokenSource();
            var _token = _tokenSource.Token;
            var progress = new Progress<BigInteger>(value =>
            {
                _logger.LogInformation("Completed: " + value.ToString());
            });

            await Task.Run(() => CancellableFindMersennePrimes(start, _token, progress));
        }

        public bool IsMersennePrime(BigInteger candidate)
        {
            if (candidate > 2)
            {
                var working = new mpz_t(candidate.ToString());
                var rop = new mpz_t();
                mpir.mpz_sqrt(rop, working);
                BigInteger convertedSquare = rop.ToBigInteger();
                // Only odd numbers
                for (BigInteger i = 3; i < convertedSquare; i += 2)
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

        private void CancellableFindMersennePrimes(BigInteger start, CancellationToken token, IProgress<BigInteger> progress)
        {
            BigInteger working = start;
            while (!token.IsCancellationRequested)
            {
                if (IsMersennePrime(working - 1))
                {
                    progress.Report(working);
                }
                working *= 2;
            }
        }
    }
}