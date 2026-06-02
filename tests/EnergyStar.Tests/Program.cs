using EnergyStar.Core;

var tests = new (string Name, Action Body)[]
{
    ("refills stamina and clears exhaustion", TestRefillsStaminaAndClearsExhaustion),
    ("does not report changes when already full and rested", TestNoChangeWhenAlreadyFullAndRested),
    ("does not overfill invalid max stamina", TestInvalidMaxStaminaIsIgnored),
    ("leaves early day clock unchanged", TestLeavesEarlyDayClockUnchanged),
    ("freezes clock timer at 1:50 AM", TestFreezesClockTimerAtOneFifty),
    ("clamps clock after 1:50 AM", TestClampsClockAfterOneFifty),
    ("restores health to maximum", TestRestoresHealthToMaximum),
    ("does not report health changes when already full", TestNoChangeWhenHealthAlreadyFull),
    ("does not overfill invalid max health", TestInvalidMaxHealthIsIgnored)
};

var failures = 0;

foreach (var test in tests)
{
    try
    {
        test.Body();
        Console.WriteLine($"PASS {test.Name}");
    }
    catch (Exception ex)
    {
        failures++;
        Console.WriteLine($"FAIL {test.Name}: {ex.Message}");
    }
}

if (failures > 0)
    Environment.Exit(1);

static void TestRefillsStaminaAndClearsExhaustion()
{
    var state = new FakeEnergyState(stamina: 4f, maxStamina: 270, exhausted: true);

    var changed = EnergyKeeper.Apply(state);

    AssertTrue(changed, "expected a state change");
    AssertEqual(270f, state.Stamina, "stamina should be refilled to max");
    AssertFalse(state.Exhausted, "exhaustion should be cleared");
}

static void TestNoChangeWhenAlreadyFullAndRested()
{
    var state = new FakeEnergyState(stamina: 270f, maxStamina: 270, exhausted: false);

    var changed = EnergyKeeper.Apply(state);

    AssertFalse(changed, "already-full state should not change");
    AssertEqual(270f, state.Stamina, "stamina should stay at max");
    AssertFalse(state.Exhausted, "exhaustion should stay false");
}

static void TestInvalidMaxStaminaIsIgnored()
{
    var state = new FakeEnergyState(stamina: 12f, maxStamina: 0, exhausted: true);

    var changed = EnergyKeeper.Apply(state);

    AssertTrue(changed, "clearing exhaustion should count as a state change");
    AssertEqual(12f, state.Stamina, "stamina should not be changed without a positive max");
    AssertFalse(state.Exhausted, "exhaustion should still be cleared");
}

static void TestLeavesEarlyDayClockUnchanged()
{
    var state = new FakeClockState(timeOfDay: 2540, gameTimeInterval: 1234);

    var changed = DayClockKeeper.Apply(state);

    AssertFalse(changed, "clock should not change before 1:50 AM");
    AssertEqualInt(2540, state.TimeOfDay, "time should stay before the freeze point");
    AssertEqualInt(1234, state.GameTimeInterval, "timer should keep running before the freeze point");
}

static void TestFreezesClockTimerAtOneFifty()
{
    var state = new FakeClockState(timeOfDay: 2550, gameTimeInterval: 1234);

    var changed = DayClockKeeper.Apply(state);

    AssertTrue(changed, "timer should be reset at the freeze point");
    AssertEqualInt(2550, state.TimeOfDay, "time should stay at 1:50 AM");
    AssertEqualInt(0, state.GameTimeInterval, "timer should be reset so the clock cannot advance");
}

static void TestClampsClockAfterOneFifty()
{
    var state = new FakeClockState(timeOfDay: 2600, gameTimeInterval: 4321);

    var changed = DayClockKeeper.Apply(state);

    AssertTrue(changed, "clock should change after the freeze point");
    AssertEqualInt(2550, state.TimeOfDay, "time should be clamped back to 1:50 AM");
    AssertEqualInt(0, state.GameTimeInterval, "timer should be reset after clamping");
}

static void TestRestoresHealthToMaximum()
{
    var state = new FakeHealthState(health: 12, maxHealth: 150);

    var changed = HealthKeeper.Apply(state);

    AssertTrue(changed, "expected health to be restored");
    AssertEqualInt(150, state.Health, "health should be restored to max");
}

static void TestNoChangeWhenHealthAlreadyFull()
{
    var state = new FakeHealthState(health: 150, maxHealth: 150);

    var changed = HealthKeeper.Apply(state);

    AssertFalse(changed, "already-full health should not change");
    AssertEqualInt(150, state.Health, "health should stay at max");
}

static void TestInvalidMaxHealthIsIgnored()
{
    var state = new FakeHealthState(health: 12, maxHealth: 0);

    var changed = HealthKeeper.Apply(state);

    AssertFalse(changed, "invalid max health should not change health");
    AssertEqualInt(12, state.Health, "health should not change without a positive max");
}

static void AssertTrue(bool value, string message)
{
    if (!value)
        throw new InvalidOperationException(message);
}

static void AssertFalse(bool value, string message)
{
    if (value)
        throw new InvalidOperationException(message);
}

static void AssertEqual(float expected, float actual, string message)
{
    if (Math.Abs(expected - actual) > 0.001f)
        throw new InvalidOperationException($"{message}: expected {expected}, got {actual}");
}

static void AssertEqualInt(int expected, int actual, string message)
{
    if (expected != actual)
        throw new InvalidOperationException($"{message}: expected {expected}, got {actual}");
}

internal sealed class FakeEnergyState : IEnergyState
{
    public FakeEnergyState(float stamina, int maxStamina, bool exhausted)
    {
        this.Stamina = stamina;
        this.MaxStamina = maxStamina;
        this.Exhausted = exhausted;
    }

    public float Stamina { get; set; }

    public int MaxStamina { get; }

    public bool Exhausted { get; set; }
}

internal sealed class FakeClockState : IClockState
{
    public FakeClockState(int timeOfDay, int gameTimeInterval)
    {
        this.TimeOfDay = timeOfDay;
        this.GameTimeInterval = gameTimeInterval;
    }

    public int TimeOfDay { get; set; }

    public int GameTimeInterval { get; set; }
}

internal sealed class FakeHealthState : IHealthState
{
    public FakeHealthState(int health, int maxHealth)
    {
        this.Health = health;
        this.MaxHealth = maxHealth;
    }

    public int Health { get; set; }

    public int MaxHealth { get; }
}
