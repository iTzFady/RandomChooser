# RandomChooser

[![Build and Release](https://github.com/iTzFady/RandomChooser/actions/workflows/build.yml/badge.svg)](https://github.com/iTzFady/RandomChooser/actions/workflows/build.yml)

A cross-platform desktop app for picking a random number or a random word, building random teams, and presenting results on a dual-monitor setup.

## Features

- Random number picking from a configurable inclusive range.
- Random word picking from a user-managed word list.
- Team builder that shuffles names into balanced teams with editable team names.
- Automatic or manual team count selection.
- Friendly, plain-language validation messages instead of raw conversion errors.
- Explicit Start button on picker pages so opening a picker never launches presentation by accident.
- No-repeat number generation until the full range has been used.
- Two presentation modes: **Extend** (result on the second monitor) and **Duplicate** (fullscreen on the main monitor).
- Light and dark UI themes with an in-app toggle.
- Configurable text color and background color for the presentation screen.
- Adjustable selection sound volume from 0–100%.
- Self-contained, single-file releases for Windows, Linux, and macOS.

## Downloads

Grab the latest pre-built archive from [GitHub Releases](https://github.com/iTzFady/RandomChooser/releases/latest).

| Platform | Artifact | How to run |
| --- | --- | --- |
| Windows x64 | `RandomChooser-windows-x64.zip` | Extract and run `RandomChooser.exe` |
| Linux x64 | `RandomChooser-linux-x64.tar.gz` | Extract, `chmod +x RandomChooser`, run `./RandomChooser` |
| macOS Apple Silicon | `RandomChooser-macos-arm64.tar.gz` | Extract, `chmod +x RandomChooser`, run `./RandomChooser` |

All release builds are self-contained. No .NET runtime installation is required.

## Quick start from source

Requirements: [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0).

```bash
git clone https://github.com/iTzFady/RandomChooser.git
cd RandomChooser
dotnet restore
dotnet run
```

Run the automated tests:

```bash
dotnet test tests/RandomChooser.Tests/RandomChooser.Tests.csproj
```

Build a release for your current platform:

```bash
dotnet publish RandomChooser.csproj -c Release --self-contained true \
  -p:PublishSingleFile=true \
  -p:IncludeNativeLibrariesForSelfExtract=true \
  -p:EnableCompressionInSingleFile=true
```

## How it works

1. Pick **Random number**, **Random word**, or **Team builder** on the home screen.
2. On a picker page, press **Start** to enter presentation mode.
3. Press the dice button for each new pick.
4. Use **Home** to return to the previous screen, or **Exit** to close the app.
5. Open **Settings** to change the range, colors, words, or display mode.

The **Back home** button on picker start screens returns to the main screen without starting presentation mode.

### Display modes

| Mode | Behavior |
| --- | --- |
| **Extend** | The main window snaps to the primary monitor and a borderless result window opens on the secondary monitor. Closing the main window closes the result window, just like ending a PowerPoint slideshow. |
| **Duplicate** | The result stays on the main monitor in fullscreen. |

The **Extend** toggle is automatically disabled when only one monitor is detected.

### Team builder

Enter one name per line, then choose **Automatic** or **Manual** team count. Automatic mode creates about the square root of the name count (for example, 10 names → 4 teams). Manual mode accepts any count from 1 to the number of names. Teams are balanced, shuffled, named `Team 1`, `Team 2`, and so on, and every team name can be edited in the results.

## Configuration and data

Settings are stored per user in the application data folder:

| File | Purpose |
| --- | --- |
| `settings.json` | Number range, display mode, theme, volume, text color, background color |
| `items.json` | Word list used by the random word picker |

Locations:

- Windows: `%AppData%\RandomChooser\`
- Linux/macOS: `~/.config/RandomChooser/`

See [Configuration](docs/configuration.md) for the full schema.

## Project structure

```text
RandomChooser/
├── Assets/                 # Icons, images, fonts, selection sound
├── Services/               # Settings, navigation, sound, presentation, team logic
├── Styles/                 # Shared Avalonia styles and palette
├── Views/                  # Avalonia pages and windows
├── tests/                  # xUnit test project
├── App.axaml*              # Application setup
├── MainWindow.axaml*       # Root shell
├── Program.cs              # Entry point
└── RandomChooser.csproj    # Project and package references
```

## Documentation

- [Documentation index](docs/README.md)
- [Getting started](docs/getting-started.md)
- [User guide](docs/user-guide.md)
- [Configuration](docs/configuration.md)
- [Architecture](docs/architecture.md)
- [Build and release](docs/build-and-release.md)
- [Troubleshooting](docs/troubleshooting.md)
- [Architecture decisions](docs/decisions/)

## CI/CD

Every push and pull request runs the unit tests and builds all three release targets via [`.github/workflows/build.yml`](.github/workflows/build.yml). Pushing a `v*` tag (for example `v1.0.0`) additionally creates a GitHub Release and attaches every platform archive.

There is currently no automated test project; CI enforces restore and publish on every supported platform.

## Contributing

1. Fork the repository and create a feature branch.
2. Keep changes focused and follow the existing code style.
3. Run `dotnet build RandomChooser.csproj` before opening a pull request.
4. Update the documentation when behavior or configuration changes.

See [CONTRIBUTING.md](CONTRIBUTING.md) for details.
