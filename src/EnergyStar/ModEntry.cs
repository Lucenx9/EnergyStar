using EnergyStar.Core;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace EnergyStar;

public sealed class ModEntry : Mod
{
    public override void Entry(IModHelper helper)
    {
        helper.Events.GameLoop.SaveLoaded += this.OnSaveLoaded;
        helper.Events.GameLoop.DayStarted += this.OnDayStarted;
        helper.Events.GameLoop.UpdateTicked += this.OnUpdateTicked;
    }

    private void OnSaveLoaded(object? sender, SaveLoadedEventArgs e)
    {
        this.ApplyPolicies();
    }

    private void OnDayStarted(object? sender, DayStartedEventArgs e)
    {
        this.ApplyPolicies();
    }

    private void OnUpdateTicked(object? sender, UpdateTickedEventArgs e)
    {
        this.ApplyPolicies();
    }

    private void ApplyPolicies()
    {
        if (!Context.IsWorldReady || Game1.player is null)
            return;

        EnergyKeeper.Apply(new FarmerEnergyState(Game1.player));
        HealthKeeper.Apply(new FarmerHealthState(Game1.player));

        // DayClockKeeper touches host-owned Game1 clock fields; energy and health are player-local.
        if (Context.IsMainPlayer)
            DayClockKeeper.Apply(new GameClockState());
    }
}
