# EnergyStar

[Italiano](README.it.md)

EnergyStar is a tiny SMAPI mod for Stardew Valley which keeps your farmer's stamina and health full, clears exhaustion, and prevents the 2:00 AM pass-out by freezing the clock at 1:50 AM.

## Install

1. Install SMAPI for your Stardew Valley setup.
   - If you run Stardew Valley through Proton, install the Windows version of SMAPI and launch `StardewModdingAPI.exe` through Proton.
   - If you run Stardew Valley on Windows, install SMAPI normally for Windows.
2. Download `EnergyStar 1.2.0.zip` from this repo's `dist` folder or from the GitHub release.
3. Extract the zip, then copy the resulting `EnergyStar` folder into your `Stardew Valley/Mods` folder.
4. Launch the game through SMAPI.

The mod does not replace game files. To uninstall it, delete `Stardew Valley/Mods/EnergyStar`.

## Build From Source

Install the .NET 6 SDK, Stardew Valley, and SMAPI.

If the SMAPI build package can't auto-detect your game folder, copy `Directory.Build.props.example` to `Directory.Build.props` and set `GamePath` to the folder containing `Stardew Valley.dll` and `StardewModdingAPI.dll`.

Then run:

```bash
dotnet run --project tests/EnergyStar.Tests/EnergyStar.Tests.csproj
dotnet build src/EnergyStar/EnergyStar.csproj -c Release
```

The release zip is written to `dist/`.
