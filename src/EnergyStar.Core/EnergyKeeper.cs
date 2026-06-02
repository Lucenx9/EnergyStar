namespace EnergyStar.Core;

public static class EnergyKeeper
{
    public static bool Apply(IEnergyState state)
    {
        var changed = false;

        if (state.MaxStamina > 0 && state.Stamina < state.MaxStamina)
        {
            state.Stamina = state.MaxStamina;
            changed = true;
        }

        if (state.Exhausted)
        {
            state.Exhausted = false;
            changed = true;
        }

        return changed;
    }
}
