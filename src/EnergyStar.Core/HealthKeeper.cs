namespace EnergyStar.Core;

public static class HealthKeeper
{
    public static bool Apply(IHealthState state)
    {
        if (state.MaxHealth <= 0 || state.Health >= state.MaxHealth)
            return false;

        state.Health = state.MaxHealth;
        return true;
    }
}
