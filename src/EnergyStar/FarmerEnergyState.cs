using EnergyStar.Core;
using StardewValley;

namespace EnergyStar;

internal sealed class FarmerEnergyState : IEnergyState
{
    private readonly Farmer farmer;

    public FarmerEnergyState(Farmer farmer)
    {
        this.farmer = farmer;
    }

    public float Stamina
    {
        get => this.farmer.Stamina;
        set => this.farmer.Stamina = value;
    }

    public int MaxStamina => this.farmer.MaxStamina;

    public bool Exhausted
    {
        get => this.farmer.exhausted.Value;
        set => this.farmer.exhausted.Value = value;
    }
}
