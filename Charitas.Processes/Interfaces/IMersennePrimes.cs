using System.Collections.Generic;
using System.Threading.Tasks;

namespace Charitas.Processes.Interfaces
{
    public interface IMersennePrimes
    {
        List<int> KnownValues { get; set; }

        Task FindMersennePrimes(int start);

        bool IsMersennePrime(int candidate);
    }
}