namespace EnergyStar.Core;

public interface IEnergyState
{
    float Stamina { get; set; }

    int MaxStamina { get; }

    bool Exhausted { get; set; }
}
