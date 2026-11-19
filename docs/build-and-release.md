# Build and release

## Local development

```bash
dotnet restore RandomChooser.csproj
dotnet build RandomChooser.csproj
dotnet test tests/RandomChooser.Tests/RandomChooser.Tests.csproj
dotnet run
```

## Local self-contained publishes

Each command produces a single-file executable with native libraries included.

### Windows x64

```bash
dotnet publish RandomChooser.csproj -c Release -r win-x64 --self-contained true \
  -p:PublishSingleFile=true \
  -p:IncludeNativeLibrariesForSelfExtract=true \
  -p:EnableCompressionInSingleFile=true \
  -p:DebugType=None \
  -p:DebugSymbols=false \
  -o publish/windows-x64
```

### Linux x64

```bash
dotnet publish RandomChooser.csproj -c Release -r linux-x64 --self-contained true \
  -p:PublishSingleFile=true \
  -p:IncludeNativeLibrariesForSelfExtract=true \
  -p:EnableCompressionInSingleFile=true \
  -p:DebugType=None \
  -p:DebugSymbols=false \
  -o publish/linux-x64
```

### macOS Apple Silicon

```bash
dotnet publish RandomChooser.csproj -c Release -r osx-arm64 --self-contained true \
  -p:PublishSingleFile=true \
  -p:IncludeNativeLibrariesForSelfExtract=true \
  -p:EnableCompressionInSingleFile=true \
  -p:DebugType=None \
  -p:DebugSymbols=false \
  -o publish/macos-arm64
```

The project uses `<OutputType>WinExe</OutputType>`, so the Windows executable has a GUI subsystem and does not open a console window.

## GitHub Actions

The workflow is defined in `.github/workflows/build.yml`.

### Triggers

| Event | Result |
| --- | --- |
| Push to `master` | Runs unit tests, builds all targets, and uploads artifacts |
| Pull request to `master` | Runs unit tests, builds all targets, and uploads artifacts |
| Push of a `v*` tag | Runs unit tests, builds all targets, uploads artifacts, and creates a GitHub Release |
| Manual `workflow_dispatch` | Runs unit tests, builds all targets, and uploads artifacts |

### Build matrix

| Runner | RID | Artifact |
| --- | --- | --- |
| `windows-latest` | `win-x64` | `RandomChooser-windows-x64.zip` |
| `ubuntu-latest` | `linux-x64` | `RandomChooser-linux-x64.tar.gz` |
| `macos-latest` | `osx-arm64` | `RandomChooser-macos-arm64.tar.gz` |

### Cutting a release

```bash
git tag v1.0.0
git push origin v1.0.0
```

The workflow downloads all build artifacts into one folder and attaches them to the generated release with automatic release notes.

Intel Macs can run the Apple Silicon artifact through Rosetta 2. The macOS artifacts are standalone executables, not `.app` bundles.
