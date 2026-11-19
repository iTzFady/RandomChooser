namespace RandomChooser.Services;

public sealed class Team
{
    public string Name { get; set; } = string.Empty;

    public List<string> Members { get; } = new();
}

public sealed record TeamBuildResult(
    IReadOnlyList<Team> Teams,
    int NameCount,
    int TeamCount,
    string? Error)
{
    public bool Success => Error is null;

    public static TeamBuildResult Failure(string error)
    {
        return new TeamBuildResult(Array.Empty<Team>(), 0, 0, error);
    }
}

public static class TeamBuilder
{
    public static int SuggestTeamCount(int nameCount)
    {
        if (nameCount <= 1)
        {
            return nameCount;
        }

        return Math.Clamp((int)Math.Ceiling(Math.Sqrt(nameCount)), 2, nameCount);
    }

    public static TeamBuildResult Build(string? namesInput, int? requestedTeamCount)
    {
        var names = (namesInput ?? string.Empty)
            .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(name => name.Trim())
            .Where(name => name.Length > 0)
            .ToList();

        if (names.Count == 0)
        {
            return TeamBuildResult.Failure("Add at least one name, one per line, before creating teams.");
        }

        int teamCount;
        if (requestedTeamCount.HasValue)
        {
            teamCount = requestedTeamCount.Value;
            if (teamCount < 1 || teamCount > names.Count)
            {
                return TeamBuildResult.Failure(
                    $"Choose a team count between 1 and {names.Count}. We found {names.Count} names.");
            }
        }
        else
        {
            teamCount = SuggestTeamCount(names.Count);
        }

        Shuffle(names);

        var teams = Enumerable.Range(1, teamCount)
            .Select(index => new Team { Name = $"Team {index}" })
            .ToList();

        for (int index = 0; index < names.Count; index++)
        {
            teams[index % teamCount].Members.Add(names[index]);
        }

        return new TeamBuildResult(teams, names.Count, teamCount, null);
    }

    private static void Shuffle(List<string> names)
    {
        for (int index = names.Count - 1; index > 0; index--)
        {
            int swapWith = Random.Shared.Next(index + 1);
            (names[index], names[swapWith]) = (names[swapWith], names[index]);
        }
    }
}
