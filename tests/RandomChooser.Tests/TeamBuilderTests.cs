using RandomChooser.Services;
using Xunit;

namespace RandomChooser.Tests;

public class TeamBuilderTests
{
    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(4, 2)]
    [InlineData(10, 4)]
    [InlineData(30, 6)]
    [InlineData(100, 10)]
    public void SuggestTeamCount_ReturnsExpectedCount(int nameCount, int expected)
    {
        Assert.Equal(expected, TeamBuilder.SuggestTeamCount(nameCount));
    }

    [Fact]
    public void Build_WithAutomaticCount_CreatesBalancedTeamsWithDefaultNames()
    {
        string[] names = Enumerable.Range(1, 10).Select(index => $"Name {index}").ToArray();

        TeamBuildResult result = TeamBuilder.Build(string.Join('\n', names) + "\n\n  ", null);

        Assert.True(result.Success);
        Assert.Equal(10, result.NameCount);
        Assert.Equal(4, result.TeamCount);
        Assert.Equal(new[] { "Team 1", "Team 2", "Team 3", "Team 4" },
            result.Teams.Select(team => team.Name));
        Assert.Equal(new[] { 2, 2, 3, 3 }, result.Teams.Select(team => team.Members.Count).OrderBy(count => count));
        Assert.Equal(names.OrderBy(name => name), result.Teams.SelectMany(team => team.Members).OrderBy(name => name));
    }

    [Fact]
    public void Build_WithManualCount_HonorsRequestedCountAndKeepsTeamsBalanced()
    {
        string[] names = Enumerable.Range(1, 10).Select(index => $"Name {index}").ToArray();

        TeamBuildResult result = TeamBuilder.Build(string.Join('\n', names), 3);

        Assert.True(result.Success);
        Assert.Equal(3, result.TeamCount);
        Assert.Equal(new[] { 3, 3, 4 }, result.Teams.Select(team => team.Members.Count).OrderBy(count => count));
        Assert.Equal(names.OrderBy(name => name), result.Teams.SelectMany(team => team.Members).OrderBy(name => name));
    }

    [Fact]
    public void Build_WithBlankInput_ReturnsFriendlyError()
    {
        TeamBuildResult result = TeamBuilder.Build("  \n\n", null);

        Assert.False(result.Success);
        Assert.Contains("Add at least one name", result.Error, StringComparison.Ordinal);
        Assert.Empty(result.Teams);
    }

    [Fact]
    public void Build_WithOutOfRangeTeamCount_ReturnsFriendlyError()
    {
        TeamBuildResult result = TeamBuilder.Build("Alex\nBob\nCharlie", 4);

        Assert.False(result.Success);
        Assert.Contains("between 1 and 3", result.Error, StringComparison.Ordinal);
    }

    [Fact]
    public void Build_WithDuplicateNames_TreatsThemAsSeparatePeople()
    {
        TeamBuildResult result = TeamBuilder.Build("Alex\nAlex\nBob", 2);

        Assert.True(result.Success);
        Assert.Equal(3, result.NameCount);
        Assert.Equal(2, result.Teams.Sum(team => team.Members.Count(name => name == "Alex")));
        Assert.Equal(3, result.Teams.Sum(team => team.Members.Count));
    }
}
