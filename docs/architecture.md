# Architecture

## Overview

RandomChooser is a single-project Avalonia application targeting `net9.0`. It uses one shared codebase for Windows, Linux, and macOS releases.

```text
Program.cs
   │
   ▼
App (App.axaml / App.axaml.cs)
   ├── static AppSettings ──── settings.json / items.json
   ├── static NavigationService ─── page stack
   └── MainWindow ─── ContentControl bound to NavigationService.Current
          │
          ▼
Views (MainPage, RandomNumberPage, RandomWordPage, TeamBuilderPage, SettingsPage, ...)
          │
          ▼
Services (Presenter, SoundService, TeamBuilder, NumberPicker, FriendlyInput)
```

## Startup flow

1. `Program.Main` configures Avalonia with platform detection, Inter font, and trace logging.
2. `App.Initialize` loads `App.axaml`, which installs the Fluent theme and `Styles/Styles.axaml`.
3. `App.OnFrameworkInitializationCompleted` creates `MainWindow` and navigates to `MainPage`.
4. `MainWindow` hosts a `ContentControl` bound to `App.Navigation.Current`.

Initial navigation is deliberate: without it the root `ContentControl` has no content and the window appears blank.

## Navigation

`NavigationService` keeps a `Stack<UserControl>` history and exposes:

- `Current` — the visible page.
- `CanGoBack` — whether history exists.
- `Navigate(page)` — pushes the current page and shows the new one.
- `GoBack()` — pops and restores the previous page.

Pages notify navigation through event handlers; there is no MVVM framework dependency. Picker pages show a start card first; presentation mode begins only when **Start** is pressed, so opening or leaving a picker never snaps the window or opens the extended window.

## Presentation flow

Random result pages create a `Presenter` tied to their result `TextBlock`.

1. `Start()` resolves the owning `Window` via `TopLevel.GetTopLevel`.
2. It captures the current window layout, then snaps the main window to the primary monitor's working area.
3. In Extend mode it creates an `ExtendedWindow`, configures label/font size, and calls `Show(mainWindow)`. Passing the owner makes the result window a child, so normal window closing closes both.
4. A `Closing` handler on the main window closes the result window as a fallback.
5. `Update(text)` writes to both the inline result and the extended window.
6. `Stop()` closes the extended window, detaches the handler, and restores the original main-window layout.

Monitor selection uses `Window.Screens`: primary is `Screens.Primary`, and the presentation screen is the first screen where `IsPrimary` is false. Positions are set in physical pixels; widths and heights are divided by `Screen.Scaling`.

## Settings

`AppSettings` is a static, process-wide instance loaded from JSON on first access. It implements `INotifyPropertyChanged` so color changes update exposed brush properties without restarting. Settings pages bind directly to `App.Settings` via `x:Static` and call `TrySave()` explicitly; theme and volume changes are persisted immediately by `App`. Derived brush properties are marked `JsonIgnore`.

`App.ApplyTheme()` maps `IsDarkMode` to `RequestedThemeVariant`. Theme-aware brushes are declared in `Light` and `Dark` resource dictionaries in `App.axaml`; all style selectors use `DynamicResource` so they update at runtime.

Word data lives in `RandomWordConfig.cs` (`WordGen` / `WordsList`) and is stored in `items.json` next to settings.

## Number picking

`NumberPicker` wraps `RandomGen` and normalizes the configured range on every pick. It creates a fresh generator when the range changes, so a page left open across a settings edit never keeps using stale bounds. `RandomGen` holds the no-repeat cycle using `Random.NextInt64` to avoid overflow near `int.MaxValue`.

## Team building

`TeamBuilder` is a dependency-free service used by `TeamBuilderPage`.

1. `Build()` trims names and drops blank lines.
2. Manual counts are validated against the name count; automatic counts use `ceil(sqrt(count))` clamped to 2–count.
3. Names are shuffled with Fisher–Yates using `Random.Shared`.
4. Names are dealt round-robin to `Team` objects named `Team 1`, `Team 2`, etc.
5. Results are held in an `ObservableCollection<Team>` so inline name edits update immediately.

`FriendlyInput` centralizes readable validation for numeric fields and wraps `MessageWindow.Info` for errors. No raw conversion exceptions are shown to users.

## Sound

`Assets/Audio/select.wav` is embedded as a resource. `SoundService` scales the 16-bit PCM samples by `AppSettings.Volume`, writes the adjusted WAV to a temporary file, and plays it with the platform's available player: PowerShell on Windows, `afplay` on macOS, or `paplay`/`aplay`/`ffplay` on Linux. A volume of 0 skips playback entirely. Failures are ignored because sound is cosmetic.

## Styling

`App.axaml` defines the Light/Dark palette in theme dictionaries and requests the Fluent theme. `Styles/Styles.axaml` references those theme brushes and defines named styles for cards, buttons, text, text boxes, team cards, and member chips.

## Testing status

There is currently no automated test project. Verification is performed by compiling all target platforms in CI and by manual GUI checks on Linux and Windows.
