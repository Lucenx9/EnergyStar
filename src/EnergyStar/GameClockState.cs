using EnergyStar.Core;
using StardewValley;

namespace EnergyStar;

internal sealed class GameClockState : IClockState
{
    public int TimeOfDay
    {
        get => Game1.timeOfDay;
        set => Game1.timeOfDay = value;
    }

    public int GameTimeInterval
    {
        get => Game1.gameTimeInterval;
        set => Game1.gameTimeInterval = value;
    }
}
