# ADR-001: Adopt Avalonia instead of WPF

## Status

Accepted

## Date

2026-11-19

## Context

The original application was a WPF project targeting `net9.0-windows`. It built successfully on Windows but could not start on Linux:

```text
Framework: 'Microsoft.NETCore.App', version '9.0.0' (x64)
The following frameworks were found:
  10.0.14 at [/usr/share/dotnet/shared/Microsoft.NETCore.App]
```

Beyond the runtime mismatch, WPF itself is Windows-only. The project needed to run on Windows and Linux from one codebase, with macOS support also desirable.

## Decision

Port the application in place to Avalonia 11.3 with:

- `net9.0` as the target framework.
- Fluent theme plus a custom shared style file.
- Self-contained single-file release publishing for Windows, Linux, and macOS.
- `rollForward: Major` for developer `dotnet run` scenarios with newer runtimes.

## Alternatives considered

### Keep WPF and run through Wine or compatibility layers

- Pros: no UI rewrite.
- Cons: fragile deployment, poor Linux/macOS integration, no official support.
- Rejected: does not meet the cross-platform requirement.

### .NET MAUI

- Pros: first-party Microsoft cross-platform stack.
- Cons: heavier workload and project model, mobile-oriented tooling, unnecessary for a desktop-only app.
- Rejected: Avalonia is a better fit for a small desktop app with custom window placement.

### Electron or web wrapper

- Pros: familiar web UI tooling.
- Cons: large runtime, different language stack, awkward native multi-monitor window control.
- Rejected: would discard the existing C# domain code and add unnecessary weight.

## Consequences

- One codebase now produces all platform artifacts.
- WPF-specific controls, themes, and code-behind were replaced with Avalonia equivalents.
- Multi-monitor logic was rewritten against Avalonia's `Screens` API.
- CI must publish each RID explicitly because single-file self-contained output is platform-specific.
