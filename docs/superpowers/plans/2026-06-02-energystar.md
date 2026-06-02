# EnergyStar Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Build a packaged SMAPI mod that keeps the Stardew Valley player's stamina full without editing the game installation.

**Architecture:** Put game-independent stamina behavior in `EnergyStar.Core`, then call it from a minimal SMAPI `ModEntry`. The build disables automatic mod deployment and only emits local build/package artifacts.

**Tech Stack:** C# 10, .NET 6, SMAPI via `Pathoschild.Stardew.ModBuildConfig`, shell verification with `dotnet`.

---

### Task 1: Core Energy Policy

**Files:**
- Create: `src/EnergyStar.Core/EnergyStar.Core.csproj`
- Create: `src/EnergyStar.Core/IEnergyState.cs`
- Create: `src/EnergyStar.Core/EnergyKeeper.cs`
- Create: `tests/EnergyStar.Tests/EnergyStar.Tests.csproj`
- Create: `tests/EnergyStar.Tests/Program.cs`

- [ ] **Step 1: Write the failing test**

Create a console test runner that constructs fake energy states and asserts stamina is refilled and exhaustion is cleared.

- [ ] **Step 2: Run test to verify it fails**

Run: `dotnet run --project tests/EnergyStar.Tests/EnergyStar.Tests.csproj`
Expected: FAIL because `EnergyStar.Core` has no public policy types yet.

- [ ] **Step 3: Write minimal implementation**

Add `IEnergyState` and `EnergyKeeper.Apply`.

- [ ] **Step 4: Run test to verify it passes**

Run: `dotnet run --project tests/EnergyStar.Tests/EnergyStar.Tests.csproj`
Expected: PASS with all custom assertions passing.

### Task 2: SMAPI Wrapper

**Files:**
- Create: `src/EnergyStar/EnergyStar.csproj`
- Create: `src/EnergyStar/manifest.json`
- Create: `src/EnergyStar/FarmerEnergyState.cs`
- Create: `src/EnergyStar/ModEntry.cs`
- Create: `stardewvalley.targets`

- [ ] **Step 1: Add SMAPI project files**

Create a `.NET 6` SMAPI project with `EnableModDeploy=false`, `EnableModZip=true`, and the known local `GamePath`.

- [ ] **Step 2: Add the Farmer adapter**

Map `Farmer.Stamina`, `Farmer.MaxStamina`, and `Farmer.exhausted.Value` onto `IEnergyState`.

- [ ] **Step 3: Add the mod entry**

Subscribe to `SaveLoaded`, `DayStarted`, and `UpdateTicked`; call `EnergyKeeper.Apply` only when `Context.IsWorldReady`.

- [ ] **Step 4: Build**

Run: `dotnet build src/EnergyStar/EnergyStar.csproj -c Release`
Expected: exit 0 and a release zip in the project output.

### Task 3: Package Verification

**Files:**
- Read generated package output under `src/EnergyStar/bin/Release`.

- [ ] **Step 1: Run full verification**

Run: `dotnet run --project tests/EnergyStar.Tests/EnergyStar.Tests.csproj`
Run: `dotnet build src/EnergyStar/EnergyStar.csproj -c Release`

- [ ] **Step 2: Inspect zip contents**

Run: `find src/EnergyStar/bin/Release -maxdepth 4 -type f`
Expected: built DLL and release zip are present.
