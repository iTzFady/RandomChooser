# Changelog

All notable changes to this project are documented here.

## [Unreleased]

### Added

- Cross-platform Avalonia UI for Windows, Linux, and macOS.
- GitHub Actions workflow that builds all target platforms on every push and pull request.
- Automatic GitHub Release creation for `v*` tags with per-platform artifacts.
- Full documentation set and professional project README.
- PowerPoint-style Extend mode with primary-monitor snapping and owned result window.
- Light and dark UI themes with an in-app toggle.
- Adjustable selection sound volume from 0–100% with mute support.
- Team builder with shuffled, balanced teams, automatic/manual team counts, and editable team names.
- Plain-language validation messages for numeric fields, color input, and team creation.
- xUnit test project covering team building, no-repeat number generation, range changes, settings persistence, and word-list normalization.

### Changed

- Replaced the WPF project with a single `net9.0` Avalonia project.
- Switched Windows output to GUI subsystem (`WinExe`) to avoid a console window.
- Release builds are now self-contained, compressed, single-file executables.
- Word list and settings are stored in the per-user application data folder.

### Fixed

- Blank startup window caused by missing initial navigation to `MainPage`.
- Extended window not being positioned on the secondary monitor.
- Extended window not closing with the main window.
- Main window not snapping to the primary monitor during presentation.
- Cross-platform sound playback without `System.Media.SoundPlayer`.
- Empty or non-numeric min/max fields no longer throw `InvalidCastException`; they show a friendly message instead.
- Word and number page switching now navigates directly instead of going back.
- Switching to Random number no longer starts presentation mode immediately; a Start button is shown first.
- Picker start screens now use **Back home** instead of switch buttons.
- Generated team names are now clearly labeled as click-to-edit fields.
- Random number generation no longer overflows near `int.MaxValue`.
- Random number picks now use the current range after settings are changed.
- Opening settings during a presentation stops it and returns to the Start screen.
- Theme and volume changes are saved immediately.
- Blank or null word entries are ignored, and blank names are removed when saving.
- The color picker opens with the current color instead of always using indigo.
- Temporary audio files are deleted after playback.
- Save and reset failures show plain-language messages instead of throwing.
- Selection sound no longer blocks number and word picks while the platform player is running.
