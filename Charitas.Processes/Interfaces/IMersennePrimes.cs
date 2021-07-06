using System.Threading.Tasks;

namespace Charitas.Processes.Interfaces
{
    public interface IMersennePrimes
    {
        Task FindMersennePrimes(long start);

        bool IsMersennePrime(long candidate);
    }
}