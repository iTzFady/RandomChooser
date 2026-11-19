# ADR-004: User-selectable theme and sound volume

## Status

Accepted

## Date

2026-11-19

## Context

Users work in different lighting conditions and have different sound preferences. The original UI was fixed to a light palette, and the selection sound always played at the embedded file's native amplitude with no way to mute or lower it.

## Decision

Add two persisted settings:

- `IsDarkMode` controls `Application.RequestedThemeVariant`. Theme-aware brushes are defined in `Light` and `Dark` resource dictionaries in `App.axaml` and referenced with `DynamicResource`.
- `Volume` (0–100) scales the embedded 16-bit PCM WAV samples before playback, so muting and volume changes work with every platform player without adding an audio dependency.

Both are editable from the Theme / Volume controls in either settings page, applied immediately, and saved with the rest of `settings.json`.

## Alternatives considered

### Follow the OS theme only

- Pros: no setting needed.
- Cons: users cannot force a theme; the app previously forced light anyway.
- Rejected: explicit user control was requested.

### Third-party audio library for volume

- Pros: efficient runtime mixing.
- Cons: extra dependency, platform native libraries, larger release, more failure modes.
- Rejected: the sound is short and 16-bit PCM, so direct sample scaling is sufficient.

### Save only a theme string and volume level on disk

- Considered and partially adopted: a boolean and integer are simpler for UI binding and JSON, while theme dictionaries handle the actual palette switch.

## Consequences

- Both themes use the same layout and styles; only palette resources change.
- Presentation text/background colors remain independent from the UI theme.
- Volume changes affect only the selection sound, not OS volume.
- `settings.json` now contains `IsDarkMode` and `Volume`; derived brush properties are explicitly excluded from serialization.
