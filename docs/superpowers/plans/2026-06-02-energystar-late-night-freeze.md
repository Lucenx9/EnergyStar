# EnergyStar Late Night Freeze Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Extend EnergyStar so the farmer does not pass out at 2:00 AM by freezing the game clock at 1:50 AM.

**Architecture:** Add a game-independent clock policy in `EnergyStar.Core`, then adapt `Game1.timeOfDay` and the internal game-time interval from the SMAPI mod. Rebuild the same distributable zip and reinstall it over the existing `Mods/EnergyStar` folder.

**Tech Stack:** C# 10, .NET 6, SMAPI `Pathoschild.Stardew.ModBuildConfig`, local console test runner.

---

### Task 1: Clock Policy

**Files:**
- Modify: `tests/EnergyStar.Tests/Program.cs`
- Create: `src/EnergyStar.Core/IClockState.cs`
- Create: `src/EnergyStar.Core/DayClockKeeper.cs`

- [ ] **Step 1: Write the failing test**

Add tests proving that times below `2550` stay unchanged, `2550` resets the game timer to zero, and times above `2550` are clamped back to `2550`.

- [ ] **Step 2: Run test to verify it fails**

Run: `dotnet run --project tests/EnergyStar.Tests/EnergyStar.Tests.csproj`
Expected: FAIL because `DayClockKeeper` and `IClockState` do not exist yet.

- [ ] **Step 3: Write minimal implementation**

Add the interface and policy with a `LateNightFreezeTime` constant set to `2550`.

- [ ] **Step 4: Run test to verify it passes**

Run: `dotnet run --project tests/EnergyStar.Tests/EnergyStar.Tests.csproj`
Expected: PASS.

### Task 2: SMAPI Integration

**Files:**
- Create: `src/EnergyStar/GameClockState.cs`
- Modify: `src/EnergyStar/ModEntry.cs`
- Modify: `src/EnergyStar/manifest.json`
- Modify: `README.md`

- [ ] **Step 1: Adapt the game clock**

Expose `Game1.timeOfDay` and `Game1.gameTimeInterval` to the core `IClockState`.

- [ ] **Step 2: Apply the policy on each tick**

Call the clock policy from `UpdateTicked` after the energy policy.

- [ ] **Step 3: Build**

Run: `dotnet build src/EnergyStar/EnergyStar.csproj -c Release`
Expected: PASS with 0 warnings and 0 errors.

### Task 3: Package And Install

**Files:**
- Generated: `dist/EnergyStar 1.1.0.zip`
- Installed: `/mnt/games/SteamLibrary/steamapps/common/Stardew Valley/Mods/EnergyStar`

- [ ] **Step 1: Verify tests and build**

Run the console tests, build Release, and check the zip with `unzip -t`.

- [ ] **Step 2: Install the zip**

Extract the updated zip into the Stardew Valley `Mods` folder with escalation.
