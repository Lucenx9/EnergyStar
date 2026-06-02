# EnergyStar Health Freeze Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Extend EnergyStar so player health is restored to maximum after damage.

**Architecture:** Add a game-independent health policy in `EnergyStar.Core`, then expose the Stardew `Farmer` health fields through a small adapter. Rebuild, reinstall, and publish version `1.2.0`.

**Tech Stack:** C# 10, .NET 6, SMAPI `Pathoschild.Stardew.ModBuildConfig`, local console test runner.

---

### Task 1: Health Policy

**Files:**
- Modify: `tests/EnergyStar.Tests/Program.cs`
- Create: `src/EnergyStar.Core/IHealthState.cs`
- Create: `src/EnergyStar.Core/HealthKeeper.cs`

- [ ] **Step 1: Write the failing test**

Add tests proving health below max is restored, already-full health is unchanged, and invalid max health does not change current health.

- [ ] **Step 2: Run test to verify it fails**

Run: `dotnet run --project tests/EnergyStar.Tests/EnergyStar.Tests.csproj`
Expected: FAIL because `IHealthState` and `HealthKeeper` do not exist.

- [ ] **Step 3: Write minimal implementation**

Add the interface and policy.

- [ ] **Step 4: Run test to verify it passes**

Run: `dotnet run --project tests/EnergyStar.Tests/EnergyStar.Tests.csproj`
Expected: PASS.

### Task 2: SMAPI Integration

**Files:**
- Create: `src/EnergyStar/FarmerHealthState.cs`
- Modify: `src/EnergyStar/ModEntry.cs`
- Modify: `src/EnergyStar/EnergyStar.csproj`
- Modify: `src/EnergyStar/manifest.json`
- Modify: `README.md`

- [ ] **Step 1: Adapt Farmer health**

Expose `Farmer.health` and `Farmer.maxHealth`.

- [ ] **Step 2: Apply the policy**

Call `HealthKeeper.Apply` alongside the existing energy and clock policies.

- [ ] **Step 3: Build**

Run: `dotnet build src/EnergyStar/EnergyStar.csproj -c Release`
Expected: PASS with 0 warnings and 0 errors.

### Task 3: Release

**Files:**
- Generated: `dist/EnergyStar 1.2.0.zip`
- Installed: `/mnt/games/SteamLibrary/steamapps/common/Stardew Valley/Mods/EnergyStar`

- [ ] **Step 1: Verify**

Run tests, build Release, and check the zip with `unzip -t`.

- [ ] **Step 2: Install and publish**

Extract the updated zip locally, push to GitHub, and create release `v1.2.0`.
