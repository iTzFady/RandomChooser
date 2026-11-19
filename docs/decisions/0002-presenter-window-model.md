# ADR-002: PowerPoint-style presenter window model

## Status

Accepted

## Date

2026-11-19

## Context

Dual-monitor users want the result to appear like a slideshow: the controlling window on the main monitor and the audience-facing result on the second monitor. Closing the controlling window must end the presentation and close the result window, matching PowerPoint presenter view.

The original implementation opened an extended window on a non-primary screen but left the main window at its previous size and position, and did not reliably close the extended window.

## Decision

Introduce a `Presenter` service used by both result pages:

- Capture the main window's original position, size, and state when a pick screen loads.
- Snap the main window to `Screens.Primary.WorkingArea`, scaling for `Screen.Scaling`.
- Open a borderless `ExtendedWindow` on the first non-primary screen.
- Call `Show(mainWindow)` so the result window is an owned child of the main window.
- Also close the result window from the main window's `Closing` handler as a fallback.
- Restore the original main-window layout when returning home.

When Extend mode is off or only one monitor exists, the main window goes fullscreen instead.

## Alternatives considered

### Fullscreen the main window and mirror content manually

- Pros: simpler, one window.
- Cons: no true audience display; the presenter sees the same fullscreen result with no controls.
- Rejected: does not match the requested behavior.

### Use a second process for the result window

- Pros: strong isolation.
- Cons: complex lifecycle, IPC for updates, much harder close-chain.
- Rejected: unnecessary for a single result string.

### Rely only on the `Closing` event

- Pros: straightforward.
- Cons: platform close paths vary; owned-child behavior is handled by the framework itself.
- Rejected as the only mechanism; kept as a fallback instead.

## Consequences

- Presenter and audience windows now behave like PowerPoint: main on primary, result on secondary, close chain on exit.
- Layout is restored after a presentation ends.
- Monitor detection depends on Avalonia's `Screens` API and OS reporting of primary/non-primary displays.
- `Screen.WorkingArea` is used instead of full bounds so taskbars and docks are respected.
