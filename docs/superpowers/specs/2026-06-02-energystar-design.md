# EnergyStar Design

## Goal

Build a small SMAPI mod for Stardew Valley that removes practical energy fatigue by keeping the local player's stamina full and clearing the exhausted flag during play.

## Constraints

- Do not modify the real Stardew Valley installation directly.
- Produce a package that can be copied into `Stardew Valley/Mods/EnergyStar`.
- Keep the package cross-platform for SMAPI users on Proton/Windows.
- Avoid Harmony patches or game binary edits.

## Approach

The mod has two parts. `EnergyStar.Core` contains a tiny testable stamina policy over an `IEnergyState` interface. `EnergyStar` adapts Stardew's `Farmer` object to that interface and runs the policy through SMAPI game-loop events.

The mod will target `.NET 6`, use `Pathoschild.Stardew.ModBuildConfig` for SMAPI/Stardew references, and disable automatic deploy so builds do not copy anything into the user's game folder.

## Behavior

- When a save is loaded, refill stamina to max and clear exhaustion.
- When a new day starts, refill stamina to max and clear exhaustion.
- During active gameplay, periodically refill stamina to max and clear exhaustion.
- During active gameplay, keep the clock from advancing past 1:50 AM (`2550`) so the 2:00 AM pass-out does not trigger.
- If the world is not loaded or the player object is unavailable, do nothing.

## Verification

- Run the pure core test runner.
- Build the SMAPI mod project.
- Confirm the generated package contains `manifest.json` and `EnergyStar.dll`.
