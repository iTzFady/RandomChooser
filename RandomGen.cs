using System.Diagnostics;
using System.Security.Cryptography;

namespace RandomChooser
{
    internal class RandomGen
    {
        private readonly Random _rnd;
        private HashSet<int> _used = new HashSet<int>();
        private int _min;
        private int _max;
        private bool _rangeInitialized = false;

        public RandomGen()
        {
            long ticks = DateTime.UtcNow.Ticks ^ Stopwatch.GetTimestamp();
            byte[] timeBytes = BitConverter.GetBytes(ticks);

            byte[] hash;
            using (SHA256 sha = SHA256.Create())
                hash = sha.ComputeHash(timeBytes);

            int seed = BitConverter.ToInt32(hash, 0) & int.MaxValue;
            _rnd = new Random(seed);
        }

        public int Next(int min, int max)
        {
            if (min > max)
                throw new ArgumentOutOfRangeException(nameof(min), "Min must be less than or equal to Max");

            if (!_rangeInitialized)
            {
                _min = min;
                _max = max;
                _rangeInitialized = true;
            }
            else if (_min != min || _max != max)
            {
                throw new InvalidOperationException("Cannot change range after first use.");
            }

            int rangeSize = _max - _min + 1;

            if (_used.Count >= rangeSize)
            {
                _used.Clear();
            }

            int value;
            do
            {
                value = _rnd.Next(_min, _max + 1);
            } while (_used.Contains(value));

            _used.Add(value);
            return value;
        }
    }
}
