# ADR-003: Self-contained single-file releases

## Status

Accepted

## Date

2026-11-19

## Context

Users downloading a release should be able to run the app without installing .NET, and should not have to manage a folder of DLLs and native libraries. Earlier Linux confusion around missing runtime versions showed how fragile framework-dependent distribution can be.

## Decision

Publish all release artifacts with:

```text
--self-contained true
-p:PublishSingleFile=true
-p:IncludeNativeLibrariesForSelfExtract=true
-p:EnableCompressionInSingleFile=true
-p:DebugType=None
-p:DebugSymbols=false
```

Windows artifacts are zipped; Linux and macOS artifacts are gzip-compressed tarballs containing the single executable.

## Alternatives considered

### Framework-dependent builds

- Pros: small download.
- Cons: requires matching .NET runtime; caused previous startup failures.
- Rejected: worse user experience.

### Self-contained folder output

- Pros: still runtime-independent.
- Cons: many files to extract and launch; easier to break by moving one file.
- Rejected: single file is simpler to distribute.

### Installer packages (MSI, DEB, DMG)

- Pros: familiar installation flow.
- Cons: signing, packaging, and update complexity; unnecessary for a portable utility.
- Deferred: can be added later if demanded.

## Consequences

- Release executables are larger but require no installed runtime.
- Native Avalonia libraries are extracted at first launch and included in the single file.
- CI must publish one artifact per RID.
- Debug symbols are excluded from release artifacts.
