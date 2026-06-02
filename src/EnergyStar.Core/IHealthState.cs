namespace EnergyStar.Core;

public interface IHealthState
{
    int Health { get; set; }

    int MaxHealth { get; }
}
