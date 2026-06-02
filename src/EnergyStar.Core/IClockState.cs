namespace EnergyStar.Core;

public interface IClockState
{
    int TimeOfDay { get; set; }

    int GameTimeInterval { get; set; }
}
