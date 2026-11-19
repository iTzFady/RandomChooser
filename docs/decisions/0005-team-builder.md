# ADR-005: Team builder with balanced random assignment

## Status

Accepted

## Date

2026-11-19

## Context

Users need to split a list of names into teams. They may know how many teams they want, or they may prefer the app to pick a sensible number. Team names should be useful by default but editable.

## Decision

Add a `TeamBuilderPage` and a pure `TeamBuilder` service.

- Names are entered one per line; blank lines are ignored and duplicates are kept as separate people.
- Automatic mode uses `ceil(sqrt(nameCount))`, clamped to at least 2 teams and at most one team per name.
- Manual mode accepts any whole number from 1 to the number of names.
- Names are shuffled with `Random.Shared` using Fisher–Yates and dealt round-robin, so team sizes differ by at most one.
- Default names are `Team 1`, `Team 2`, etc., bound to editable text boxes in the results.

Input problems are reported through `FriendlyInput` / `MessageWindow` using plain-language messages rather than raw exceptions.

## Alternatives considered

### Fixed two teams

- Pros: predictable.
- Cons: poor for large or small groups.
- Rejected: the user asked for automatic sizing to scale with the group.

### Random group sizes

- Pros: simplest implementation.
- Cons: can create empty or wildly uneven teams.
- Rejected: balanced teams are fairer and easier to reason about.

### Generate themed team names only

- Pros: more colorful.
- Cons: users often need neutral or custom names.
- Rejected: numbered defaults with inline renaming satisfy both cases.

## Consequences

- Team creation is deterministic in shape but random in membership.
- Team membership is not persisted; renaming a team affects only the current result set.
- The team builder reuses the selection sound and theme resources but does not use presentation mode.
- `TeamBuilder` has no Avalonia dependency, so its logic is covered by the xUnit test project.
