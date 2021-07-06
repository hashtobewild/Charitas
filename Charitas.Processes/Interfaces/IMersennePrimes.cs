using System.Numerics;
using System.Threading.Tasks;

namespace Charitas.Processes.Interfaces
{
    public interface IMersennePrimes
    {
        Task FindMersennePrimes(BigInteger start);

        bool IsMersennePrime(BigInteger candidate);
    }
}