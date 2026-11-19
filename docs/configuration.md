# Configuration

## Storage locations

RandomChooser stores all user data in the per-user application data folder:

| Platform | Path |
| --- | --- |
| Windows | `%AppData%\RandomChooser\` |
| Linux | `~/.config/RandomChooser/` (or `$XDG_CONFIG_HOME/RandomChooser`) |
| macOS | `~/.config/RandomChooser/` |

Two files are created on demand:

| File | Written by | Purpose |
| --- | --- | --- |
| `settings.json` | Settings / Word settings pages | Range, display mode, theme, volume, colors |
| `items.json` | Word settings page | Saved word list |

## `settings.json`

```json
{
  "MinRange": 1,
  "MaxRange": 100,
  "DisplayMode": true,
  "IsDarkMode": false,
  "Volume": 70,
  "TextColor": "#191A1F",
  "PageColor": "#F5F6F8"
}
```

| Field | Type | Default | Description |
| --- | --- | --- | --- |
| `MinRange` | integer | `1` | Inclusive lower bound for number picks |
| `MaxRange` | integer | `100` | Inclusive upper bound for number picks; must be greater than `MinRange` |
| `DisplayMode` | boolean | `true` | `true` uses Extend mode when a second monitor exists; `false` uses fullscreen Duplicate mode |
| `IsDarkMode` | boolean | `false` | `true` switches the application UI to the dark theme; `false` uses light |
| `Volume` | integer | `70` | Selection sound level from `0` (muted) to `100` |
| `TextColor` | hex string | `#191A1F` | Color of the result text |
| `PageColor` | hex string | `#F5F6F8` | Color of the presentation background |

Invalid saved values are ignored and replaced with defaults on load. Derived brush properties are excluded from the saved JSON.

## `items.json`

```json
{
  "Words": [
    { "Name": "Alice" },
    { "Name": "Bob" }
  ]
}
```

Each entry has a single editable `Name` field. Empty entries can be added and filled in from the word settings page. Blank names, null entries, and whitespace-only names are ignored when the list is loaded and are removed when the word settings page is saved.

## Manual editing

You can edit these files while the app is closed. The app reads them on startup and overwrites them when you press **Save** or **Reset** in settings. Theme and volume changes are also saved immediately. If a save fails because the folder is not writable, the app shows a plain-language message instead of throwing.
