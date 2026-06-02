namespace EnergyStar.Core;

public static class DayClockKeeper
{
    public const int LateNightFreezeTime = 2550;

    public static bool Apply(IClockState state)
    {
        var changed = false;

        if (state.TimeOfDay > LateNightFreezeTime)
        {
            state.TimeOfDay = LateNightFreezeTime;
            changed = true;
        }

        if (state.TimeOfDay >= LateNightFreezeTime && state.GameTimeInterval != 0)
        {
            state.GameTimeInterval = 0;
            changed = true;
        }

        return changed;
    }
}
