# Innumerati
Roman Numerals (console application)

I declare that this is my own work :)
-Loki

# Discussion:

The largest limitations of this algorithm and implementation stem from the nature of the data that is being processed. The data gets larger with each increase in the candidate size and nature of the primality check does not obviously lend itself to desktop or even server CPU based multi-threading. 

The reason for this is that the calculations are performed on relatively large chunks of data that easily flood the CPU caches, diminishing the returns of muti threading.

Although this implementation will likely be able to eventually discover Mersenne Primes beyond the currently known 51st, it will take an inordinate amount of time to do so on its own (plausibly measured in years). There is some conjecture about the distribution of Mersenne Primes, that may speed up the search, but the primes you find are then not guaranteed to be the next in numerical order (which is why known Mersenne Primes > 47 are tentatively numbered).

A much better implementation would distribute the processing horizontally as is done with the GIMPS (Great Internet Mersenne Prime Search https://www.mersenne.org/)  project.

To add context, the 51st Mersenne Prime is 2^82589932... a number with 24'862'048 digits, so > 10.3MB for one number... which has to go through some of the slowest arithmetic computations (e.g. division etc.) a lot.

Next step improvements would be to swap out the standard division operations (including the modular math and square root calculation) with bit shifting, but I would need to google the heck out of that, because it is not trivial. 

Anecdotally, I am sure that C# is **much** slower than Python in this task. It feels even slower thanJavaScript, which is not great. For a "production" solution, I'd probably use a very different technology stack - likely GPGPU or FPGA based, once the bit shifting math is in place.

