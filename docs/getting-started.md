# Getting started

## Requirements

| Task | Requirement |
| --- | --- |
| Run a published release | No extra runtime required |
| Build from source | .NET 9 SDK |
| Extend mode | Two monitors connected |

## Install a release

1. Open [GitHub Releases](https://github.com/iTzFady/RandomChooser/releases/latest).
2. Download the archive for your platform.
3. Extract it into a folder of your choice.
4. Run the executable.

On Linux and macOS, mark the binary as executable first:

```bash
chmod +x RandomChooser
./RandomChooser
```

## Run from source

```bash
git clone https://github.com/iTzFady/RandomChooser.git
cd RandomChooser
dotnet restore
dotnet run
```

The debug build targets `net9.0` and uses `rollForward: Major`, so it also runs on a machine that only has a newer .NET runtime installed.

## First run

On first launch the app creates its per-user data folder and fills in defaults:

- Number range: `1` to `100`
- Display mode: `Extend` (used only when two or more monitors are detected)
- Text color: `#191A1F`
- Background color: `#F5F6F8`
- Word list: empty

Open **Settings** to add words before using the random word picker.

## Verify your setup

1. Choose **Random number** and press the dice button. A result should appear.
2. Open **Settings**. If two monitors are detected, the **Extend** toggle is enabled.
3. On a dual-monitor system, start a pick in **Extend** mode and confirm the result window opens on the second monitor.
