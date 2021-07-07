using System.Threading.Tasks;

namespace Charitas.Processes.Interfaces
{
    public interface IMersennePrimes
    {
        Task FindMersennePrimes(int start);

        bool IsMersennePrime(int candidate);
    }
}