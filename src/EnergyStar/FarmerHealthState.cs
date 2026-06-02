using EnergyStar.Core;
using StardewValley;

namespace EnergyStar;

internal sealed class FarmerHealthState : IHealthState
{
    private readonly Farmer farmer;

    public FarmerHealthState(Farmer farmer)
    {
        this.farmer = farmer;
    }

    public int Health
    {
        get => this.farmer.health;
        set => this.farmer.health = value;
    }

    public int MaxHealth => this.farmer.maxHealth;
}
