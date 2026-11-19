using RandomChooser.Services;
using Xunit;

namespace RandomChooser.Tests;

public class NumberPickerTests
{
    [Fact]
    public void Next_NormalizesReversedRange()
    {
        var picker = new NumberPicker();

        for (int index = 0; index < 10; index++)
        {
            Assert.InRange(picker.Next(7, 5), 5, 7);
        }
    }

    [Fact]
    public void Next_UsesCurrentRangeAfterRangeChange()
    {
        var picker = new NumberPicker();

        for (int index = 0; index < 3; index++)
        {
            Assert.InRange(picker.Next(5, 7), 5, 7);
        }

        Assert.InRange(picker.Next(10, 12), 10, 12);
    }

    [Fact]
    public void Next_DoesNotRepeatUntilRangeIsExhausted()
    {
        var picker = new NumberPicker();
        var values = new HashSet<int>();

        for (int index = 0; index < 3; index++)
        {
            values.Add(picker.Next(5, 7));
        }

        Assert.Equal(3, values.Count);
    }
}
