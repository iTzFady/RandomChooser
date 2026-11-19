namespace RandomChooser.Services;

public sealed class NumberPicker
{
    private RandomGen _generator = new();
    private int _min;
    private int _max;
    private bool _hasRange;

    public int Next(int min, int max)
    {
        int low = Math.Min(min, max);
        int high = Math.Max(min, max);

        if (!_hasRange || _min != low || _max != high)
        {
            _generator = new RandomGen();
            _min = low;
            _max = high;
            _hasRange = true;
        }

        return _generator.Next(low, high);
    }
}
