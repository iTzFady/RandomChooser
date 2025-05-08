using System.Diagnostics;
using System.Security.Cryptography;

namespace RandomChooser
{
    internal class RandomGen
    {
        private readonly RandomNumberGenerator _rnd = RandomNumberGenerator.Create();

        public int Next(int min , int max) {
            if (min > max) throw new ArgumentOutOfRangeException("Min must be less than Max");

            long ticks = DateTime.UtcNow.Ticks ^ Stopwatch.GetTimestamp();
            byte[] timeBytes = BitConverter.GetBytes(ticks);

            byte[] hash;

            using (SHA256 sha = SHA256.Create())
            { 
                hash = sha.ComputeHash(timeBytes);
            }

            int seed = BitConverter.ToInt32(hash, 0) & int.MaxValue;
            var rnd = new Random(seed);
            return rnd.Next(min, max + 1);
        }
    }
}
