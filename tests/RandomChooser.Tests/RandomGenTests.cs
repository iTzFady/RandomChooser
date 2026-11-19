using RandomChooser;
using Xunit;

namespace RandomChooser.Tests;

public class RandomGenTests
{
    [Fact]
    public void Next_ReturnsValueWithinRange()
    {
        var generator = new RandomGen();

        for (int index = 0; index < 25; index++)
        {
            Assert.InRange(generator.Next(5, 7), 5, 7);
        }
    }

    [Fact]
    public void Next_DoesNotRepeatUntilRangeIsExhausted()
    {
        var generator = new RandomGen();
        var values = new HashSet<int>();

        for (int index = 0; index < 3; index++)
        {
            values.Add(generator.Next(5, 7));
        }

        Assert.Equal(new[] { 5, 6, 7 }, values.OrderBy(value => value));
    }

    [Fact]
    public void Next_ResetsAfterRangeIsExhausted()
    {
        var generator = new RandomGen();

        for (int index = 0; index < 3; index++)
        {
            generator.Next(5, 7);
        }

        Assert.InRange(generator.Next(5, 7), 5, 7);
    }

    [Fact]
    public void Next_SupportsMaxIntBoundaryWithoutOverflow()
    {
        var generator = new RandomGen();

        int first = generator.Next(int.MaxValue - 1, int.MaxValue);
        int second = generator.Next(int.MaxValue - 1, int.MaxValue);

        Assert.InRange(first, int.MaxValue - 1, int.MaxValue);
        Assert.InRange(second, int.MaxValue - 1, int.MaxValue);
        Assert.NotEqual(first, second);
    }

    [Fact]
    public void Next_SupportsMinIntBoundary()
    {
        var generator = new RandomGen();

        int first = generator.Next(int.MinValue, int.MinValue + 1);
        int second = generator.Next(int.MinValue, int.MinValue + 1);

        Assert.InRange(first, int.MinValue, int.MinValue + 1);
        Assert.InRange(second, int.MinValue, int.MinValue + 1);
        Assert.NotEqual(first, second);
    }

    [Fact]
    public void Next_WithReversedRange_Throws()
    {
        var generator = new RandomGen();

        Assert.Throws<ArgumentOutOfRangeException>(() => generator.Next(10, 5));
    }
}
