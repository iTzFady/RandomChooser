# Troubleshooting

## The window opens but is blank or light gray

This happens when no page has been navigated to. Fixed in current builds by calling `Navigation.Navigate(new MainPage())` during application startup. Download the latest release or pull the latest source.

## Extend mode does not open a second window

Check the following:

1. Two monitors are connected and detected by the OS.
2. **Settings → Display mode** is enabled.
3. The Extend toggle is not disabled (it is disabled automatically with one monitor).
4. You started a pick after opening Settings; the mode is read when the pick screen loads.

## The main window does not fill the primary monitor

The app snaps the main window to the primary monitor's **working area**, which excludes taskbars and docks. If your desktop uses panels, the window will stop at those edges by design. Different monitor scaling is handled via `Screen.Scaling`.

## The extended window stays open after closing the main window

Current builds make the extended window an owned child of the main window and also close it from the main window's `Closing` handler. Use the **Exit** button or the window close button. If you are seeing a stale window, confirm no old `RandomChooser` process is still running.

## No sound is played

Sound is cosmetic and failures are ignored.

- Windows: PowerShell must be available.
- macOS: `afplay` is used.
- Linux: one of `paplay`, `aplay`, or `ffplay` must be installed.

## Settings are not saved

Settings are written when you press **Save** or **Reset**, and theme or volume changes are saved immediately. If the app cannot write to its configuration folder, it shows a plain-language save error instead of throwing. Check that the app has write access to the folder (see [Configuration](configuration.md)).

## `dotnet run` reports a missing .NET 9 runtime

The project includes `runtimeconfig.template.json` with `rollForward: Major`, allowing a newer installed runtime to be used. If this still fails, install the .NET 9 SDK, or use a self-contained release build, which needs no installed runtime.

## The Windows release shows a console window

This should not happen in current builds; the project uses `WinExe`. If you built locally from an older checkout, verify `<OutputType>WinExe</OutputType>` in `RandomChooser.csproj`.

## The theme does not change

The theme toggle updates the UI immediately and is saved right away. If it does not, confirm you are using a current build. `IsDarkMode` is stored in `settings.json`; derived brush properties are no longer written to that file.

## The sound is too quiet or silent

Check the **Volume** slider in settings. 0% mutes playback. Volume is applied by scaling the embedded WAV samples, so it works even when the platform player has no volume option. If there is still no sound, see the player requirements in the "No sound is played" section above.

## An error mentions "cannot convert" or raw exception text

Current builds validate input before using it. Numeric fields, hex colors, and team creation show plain-language messages such as "Enter the minimum number.", "Enter the maximum number.", or "The team count must be a whole number, like 10." If you still see raw .NET exception text such as `InvalidCastException`, update to a newer build. Report any remaining raw message with the exact steps so it can be converted into a friendly one.
