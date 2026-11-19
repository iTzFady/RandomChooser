# User guide

## Home screen

The home screen offers three actions:

| Action | Result |
| --- | --- |
| **Random number** | Opens the number picker |
| **Random word** | Opens the word picker |
| **Team builder** | Opens the team builder |
| Gear button | Opens settings |

## Picking a number

1. Set the inclusive range in **Settings → Number range**.
2. Go back home and choose **Random number**.
3. Press **Start random number** to enter presentation mode.
4. Press the dice button for each new pick.

Numbers are not repeated until every value in the range has been used, after which the cycle restarts.

**Back home** on a picker start screen returns to the main screen without starting presentation mode. Once a presentation is running, use **Home** to stop it and return.

## Picking a word

1. Open **Settings → Word settings** (the gear button on the word screen) and add at least two words.
2. Go back and choose **Start random word**.
3. Press the dice button for each new pick.

If the list has fewer than two words, the app shows a warning instead of starting.

## Building teams

1. Open **Team builder** from the home screen.
2. Type one name per line. Blank lines are ignored.
3. Choose **Automatic** to let the app decide, or switch to **Manual** and enter a team count.
4. Press **Create teams**.

Automatic mode uses about the square root of the number of names: 10 names produce 4 teams, 30 names produce 6. Manual mode accepts any whole number from 1 up to the number of names.

Names are shuffled and dealt round-robin, so team sizes differ by at most one person. Teams start with default names such as `Team 1` and `Team 2`. Each result card has a **Team name — click to edit** field; click it and type a new name to rename that team, just like editing a word in the word list. **Clear** removes the entered names and results.

## Friendly error messages

The app validates input and explains the problem in plain language. For example:

- `Enter the minimum number.`
- `The team count must be a whole number, like 10.`
- `Choose a team count between 1 and 8. We found 8 names.`
- `Use a hex color like #4F46E5.`

You will not see raw .NET conversion exceptions in these cases.

## Presentation controls

Once a pick screen is open, three buttons are available:

| Button | Behavior |
| --- | --- |
| Home | Closes the presentation window, restores the main window layout, and returns to the previous page |
| Dice | Makes another pick and updates any open presentation window |
| Exit | Closes the presentation window and shuts down the app |

## Display modes

### Extend

Available when two or more monitors are connected.

- The main window snaps to the primary monitor's working area.
- A borderless result window opens on the first non-primary monitor.
- The result font is large: 300 for numbers, 150 for words.
- Closing the main window closes the result window automatically, matching PowerPoint presenter-view behavior.
- Returning home restores the main window to its previous position and size.

### Duplicate

Used when Extend mode is off or only one monitor is available.

- The main window goes fullscreen on the current monitor.
- No secondary window is opened.

## Colors

Text and background colors apply to both presentation modes. Use the circular swatch buttons in settings to open the color picker, adjust RGB sliders or enter a `#RRGGBB` value, then press **Save**.

## Theme and volume

The **Theme** toggle in settings switches the whole interface between light and dark immediately. The choice is saved and restored on the next launch.

The **Volume** slider controls the selection sound from 0% to 100%. Setting it to 0% mutes the sound. Like all other settings, changes are written when you press **Save**.

UI theme is separate from the presentation colors: switching to dark does not change your configured result text or background colors.

## Reset

**Reset** in either settings page restores defaults. The word settings page also clears the saved word list. A confirmation dialog is shown first.
