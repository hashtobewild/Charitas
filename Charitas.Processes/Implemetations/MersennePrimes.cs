using Charitas.Processes.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

namespace Charitas.Processes.Implemetations
{
    public class MersennePrimes : IMersennePrimes
    {
        private ILogger<MersennePrimes> _logger;

        public MersennePrimes(ILogger<MersennePrimes> logger) : this()
        {
            _logger = logger;
        }

        public MersennePrimes()
        {
            DefaultFactory();
        }

        public List<int> KnownExponentValues { get; set; }

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
            if (candidate % 2 == 0)
            {
                // Lehmertest only works for odd primes (except 2)
                return candidate == 2 ? true : false;
            }
            else if (candidate == 1 || candidate == 3 || candidate == 5 || candidate == 7)
            {
                return true;
            }
            else
            {
                BigInteger mersenneCandidate = BigInteger.Pow(new BigInteger(2), candidate) - new BigInteger(1);
                BigInteger sequence = new BigInteger(4);
                // do for odd numbers up to the square root of the candidate only
                var squareRoot = (int)Math.Ceiling(Math.Sqrt(candidate));
                for (int i = 3; i <= squareRoot; i += 2)
                {
                    // check divisibility
                    if (candidate % i == 0)
                    {
                        return false;
                    }
                }
                for (int j = 3; j <= candidate; j++)
                {
                    sequence = (sequence * sequence - new BigInteger(2)) % mersenneCandidate;
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

        private void DefaultFactory()
        {
            KnownExponentValues = new List<int>()
            {
                2,
                3,
                5,
                7,
                13,
                17,
                19,
                31,
                61,
                89,
                107,
                127,
                521,
                607,
                1279,
                2203,
                2281,
                3217,
                4253,
                4423,
                9689,
                9941,
                11213,
                19937,
                21701,
                23209,
                44497,
                86243,
                110503,
                132049,
                216091,
                756839,
                859433,
                1257787,
                1398269,
                2976221,
                3021377,
                6972593,
                13466917,
                20996011,
                24036583,
                25964951,
                30402457,
                32582657,
                37156667,
                42643801,
                43112609,
                57885161,
                74207281,
                77232917,
                82589933,
            };
        }
    }
}