# EnergyStar

[English](README.md)

EnergyStar è una piccola mod SMAPI per Stardew Valley che mantiene pieni energia e salute del personaggio, rimuove la stanchezza, e impedisce lo svenimento delle 2:00 bloccando l'orologio all'1:50.

## Installazione

1. Installa SMAPI per la tua versione di Stardew Valley.
   - Se usi Stardew Valley tramite Proton, installa la versione Windows di SMAPI e avvia `StardewModdingAPI.exe` tramite Proton.
   - Se usi Stardew Valley su Windows, installa normalmente SMAPI per Windows.
2. Scarica `EnergyStar 1.2.0.zip` dalla cartella `dist` di questo repo o dalla release GitHub.
3. Estrai lo zip, poi copia la cartella `EnergyStar` ottenuta dentro la cartella `Stardew Valley/Mods`.
4. Avvia il gioco tramite SMAPI.

La mod non sostituisce file del gioco. Per disinstallarla, elimina `Stardew Valley/Mods/EnergyStar`.

## Build da sorgente

Installa .NET 6 SDK, Stardew Valley e SMAPI.

Se il pacchetto di build SMAPI non rileva automaticamente la cartella del gioco, copia `Directory.Build.props.example` in `Directory.Build.props` e imposta `GamePath` sulla cartella che contiene `Stardew Valley.dll` e `StardewModdingAPI.dll`.

Poi esegui:

```bash
dotnet run --project tests/EnergyStar.Tests/EnergyStar.Tests.csproj
dotnet build src/EnergyStar/EnergyStar.csproj -c Release
```

Lo zip di release viene scritto in `dist/`.
