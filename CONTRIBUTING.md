# Contributing

Thanks for helping improve RandomChooser.

## Development setup

Requirements:

- .NET 9 SDK
- A desktop environment with at least one monitor
- Optional: a second monitor to test Extend mode

```bash
git clone https://github.com/iTzFady/RandomChooser.git
cd RandomChooser
dotnet restore
dotnet build RandomChooser.csproj
dotnet test tests/RandomChooser.Tests/RandomChooser.Tests.csproj
dotnet run
```

## Code style

- Follow the existing C# file conventions in the repository.
- Keep view-specific logic in `Views/` and shared behavior in `Services/`.
- Use `App.Settings` for persisted values instead of adding new global state.
- Add Avalonia resources to `Styles/Styles.axaml` when introducing reusable styling.
- Do not add comments that restate the code; document non-obvious behavior in `docs/` instead.

## Before opening a pull request

1. Run `dotnet build RandomChooser.csproj` with zero errors.
2. Run `dotnet test tests/RandomChooser.Tests/RandomChooser.Tests.csproj` with all tests passing.
3. Manually verify any changed UI flow.
4. Update the README or relevant file under `docs/` if behavior changes.
5. Add a changelog entry under `[Unreleased]`.
6. Keep pull requests focused on one concern.

## Commit and release process

- Work on a feature branch from `master`.
- Open a pull request against `master`; CI must pass before merge.
- Maintainers create releases by pushing a `vMAJOR.MINOR.PATCH` tag.
- The GitHub Actions workflow attaches all platform artifacts to the release.

## Reporting bugs

Include:

- Platform and OS version
- Number of monitors
- App version or commit
- Steps to reproduce
- Expected and actual behavior

For startup or display issues, also include the relevant section of the [troubleshooting guide](docs/troubleshooting.md).
